using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float horiz;
    public Animator animator;

    [Header("GENERIC")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float GravScale;
    private bool isAirbourne;

    private bool isFacingRight = true;
    private int maxJumps = 2;

    //Wall jumping stuff
    private bool isWallJumping;
    private float wallJumpDir;
    private float wallJumpTime = 0.2f;
    private float wallJumpCounter;
    private float wallJumpDuration = 0.4f;
    private Vector2 wallJumpPower = new Vector2(16f, 16f);

    [Header("SOUNDS SFX")]
    [SerializeField] public AudioSource JumpSFX;
    [SerializeField] public AudioSource LandSFX;
    [SerializeField] public AudioSource FinishSFX;
    [SerializeField] public AudioSource StartSFX;


    [Header("COMPONENTS/CHECKS")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    [Header("EXTRAS")]
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeReference] private float wallSlideSpeed = 2f;

    private bool isWallSliding;
    private float coyoteTimeCounter;

    [Header("ACCELERATION")]
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float deceleration = 1f;
    private float velPower = 1f;

    private int JumpsLeft;

    //START
    private void Start()
    {
        maxJumps = 2;
        isAirbourne = false;
}
    private void Update()
    {

        horiz = Input.GetAxisRaw("Horizontal");

        //JUMP TRIGGER
        if (Input.GetButtonDown("Jump") && JumpsLeft > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            JumpsLeft -= 1;
        }

        if (JumpsLeft == 0)
        {
            coyoteTimeCounter = 0f;
        }

        if (IsGrounded())
        {
            //resets coyote time
            coyoteTimeCounter = coyoteTime;
            JumpsLeft = maxJumps;
            isAirbourne = false;
        }
        else
        {
            //counts down the CoyoteTime
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (IsGrounded() && rb.velocity.y <= 0)
        {
            JumpsLeft = maxJumps;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void FixedUpdate()
    {
        if(IsGrounded() == false)
        {
            isAirbourne = true;
        }

        if (!isWallJumping)
        {
            float targetSpeed = horiz * speed;
            //finds player max-speed
            float speedDif = targetSpeed - rb.velocity.x;
            //finds force needed to get there
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
            //applys that speed to speed diff
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

            //applys that to player
            rb.AddForce(movement * Vector2.right);

            animator.SetFloat("Speed", Mathf.Abs(movement));
        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = GravScale * 1.5f;
        }

        if (isAirbourne == true)
        {
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }
    }

        //WALL SLIDE STUFF
        private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (isWalled() && !IsGrounded() && horiz != 0f){ 
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpDir = -transform.localScale.x;
            wallJumpCounter = wallJumpTime;

            CancelInvoke(nameof(StopWallJmp));
        }
        else
        {
            // Allows player to turn from wall and still be able to jump
            wallJumpCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpCounter > 0f)
        {
            JumpsLeft = maxJumps;

            if (transform.localScale.x != wallJumpDir)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJmp), wallJumpDuration);
        }
    }

    private void StopWallJmp()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horiz < 0f || !isFacingRight && horiz > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
