using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    public float speedCarrot = 3;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    void Update(){
        if(GameManager.gameManager.isLose){
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Moving();
        Rotating();
    }

    void Moving(){
        transform.position -= new Vector3(0, speedCarrot * Time.deltaTime, 0);
    }

    void Rotating(){
        transform.Rotate(0,0,200 * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player" || other.tag == "Wall Bottom"){
            Destroy(gameObject);
        }
    }

    

}
