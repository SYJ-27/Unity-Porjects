using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool canFly;
    public GameObject explosion;
    public int protectedNumber;
    void Start()
    {
        protectedNumber = 0;
        DisablePhysics();
    }

    void Update(){
        
    }

    public void EnablePhysics(){
        gameObject.GetComponent<TrailRenderer>().enabled = true;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    public void DisablePhysics(){
        gameObject.GetComponent<TrailRenderer>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    public void SetPositionLevel(Road roadLevel){
        transform.position = roadLevel.stateBall.position;
    }

    public void Explore(){
        if(protectedNumber <= 0){
            gameObject.SetActive(false);
            GameManager.gameManager.SetLose();
            DisablePhysics();

            
            Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 0.5f);
            Invoke("EndGame", 0.8f);
        }
    }

    public void ReduceProtectNumber(){
        protectedNumber--;
    }

    private void EndGame(){
        GameManager.gameManager.GameLose();
    }


}
