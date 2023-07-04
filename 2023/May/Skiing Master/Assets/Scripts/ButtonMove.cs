using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonMove : MonoBehaviour
{
    public Player mainPlayer;
    public List<Sprite> listSpriteButtons;
    public Text buttonText;
    public int numberMove, idMove;
    // Start is called before the first frame update
    void Start()
    {
        // ResetButtonSprite();
        ResetNumberMove();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetNumberMove(){
        numberMove = UnityEngine.Random.Range(0, 4);
        buttonText.text = $"{numberMove}";
    }

    void ResetButtonSprite(){
        numberMove = UnityEngine.Random.Range(0, 4);
        buttonText.text = $"{numberMove}";
    }

    void PlayerMove(){
        if(idMove == 0){
            mainPlayer.MoveDown(numberMove);
        }
        else if(idMove == 1){
            mainPlayer.MoveCrossLeft(numberMove);
        }
        else if(idMove == 2){
            mainPlayer.MoveLeft(numberMove);
        }
        else if(idMove == 3){
            mainPlayer.MoveCrossRight(numberMove);
        }
        else{
            mainPlayer.MoveRight(numberMove);
        }
    }

    public void SelectButton(){
        PlayerMove();
        GetComponent<Button>().enabled = false;
        transform.DOScale(Vector3.one * 2, 0.15f).SetId("MoveButton1").OnComplete(()=>{
            transform.DOScale(Vector3.zero, 0.1f).SetId("MoveButton2").OnComplete(()=>{
                if(!GameManager.gameManager.isLose && !GameManager.gameManager.isStart){
                    ResetNumberMove();
                    transform.DOScale(Vector3.one, 0.1f).SetId("MoveButton3").OnComplete(()=>{
                        if(!GameManager.gameManager.isLose && !GameManager.gameManager.isStart){
                            GetComponent<Button>().enabled = true;
                        }
                    });
                }
            });
        });
    }


}
