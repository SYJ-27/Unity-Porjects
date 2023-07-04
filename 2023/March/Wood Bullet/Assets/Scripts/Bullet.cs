using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Player mainPlayer;
    public float directionBullet, forceBullet;
    private Rigidbody2D bulletRigidbody;
    void Awake(){
        mainPlayer = GameObject.Find("Player").GetComponent<Player>();
        forceBullet = 1307;
        bulletRigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate(Transform.right * direction * force * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Enemy"){
            mainPlayer.score++;
        }
    }

    public void InitBullet(float direction){
        directionBullet = direction;
        transform.localScale = new Vector3(direction * 3, 3, 0);
        bulletRigidbody.AddForce(transform.right * forceBullet * directionBullet);
    }



}
