using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeTarget", 3f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Bullet Direction" || other.tag == "CarRef"){
            ChangeTarget();
        }
    }

    void ChangeTarget(){
        transform.position = new Vector3(Random.Range(-5.5f,5.5f), Random.Range(-3.5f,3.5f), 0);
    }

}
