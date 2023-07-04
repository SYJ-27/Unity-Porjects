using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text textScore, textHighScore;
    public Button playButton;
    private int highScore = 0, score = 0;


    void Awake(){
        transform.localScale = new Vector3(0,0,0);
        gameObject.SetActive(false);
    }

    public void GameOverScene(int currentScore)
    {
        gameObject.SetActive(true);
        // PlayerPrefs.SetInt("HighScore", highScore);

        highScore = PlayerPrefs.GetInt("HighScore");
        if(currentScore > highScore){
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        score = currentScore;
        
        textScore.text = $"{score}";
        textHighScore.text = $"{highScore}";

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
