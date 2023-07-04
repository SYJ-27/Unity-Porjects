using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet4 : MonoBehaviour
{
    public Transform mainPlayer;
    public float speed;
    public Vector3 pos;
    void Awake(){
        speed = 3;
        mainPlayer = GameObject.Find("Player").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        pos = mainPlayer.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        var angle = Mathf.Atan2(pos.y - transform.position.y, pos.x - transform.position.x) * Mathf.Rad2Deg -90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
    }
}
