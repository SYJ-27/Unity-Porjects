using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    public Text textScore, textHighScore;
    public GameObject imgGameOver;
    public Button playButton;

    public void ShowGameOver(int score, int highScore)
    {
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
                playButton.gameObject.transform.DOScale(1, 0.1f);
            });
        InvokeRepeating("NewGame", 0.2f, 0.2f);
    }

    void NewGame()
    {
        GameManager.gameManager.PlayGame();
        CancelInvoke("NewGame");
    }
}
