using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public Transform mainPlayer;
    public Vector3 posPlayer;
    public BulletE2 bulletEnemy;
    public List<BulletE2> listBulletEnemy;

    public int lifeEnemy;
    public GameObject explosion;


    void Awake(){
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Invoke("EnableCollider",1f);
        lifeEnemy = Random.Range(3,5);
        // timeDestroy = 1.3f;
        // speed = 20;
        mainPlayer = GameObject.Find("Player").transform;
        // Invoke("DestroyItSelf", timeDestroy);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shooting(){
        if(!GameManager.gameManager.isLose && !GameManager.gameManager.isPause){
            posPlayer = mainPlayer.position;
            var angle = Mathf.Atan2(posPlayer.y - transform.position.y, posPlayer.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            var bulletESpawned = Instantiate(bulletEnemy, transform.position, Quaternion.identity);
            bulletESpawned.Initbullet(0);
            listBulletEnemy.Add(bulletESpawned);
        }
    }

    void EnableCollider(){
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        InvokeRepeating("Shooting", 1, 2);

    }
    void OnTriggerEnter2D(Collider2D other){
        if(!GameManager.gameManager.isPause){
            if(other.tag == "Bullet"){
                lifeEnemy--;
                Destroy(other.gameObject);
                if(lifeEnemy == 0){
                    GameManager.gameManager.UpdateScore();
                    DestroyEnemy2();
                }
            }
            if(other.tag == "Laze"){
                GameManager.gameManager.UpdateScore();
                DestroyEnemy2();
            }
            if(other.tag == "Shield"){
                GameManager.gameManager.UpdateScore();
                DestroyEnemy2();
                Destroy(other.gameObject);
            }
            else if(other.tag == "Player"){
                other.gameObject.GetComponent<Player>().MinusLife();
                GameManager.gameManager.UpdateScore();
                DestroyEnemy2();
            }
        }
    }

    void DestroyEnemy2(){
        Destroy(Instantiate(explosion, transform.position, Quaternion.identity),0.3f);
        Destroy(gameObject);
    }

}
