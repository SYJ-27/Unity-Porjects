using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    public Text scoreText, highScoreText, servedText;
    public GameObject replayButton;
    public MessageBox messageBox;
    void Awake(){
        gameObject.SetActive(false);
        transform.localScale = Vector3.zero;
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
        int score = messageBox.score;
        int servedNumber = GameManager.gameManager.customerNum;
        if(score > highScore){
            highScore = score;
            PlayerPrefs.SetInt("High Score", highScore);
        }
        servedText.text = $"You have served {servedNumber} customers!";
        scoreText.text = $"Score: {score}";
        highScoreText.text = $"Best Score: {highScore}";
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.1f);
        
    }

    public void Replay(){
        replayButton.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.15f).OnComplete(() =>{
            replayButton.transform.DOScale(new Vector3(1f, 1f, 1f), 0.15f).OnComplete(() =>{
                SceneManager.LoadScene("GamePlay");
            });
        });
    }
}
