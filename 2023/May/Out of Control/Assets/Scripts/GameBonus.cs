using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameBonus : MonoBehaviour
{
    public List<Sprite> listButtonSprites;
    public List<Button> listButtons;
    int[] idButtons;
    public Player mainPlayer;
    Vector3 orgPos;
    void Awake(){
        idButtons = new int[]{0,1,2,3};
    }
    // Start is called before the first frame update
    void Start()
    {
        orgPos = transform.position;
        ShuffleBonuses();
    }

    void ShuffleBonuses(){
        var rnd = new System.Random();
        idButtons = idButtons.OrderBy(item => rnd.Next()).ToArray();

        for(int i = 0; i < listButtons.Count; i++){
            listButtons[i].GetComponent<Image>().sprite = listButtonSprites[idButtons[i]];
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Button1(){
        Bonus(0);
    }

    public void Button2(){
        Bonus(1);
    }

    public void Button3(){
        Bonus(2);
    }

    void Bonus(int idx){
        mainPlayer.SetShootingMode(idButtons[idx] + 1);
        listButtons[idx].transform.DOScale(new Vector3(1.5f,1.5f,1.5f), 0.15f).OnComplete(() =>{
            listButtons[idx].transform.DOScale(new Vector3(1,1,1), 0.15f).OnComplete(() =>{ 
                transform.DOMoveX(orgPos.x, 0.2f).OnComplete(() =>{
                    GameManager.gameManager.ContinueGame();
                    ShuffleBonuses();
                });
            });
        });
    }

}
