using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public HandRing mainHandRing;
    Vector2 ballPos;
    // Start is called before the first frame update
    void Start()
    {
        ballPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        ResetHand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ring"){
            mainHandRing.SetCanMove(false);
            GameManager.gameManager.UpdateScoreUI();
        }
    }

    public void ResetHand(){
        transform.position = new Vector3(-ballPos.x + 1.17f, Random.Range(0f,2.5f), 0);
    }

}
