using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    public Text statusText, bananaText, coinText, elseText, loseText;
    public Button nextGame;
    public Transform nextGameButton;
    public Sprite continueImg, replayImg;
    public Player mainPlayer;
    void Awake(){
        transform.DOScale(new Vector3(0,0,0), 0);
        gameObject.SetActive(false);
    }
    public void GameOverScene(string status, int banana, int coin, int elses){
        if(status == "Win"){
            loseText.text = "TASK COMPLETED!";
            statusText.text = "Next Game?";
            nextGame.GetComponent<Image>().sprite = continueImg;
        }
        else{
            loseText.text = "TOO BAD!";
            statusText.text = status;
            nextGame.GetComponent<Image>().sprite = replayImg;
        }
        bananaText.text = $"{banana}/{GameManager.gameManager.numBanana}";
        coinText.text = $"{coin}/{GameManager.gameManager.numCoin}";
        elseText.text = $"{elses}";
        gameObject.SetActive(true);
        transform.DOScale(new Vector3(1,1,0), 0.3f);

    }

    public void Replay(){
        nextGameButton.DOScale(1.5f, 0.1f).OnComplete(() =>{
            nextGameButton.DOScale(1f, 0.1f).OnComplete(() =>{
                SceneManager.LoadScene("GamePlay");
            });
        });

    }
}
