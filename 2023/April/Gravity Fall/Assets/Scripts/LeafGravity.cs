using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafGravity : MonoBehaviour
{

    public Transform player;
    Rigidbody2D playerBody;
    public float influenceRange;
    public float intensity;
    public float distanceToPlayer;
    private Vector2 pullForce;
    private float scaleNumber;


    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0,0, Random.Range(0, 360));
        playerBody = player.GetComponent<Rigidbody2D>();
        scaleNumber = Random.Range(1, 1.5f);
        transform.localScale = new Vector3(scaleNumber, scaleNumber, 1);
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector2.Distance(player.position, transform.position);
        if(/*distanceToPlayer <= influenceRange &&*/ distanceToPlayer > 0.1f){
            pullForce = (transform.position - player.position).normalized / distanceToPlayer * intensity;
            playerBody.AddForce(pullForce, ForceMode2D.Force);
        }
    }
}
