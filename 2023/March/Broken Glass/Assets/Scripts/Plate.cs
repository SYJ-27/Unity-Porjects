using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public GameObject explosion;

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            Player playerTemp = other.gameObject.GetComponent<Player>();
            if(playerTemp.isHitting){
                Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 0.3f);
            }
        }
        if(other.gameObject.tag == "Plate" && other.gameObject.tag == "Cup"){
            Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 0.3f);
            Destroy(gameObject);
        }
    }
}
