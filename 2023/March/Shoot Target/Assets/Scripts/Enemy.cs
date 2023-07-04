using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform mainPlayer;
    public float speed;
    public int lifeEnemy;
    public GameObject explosion;
    void Awake(){
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Invoke("EnableCollider",0.5f);
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        lifeEnemy = Random.Range(4, 6);
        mainPlayer = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.gameManager.isPause && mainPlayer != null){
            transform.position = Vector2.MoveTowards(transform.position, mainPlayer.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(!GameManager.gameManager.isPause){
            if(other.tag == "Bullet"){
                lifeEnemy--;
                Destroy(other.gameObject);
                if(lifeEnemy == 0){
                    GameManager.gameManager.UpdateScore();
                    DestroyEnemy();
                }
            }
            if(other.tag == "Laze"){
                GameManager.gameManager.UpdateScore();
                DestroyEnemy();
            }
            if(other.tag == "Shield"){
                GameManager.gameManager.UpdateScore();
                DestroyEnemy();
                Destroy(other.gameObject);
            }
            else if(other.tag == "Player"){
                other.gameObject.GetComponent<Player>().MinusLife();
                GameManager.gameManager.UpdateScore();
                DestroyEnemy();
            }
        }
    }

    public void InitSpeed(){
        speed = Random.Range(3, 5);
    }

    void EnableCollider(){
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    void DestroyEnemy(){
        Destroy(Instantiate(explosion, transform.position, Quaternion.identity),0.3f);
        Destroy(gameObject);
    }
}
