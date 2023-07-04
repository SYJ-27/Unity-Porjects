using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Player mainPlayer;
    // Start is called before the first frame update
    void Start()
    {
        mainPlayer = GameObject.Find("Earth Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(!GameManager.gameManager.isAttackTime){
            if(other.tag == "Enemy" || other.tag == "Bomb" || other.tag == "Laser"){
                if(other.tag == "Laser"){
                    Destroy(other.gameObject);
                }
                else{
                    mainPlayer.score++;
                }
                Destroy(gameObject);
            }
        }
    }

}
