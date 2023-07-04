using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text textRank, textBestRank;
    public List<Text> orderTexts;
    public Button playButton;
    private int rank = 0, bestRank = 0;
    public Monster mainMonster;


    void Awake(){
        transform.localScale = new Vector3(0,0,0);
        gameObject.SetActive(false);
    }

    public void GameOverScene(int currentRank)
    {
        bestRank = PlayerPrefs.GetInt("Best Rank");
        UpdateOrderText();
        gameObject.SetActive(true);
        rank = currentRank;
        if(currentRank > bestRank){
            bestRank = currentRank;
            PlayerPrefs.SetInt("Best Rank", bestRank);
        }
        textRank.text = $"Rank: {rank}";
        textBestRank.text = $"Best Rank: {bestRank}";

        transform.DOScale(1, 0.3f);
        
    }

    public void Play()
    {
        playButton.gameObject.transform.DOScale(1.5f, 0.1f).OnComplete(() =>
        {
            playButton.gameObject.transform.DOScale(1, 0.1f).OnComplete(() =>{
                transform.DOMoveY(-100, 0.1f).OnComplete(() =>{
                    SceneManager.LoadScene("GamePlay");
                });
            });
        });
    }

    public void UpdateOrderText(){
        for(int i = 0; i < orderTexts.Count; i++){
            orderTexts[i].text = $"{mainMonster.listOfNumberOver[i]}";
        }
    }

}
