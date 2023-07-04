using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCountDown : MonoBehaviour
{
    public static TimeCountDown timeCountDown;
    public float TimeLeft;
    public float TimerUpdate;
    public bool TimerOn = false;

    public Text TimerTxt;

    void Awake()
    {
        TimeLeft = 20;
        timeCountDown = this;
    }

    void Start()
    {
        updateTimer(TimeLeft - 1);
        // TimerOn = true;
    }

    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                GameManager.gameManager.GameLosing();
                TimeLeft = 0;
                TimerOn = false;
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ReTimeCount(float time){
        TimeLeft += time;
        if(TimeLeft > 20){
            TimeLeft = 20;
        }
    }

}
