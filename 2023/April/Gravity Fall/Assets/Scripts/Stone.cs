using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public GameObject stoneExplore;
    Vector3 playerPos;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = GameManager.gameManager.speedStone;
        Destroy(gameObject, 5);
        playerPos = GameObject.Find("Frog Player").transform.position;
        Rotating();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        // Rotating();
        Moving();
    }
    
    void Rotating(){
        // Debug.Log(playerPos);
        var angleTemp = Mathf.Atan2(playerPos.y - transform.position.y, playerPos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,angleTemp);
        // transform.Rotate(0,0,angleTemp);
        // transform.Rotate(0,0, 50);
    }

    void Moving(){
        transform.position += transform.right * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            // other.gameObject.GetComponent<Player>().life--;
            // if(other.gameObject.GetComponent<Player>().life < 0){
            //     GameManager.gameManager.GameLosing();
            // }
            // Destroy(gameObject);
            Destroy(Instantiate(stoneExplore, transform.position, Quaternion.identity), 0.3f);
        }
    }

}
