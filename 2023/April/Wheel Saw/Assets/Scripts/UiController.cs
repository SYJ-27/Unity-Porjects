using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiController : MonoBehaviour
{
    public GameOver gameOver;
    public Text textTimePlay;
    public GameObject moveButtons;
    public Image dangerImage;
    public Color color1, color2;
    public float timePlay;
    public int idx = 0;
    public bool isDangerTime;
    // Start is called before the first frame update
    void Start()
    {
        isDangerTime = false;
        timePlay = 0;
        textTimePlay.text = $"{timePlay}";
        moveButtons.SetActive(false);
        dangerImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.gameManager.isLose){
            UpdateTimeUI();
            if((int)Math.Round(timePlay, 0) % 15 == 0 && timePlay > 1 && !isDangerTime){
                isDangerTime = true;
                moveButtons.SetActive(true);
                GameManager.gameManager.ExtremeSpawnSaw();
            }
        }
    }

    void UpdateTimeUI(){
        timePlay += Time.deltaTime;
        timePlay = (float)Math.Round(timePlay, 2);
        textTimePlay.text = $"{timePlay}";
    }

    public void GameLosingUI(){
        timePlay = (float)Math.Round(timePlay, 2);
        gameOver.GameOverScene(timePlay);
    }

    public void DangerNotice(){
        if(isDangerTime){
            dangerImage.gameObject.SetActive(true);
            if(idx % 2 == 0){
                dangerImage.DOColor(color2, 0.3f).OnComplete(()=>{
                    idx++;
                    DangerNotice();
                });
            }
            else{
                dangerImage.DOColor(color1, 0.3f).OnComplete(()=>{
                    idx++;
                    DangerNotice();
                });
            }
        }
        else{
            idx = 0;
            dangerImage.color = color1;
            dangerImage.gameObject.SetActive(false);
            moveButtons.SetActive(false);
        }
    }

    public void CancelNotice(){
        isDangerTime = false;
    }
}
