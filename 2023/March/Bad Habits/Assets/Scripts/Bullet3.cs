using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet3 : MonoBehaviour
{
    public Transform mainPlayer;
    public float speed;
    public Vector3 pos;
    void Awake(){
        speed = 7;
        mainPlayer = GameObject.Find("Player").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        pos = mainPlayer.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == pos){
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        var angle = Mathf.Atan2(pos.y - transform.position.y, pos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
    }
}
