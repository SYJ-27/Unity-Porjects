using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float stepMove;

    // Start is called before the first frame update
    void Start()
    {
        stepMove = GameManager.gameManager.foodStep;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Wall" || other.tag == "Mouth"){
            Destroy(gameObject);
        }
    }
}
