using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text scoreText, highScoreText;
    public GameObject replayButton;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverScene(){
        int highScore = PlayerPrefs.GetInt("High Score");
        int score = GameManager.gameManager.score;
        if(highScore < score){
            highScore = score;
            PlayerPrefs.SetInt("High Score", highScore);
        }
        highScoreText.text = $"Score: {highScore}";
        scoreText.text = $"High Score: {score}";
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.3f);
    }

    public void ReplayGame(){
        replayButton.transform.DOScale(new Vector3(2f,2f,2f), 0.15f).OnComplete(() =>{
            replayButton.transform.DOScale(new Vector3(0f,0f,0f), 0.2f).OnComplete(() =>{
                transform.DOScale(new Vector3(0f,0f,0f), 0.15f).OnComplete(() =>{
                    SceneManager.LoadScene("GamePlay");
                });
            });
        });
    }

}
