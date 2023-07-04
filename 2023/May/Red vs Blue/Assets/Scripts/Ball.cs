using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Player mainPlayer;
    public Obstacle thisObstacle;
    public List<Sprite> listSpriteID;
    public int idBall ;    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // SetBallID(1 - mainPlayer.idPlayer);
    }

    public void SetBallID(int id){
        idBall = id;
        GetComponent<SpriteRenderer>().sprite = listSpriteID[idBall];
    }

    public bool CheckPos(Vector3 pos){
        if(Math.Round(transform.position.x, 1) == Math.Round(pos.x, 1) && Math.Round(transform.position.y, 1) == Math.Round(pos.y,1)){
            return true;
        }
        return false;
    }

    public void SetPos(Vector3 pos){
        transform.position = pos;
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Obstacle"){
            thisObstacle = other.gameObject.GetComponent<Obstacle>();
            thisObstacle.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    public void GetBall(int idPlayer){
        thisObstacle.SetObstacleID(idPlayer + 2);
        thisObstacle.gameObject.GetComponent<CircleCollider2D>().enabled = true;

    }

}
