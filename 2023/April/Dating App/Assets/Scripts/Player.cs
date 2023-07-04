using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public List<GameObject> listEyes, listAccessories;
    public int heartNumber;

    void Awake(){
        heartNumber = 3;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateItem(int idxAccessory, int idxEyes){
        for(int i = 0; i < listAccessories.Count; i++){
            if(i == idxAccessory){
                listAccessories[i].SetActive(true);
            }
            else{
                listAccessories[i].SetActive(false);
            }
        }

        for(int i = 0; i < listEyes.Count; i++){
            if(i == idxEyes){
                listEyes[i].SetActive(true);
            }
            else{
                listEyes[i].SetActive(false);
            }
        }
    }

    public void MinusHeart(){
        heartNumber--;
        if(heartNumber <= 0){
            heartNumber = 0;
            GameManager.gameManager.GameLosing();
        }
    }

    public void MoveUp(){
        transform.DOMoveY(1, 0.1f).OnComplete(() =>{
            transform.DOMoveY(0, 0.1f);
        });
    }

    public void MoveDown(){
        transform.DOMoveY(-1, 0.1f).OnComplete(() =>{
            transform.DOMoveY(0, 0.1f);
        });
    }

    public void MoveLeft(){
        transform.DOMoveX(-4.7f, 0.1f).OnComplete(() =>{
            transform.DOMoveX(-3.7f, 0.1f);
        });
    }

    public void MoveRight(){
        transform.DOMoveX(-2.7f, 0.1f).OnComplete(() =>{
            transform.DOMoveX(-3.7f, 0.1f);
        });
    }

    public void Taping(){
        transform.DOScale(new Vector3(2f, 2f, 2f), 0.05f).OnComplete(() =>{
            transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.05f);
        });
    }

}
