using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int score;
    void Awake(){
        score = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        RandomFoodPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            score++;
            GameManager.gameManager.SpawningEnemy(score);
            // if(score >= 3){
            //     GameManager.gameManager.SpawnBomb();
            // }
            RandomFoodPosition();
        }
    }

    public void RandomFoodPosition(){
        transform.position = GameManager.gameManager.GetRandomPosition();
    }
}
