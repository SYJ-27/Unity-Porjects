using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speedBullet;
    void Awake(){
        speedBullet = 5;
        Destroy(gameObject, 7);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        if(GameManager.gameManager.isLose){
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Moving();
    }

    void Moving(){
        transform.Translate(transform.up * speedBullet * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Wall Top"){
            Destroy(gameObject);
        }
    }

}
