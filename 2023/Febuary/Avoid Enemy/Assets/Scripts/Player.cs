using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player mainPlayer;
    public Joystick myJoystick;
    public int force;
    public Rigidbody2D playerRigidbody2D;
    public GameObject explosionPlayer;

    void Awake(){
        mainPlayer = this;
        force = 7;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        Vector2 direction = Vector2.up * myJoystick.Vertical + Vector2.right * myJoystick.Horizontal;
        transform.Translate(direction * force * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy"){
            var explose = Instantiate(explosionPlayer, transform.position, Quaternion.identity);
            Destroy(explose, 0.3f);
            StopPlayer();
            GameManager.gameManager.GameLose();
        }
    }

    public void StopPlayer(){
        myJoystick.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void UnStopPlayer(){
        myJoystick.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }


}
