using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static GameOver mainGameOver;
    public Text textScore, textHighScore;
    public GameObject imgGameOver;
    public Button playButton;
    
    public int highScore = 0, score = 0;

    void Awake(){
        // PlayerPrefs.SetInt("HighScore", highScore);
        mainGameOver = this;
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    public void ShowGameOver(int currentScore)
    {
        if(currentScore > highScore){
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        score = currentScore;
        imgGameOver.transform.DOMoveY(imgGameOver.transform.position.y - 10, 0.3f);
        textScore.text = $"{score}";
        textHighScore.text = $"{highScore}";
        imgGameOver.SetActive(true);
    }

    public void HideGameOver()
    {
        imgGameOver.transform.DOMoveY(imgGameOver.transform.position.y + 10, 0);
        imgGameOver.SetActive(false);
    }

    public void Play()
    {
        playButton.gameObject.transform.DOScale(1.5f, 0.1f)
         .OnComplete(() =>
            {
                playButton.gameObject.transform.DOScale(1, 0.1f).OnComplete(() =>
                {
                    NewGame();
                    // Invoke("NewGame", 0.2f);
                });
            });
    }

    void NewGame()
    {
        SceneManager.LoadScene("GamePlay");
        // GameManager.gameManager.PlayGame();

    }
}
