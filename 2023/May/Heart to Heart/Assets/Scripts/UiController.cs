using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiController : MonoBehaviour
{
    public GameOver gameOver;
    public Player mainHeart;
    public List<GameObject> listLifePlayer;
    public Text scoreText;
    int score, playerLife;
    // Start is called before the first frame update
    void Start()
    {
        score = -1;
        playerLife = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.gameManager.isLose){
            UpdateLifeUI();
            UpdateScoreUI();
        }
    }

    void UpdateLifeUI(){
        if(playerLife != mainHeart.life){
            for(int i = 0; i < listLifePlayer.Count; i++){
                if(i < mainHeart.life){
                    // listLifePlayer[i].SetActive(true);
                    listLifePlayer[i].transform.DOScale(Vector3.one * 1.3f, 0.1f);
                }
                else{
                    // listLifePlayer[i].SetActive(false);
                    listLifePlayer[i].transform.DOScale(Vector3.zero, 0.1f);
                }
            }
            playerLife = mainHeart.life;
        }
        if(mainHeart.life <= 0){
            GameManager.gameManager.GameLosing();
        }
    }

    void UpdateScoreUI(){
        if(score != GameManager.gameManager.score){
            score = GameManager.gameManager.score;
            scoreText.text = $"{score}";
        }
    }

    public void GameLosingUI(){
        gameOver.GameOverScene();
    }
}
