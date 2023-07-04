using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Transform playerTrans, bigSaw, posTrajector;
    public GameObject explosion;
    void Awake(){
        
    }
    // Start is called before the first frame update
    void Start()
    {
        float scaleNum = Random.Range(1.5f, 4.6f);
        posTrajector.localScale = new Vector3(scaleNum, scaleNum, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            MoveUp();
        }
        if(Input.GetKey(KeyCode.S)){
            MoveDown();
        }
        LimitScale();
        transform.position = playerTrans.position;
    }

    public void MoveUp(){
        posTrajector.localScale += new Vector3(0.05f, 0.05f, 0);        
    }

    public void MoveDown(){
        posTrajector.localScale -= new Vector3(0.05f, 0.05f, 0);
    }

    public void ScaleDown(){
        InvokeRepeating("MoveDown", 0, 0.01f);
    }

    public void ScaleUp(){
        
        InvokeRepeating("MoveUp", 0, 0.01f);
    }

    public void ScaleStay(){
        
        CancelInvoke("MoveDown");
        CancelInvoke("MoveUp");
    }

    void LimitScale(){
        float x = Mathf.Clamp(posTrajector.localScale.x, 1.5f, 4.5f);
        float y = Mathf.Clamp(posTrajector.localScale.y, 1.5f, 4.5f);
        posTrajector.localScale = new Vector3(x, y, 0);
    }

    void RotateRight(){
        bigSaw.Rotate(0,0,-1);
    }

    void RotateLeft(){
        bigSaw.Rotate(0,0,1);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Small Saw"){
            ExplosionPlayer();
        }
    }

    void ExplosionPlayer(){
        Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 0.5f);
        gameObject.SetActive(false);
        Invoke("Losing", 0.5f);
    }

    void Losing(){
        GameManager.gameManager.GameLosing();
    }
    

}
