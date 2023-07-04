using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    // public Text textCurrentScore;
    // public GameObject scoreUI;
    public GameOver gameOver;
    // private int currentScore;

    public float minTime, maxTime, timePlay;
    public Image timeCircle;


    void Awake(){
        timePlay = 0;
        minTime = 10;
        maxTime = 10;
        // currentScore = 0;
        // textCurrentScore.text = $"{currentScore}";
    }

    void FixedUpdate()
    {
        timePlay += Time.fixedDeltaTime;
        ShowTime();
    }

    // public void UpdateScoreUI(){
    //     currentScore++;
    //     textCurrentScore.text = $"{currentScore}";
    // }

    public void HideScoreUI(){
        // scoreUI.SetActive(false);
        gameOver.GameOverScene(timePlay);
    }

    public void ShowTime(){
        minTime -= Time.fixedDeltaTime;
        if(minTime >= 0){
            timeCircle.fillAmount = minTime/maxTime;
        }
        else{
            minTime = maxTime;
        }
    }

}
