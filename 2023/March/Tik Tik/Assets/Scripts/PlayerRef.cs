using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRef : MonoBehaviour
{
    public Player mainPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Enemy Bullet" || (other.gameObject.tag == "Bullet" && !other.gameObject.GetComponent<Bullet>().isStart)){
            mainPlayer.ResetPlayer();
        }
    }
}
