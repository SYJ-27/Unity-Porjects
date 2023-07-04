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
        timeCountDown = this;
    }

    void Start()
    {
        TimerOn = true;
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
                GameScene.game.checkOver();
                gameObject.SetActive(false);
                //Debug.Log("Time is UP!");
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

        // TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        TimerTxt.text = string.Format("{1:00}", minutes, seconds);
    }

}
