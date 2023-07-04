using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool isJumping, canMove;
    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        isJumping = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground"){
            isJumping = false;
            GetComponent<Rigidbody2D>().mass = 0.5f;
            if(GameManager.gameManager.isStart){
                canMove = true;
            }
        }
    }

    void CheckStarting(){
        if(GameManager.gameManager.isStart){
            GameManager.gameManager.isStart = false;
            GameManager.gameManager.isPlaying = true;
            GameManager.gameManager.SpawnEgg();
            GameManager.gameManager.MoveBasket();
        }
    }

    void MoveLeft(){
        CheckStarting();
        GetComponent<Rigidbody2D>().AddForce(Vector2.left * 3);
    }

    public void ButtonLeft(){
        CancelInvoke("MassBody");
        if(canMove){
            GetComponent<Rigidbody2D>().mass = 0.5f;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            InvokeRepeating("MoveLeft", 0, 0.01f);
        }
    }

    public void ButtonLeftRelease(){
        if(isJumping){
            GetComponent<Rigidbody2D>().mass = 2f;
        }
        CancelInvoke("MoveLeft");
    }

    void MoveRight(){
        CheckStarting();
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * 3);
    }

    public void ButtonRight(){
        CancelInvoke("MassBody");
        if(canMove){
            GetComponent<Rigidbody2D>().mass = 0.5f;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            InvokeRepeating("MoveRight", 0, 0.01f);
        }
    }

    public void ButtonRightRelease(){
        if(isJumping){
            GetComponent<Rigidbody2D>().mass = 2f;
        }
        CancelInvoke("MoveRight");
    }
    
    public void Jumping(){
        if(!isJumping){
            isJumping = true;
            CheckStarting();
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 137);
            Invoke("MassBody", 0.2f);
        }
    }

    void MassBody(){
        GetComponent<Rigidbody2D>().mass = 2;
    }


}
