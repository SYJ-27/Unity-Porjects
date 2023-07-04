using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletExplore;
    public int dameBullet;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        if(!GameManager.gameManager.isStart && !GameManager.gameManager.isPause){
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        Destroy(gameObject, 10);
    }

    public void Rotate(float zRotate){
        transform.Rotate(0,0,zRotate);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GameManager.gameManager.isStart && !GameManager.gameManager.isPause){
            Moving();
        }
    }

    void Moving(){
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Planet"){
            Destroy(Instantiate(bulletExplore, transform.position, Quaternion.identity), 0.3f);
        }
    }

}
