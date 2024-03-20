using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateLogic : MonoBehaviour
{
    [Header("Colours")]
    [SerializeField] public SpriteRenderer rend;
    [SerializeField] public Color gateColour;
    [SerializeField] public float gateColourID;

    [SerializeField] public GameObject GameObjectGate;
    [SerializeField] public GameObject player;

    public float alpha = 0.5f;
    public float alphaFull = 1f;

    private void FixedUpdate()
    {
        ColourManager();
        rend.color = gateColour;

        GateToggle();
    }

    private void GateToggle()
    {
        if(gateColourID == player.GetComponent<ColourSwap>().ColourID)
        {
            GameObjectGate.GetComponent<BoxCollider2D>().isTrigger = true;
            gateColour.a = alpha;
        }
        else
        {
            GameObjectGate.GetComponent<BoxCollider2D>().isTrigger = false;
            gateColour.a = alphaFull;
        }
    }

    private void ColourManager()
    {
        if (gateColourID == 0)
        {
            gateColour = Color.white;
        }
        if (gateColourID == 1)
        {
            gateColour = Color.red;
        }
        if (gateColourID == 2)
        {
            gateColour = Color.green;
        }
        if (gateColourID == 3)
        {
            gateColour = Color.magenta;
        }

        //transparent
        if (gateColourID == 10)
        {
            gateColour = new Color(193, 190, 190);
        }
    }
}
