using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBot : MonoBehaviour
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
        transform.Rotate(0,0, 100 * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Enemy"){
            Destroy(other.gameObject);
            if(!GameManager.gameManager.isAttackTime){
                mainPlayer.score++;
            }
        }
    }
}
