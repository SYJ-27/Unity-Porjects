using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    // public Text textCurrentRank;
    public GameObject scoreUI;
    public GameOver gameOver;
    private int currentRank;
    public Monster mainMonster;

    public List<GameObject> listOfHeart;

    void Awake(){
        currentRank = 0;
        // textCurrentRank.text = $"{currentRank}";
    }
    void Update(){
        UpdateMonsterLife();
    }
    public void UpdateScoreUI(){
        currentRank++;
        // textCurrentRank.text = $"{currentRank}";
    }

    public void HideScoreUI(){
        scoreUI.SetActive(false);
        gameOver.GameOverScene(currentRank);
    }
    public void UpdateMonsterLife(){
        if(mainMonster.life >= 0){
            int disableHeart = (listOfHeart.Count - mainMonster.life);
            for(int i = 0; i < disableHeart; i++){
                if(i == disableHeart - 1 && listOfHeart[i] != null){
                    listOfHeart[i].GetComponent<Heart>().DestroyItSelf();
                }
            }
            
        }

    }

}
