using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Soil : MonoBehaviour
{
    public bool isPick, isPlanted;
    public Transform pickIcon;
    void Awake(){
        isPick = false;
        isPlanted = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlanted){
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else{
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        if(isPick){
            MovePickIcon();
        }
    }

    void OnMouseDown(){
        GameManager.gameManager.UnPickAllSoil();
        isPick = true;
        MovePickIcon();
    }

    public void SetIsPick(bool state){
        if(state){
            GameManager.gameManager.UnPickAllSoil();
        }
        isPick = state;
    }

    public void SetIsPlanted(bool state){
        isPlanted = state;
    }

    private void MovePickIcon(){
        
        float y1 = (float)Math.Round(transform.position.y + 1.23f, 2);
        float y2 = (float)Math.Round(pickIcon.position.y, 2);
        if(y1 == y2){
            pickIcon.DOMoveX(transform.position.x, 0.3f);
        }
        else{
            pickIcon.position = new Vector3(transform.position.x, y1, 0);
        }
    }

}
