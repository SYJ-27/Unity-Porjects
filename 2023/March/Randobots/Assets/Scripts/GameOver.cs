using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text textWin, textBrownBreak, textBlueBreak, textTime;
    public Button playButton;
    public Image healthPlayer, healthEnemy;

    public Enemy carEnemy;
    public Player carPlayer;


    void Awake(){
        transform.localScale = new Vector3(0,0,0);
        gameObject.SetActive(false);
    }

    public void GameOverScene(float timePlay)
    {
        gameObject.SetActive(true);
        if(carEnemy.life <= 0){
            textWin.text = $"BLUE WINS";
        }
        else{
            textWin.text = $"BROWN WINS";

        }
        timePlay = (float)Math.Round(timePlay, 2);
        textBrownBreak.text = $"{carEnemy.breakNumber}\nBREAKs";
        textBlueBreak.text = $"{carPlayer.breakNumber}\nBREAKs";
        textTime.text = $"Times:\n{timePlay}s";
        healthEnemy.GetComponent<RectTransform>().anchoredPosition  = new Vector3(-200, 42);
        healthPlayer.GetComponent<RectTransform>().anchoredPosition = new Vector3(200, 42);
        transform.DOScale(1, 0.3f);
        
    }

    public void Play()
    {
        playButton.gameObject.transform.DOScale(1.5f, 0.1f).OnComplete(() =>
        {
            playButton.gameObject.transform.DOScale(1, 0.1f).OnComplete(() =>{
                transform.DOMoveY(-100, 0f).OnComplete(() =>{
                    SceneManager.LoadScene("GamePlay");
                });
            });
        });
    }

}
