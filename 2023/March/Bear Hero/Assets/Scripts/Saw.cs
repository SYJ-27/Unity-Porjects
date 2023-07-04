using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public Rigidbody2D sawRigidbody2D;
    public int lifeSaw;
    void Awake(){
        lifeSaw = 2;
    }
    // Start is called before the first frame update
    void Start()
    {
        sawRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        int randomNegative = UnityEngine.Random.Range(0,2);
        if(randomNegative == 0){
            randomNegative = 1;
        }
        else{
            randomNegative = -1;
        }
        sawRigidbody2D.AddForce(new Vector3(UnityEngine.Random.Range(300,310) * randomNegative,UnityEngine.Random.Range(300,310) * randomNegative, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeSaw > 0){
            float x = Mathf.Clamp(sawRigidbody2D.velocity.x, -7, 7);
            float y = Mathf.Clamp(sawRigidbody2D.velocity.y, -7, 7);
            sawRigidbody2D.velocity = new Vector2(x,y);
        }
        else{
            GameManager.gameManager.currentScore += 1;
            GameManager.gameManager.UpdateScoreUI();

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Wall Right" || other.tag == "Wall Left"){
            sawRigidbody2D.velocity = new Vector2(-sawRigidbody2D.velocity.x * UnityEngine.Random.Range(1,3), sawRigidbody2D.velocity.y * UnityEngine.Random.Range(1,3));
        }
        if(other.tag == "Wall Top" || other.tag == "Wall Bottom"){
            sawRigidbody2D.velocity = new Vector2(sawRigidbody2D.velocity.x * UnityEngine.Random.Range(1,3), -sawRigidbody2D.velocity.y * UnityEngine.Random.Range(1,3));
        }
        if(other.tag == "Base"){
            lifeSaw--;
            Base baseHit = other.gameObject.GetComponent<Base>();
            if(baseHit.isRightSide){
                sawRigidbody2D.velocity = new Vector2(-Mathf.Abs(sawRigidbody2D.velocity.x * UnityEngine.Random.Range(1,3)), Mathf.Abs(sawRigidbody2D.velocity.y * UnityEngine.Random.Range(1,3)));
            }
            else{
                sawRigidbody2D.velocity = new Vector2(Mathf.Abs(sawRigidbody2D.velocity.x * UnityEngine.Random.Range(1,3)), Mathf.Abs(sawRigidbody2D.velocity.y * UnityEngine.Random.Range(1,3)));
            }
            baseHit.DisableCollider();
        }
        if(other.tag == "Player"){
            GameManager.gameManager.GameLose();
        }
        if(other.tag == "Wall End"){
            GameManager.gameManager.currentScore += 1;
            GameManager.gameManager.UpdateScoreUI();
            Destroy(gameObject);
        }
    }
}
