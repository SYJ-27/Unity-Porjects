using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockEnemy : MonoBehaviour
{
    public GameObject explosion;
    public float direction, speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
        Destroy(gameObject, 22);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    public void InitPosition(Vector3 worldPoint){
        int randomNumber = Random.Range(0, 2);
        if(randomNumber == 0){
            direction = -1;
        }
        else{
            direction = 1;
        }
        transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(-direction * worldPoint.x - direction * 1f, Random.Range(-worldPoint.y + 1.5f, worldPoint.y - 1.5f), 0);
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Bullet"){
            ExplosionEnemy();
            Destroy(other.gameObject);
        }
    }
    public void ExplosionEnemy(){
        Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 0.3f);
        Destroy(gameObject);
    }
}
