using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public GameObject playerGO, explosion;
    public float direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = 0;
        playerGO = GameObject.Find("Bird Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Move(){
        if(transform.position.x < playerGO.transform.position.x){
            direction = 1;
        }
        else{
            direction = -1;
        }
        transform.Translate(Vector2.right * direction * 47 * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Base"){
            if(direction == 0){
                direction = 1;
                InvokeRepeating("Move", 1, 1);
            }
        }
        if(other.gameObject.tag == "Player"){
            Player playerTemp = other.gameObject.GetComponent<Player>();
            if(playerTemp.isHitting){
                Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 0.3f);
            }
        }
        if(other.gameObject.tag == "Cup" && other.gameObject.tag == "Plate"){
            Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 0.3f);
            Destroy(gameObject);
        }
    }

    
}
