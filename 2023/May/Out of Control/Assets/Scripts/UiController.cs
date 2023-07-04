using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiController : MonoBehaviour
{
    public Player mainPlayer;
    public List<Image> listBonusItem;
    public List<Sprite> listBonusSprite;
    public Transform canvasUi, bonusPanel, buttonsPanel;
    public GameOver gameOver;
    public Text scoreText, timeText, plusScore;
    int score;
    float time;
    bool isDangerTime;
    Vector3 orgBonusPos, orgButtonsPos;
    // Start is called before the first frame update
    void Start()
    {
        orgBonusPos = bonusPanel.position;
        orgButtonsPos = buttonsPanel.position;
        isDangerTime = false;
    }

    void Update(){
        UpdateScoreUI();
        UpdateBonusItemUI();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if(!GameManager.gameManager.isLose && !GameManager.gameManager.isStart && !GameManager.gameManager.isPause){
        //     UpdateTimeUI();
        // }
    }

    public void BonusTimeUI(){
        bonusPanel.DOMoveX(orgBonusPos.x - 3, 0.1f);
        buttonsPanel.DOMoveY(orgBonusPos.y - 5, 0.1f);
    }

    public void ResetGameUI(){
        buttonsPanel.DOMoveY(orgBonusPos.y, 0.1f);
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
            timeText.color =  Color.white;
            isDangerTime = false;
        }
        timeText.text = $"{time}s";
    }

    public void GameLosingUI(){
        gameOver.GameOverScene();
    }

    // public void PlusTimeUI(int time){
    //     var timeX = Instantiate(plusTime, timeText.gameObject.transform.position, Quaternion.identity, canvasUi);
    //     timeX.text = $"+{time}s";
    //     timeX.transform.DOMoveY(timeText.gameObject.transform.position.y - 1.5f, 0.5f).OnComplete(() =>{
    //         Destroy(timeX.gameObject);
    //     });
    // }

    public void PlusScoreUI(Vector3 pos){
        var scoreX = Instantiate(plusScore, pos, Quaternion.identity, canvasUi);
        // scoreX.text = $"+1";
        scoreX.transform.DOMoveY(pos.y + 1.5f, 0.5f).OnComplete(() =>{
            Destroy(scoreX.gameObject);
        });
    }

    
    public void MinusPlanetLifeUI(Vector3 pos, int dameBullet){
        var scoreX = Instantiate(plusScore, pos, Quaternion.identity, canvasUi);
        scoreX.text = $"-{dameBullet}";
        scoreX.color = Color.red;
        scoreX.transform.DOMoveX(pos.x + 1.5f, 0.15f).OnComplete(() =>{
            Destroy(scoreX.gameObject);
        });
    }

    public void UpdateBonusItemUI(){
        listBonusItem[0].gameObject.SetActive(mainPlayer.canRevive);
        if(mainPlayer.shootingMode != 0){
            listBonusItem[1].gameObject.SetActive(true);
            listBonusItem[1].sprite = listBonusSprite[mainPlayer.shootingMode - 1];
        }
        else{
            listBonusItem[1].gameObject.SetActive(false);
        }
    }

}
