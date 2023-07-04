using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    public float speedBullet, direction;
    public Enemy carEnemy;
    public Player carPlayer;
    // Start is called before the first frame update
    void Start()
    {
        carEnemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        carPlayer = GameObject.Find("Car").GetComponent<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Vector2.left * speedBullet * Time.deltaTime);
    }

    public void InitBullet(float zAxis){
        transform.localScale = new Vector3(direction * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        transform.eulerAngles = new Vector3(0, 0, zAxis);
    }

    void TurnOffTrigger(){
        GetComponent<BoxCollider2D>().isTrigger = false;
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Wall" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player" ){
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "Stone" ){
            if(direction == -1)
                carEnemy.breakNumber++;
            else if(direction == 1)
                carPlayer.breakNumber++;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }



}
