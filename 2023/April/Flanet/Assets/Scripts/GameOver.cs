using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text scoreFlowerText, highScoreFlowerText, scoreBulletText, scoreButterflyText;
    public GameObject replayButton;
    void Awake(){
        gameObject.transform.localScale = Vector3.zero;
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
        int score = GameManager.gameManager.GetTotalScoreFlower();
        if(highScore < score){
            highScore = score;
            PlayerPrefs.SetInt("High Score", highScore);
        }
        highScoreFlowerText.text = $"{highScore}";
        scoreFlowerText.text = $"{score}";
        scoreBulletText.text = $"{GameManager.gameManager.scoreEnemies[0]}";
        scoreButterflyText.text = $"{GameManager.gameManager.scoreEnemies[1]}";
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.3f);
    }

    public void ReplayGame(){
        replayButton.transform.DOScale(new Vector3(2f,2f,2f), 0.15f).OnComplete(() =>{
            replayButton.transform.DOScale(new Vector3(1.5f,1.5f,1.5f), 0.15f).OnComplete(() =>{
                SceneManager.LoadScene("GamePlay");
            });
        });
    }
}
