using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public GameOver gameOver;

    public Text textTime;
    float timeCount;
    void Awake(){
        timeCount = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        if(!GameManager.gameManager.isLose){
            UpdateTimeUI();
        }
    }

    void UpdateTimeUI(){
        timeCount += Time.fixedDeltaTime;
        timeCount = (float)Math.Round(timeCount, 2);
        textTime.text = $"{timeCount}s";
        if(timeCount >= 60){
            GameManager.gameManager.GameLosing("Time Out");
        }
    }

    public void GetGameOverScene(string reasonLose, int fire, int heart, int sos, int house){
        gameOver.GameOverScene(reasonLose, fire, heart, sos, house, timeCount);
    }
}
