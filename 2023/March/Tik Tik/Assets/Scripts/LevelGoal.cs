using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    public Player mainPlayer;
    public Sprite goalSprite, ungoalSprite;
    public Transform clockTrans;
    bool isGoal;

    // Start is called before the first frame update
    void Start()
    {
        isGoal = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if(transform.position.x > -0.1f && transform.position.x < 0.1f && !mainPlayer.isJumping && transform.position.y < 0 && isGoal){
        //     mainPlayer.NewLevel();
        //     ResetGoal();
            
        // }
        if(GameManager.gameManager.CheckIsAllEnemyNull()){
            gameObject.GetComponent<SpriteRenderer>().sprite = goalSprite;
            isGoal = true;
            // gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        }
        else{
            gameObject.GetComponent<SpriteRenderer>().sprite = ungoalSprite;
            isGoal = false;
            // gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }

    // void OnCollisionEnter2D(Collision2D other){
    //     if(other.gameObject.tag == "Player"){
    //         ResetGoal();
    //     }
    // }

    public void ResetGoal(){
        isGoal = false;
        clockTrans.localRotation = new Quaternion(0,0,0,0);
        GameManager.gameManager.BonusScene();
        gameObject.GetComponent<SpriteRenderer>().sprite = ungoalSprite;
        gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player Ref" && isGoal){
            mainPlayer.NewLevel();
            ResetGoal();
        }
    }
}
