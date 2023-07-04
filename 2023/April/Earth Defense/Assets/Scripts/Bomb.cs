using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bomb : MonoBehaviour
{
    public GameObject explosionObject;
    public Transform mainPlayer;
    public float speed;
    public GameObject safeMode, explosion;
    public bool isExplore;
    // Start is called before the first frame update
    void Start()
    {
        isExplore = false;
        speed = 3;
        mainPlayer = GameObject.Find("Earth Player").transform;
    }

    void FixedUpdate(){
        if(!GameManager.gameManager.isAttackTime && !isExplore){
            GetComponent<SpriteRenderer>().color = Color.white;
            Moving();
        }
        else{
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(safeMode == null && !isExplore && !GameManager.gameManager.isAttackTime){
            isExplore = true;
            GetComponent<SpriteRenderer>().color = Color.white;
            Invoke("ExploreBomb", 0.3f);
        }
        if(GameManager.gameManager.isAttackTime){
            isExplore = false;
            CancelInvoke("ExploreBomb");
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(!GameManager.gameManager.isAttackTime)
                other.gameObject.GetComponent<Player>().health -= 5;
            other.gameObject.GetComponent<Player>().score++;
            // Destroy(gameObject);
            ExploreEnemy();
            CancelInvoke("ExploreBomb");
        }
        if(other.tag == "Player Shield" || other.tag == "Bot"){
            ExploreEnemy();
        }
    }

    void Moving(){
        transform.position = Vector2.MoveTowards(transform.position, mainPlayer.position, speed * Time.deltaTime);
        transform.Rotate(0,0, 50 * Time.fixedDeltaTime);
    }


    void ExploreBomb(){
        var exBomb = Instantiate(explosion, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        exBomb.transform.localScale = Vector3.zero;
        exBomb.transform.DOScale(new Vector3(2f, 2f, 2f), 0.3f).OnComplete(() =>{
            Destroy(exBomb);
            Destroy(gameObject);
        });
    }

    public void ExploreEnemy(){
        Destroy(Instantiate(explosionObject, transform.position, Quaternion.identity), 0.3f);
        Destroy(gameObject);
    }
}
