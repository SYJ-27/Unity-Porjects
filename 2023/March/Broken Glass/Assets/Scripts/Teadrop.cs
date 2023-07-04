using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teadrop : MonoBehaviour
{
    public Transform mainPlayer;
    public float speed;
    public Vector3 pos;
    void Awake(){
        speed = 6;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        Invoke("EnableCollider", 0.2f);
        mainPlayer = GameObject.Find("Bird Player").transform;
        Destroy(gameObject, 5);
    }
    // Start is called before the first frame update
    void Start()
    {
        pos = mainPlayer.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == pos || GameManager.gameManager.isLose){
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        var angle = Mathf.Atan2(pos.y - transform.position.y, pos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void InitSpeed(float sp){
        speed = sp;
    }

    void EnableCollider(){
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Teadrop" || other.gameObject.tag == "Base" || other.gameObject.tag == "Wall" || other.gameObject.tag == "Player" || other.gameObject.tag == "Cup" || other.gameObject.tag == "Plate" || other.gameObject.tag == "Teapot"){
            Destroy(gameObject);
        }
    }
}
