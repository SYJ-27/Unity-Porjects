using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public int countCoin, countBanana, countElse;
    public GameObject rightHand;
    public Transform hand;
    void Awake(){
        countBanana = 0;
        countCoin = 0;
        countElse = 0;
        rightHand.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(countCoin == GameManager.gameManager.numCoin && countBanana == GameManager.gameManager.numBanana){
            GameManager.gameManager.GameLosing("");
        }
    }
    public void UpdateCoin(){
        countCoin++;
    }

    public void UpdateBanana(){
        countBanana++;
    }

    public void UpdateElse(){
        countElse++;
    }

    public void RightHandEat(GameObject coin){
        coin.transform.parent = rightHand.transform;
        coin.transform.position = hand.position;
        rightHand.SetActive(true);
        rightHand.transform.DORotate(new Vector3(0,0,340), 0.4f).OnComplete(() =>{
            rightHand.transform.DORotate(new Vector3(0,0,180), 0);
            rightHand.SetActive(false);
        });
    }

    public void RightHandCatch(GameObject coin){
        coin.transform.parent = rightHand.transform;
        coin.transform.position = hand.position;
        rightHand.SetActive(true);
        rightHand.transform.DORotate(new Vector3(0,0,340), 0.15f).OnComplete(() =>{
            rightHand.transform.DORotate(new Vector3(0,0,440), 0.15f).OnComplete(() =>{
                rightHand.transform.DORotate(new Vector3(0,0,180), 0);
                rightHand.SetActive(false);
                Destroy(coin);
            });
        });
    }
}
