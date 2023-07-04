using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Player mainPlayer;
    public GameOver gameOver;
    public Image timeBar, healthBar;
    public Text scoreText;
    public float maxTime, currentTime;
    // Start is called before the first frame update
    void Start()
    {
        maxTime = 15;
        currentTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(mainPlayer.isMoving & GameManager.gameManager.score != 0){
            UpdateTimeUI();
        }
        if(!GameManager.gameManager.isLose){
            UpdatePlayerUI();
        }
    }

    void UpdateTimeUI(){
        currentTime -= Time.deltaTime;
        if(currentTime >= 0){
            timeBar.fillAmount = currentTime/maxTime;
        }
        else{
            if(!GameManager.gameManager.isLose){
                GameManager.gameManager.GameLosing("TIME OUT!");
            }
        }
    }

    void UpdatePlayerUI(){
        int score = GameManager.gameManager.score;
        scoreText.text = $"{score}";
        healthBar.fillAmount = (float)mainPlayer.life/ mainPlayer.maxLife;
    }

    public void ResetTimeBarUI(){
        currentTime = maxTime;
        timeBar.fillAmount = currentTime/maxTime;
    }

    public void GameLosingUI(string dieReason){
        gameOver.GameOverScene(dieReason);
    }

}
