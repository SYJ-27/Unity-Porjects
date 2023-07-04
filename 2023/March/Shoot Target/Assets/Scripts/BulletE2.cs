using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletE2 : MonoBehaviour
{
    public Transform mainPlayer;
    public float speed, timeDestroy, bulletRotate;
    public Vector3 posPlayer;
    public bool isDestroying;
    void Awake(){
        timeDestroy = 1.3f;
        speed = 20;
        mainPlayer = GameObject.Find("Player").transform;
        isDestroying = true;
        Invoke("DestroyItSelf", timeDestroy);
    }
    // Start is called before the first frame update
    void Start()
    {
        posPlayer = mainPlayer.position;
        var angle = Mathf.Atan2(posPlayer.y - transform.position.y, posPlayer.x - transform.position.x) * Mathf.Rad2Deg + bulletRotate;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if(timeDestroy >= 0){
            timeDestroy -= Time.deltaTime;
        }
        else{
            timeDestroy = 0;
        }
    }

    public void Initbullet(int rotate){
        bulletRotate = rotate;
    }

    private void FixedUpdate()
    {
        if(!GameManager.gameManager.isPause){
            if(!isDestroying){
                if(timeDestroy < 0){
                    timeDestroy = 0;
                }
                isDestroying = true;
                Invoke("DestroyItSelf", timeDestroy);
            }
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else{
            isDestroying = false;
            CancelInvoke("DestroyItSelf");
        }
    }

    void DestroyItSelf(){
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(!GameManager.gameManager.isPause){
            if(other.tag == "Player"){
                other.gameObject.GetComponent<Player>().MinusLife();
                GameManager.gameManager.UpdateScore();
                Destroy(gameObject);
            }
        }
    }
}
