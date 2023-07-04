using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour
{
    public Transform earthTransform;
    float speed;
    void Awake(){
        speed = Random.Range(1f, 1.5f);
        earthTransform = GameObject.Find("Earth").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x != 0 || transform.position.y != 0){
            var angleTemp = Mathf.Atan2(earthTransform.position.y - transform.position.y, earthTransform.position.x - transform.position.x) * Mathf.Rad2Deg + 127;
            transform.rotation = Quaternion.AngleAxis(angleTemp, Vector3.forward);
        }
    
        transform.position = Vector3.MoveTowards(transform.position, earthTransform.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            GameManager.gameManager.SetScoreEnemy(1);
            Destroy(gameObject);
        }
    }
}
