using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColourSwap : MonoBehaviour
{
    [Header("Colours")]
    [SerializeField] public SpriteRenderer rend;
    [SerializeField] public Color mainColor;
    [SerializeField] public float ColourID;

    private Collision2D objInQuestion;
    private float TempId = 0;

    private void FixedUpdate()
    {
        ColourManager();
        rend.color = mainColor;

        if (Input.GetMouseButtonDown(0))
        {
            Swap();
        }
    }

    private void Swap()
    {
            TempId = ColourID;
            ColourID = objInQuestion.gameObject.GetComponent<ObjectColour>().ObjColourID;
            objInQuestion.gameObject.GetComponent<ObjectColour>().ObjColourID = TempId;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ColourObj"))
        {
            objInQuestion = collision;
        }
    }
    private void ColourManager()
    {
        if(ColourID == 0)
        {
            mainColor = Color.white;
        }
        if (ColourID == 1)
        {
            mainColor = Color.red;
        }
        if (ColourID == 2)
        {
            mainColor = Color.green;
        }
        if (ColourID == 3)
        {
            mainColor = Color.magenta;
        }
    }
}
