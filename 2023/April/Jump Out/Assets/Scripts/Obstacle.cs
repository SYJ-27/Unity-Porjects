using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int mode;
    Vector2 direction;
    float rotateZ, scaleX, scaleY, speed;
    void Awake(){
        speed = Random.Range(3f, 5f);
        Destroy(gameObject, 10);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void InitObstacle(float x, float y, float z, int directionMode){
        if(directionMode == 0){
            direction = Vector2.up;
        }
        else if(directionMode == 1){
            direction = Vector2.down;
        }
        else if(directionMode == 2){
            direction = Vector2.down;
        }
        else{
            direction = Vector2.up;
        }
        mode = directionMode;
        rotateZ = z;
        scaleX = x;
        scaleY = y;
        transform.localScale = new Vector3(x, y, 1);
        transform.Rotate(0,0,z);
    }

    void Move(){
        transform.Translate(direction * speed * Time.deltaTime);
    }

}
