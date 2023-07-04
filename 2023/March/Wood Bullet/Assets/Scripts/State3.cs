using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State3 : MonoBehaviour
{
    private GameObject mainPlayer;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        mainPlayer = GameObject.Find("Player");
        InitPosition();
        force = 700;
    }

    // Update is called once per frame
    void Update()
    {
        Rotating();
        if(Input.GetMouseButtonDown(0)){
            AddingForceToHuman();
        }
    }

    void Rotating(){
        transform.position = mainPlayer.transform.position;
        transform.Rotate(0,0,-0.5f);
    }

    void AddingForceToHuman(){
        mainPlayer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        mainPlayer.GetComponent<Rigidbody2D>().AddForce(transform.right * force);
        Destroy(gameObject);
        mainPlayer.GetComponent<Player>().LimitVeloc();
        mainPlayer.GetComponent<Player>().GetRandomState();
    }

    private void InitPosition(){
        transform.parent = mainPlayer.transform;
        transform.position = mainPlayer.transform.position;
    }
}
