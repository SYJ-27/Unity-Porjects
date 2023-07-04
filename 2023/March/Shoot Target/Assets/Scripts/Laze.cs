using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laze : MonoBehaviour
{
    public Transform mainTarget;
    public float speed, timeDestroy, bulletRotate;
    public Vector3 posTarget;
    public bool isDestroying;
    void Awake(){
        timeDestroy = 1f;
        speed = 20;
        mainTarget = GameObject.Find("Target").transform;
        isDestroying = true;
        Invoke("DestroyItSelf", timeDestroy);
    }
    // Start is called before the first frame update
    void Start()
    {
        posTarget = mainTarget.position;
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
            var angle = Mathf.Atan2(posTarget.y - transform.position.y, posTarget.x - transform.position.x) * Mathf.Rad2Deg + bulletRotate;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            // transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else{
            isDestroying = false;
            CancelInvoke("DestroyItSelf");
        }
    }

    void DestroyItSelf(){
        Destroy(gameObject);
    }
}
