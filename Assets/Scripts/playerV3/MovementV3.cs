using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MovementV3 : MonoBehaviour
{
    public ColliderScript _coll;

    [Header("PLAYER")]
    private Rigidbody2D _rb;
    public float _playerSpeed = 10f;
    public float _jumpvel = 10f;
    public float _jumpsLeft = 1;

    [Header("DASH")]
    [SerializeField] public float _DashingVelocity = 5f;
    [SerializeField] public float _DashTime = 0.5f;
    public bool _canDash;
    public bool _isDashing;
    private Vector2 _dashDir;

    [Header("WALL")]
    [SerializeField] public float _GripSlide = 5f;
    public bool _isWallClimbing;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<ColliderScript>();
        _jumpsLeft = 1;
        _rb.constraints = RigidbodyConstraints2D.None;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _canDash = true;
        _isWallClimbing = false;
    }

    private void Update()
    {
        float _x = Input.GetAxisRaw("Horizontal");
        float _y = Input.GetAxisRaw("Vertical");
        Vector2 _dir = new Vector2(_x, _y);

        var dashInput = Input.GetButton("Dash");

        Move(_dir);

        //JUMPING
        if (Input.GetButton("Jump") && _jumpsLeft > 0)
        {
            _rb.velocity = Vector2.up * _jumpvel;
            _jumpsLeft = _jumpsLeft - 1;
        }

        if (_coll.onGround == true || _coll.onWall == true)
        {
            _jumpsLeft = 1;
            _isWallClimbing = false;
        }

        //WALL JUMPING
        while (_isWallClimbing == true)
        {
            if (_coll.onWall == true && Input.GetButton("Jump"))
            {
                _rb.velocity = Vector2.Lerp(_rb.velocity, (new Vector2(_dir.x * _playerSpeed, _rb.velocity.y)), 0.5f * Time.deltaTime);
                _isWallClimbing = true;
            }
        }

        //DASHING
        if (_coll.onGround == true)
        {
            _canDash = true;
            _isDashing = false;
        }

        if (dashInput && _canDash == true && !_coll.onGround)
        {
            _canDash = false;
            _isDashing = true;
            _dashDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (_dashDir == Vector2.zero)
            {
                _dashDir = new Vector2(transform.localScale.x, 0);
            }
            StartCoroutine(stopDashing());
        }

        if (_isDashing == true)
        {
            _rb.velocity = _dashDir * _DashingVelocity;
            return;
        }

        //SLIDE
        if (_coll.onWall && !_coll.onGround)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -_GripSlide);
        }
    }

    private IEnumerator stopDashing()
    {
        yield return new WaitForSeconds(_DashTime);
        _isDashing = false;
    }

    void FixedUpdate()
    {

    }

    //MOVE
    private void Move(Vector2 _dir)
    {
        _rb.velocity = (new Vector2(_dir.x * _playerSpeed, _rb.velocity.y));
    }

}
