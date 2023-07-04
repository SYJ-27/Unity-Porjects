using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class State4 : MonoBehaviour
{
    private GameObject mainPlayer;
    public bool isActived;
    void Awake(){
        isActived = false;
        mainPlayer = GameObject.Find("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        InitPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0)){
            isActived = true;
            ActivePower();
        }
        if(!isActived){
            transform.position = mainPlayer.transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if((other.tag == "Enemy" || other.tag == "Bat") && isActived){
            mainPlayer.GetComponent<Player>().score++;
            float x1 = other.gameObject.transform.position.x;
            float y1 = other.gameObject.transform.position.y;
            float x2 = transform.position.x;
            float y2 = transform.position.y;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(x1-x2,y1-y2,0) * 1000);
            Destroy(other.gameObject, 1);
        }
    }

    private void InitPosition(){
        transform.localScale = new Vector3(0, 0, 0);
        transform.parent = mainPlayer.transform;
    }

    private void ActivePower(){
        transform.DOScale(new Vector3(3,3,3), 0.1f).OnComplete(() =>{
            NextState();
        });
        // transform.localScale = new Vector3(3, 3, 3);
        // Invoke("NextState", 0.3f);
    }

    private void NextState(){
        Destroy(gameObject);
        mainPlayer.GetComponent<Player>().GetRandomState();
    }
}
