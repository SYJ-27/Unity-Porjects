using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    public Text scoreText, highScoreText, eggBrokedText;
    public GameObject replayButton, scoreObj, highScoreObj, eggBrokedObj, gameOverObj;
    int score, highScore, eggBroked, scoreNumber;
    // Start is called before the first frame update
    void Start()
    {
        scoreNumber = 1;
        gameObject.SetActive(false);
        gameOverObj.transform.localScale = Vector3.zero;
        scoreObj.transform.localScale = Vector3.zero;
        highScoreObj.transform.localScale = Vector3.zero;
        eggBrokedObj.transform.localScale = Vector3.zero;
        replayButton.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverScene(){
        score = GameManager.gameManager.score;
        eggBroked = GameManager.gameManager.eggBroked;
        highScore = PlayerPrefs.GetInt("High Score");
        if(highScore < score){
            highScore = score;
            PlayerPrefs.SetInt("High Score", highScore);
        }
        scoreText.text = $"{score}";
        highScoreText.text = $"{highScore}";
        eggBrokedText.text = $"{eggBroked}";
        gameObject.SetActive(true);
        gameOverObj.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).OnComplete(() =>{
            scoreObj.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).OnComplete(() =>{
                ScaleScoreText();
                highScoreObj.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).OnComplete(() =>{
                    eggBrokedObj.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).OnComplete(() =>{
                            replayButton.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
                    });
                });
            });
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
