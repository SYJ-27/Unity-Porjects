using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text textScore, textHighScore;
    public GameObject imgGameOver;
    public Button playButton;
    public int highScore = 0, score = 0;

    public void ShowGameOver(int currentScore)
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        if(currentScore > highScore){
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        score = currentScore;
        imgGameOver.transform.DOScale(1, 0.3f);
        textScore.text = $"{score}";
        textHighScore.text = $"{highScore}";
        imgGameOver.SetActive(true);
    }

    public void HideGameOver()
    {
        imgGameOver.transform.DOScale(0, 0);
        imgGameOver.SetActive(false);
    }

    public void Play()
    {
        playButton.gameObject.transform.DOScale(1.5f, 0.1f)
         .OnComplete(() =>
            {
                playButton.gameObject.transform.DOScale(1, 0.1f).OnComplete(() =>{
                    // SceneManager.LoadScene("GamePlay");
                    // Invoke("NewGame", 5f);
                    NewGame();
                });
            });
        
    }

    void NewGame()
    {
        SceneManager.LoadScene("GamePlay");
        // GameManager.gameManager.PlayGame();
        // CancelInvoke("NewGame");
    }
}
