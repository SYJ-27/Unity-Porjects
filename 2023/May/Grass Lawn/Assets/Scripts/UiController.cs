using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiController : MonoBehaviour
{
    public Transform canvasUi;
    public GameOver gameOver;
    public Text levelText, timeText/*, plusBean, plusTime*/;
    public Image playerHealthImg, playerDadHealthImg;
    public Player mainPlayer;
    public PlayerDad mainPlayerDad;
    int level;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        // time = 0;
    }

    void Update(){
        UpdateLevelUI();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if(!GameManager.gameManager.isLose){
        //     UpdateTimeUI();
        // }
        UpdatePlayerDadHealthUI();
        UpdatePlayerHealthUI();
    }

    void UpdateLevelUI(){
        level = GameManager.gameManager.level;
        levelText.text = $"{level}";
    }

    void UpdateTimeUI(){
        // time = GameManager.gameManager.time;
        // time = (float)Math.Round(time, 1);
        // if(time <= 5){
        //     if(!isDangerTime){
        //         timeText.color =  Color.red;
        //         isDangerTime = true;
        //         timeText.gameObject.transform.DOScale(2, 0.1f).OnComplete(() =>{
        //             timeText.gameObject.transform.DOScale(1, 0.1f);
        //         });
        //     }
        // }
        // else{
        //     timeText.color =  Color.black;
        //     isDangerTime = false;
        // }
        // timeText.text = $"{time}s";
    }

    public void UpdatePlayerHealthUI(){
        float life = (float)mainPlayer.life;
        float maxLife = (float)mainPlayer.maxLife;
        // Debug.Log(life);
        if(life >= 0){
            playerHealthImg.fillAmount = life / maxLife;
        }
    }

    public void UpdatePlayerDadHealthUI(){
        float life = (float)mainPlayerDad.life;
        float maxLife = (float)mainPlayerDad.maxLife;
        // Debug.Log(life);
        if(life >= 0){
            playerDadHealthImg.fillAmount = life / maxLife;
        }
    }

    public void GameLosingUI(){
        gameOver.GameOverScene();
    }

    public void GameWinUI(){
        gameOver.GameWinScene();
    }

    // public void PlusTimeUI(int time){
    //     var timeX = Instantiate(plusTime, timeText.gameObject.transform.position, Quaternion.identity, canvasUi);
    //     timeX.text = $"+{time}s";
    //     timeX.transform.DOMoveY(timeText.gameObject.transform.position.y - 1.5f, 0.5f).OnComplete(() =>{
    //         Destroy(timeX.gameObject);
    //     });
    // }

    // public void PlusBeanUI(){
    //     var beanX = Instantiate(plusBean, levelText.gameObject.transform.position, Quaternion.identity, canvasUi);
    //     beanX.text = $"+1";
    //     beanX.transform.DOMoveY(levelText.gameObject.transform.position.y - 1.5f, 0.5f).OnComplete(() =>{
    //         Destroy(beanX.gameObject);
    //     });
    // }


}
