using UnityEngine;

public class ObjectColour : MonoBehaviour
{
    [Header("Colours")]
    [SerializeField] public SpriteRenderer rend;
    [SerializeField] public Color objColor;
    [SerializeField] public float ObjColourID;

    private void FixedUpdate()
    {
        ColourManager();
        rend.color = objColor;
    }

    private void ColourManager()
    {
        if (ObjColourID == 0)
        {
            objColor = Color.white;
        }
        if (ObjColourID == 1)
        {
            objColor = Color.red;
        }
        if (ObjColourID == 2)
        {
            objColor = Color.green;
        }   
        if (ObjColourID == 3)
        {
            objColor = Color.magenta;
        }
    }
}