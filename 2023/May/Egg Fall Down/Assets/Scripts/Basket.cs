using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public int direction;
    public float speed;
    
    void Awake(){
        speed = 3;
        direction = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Moving();
    }

    void Moving(){
        transform.Translate(transform.right * direction * speed * Time.deltaTime);
    }


    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Wall H"){
            direction = -direction;
        }
    }

}
