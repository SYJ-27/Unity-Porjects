using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    public Text scoreText, highScoreText;
    public int score, highScore;
    public GameObject buttonPlay;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(new Vector3(0,0,0), 0);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverScene(int scoreCurrent){
        score = scoreCurrent;
        highScore = PlayerPrefs.GetInt("HighScore");
        if(score > highScore){
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        scoreText.text = $"SCORE: {score}";
        highScoreText.text = $"HIGH SCORE: {highScore}";
        gameObject.SetActive(true);
        transform.DOScale(new Vector3(1,1,1), 0.3f);
    }

    public void Replay(){
        buttonPlay.transform.DOScale(new Vector3(1.5f,1.5f,1.5f), 0.15f).OnComplete(() =>{
            buttonPlay.transform.DOScale(new Vector3(1,1,1), 0.15f).OnComplete(() =>{ 
                SceneManager.LoadScene("Gameplay");
            });
        });
    }
}
