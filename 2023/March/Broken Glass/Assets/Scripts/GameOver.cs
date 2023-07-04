using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    public Text cupText, plateText, teapotText, scoreText, highScoreText;
    public int score, highScore, cupC, plateC, teapotC;
    public Transform buttonReplay;
    public Player mainPlayer;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        highScore = 0;
        cupC = 0;
        plateC = 0;
        teapotC = 0;
        transform.DOScale(new Vector3(0,0,0), 0);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverScene(){
        score = GameManager.gameManager.score;
        highScore = PlayerPrefs.GetInt("HighScore");
        if(score > highScore){
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        cupC = mainPlayer.numCup;
        teapotC = mainPlayer.numTeapot;
        plateC = mainPlayer.numPlate;
        scoreText.text = $"Score: {score}";
        highScoreText.text = $"High Score: {highScore}";
        cupText.text = $"x{cupC}";
        plateText.text = $"x{plateC}";
        teapotText.text = $"x{teapotC}";
        gameObject.SetActive(true);
        transform.DOScale(new Vector3(1,1,1), 0.3f);
        
    }

    public void Replay(){
        buttonReplay.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.15f).OnComplete(() =>{
            buttonReplay.DOScale(new Vector3(1f, 1f, 1f), 0.15f).OnComplete(() =>{
                SceneManager.LoadScene("GamePlay");
            });
        });
    }

}
