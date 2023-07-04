using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public static Player mainPlayer;
    public int speed = 7;
    public Transform leftBound, rightBound, topBound, bottomBound;

    void Awake(){
        mainPlayer = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Clamp(transform.position.x, leftBound.position.x + 1, rightBound.position.x - 1);
        float y = Mathf.Clamp(transform.position.y, bottomBound.position.y + 1, topBound.position.y - 1);
        transform.position = new Vector3(x, y, 0);
    }

    public void MoveUp(){
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public void MoveDown(){
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    public void MoveLeft(){
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void MoveRight(){
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    
}
