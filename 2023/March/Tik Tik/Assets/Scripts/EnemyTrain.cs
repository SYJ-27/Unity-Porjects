using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrain : MonoBehaviour
{
    public int direction, lifeCount;
    public GameObject explosion, explosionPos;

    void Awake(){
        lifeCount = 3;
    }
    // Start is called before the first frame update
    void Start()
    {

        direction = Random.Range(0,2);
        if(direction == 0){
            direction = -1;
        }
        transform.localScale = new Vector3(direction * 1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,-55f * direction * Time.deltaTime);
    }

    // void OnCollisionEnter2D(Collision2D other){
    //     if(other.gameObject.tag == "Bullet" || other.gameObject.tag == "Player"){
    //         if(other.gameObject.tag == "Bullet"){
    //             lifeCount -= other.gameObject.GetComponent<Bullet>().damage;
    //         }
    //         else{
    //             lifeCount --;
    //         }
    //         if(lifeCount <= 0){
    //             GameManager.gameManager.UpdateClearScore();
    //             Destroy(Instantiate(explosion, explosionPos.transform.position, Quaternion.identity), 0.3f);
    //             Destroy(gameObject);
    //         }
    //     }
    // }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Bullet" || other.tag == "Player Ref"){
            if(other.tag == "Bullet"){
                lifeCount -= other.GetComponent<Bullet>().damage;
            }
            else{
                lifeCount --;
            }
            if(lifeCount <= 0){
                GameManager.gameManager.UpdateClearScore();
                Destroy(Instantiate(explosion, explosionPos.transform.position, Quaternion.identity), 0.3f);
                Destroy(gameObject);
            }
        }
    }
}
