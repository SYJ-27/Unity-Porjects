using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject bombExplore;
    public int dame;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        Moving();
        Invoke("ActiveGravity", 1f);
        Destroy(gameObject, 10);
        // Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.gameManager.isStart && !GameManager.gameManager.isPause){
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        else{
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    void Moving()
    {
        transform.GetComponent<Rigidbody2D>().AddForce((transform.up + transform.right) * force);
        // transform.GetComponent<Rigidbody2D>().velocity = (transform.up + transform.right) * force;
    }

    void ActiveGravity()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Planet"){
            Destroy(Instantiate(bombExplore, transform.position, Quaternion.identity), 0.3f);
            // Destroy(gameObject);
        }
    }

}
