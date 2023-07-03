using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float[] yPositions = {2.35f, 4.35f, 0.99f, -0.99f, -4.35f, -2.35f};
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        // speed = Random.Range(2f,4f);
        speed = 3f;
        // transform.position = new Vector3(10, yPositions[Random.Range(0, yPositions.Length)]);
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.gameManager.isLose)
            transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Car"){
            InvokeRepeating("GameOverScene", 1,1);
        }
        if(other.tag == "Score Wall"){
            Destroy(gameObject);
            GameManager.gameManager.UpdateScoreUI();
        }
        if(other.tag == "Ending Wall"){
            Destroy(gameObject);
        }
    }

    void GameOverScene(){
        CancelInvoke("GameOverScene");
        GameManager.gameManager.GameLose();
    }

    public void InitObstacle(float min, float max){
        int random = Random.Range(0,2);
        if(random == 0)
            transform.position = new Vector3(10, min);
        else
            transform.position = new Vector3(10, max);
    }
}
