using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    public Text scoreText, highScoreText, gameOverText;
    public GameObject replayButton, scoreText1, highScoreText1;
    int score, highScore, gameOverNumber, scoreNumber;

    // Start is called before the first frame update
    void Start()
    {
        gameOverNumber = 1;
        scoreNumber = 1;
        gameObject.SetActive(false);
        // transform.localScale = Vector3.zero;
        gameOverText.gameObject.transform.localScale = Vector3.zero;
        scoreText1.transform.localScale = Vector3.zero;
        highScoreText1.transform.localScale = Vector3.zero;
        replayButton.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverScene(string dieReason){
        score = GameManager.gameManager.score;
        highScore = PlayerPrefs.GetInt("High Score");
        if(highScore < score){
            highScore = score;
            PlayerPrefs.SetInt("High Score", highScore);
        }
        gameOverText.text = dieReason;
        scoreText.text = $"{score}";
        highScoreText.text = $"{highScore}";
        gameObject.SetActive(true);
        gameOverText.gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).OnComplete(() =>{
            ScaleGameOverText();
            scoreText1.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).OnComplete(() =>{
                // ScaleScoreText();
                highScoreText1.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).OnComplete(() =>{
                    replayButton.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
                });
            });
        });
        // transform.DOScale(new Vector3(1f, 1f,1f), 0.15f);
    }

    void ScaleGameOverText(){
        gameOverText.gameObject.transform.DOScale(new Vector3(1f + gameOverNumber * 0.1f, 1f + gameOverNumber * 0.1f, 1f + gameOverNumber * 0.1f), 0.3f).OnComplete(() =>{
            gameOverNumber = -gameOverNumber;
            ScaleGameOverText();
        });
    }

    void ScaleScoreText(){
        scoreText.gameObject.transform.DOScale(new Vector3(1f + scoreNumber * 0.1f, 1f + scoreNumber * 0.1f, 1f + scoreNumber * 0.1f), 0.3f).OnComplete(() =>{
            scoreNumber = -scoreNumber;
            ScaleScoreText();
        });
    }

    public void Replay(){
        replayButton.transform.DOScale(new Vector3(1.5f, 1.5f,1.5f), 0.15f).OnComplete(() =>{
            replayButton.transform.DOScale(new Vector3(1f, 1f,1f), 0.15f).OnComplete(() =>{
                SceneManager.LoadScene("GamePlay");
            });
        });
    }
}
