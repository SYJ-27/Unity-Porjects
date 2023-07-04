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
    public Text scoreText, timeText, plusBean, plusTime;
    int score;
    float time;
    bool isDangerTime;
    // Start is called before the first frame update
    void Start()
    {
        isDangerTime = false;
    }

    void Update(){
        UpdateScoreUI();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if(!GameManager.gameManager.isLose){
        //     UpdateTimeUI();
        // }
    }

    void UpdateScoreUI(){
        score = GameManager.gameManager.score;
        scoreText.text = $"{score}";
    }

    void UpdateTimeUI(){
        time = GameManager.gameManager.time;
        time = (float)Math.Round(time, 1);
        if(time <= 5){
            if(!isDangerTime){
                timeText.color =  Color.red;
                isDangerTime = true;
                timeText.gameObject.transform.DOScale(2, 0.1f).OnComplete(() =>{
                    timeText.gameObject.transform.DOScale(1, 0.1f);
                });
            }
        }
        else{
            timeText.color =  Color.black;
            isDangerTime = false;
        }
        timeText.text = $"{time}s";
    }

    public void GameLosingUI(){
        gameOver.GameOverScene();
    }

    public void PlusTimeUI(int time){
        var timeX = Instantiate(plusTime, timeText.gameObject.transform.position, Quaternion.identity, canvasUi);
        timeX.text = $"+{time}s";
        timeX.transform.DOMoveY(timeText.gameObject.transform.position.y - 1.5f, 0.5f).OnComplete(() =>{
            Destroy(timeX.gameObject);
        });
    }

    public void PlusBeanUI(){
        var beanX = Instantiate(plusBean, scoreText.gameObject.transform.position, Quaternion.identity, canvasUi);
        beanX.text = $"+1";
        beanX.transform.DOMoveY(scoreText.gameObject.transform.position.y - 1.5f, 0.5f).OnComplete(() =>{
            Destroy(beanX.gameObject);
        });
    }


}
