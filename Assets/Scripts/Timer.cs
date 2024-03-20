using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static float timeElapsed = 0;
    public static bool isCounting = false;
    public TMP_Text timerText;
    public Color CurrentColor;

    private void Start()
    {
        isCounting = false;
        timerText.color = Color.black;
    }

    private void FixedUpdate()
    {
        if (isCounting){
            if (timeElapsed >= 0)
            {
                timeElapsed += Time.deltaTime;
                DisplayTime(timeElapsed);

            }

            timerText.color = Color.green;
        }
        else
        {
            timerText.color = Color.red;
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt (timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format ("{0:00} : {1:00}", minutes, seconds);
    }
}
