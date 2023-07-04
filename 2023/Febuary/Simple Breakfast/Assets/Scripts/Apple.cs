using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public float[] rangeX = {-3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7};
    public float[] rangeY = {-3.5f, -2.5f, -1.5f, -0.5f, 0.5f, 1.5f, 2.5f};
    private int randomPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(randomPos == 0){ //Top
            transform.Translate(0, -Time.deltaTime * 3, 0);
        }
        else if(randomPos == 1){ //Right
            transform.Translate(0, Time.deltaTime * 3, 0);
        }
        else if(randomPos == 2){ //Bottom
            transform.Translate(0, Time.deltaTime * 3, 0);
        }
        else if(randomPos == 3){ //Left
            transform.Translate(0, Time.deltaTime * 3, 0);
        }
    }

    public void RandomGenerateApple(int x, int y){
        randomPos = Random.Range(0,4);
        int Xpos = Random.Range(5-x, 6+x);
        int Ypos = Random.Range(3-y, 4+y);
        if(randomPos == 0){ //Top
            transform.position = new Vector3(rangeX[Xpos], 10, 0);
        }
        else if(randomPos == 1){ //Right
            
            transform.position = new Vector3(10, rangeY[Ypos], 0);
            transform.Rotate(0,0,90);
        }
        else if(randomPos == 2){ //Bottom
            transform.position = new Vector3(rangeX[Xpos], -10, 0);
        }
        else if(randomPos == 3){ //Left
            
            transform.position = new Vector3(-10, rangeY[Ypos], 0);
            transform.Rotate(0,0,-90);
        }
        Destroy(gameObject, 5);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            GameManager.gameManager.GameLose();
        }
    }
}
