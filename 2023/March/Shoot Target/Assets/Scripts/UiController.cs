using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public List<GameObject> listLifes;
    
    public GameOver gameOver;
    public Player mainPlayer;
    public GameObject gameUI;
    public Text scoreText;
    public int score;
    public GameObject buttonPause;
    public Sprite pauseS, continueS;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText.text = $"{score}";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLifePlayer();
        ChangePauseIcon();
    }

    void UpdateLifePlayer(){
        for(int i =0 ; i < listLifes.Count; i++){
            listLifes[i].SetActive(false);
        }
        for(int i =0 ; i < mainPlayer.lifePlayer; i++){
            listLifes[i].SetActive(true);
        }
        if(mainPlayer.lifePlayer <= 0 && !GameManager.gameManager.isLose){
            mainPlayer.DestroyPlayer();
            GameLose();
        }
    }

    public void UpdateScoreUI(int scoreCurrent){
        score = scoreCurrent;
        scoreText.text = $"{score}";
    }

    public void GameLose(){
        GameManager.gameManager.GameLosing();
        gameUI.SetActive(false);
        gameOver.GameOverScene(score);
    }

    public void ChangePauseIcon(){
        if(!GameManager.gameManager.isPause){
            buttonPause.GetComponent<Image>().sprite = pauseS;
        }
        else{
            buttonPause.GetComponent<Image>().sprite = continueS;
        }
    }

}
