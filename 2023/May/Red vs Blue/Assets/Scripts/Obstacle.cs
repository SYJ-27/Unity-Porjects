using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public List<Color> listColorStatus;
    public int idObstacle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitObstacle(int id){
        idObstacle = id;
        GetComponent<SpriteRenderer>().color = listColorStatus[id];
    }

    public bool CheckPos(Vector3 pos){
        if(Math.Round(transform.position.x, 1) == Math.Round(pos.x, 1) && Math.Round(transform.position.y, 1) == Math.Round(pos.y,1)){
            return true;
        }
        return false;
    }

    public bool CheckPosX(Vector3 pos){
        if(Math.Round(transform.position.x, 1) == Math.Round(pos.x, 1)){
            return true;
        }
        return false;
    }

    public bool CheckPosY(Vector3 pos){
        if(Math.Round(transform.position.y, 1) == Math.Round(pos.y,1)){
            return true;
        }
        return false;
    }

    public void SetObstacleID(int id){
        idObstacle = id;
        GetComponent<SpriteRenderer>().color = listColorStatus[id];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if(other.tag == "Player"){
        //     int tempPlayerID = other.gameObject.GetComponent<Player>().idPlayer;
        //     // GetComponent<SpriteRenderer>().color = listColorStatus[tempPlayerID + 2];
        // }
    }

}
