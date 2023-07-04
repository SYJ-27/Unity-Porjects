using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    public int directionHorizontal;
    float speedVertical;

    public int lifeCow, idCow;
    void Awake(){
        speedVertical = 1;
        directionHorizontal = 1;
        lifeCow = 2;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    public void InitCow(int id){
        idCow = id;
    }

    void Moving(){
        transform.Translate(transform.right * directionHorizontal * 3 * Time.deltaTime - transform.up * Time.deltaTime * speedVertical);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Wall Right"){
            GameManager.gameManager.ChangeCowLineDirection(idCow, -1);
        }
        if(other.tag == "Wall Left"){
            GameManager.gameManager.ChangeCowLineDirection(idCow, 1);
        }
        if(other.tag == "Wall Bottom"){
            GameManager.gameManager.ClearLineId(idCow);
        }
        if(other.tag == "Bullet"){
            lifeCow--;
            if(lifeCow <= 0){
                GameManager.gameManager.score++;
                Destroy(gameObject);
            }
            Destroy(other.gameObject);
        }
        if(other.tag == "Player"){
            GameManager.gameManager.score++;
            Destroy(gameObject);
        }
    }

}
