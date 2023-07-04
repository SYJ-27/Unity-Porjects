using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : MonoBehaviour
{
    public float speedBread;
    void Awake(){
        speedBread = Random.Range(3f, 4f);
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 7);
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    void Moving(){
        transform.Translate(-transform.up * speedBread * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            GameManager.gameManager.breadScore++;
            Destroy(gameObject);
        }
    }
}
