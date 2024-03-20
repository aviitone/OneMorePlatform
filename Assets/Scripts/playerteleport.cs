using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerteleport : MonoBehaviour
{
    [SerializeField] public GameObject StartLine;
    [SerializeField] public GameObject Player;
    public Transform PlayerTrans;
    public Transform GoToHereYouNumpty;
    private void Start()
    {
        GoToHereYouNumpty = StartLine.transform;
        PlayerTrans = Player.transform;
        Debug.Log("started");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EndGoal"))
        {
            Timer.isCounting = false;
            Restart();
        }

        if (collision.CompareTag("StartLine"))
        {
            Timer.timeElapsed = 0;
            Timer.isCounting = true;
        }
    }
    public void Restart() {
        Debug.Log("TELEPORT");
        PlayerTrans.position = GoToHereYouNumpty.position;
    }

}
