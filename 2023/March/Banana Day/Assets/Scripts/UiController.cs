using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public List<Image> imgCoins, imgBananas;
    public List<GameObject> refCoins;
    public Player mainPlayer;
    public Image timeScale;
    public float minTime, maxTime;
    void Awake(){
        minTime = maxTime;
        for(int i = 0; i < refCoins.Count; i++){
            refCoins[i].SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        for(int i = 0; i < GameManager.gameManager.numCoin; i++){
            refCoins[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.gameManager.isLose){
            UpdateCoinUI();
            UpdateTimeUI();
        }
    }

    public void UpdateCoinUI(){
        for(int i = 0; i < mainPlayer.countCoin; i++){
            Debug.Log(i);
            imgCoins[i].color = Color.white;
        }
    }

    public void UpdateTimeUI(){
        minTime -= Time.fixedDeltaTime;
        if(minTime >= 0){
            timeScale.fillAmount = minTime/maxTime;
            if(minTime == 0){
                GameManager.gameManager.GameLosing("");
            }
        }
        else{
                GameManager.gameManager.GameLosing("");

        }
    }

}
