using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Player mainPlayer;
    public Text textScore, textHighScore, textReasonLose;
    public Button playButton;
    public int highScore = 0, score = 0;

    void Awake(){
        transform.DOScale(0, 0f);
        gameObject.SetActive(false);
    }

    public void GameOverScene(string reasonLose, int currentScore)
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        if(currentScore > highScore){
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        score = currentScore;
        textReasonLose.text = reasonLose;
        textScore.text = $"Score: {score}";
        textHighScore.text = $"High Score: {highScore}";
        gameObject.SetActive(true);
        transform.DOScale(1, 0.3f);
    }

    public void RePlay()
    {
        playButton.gameObject.transform.DOScale(1.5f, 0.2f).OnComplete(() => {
            playButton.gameObject.transform.DOScale(1, 0.2f).OnComplete(() => {
                SceneManager.LoadScene("GamePlay");
            });
        });
    }
}
