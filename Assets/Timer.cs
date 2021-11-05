using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{


    public float currentTime = 0;
    public float startingTime = 600;

    [SerializeField]
    Text countdownText;

    private void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        if(currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
        }
        else
        {
            currentTime = 0;
        }

        DisplayTime(currentTime);
    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;

        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
