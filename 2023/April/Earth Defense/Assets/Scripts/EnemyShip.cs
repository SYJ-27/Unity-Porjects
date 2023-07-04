using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    public GameObject explosion;
    public Transform mainPlayer;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2;
        mainPlayer = GameObject.Find("Earth Player").transform;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(- transform.position.x, - transform.position.y) * 50);
        Invoke("DisableVelocity", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        if(!GameManager.gameManager.isAttackTime){
            GetComponent<SpriteRenderer>().color = Color.white;
            Moving();
        }
        else{
            DisableVelocity();
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    void DisableVelocity(){
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    void Moving(){
        transform.position = Vector2.MoveTowards(transform.position, mainPlayer.position, speed * Time.deltaTime);
        if(transform.position.x != mainPlayer.position.x && transform.position.y != mainPlayer.position.y){
            var angle = Mathf.Atan2(mainPlayer.position.y - transform.position.y, mainPlayer.position.x - transform.position.x) * Mathf.Rad2Deg - 145;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            other.gameObject.GetComponent<Player>().score++;
            // Destroy(gameObject);
            ExploreEnemy();
        }
        if(other.tag == "Player Shield" || other.tag == "Bot"){
            ExploreEnemy();
        }
    }

    public void ExploreEnemy(){
        Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 0.3f);
        Destroy(gameObject);
    }

}
