using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    public Text scoreText, highScoreText, timeText;
    private int highScore;
    public Transform replayButton;
    void Awake(){
        transform.localScale = new Vector3(0,0,0);
        gameObject.SetActive(false);
        scoreText.color = Color.black;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOverScene(int score, float time){
        gameObject.SetActive(true);

        highScore = PlayerPrefs.GetInt("High Score");
        if(score > highScore){
            highScore = score;
            PlayerPrefs.SetInt("High Score", highScore);
            scoreText.color = Color.white;
        }
        scoreText.text = $"SCORE\n{score}";
        highScoreText.text = $"HIGH SCORE\n{highScore}";
        time = (float)Math.Round(time, 2);
        timeText.text = $"Times: {time}";
        transform.DOScale(new Vector3(1, 1, 1), 0.3f);
    }

    public void RePlay(){
        replayButton.DOScale(new Vector3(1.5f,1.5f,1.5f), 0.1f).OnComplete(() =>{
            replayButton.DOScale(new Vector3(1f,1f,1f), 0.1f).OnComplete(() =>{
                SceneManager.LoadScene("GamePlay");
            });
        });
    }

}
