using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameBonus : MonoBehaviour
{
    public int[] listTypeBonus;
    public List<Sprite> bonusButtonSpritesList;
    public List<GameObject> bonusButtonsList;
    public Player mainPlayer;
    // public GameObject playerShield;
    void Awake(){
        listTypeBonus = new int[]{0, 1, 2};
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
        // mainPlayer.isCountingChangeTime = false;
        // mainPlayer.CancelResetBullet();
        gameObject.SetActive(true);
        GameManager.gameManager.PauseResetBonus();
        var rnd = new System.Random();
        listTypeBonus = listTypeBonus.OrderBy(item => rnd.Next()).ToArray();
        for(int i = 0; i < bonusButtonsList.Count; i++){
            bonusButtonsList[i].GetComponent<Image>().sprite = bonusButtonSpritesList[listTypeBonus[i]];
        }
    }

    public void Button1(){
       ButtonAction(0);
    }

    public void Button2(){
        ButtonAction(1);
    }

    void ButtonAction(int idx){
        bonusButtonsList[idx].transform.DOScale(new Vector3(1.5f,1.5f,1.5f), 0.15f).OnComplete(() =>{
            bonusButtonsList[idx].transform.DOScale(new Vector3(1,1,1), 0.15f).OnComplete(() =>{
                if(listTypeBonus[idx] == 0){
                    mainPlayer.AddLife();
                }
                else if(listTypeBonus[idx] == 1){
                    GameManager.gameManager.bulletDamage = 3;
                    GameManager.gameManager.resetDamageTime = 15;
                }
                else if(listTypeBonus[idx] == 2){
                    GameManager.gameManager.resetBulletTime = 15;
                    GameManager.gameManager.canDestroyBullet2 = true;
                }
                // if(listTypeBonus[idx] == 1 || listTypeBonus[idx] == 2 || listTypeBonus[idx] == 3){
                //     mainPlayer.bonusType = listTypeBonus[idx];
                //     mainPlayer.changeTime = 10;
                // }
                // if(listTypeBonus[idx] == 0){
                //     Instantiate(playerShield, player.transform.position, Quaternion.identity, player.transform);
                // }
                // else if(listTypeBonus[idx] == 4){
                //     mainPlayer.AddLife();
                // }
                // mainPlayer.ResetBullet();
                // GamerReplay();
                gameObject.SetActive(false);
                GameManager.gameManager.NewLevel();
            });
        });
        
    }

    public void GamerReplay(){
        // GameManager.gameManager.enemyTimeSpawn = 1;
        // GameManager.gameManager.isLose = false;
        // gameObject.SetActive(false);
        // GameManager.gameManager.GameContinue();
        // player.SetActive(true);
        // target.SetActive(true);
    }

}
