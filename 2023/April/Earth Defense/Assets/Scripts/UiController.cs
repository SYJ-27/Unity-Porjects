using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiController : MonoBehaviour
{
    public Player mainPlayer;
    public GameOver gameOver;
    public Text timeCountText, scoreText;
    public Image healthPlayerImage;
    float timeCount, healthPlayer, fullHealth;

    public GameObject destroyButton;

    void Awake(){
        healthPlayer = 100;
        fullHealth = 100;
        destroyButton.SetActive(false);
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
        UpdatePlayerUI();
    }

    void UpdatePlayerUI(){
        if(!GameManager.gameManager.isAttackTime){
            timeCount = mainPlayer.timeToAttack - mainPlayer.attackTime;
            timeCount = (float)Math.Round(timeCount, 2);
            timeCountText.text = $"{timeCount}";
        }
        else{
            timeCountText.text = "Immortal Time";
        }
        if(GameManager.gameManager.isBonusTime){
            timeCountText.text = "Bonus Time";
        }
        scoreText.text = $"{mainPlayer.score}";
        healthPlayer = mainPlayer.health;
        healthPlayerImage.fillAmount = healthPlayer/fullHealth;
    }

    public void GameOverUI(){
        gameOver.GameOverScene(mainPlayer.score);
    }

    public void DestroyBonus(){
        destroyButton.transform.DOScale(new Vector3(1.3f,1.3f,1.3f), 0.1f).OnComplete(() =>{
            destroyButton.transform.DOScale(new Vector3(0.7f,0.7f,0.7f), 0.1f).OnComplete(() =>{
                GameManager.gameManager.AllDestroyBonus();
                destroyButton.SetActive(false);
            });
        });
    }

}
