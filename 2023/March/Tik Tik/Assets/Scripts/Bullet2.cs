using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    public int direction;
    public GameObject explosion, explosionPos;

    void Awake(){
        GameManager.gameManager.listExtraGameObject.Add(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        transform.position = Vector3.zero;
        if(!GameManager.gameManager.isPause){
            transform.Rotate(0,0,-65f * direction * Time.deltaTime);
        }
    }
    public void InitBullet(int direct){
        direction = direct;
        transform.localScale = new Vector3(direct, 1, 1);
    }
    void DisableTrigger(){
        gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
    }

    // void OnCollisionEnter2D(Collision2D other){
    //     if(other.gameObject.tag == "Player"){
    //         Destroy(gameObject);
    //     }
    //     if(other.gameObject.tag == "Goal" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Enemy Bullet"){
    //         gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
    //         Invoke("DisableTrigger", 0.1f);
    //     }
    //     if(other.gameObject.tag == "Bullet"){
    //         if(GameManager.gameManager.canDestroyBullet2){
    //             ExplosionBullet2();
    //         }
    //         else{
    //             gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
    //             Invoke("DisableTrigger", 0.1f);
    //         }
    //     }
    // }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player Ref"){
            Destroy(gameObject);
        }
        if(other.tag == "Bullet"){
            if(GameManager.gameManager.canDestroyBullet2){
                ExplosionBullet2();
            }
        }
    }

    void ExplosionBullet2(){
        Destroy(Instantiate(explosion, explosionPos.transform.position, Quaternion.identity), 0.3f);
        Destroy(gameObject);
    }
}
