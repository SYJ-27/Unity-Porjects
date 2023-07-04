using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    public Text scoreText, highScoreText, breadText, milkText;
    public GameObject replayButton, collectedText;
    int score, highScore, breadScore, milkScore;
    public bool isShow;
    void Awake()
    {
        score = 0;
        gameObject.SetActive(false);
        transform.localScale = Vector3.zero;
    }
    // Start is called before the first frame update
    void Start()
    {
        isShow = false;
        scoreText.text = $"Score\n{score}";
        highScoreText.gameObject.transform.localScale = Vector3.zero;
        collectedText.gameObject.transform.localScale = Vector3.zero;
        replayButton.gameObject.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isShow){
            IncreaseToScore();
        }
    }

    void IncreaseToScore()
    {
        if (score < GameManager.gameManager.score)
        {
            score++;
            scoreText.text = $"Score\n{score}";
        }
        else
        {
            isShow = false;
            scoreText.text = $"Score\n{score}";
            highScoreText.gameObject.transform.DOScale(Vector3.one, 0.1f).OnComplete(() =>{
                collectedText.gameObject.transform.DOScale(Vector3.one, 0.1f).OnComplete(() =>{
                    replayButton.gameObject.transform.DOScale(Vector3.one, 0.1f);
                });
            });
        }
    }


    public void GameOverScene()
    {
        highScore = PlayerPrefs.GetInt("High Score");
        breadScore = GameManager.gameManager.breadScore;
        milkScore = GameManager.gameManager.milkScore;
        if (GameManager.gameManager.score > highScore)
        {
            highScore = GameManager.gameManager.score;
            PlayerPrefs.SetInt("High Score", highScore);
        }
        highScoreText.text = $"High Score\n{highScore}";
        breadText.text = $"x{breadScore}";
        milkText.text = $"x{milkScore}";
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.1f).OnComplete(()=>{
            isShow = true;
        });
    }

    public void Replay()
    {
        replayButton.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.15f).OnComplete(() =>
        {
            replayButton.transform.DOScale(new Vector3(1f, 1f, 1f), 0.15f).OnComplete(() =>
            {
                SceneManager.LoadScene("GamePlay");
            });
        });
    }
}
