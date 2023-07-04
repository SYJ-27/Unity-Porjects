using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public GameOver gameOver;
    public Text scoreText, bulletText;
    public Image heartBar;
    public Player mainPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGameUI();
    }

    void UpdateGameUI(){
        int score = GameManager.gameManager.score;
        int bulletNum = mainPlayer.numBullet;
        float currentHealth = mainPlayer.lifePlayer;
        float maxHealth = mainPlayer.maxLife;
        scoreText.text = $"{score}";
        bulletText.text = $"{bulletNum}";
        if(currentHealth > 0){
            heartBar.fillAmount = currentHealth/maxHealth;
        }
        else{
            currentHealth = 0;
            heartBar.fillAmount = currentHealth/maxHealth;
            if(!GameManager.gameManager.isLose){
                Invoke("GameLosingScene", 0.1f);
            }
        }
    }

    void GameLosingScene(){
        GameManager.gameManager.GameLosing();
        gameOver.GameOverScene();
    }
}
