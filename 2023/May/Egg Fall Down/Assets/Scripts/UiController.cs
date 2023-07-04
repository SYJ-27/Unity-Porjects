using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Text scoreText;
    public Image timeBar;
    public GameOver gameOver;
    float maxTime, currentTime;

    // Start is called before the first frame update
    void Start()
    {
        maxTime = 30;
        currentTime = maxTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameManager.gameManager.isPlaying){
            UpdateTimeUI();
        }
        UpdateScoreUI();
    }

    void UpdateScoreUI(){
        int score = GameManager.gameManager.score;
        scoreText.text = $"{score}";
        
    }

    public void UpdateTimeUI(){
        currentTime -= Time.deltaTime;
        if(currentTime <= 0){
            currentTime = 0;
        }
        timeBar.fillAmount = currentTime/maxTime;
        if(currentTime <= 0 && !GameManager.gameManager.isLose){
            GameManager.gameManager.GameLosing();
        }
    }

    public void GameLoseUI(){
        gameOver.GameOverScene();
    }

    public void PlusTime(int plus){
        currentTime += plus;
        if(currentTime > maxTime){
            currentTime = maxTime;
        }
        UpdateTimeUI();
    }

}
