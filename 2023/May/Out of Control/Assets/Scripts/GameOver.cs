using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text scoreText, highScoreText;
    public GameObject replayButton, gameOverObj;
    void Awake(){
        // gameObject.transform.localScale = Vector3.zero;
        
        gameOverObj.transform.localScale = Vector3.zero;
        // gameOverObj.transform.position = new Vector3(0, -2, 0);
        scoreText.transform.localScale = Vector3.zero;
        highScoreText.transform.localScale = Vector3.zero;
        // beanObj.transform.localScale = Vector3.zero;
        // timeObj.transform.localScale = Vector3.zero;
        replayButton.transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverScene(){
        int highScore = PlayerPrefs.GetInt("High Score");
        int score = GameManager.gameManager.score;
        // int scoreBean = GameManager.gameManager.scoreBean;
        // int scoreTime = GameManager.gameManager.scoreTime;
        if(highScore < score){
            highScore = score;
            PlayerPrefs.SetInt("High Score", highScore);
        }
        scoreText.text = $"SCORE: {score}";
        highScoreText.text = $"HIGH SCORE: {highScore}";
        // scoreBeanText.text = $"{scoreBean}";
        // scoreTimeText.text = $"{scoreTime}";
        gameObject.SetActive(true);
        // transform.DOScale(Vector3.one, 0.3f);
        // gameOverObj.transform.DOMoveY(2.3f, 0.5f);
        gameOverObj.transform.DOScale(Vector3.one, 0.5f).OnComplete(()=>{
            scoreText.transform.DOScale(Vector3.one, 0.4f).OnComplete(()=>{
                highScoreText.transform.DOScale(Vector3.one, 0.4f).OnComplete(()=>{
        //             beanObj.transform.DOScale(Vector3.one, 0.2f);
        //             timeObj.transform.DOScale(Vector3.one, 0.2f).OnComplete(()=>{
                        replayButton.transform.DOScale(Vector3.one, 0.2f);
        //             });
                });
            });
        });
    }

    public void ReplayGame(){
        replayButton.transform.DOScale(new Vector3(1.5f,1.5f,1.5f), 0.15f).OnComplete(() =>{
            replayButton.transform.DOScale(new Vector3(1f,1f,1f), 0.15f).OnComplete(() =>{
                SceneManager.LoadScene("GamePlay");
            });
        });
    }
}
