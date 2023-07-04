using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSaw : MonoBehaviour
{
    public bool isBouncing;
    public Transform bigSaw;
    public int smallSawForce, rotateDirection;
    public float randomScale;
    public GameObject centerSaw;

    void Awake(){
        isBouncing = false;
        smallSawForce = Random.Range(2,4);
        rotateDirection = Random.Range(0,2);
        randomScale = Random.Range(1f,2f);
        if(rotateDirection == 0){
            rotateDirection = -1;
        }
        transform.localScale = new Vector3(randomScale,randomScale,1);
    }
    // Start is called before the first frame update
    void Start()
    {
        Moving();
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,rotateDirection);
    }

    void Moving(){
        float x = (transform.position.x/Mathf.Abs(transform.position.x));
        float y = (transform.position.y/Mathf.Abs(transform.position.y));
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(-x, -y, 0) * 100 * smallSawForce);
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Circle"){
            gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
            centerSaw = other.gameObject;
            other.gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
            Invoke("DisableTrigger", 0.05f);
        }
    }

    void DisableTrigger(){
        centerSaw.GetComponent<CircleCollider2D>().isTrigger = true;
        gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
    }
}
