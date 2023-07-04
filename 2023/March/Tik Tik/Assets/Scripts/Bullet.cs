using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int direction, damage;
    public bool isStart = true;
    void Awake(){
        GameManager.gameManager.listExtraGameObject.Add(gameObject);
        damage = GameManager.gameManager.bulletDamage;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        Invoke("DisableTrigger", 0.05f);
        isStart = true;
        Invoke("EnableToPlayer", 1f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        damage = GameManager.gameManager.bulletDamage;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = Vector3.zero;
        if(!GameManager.gameManager.isPause){
            transform.Rotate(0,0,-90f * direction * Time.deltaTime);
        }
    }
    public void InitBullet(int direct){
        direction = direct;
        transform.localScale = new Vector3(direct, 1, 1);
    }

    void DisableTrigger(){
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }

    // void OnCollisionEnter2D(Collision2D other){
    //     if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player"){
    //         GameManager.gameManager.UpdateScore();
    //         Destroy(gameObject);
    //     }
    //     if(other.gameObject.tag == "Goal" || other.gameObject.tag == "Bullet"){
    //         gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    //         Invoke("DisableTrigger", 0.1f);
    //     }
    //     if(other.gameObject.tag == "Enemy Bullet"){
    //         if(GameManager.gameManager.canDestroyBullet2){
    //             GameManager.gameManager.UpdateScore();
    //             Destroy(gameObject);
    //         }
    //         else{
    //             gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    //             Invoke("DisableTrigger", 0.1f);
    //         }
    //     }
    //     // if(other.gameObject.tag == "Enemy Bullet"){
    //     //     gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    //     //     Invoke("DisableTrigger", 0.1f);
    //     // }
    // }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Enemy" || (other.tag == "Player Ref" && !isStart)){
            GameManager.gameManager.UpdateScore();
            Destroy(gameObject);
        }
        if(other.tag == "Enemy Bullet"){
            if(GameManager.gameManager.canDestroyBullet2){
                GameManager.gameManager.UpdateScore();
                Destroy(gameObject);
            }
        }
    }

    void EnableToPlayer(){
        isStart = false;
    }
}
