using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public bool isBomb;
    public GameObject explosionBomb;

    void Awake(){
        player = GameObject.Find("Player");
        speed = Random.Range(2f,4f);
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("TurnOffTrigger",1f);
    }

    public void InitBomb(bool bomb){
        isBomb = bomb;
        if(isBomb){
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
        }
    }

    void TurnOffTrigger(){
        gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isBomb && player != null){
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else{
            float randomEx = Random.Range(3f, 6f);
            InvokeRepeating("AutoExplose", randomEx, randomEx);
        }
    }

    void AutoExplose(){
        CancelInvoke("AutoExplose");
        var explose = Instantiate(explosionBomb, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(explose, 1);
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Enemy"){
            GameManager.gameManager.UpdateScoreUI();
            var explose = Instantiate(explosionBomb, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(explose, 0.3f);
        }
        else if(other.gameObject.tag == "Player"){
            // var explose = Instantiate(explosionBomb, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            // Destroy(explose, 1);
        }
    }

}
