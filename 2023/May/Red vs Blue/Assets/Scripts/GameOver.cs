using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    public Text scoreText, highScoreText;
    public GameObject replayButton;
    // Start is called before the first frame update
    void Awake(){
        transform.localScale = new Vector3(0,0,0);
        gameObject.SetActive(false);
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GameOverScene(){
        int score = GameManager.gameManager.ballCount;
        int highScore = PlayerPrefs.GetInt("HighScore");
        if(score > highScore){
            highScore = score;
            PlayerPrefs.SetInt("HighScore", score);
        }
        scoreText.text = $"Score: {score}";
        highScoreText.text = $"Best Score: {highScore}";
        gameObject.SetActive(true);
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
    }

    public void Replay(){
        replayButton.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f).OnComplete(() =>{
            replayButton.transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f).OnComplete(() =>{
                SceneManager.LoadScene("GamePlay");
            });
        });
    }

}
