using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shooter : MonoBehaviour
{
    public GameObject explosion;
    public Transform mainPlayer;
    public float speed;
    public GameObject safeMode, laser;
    public bool isShooted;
    // Start is called before the first frame update
    void Start()
    {
        if(laser != null){
            laser.transform.localScale = new Vector3(0.1f, 0.1f, 0);
        }
        isShooted = false;
        speed = 1;
        mainPlayer = GameObject.Find("Earth Player").transform;

    }

    void FixedUpdate(){
        if(!GameManager.gameManager.isAttackTime){
            GetComponent<SpriteRenderer>().color = Color.white;
            Moving();
        }
        else{
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        transform.Rotate(0,0, 50 * Time.fixedDeltaTime);

    }

    // Update is called once per frame
    void Update()
    {
        if(safeMode == null && !isShooted && !GameManager.gameManager.isAttackTime){
            isShooted = true;
            if(laser != null){
                laser.SetActive(true);
            }
            GetComponent<SpriteRenderer>().color = Color.white;
            Shooting();
        }
        if(GameManager.gameManager.isAttackTime){
            isShooted = false;
            if(laser != null){
                laser.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(!GameManager.gameManager.isAttackTime)
                other.gameObject.GetComponent<Player>().health -= 5;
            other.gameObject.GetComponent<Player>().score++;
            // Destroy(gameObject);
            ExploreEnemy();
        }
        if(other.tag == "Player Shield" || other.tag == "Bot"){
            ExploreEnemy();
        }
    }

    void Moving(){
        transform.position = Vector2.MoveTowards(transform.position, mainPlayer.position, speed * Time.deltaTime);
    }


    void Shooting(){
        if(laser != null){
            laser.transform.DOScaleX(35, 1.3f);
        }
    }

    public void ExploreEnemy(){
        Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 0.3f);
        Destroy(gameObject);
    }
}
