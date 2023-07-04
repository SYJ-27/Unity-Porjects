using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Map mainMap;
    public Ball mainBall;
    public int idPlayer, lifeCount;
    // public bool isHitBall;
    public List<Sprite> listSpriteID;
    void Awake(){
        // isHitBall = false;
        lifeCount = 3;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetPlayerID(0);
        mainBall.SetBallID(1 - idPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveUp(){
        Vector3 nextPos = new Vector3(transform.position.x, transform.position.y + 0.8f, 0);
        if(mainMap.CanMove(nextPos)){
            // transform.position = nextPos;
            transform.DOMove(nextPos, 0.1f);
        }
    }

    public void MoveDown(){
        Vector3 nextPos = new Vector3(transform.position.x, transform.position.y - 0.8f, 0);
        if(mainMap.CanMove(nextPos)){
            // transform.position = nextPos;
            transform.DOMove(nextPos, 0.1f);
        }
    }

    public void MoveLeft(){
        Vector3 nextPos = new Vector3(transform.position.x - 0.8f, transform.position.y, 0);
        if(mainMap.CanMove(nextPos)){
            // transform.position = nextPos;
            transform.DOMove(nextPos, 0.1f);
        }
    }

    public void MoveRight(){
        Vector3 nextPos = new Vector3(transform.position.x + 0.8f, transform.position.y, 0);
        if(mainMap.CanMove(nextPos)){
            // transform.position = nextPos;
            transform.DOMove(nextPos, 0.1f);
        }
    }

    public void SetPosition(Vector3 pos){
        transform.position = new Vector3((float)Math.Round(pos.x, 1), (float)Math.Round(pos.y, 1), 0);
    }

    public bool CheckPos(Vector3 pos){
        if(Math.Round(transform.position.x, 1) == Math.Round(pos.x, 1) && Math.Round(transform.position.y, 1) == Math.Round(pos.y,1)){
            return true;
        }
        return false;
    }

    void SetPlayerID(int id){
        idPlayer = id;
        GetComponent<SpriteRenderer>().sprite = listSpriteID[idPlayer];
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Ball"){
            SetPlayerID(1 - idPlayer);
            mainBall.GetBall(idPlayer);
            mainMap.RePositionBall();
            mainBall.SetBallID(1 - idPlayer);
            GameManager.gameManager.ballCount++;
        }
        else if(other.tag == "Obstacle"){
            Vector3 pos = other.gameObject.transform.position;
            int idObs = other.gameObject.GetComponent<Obstacle>().idObstacle;
            if(idObs != 1 && idObs != idPlayer + 2){
                mainMap.ResetPlus(pos, 1, idPlayer + 2);
                lifeCount--;    
                if(lifeCount <= 0){
                    lifeCount = 0;
                }
            }
            other.gameObject.GetComponent<Obstacle>().SetObstacleID(idPlayer + 2);
        }
    }

}
