using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Transform centerArea;
    private int direction;
    private float posX, posY, angle;
    public float angularSpeed, rotationRadius;
    void Awake(){
        centerArea = GameObject.Find("Circle Area").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        direction = Random.Range(0, 2);
        if(direction == 0){
            direction = -1;
        }
        rotationRadius = Random.Range(3f, 3.5f);
        angle = Random.Range(0, 2 * Mathf.PI);
        if(GameManager.gameManager.score < 10){
            angularSpeed = Random.Range(1f, 1.6f)  * direction;
        }
        else{
            angularSpeed = Random.Range(1f, 4f)  * direction;
        }
        if(GameManager.gameManager.score < 13){
            Destroy(gameObject, Random.Range(2f, 3f));
        }
        else{
            Destroy(gameObject, Random.Range(2.5f, 4.5f));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        posX = centerArea.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = centerArea.position.y + Mathf.Sin(angle) * rotationRadius;
        if(!GameManager.gameManager.isPause){
            transform.position = new Vector2(posX, posY);
            angle = angle - Time.deltaTime * angularSpeed;
        }
    }

}
