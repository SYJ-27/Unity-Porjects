using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sun : MonoBehaviour
{
    public GameObject mainPlayer;
    Vector3 worldPoint;
    public float absrobForce;
    bool isMoving;
    

    void Awake(){
        isMoving = false;
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-worldPoint.x, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GameManager.gameManager.isStart && !GameManager.gameManager.isPause){
            PlayerAbsorbing();
        }
        if(GameManager.gameManager.score > 13 && !isMoving){
            isMoving = true;
            Moving();
        }
    }

    void Moving(){
        float x = Random.Range(-worldPoint.x - 0.5f,-worldPoint.x + 0.5f);
        float y = Random.Range(- 0.5f,0.5f);
        float time = Mathf.Sqrt((x + worldPoint.x) * (x + worldPoint.x) + (y) * (y));
        transform.DOMove(new Vector3(x,y,0), time).OnComplete(() =>{
            transform.DOMove(new Vector3(-worldPoint.x, 0, 0), time).OnComplete(() =>{
                Moving();
            });
        });
        // else{
        //     Moving();
        // }
    }

    void PlayerAbsorbing(){
        float playerDistance = Vector2.Distance(mainPlayer.transform.position, transform.position);
        if(playerDistance > 0.3f){
            // mainPlayer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Vector2 pullForce = (transform.position - mainPlayer.transform.position).normalized / playerDistance * absrobForce;
            // Debug.Log(pullForce);
            mainPlayer.GetComponent<Rigidbody2D>().AddForce(pullForce);
        }
        else{
            mainPlayer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }


}
