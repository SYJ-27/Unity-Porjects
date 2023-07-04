using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameBonus : MonoBehaviour
{
    int[] listTypeBonus;
    public List<Image> bonusButtonImagesList;
    // public List<Color> bonusButtonColorsList;
    public List<Sprite> bonusButtonSpritesList;
    public List<GameObject> bonusButtonsList;
    public Player mainPlayer;
    public UiController uiController;
    void Awake(){
        listTypeBonus = new int[]{0, 1, 2, 3, 4};
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

    public void GetBonusScene(){
        gameObject.SetActive(true);
        var rnd = new System.Random();
        listTypeBonus = listTypeBonus.OrderBy(item => rnd.Next()).ToArray();
        for(int i = 0; i < bonusButtonImagesList.Count; i++){
            bonusButtonImagesList[i].sprite = bonusButtonSpritesList[listTypeBonus[i]];
        }
    }

    public void Button1(){
       ButtonAction(0);
    }

    public void Button2(){
        ButtonAction(1);
    }

    void ButtonAction(int idx){
        mainPlayer.ResetBonus();
        bonusButtonsList[idx].transform.DOScale(new Vector3(2f,2f,2f), 0.15f).OnComplete(() =>{
            bonusButtonsList[idx].transform.DOScale(new Vector3(1.5f,1.5f,1.5f), 0.15f).OnComplete(() =>{
                if(listTypeBonus[idx] == 0){
                    mainPlayer.healthByEnemy = 2;
                }
                if(listTypeBonus[idx] == 1){
                    mainPlayer.botNumber++;
                    if(mainPlayer.botNumber >= 3){
                        mainPlayer.botNumber = 3;
                    }
                }
                if(listTypeBonus[idx] == 2){
                    mainPlayer.health += 10;
                    if(mainPlayer.health >= 100){
                        mainPlayer.health = 100;
                    }
                }
                if(listTypeBonus[idx] == 3){
                    mainPlayer.GetShield();
                }
                if(listTypeBonus[idx] == 4){
                    uiController.destroyButton.SetActive(true);
                }
                ContinuePlay();
            });
        });
        
    }

    public void ContinuePlay(){
        gameObject.SetActive(false);
        GameManager.gameManager.isBonusTime = false;
    }
}
