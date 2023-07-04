using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSaw : MonoBehaviour
{
    public GameObject saw;
    public float speed;
    public int direction;
    public bool isPlayerStanding;

    void Awake(){
        direction = 1;
        speed = 3;
    }
    void Update(){
        SawRotating();
        MoveLine();
    }

    void SawRotating(){
        saw.transform.Rotate(0, 0, -3);
    }

    void MoveLine(){
        if(transform.position.y <= -3f){
            direction = -1;
        }
        if(transform.position.y >= 3f){
            direction = 1;
        }
        transform.Translate(Vector2.down * direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player" && !isPlayerStanding){
            isPlayerStanding = true;
            if(other.gameObject.transform.position.y < gameObject.transform.position.y){
                direction = -1;
            }
        }
        if(other.gameObject.tag == "Teapot" || other.gameObject.tag == "Plate" || other.gameObject.tag == "Cup"){
            if(other.gameObject.transform.position.y < gameObject.transform.position.y){
                direction = -1;
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" && isPlayerStanding){
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            isPlayerStanding = false;
        }
    }

}
