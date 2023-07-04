using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    public Text loseText, scoreText, highScoreText;
    public GameObject replayButton, earthImage;
    Vector3 worldPoint;
    void Awake(){
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        earthImage.transform.Rotate(0,0,30 * Time.deltaTime);
    }

    public void GameOverScene(int score){
        loseText.gameObject.transform.localScale = new Vector3(0, 0, 0);
        replayButton.transform.localScale = new Vector3(0, 0, 0);
        int highScore = PlayerPrefs.GetInt("HighScore");
        if(score > highScore){
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        scoreText.text = $"Score: {score}";
        scoreText.gameObject.transform.position = new Vector3(-worldPoint.x - 20, scoreText.gameObject.transform.position.y, 0);
        highScoreText.text = $"High Score: {highScore}";
        highScoreText.gameObject.transform.position = new Vector3(worldPoint.x + 20, highScoreText.gameObject.transform.position.y, 0);
        gameObject.SetActive(true);
        loseText.gameObject.transform.DOScale(Vector3.one, 0.3f).OnComplete(() =>{
            scoreText.gameObject.transform.DOMoveX(0, 0.3f).OnComplete(() =>{
                highScoreText.gameObject.transform.DOMoveX(0, 0.3f).OnComplete(() =>{
                    replayButton.transform.DOScale(Vector3.one, 0.3f);
                });
            });
        });
    }

    public void Replay(){
        replayButton.transform.DOScale(new Vector3(1.5f,1.5f,1.5f), 0.3f).OnComplete(() =>{
            replayButton.transform.DOScale(new Vector3(1f,1f,1f), 0.3f).OnComplete(() =>{
                SceneManager.LoadScene("GamePlay");
            });
        });
    }
}
