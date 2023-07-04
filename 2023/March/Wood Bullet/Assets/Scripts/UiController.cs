using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Player mainPlayer;
    public float time;
    public int score;
    public Text textTime, textScore;

    void Awake(){
        time = 0;
        score = 0;
        UpdateUI();
    }

    void Update(){
        if(!GameManager.gameManager.isLose){
            score = mainPlayer.score;
            time = mainPlayer.time;
            time = (float)Math.Round(time,2);
            UpdateUI();
        }
    }

    private void UpdateUI(){
        textScore.text = $"{score}";
        textTime.text = $"{time}";
    }
}
