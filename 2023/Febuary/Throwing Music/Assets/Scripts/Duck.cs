using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
    
    public GameObject music;
    public float outForce = 0;
    private bool isRotate = false;
    public float isRight = 1;
    public bool isPress = false;
    public float speedDuck;
    public int idcheckSide;


    void Start(){
        speedDuck = Random.Range(1,4);
    }

    void Update(){
        
        transform.Translate(isRight * Vector2.left * speedDuck * Time.deltaTime);
        if(music != null){
            if(isRotate){
                music.transform.Rotate(new Vector3(0,0,5));
                    
            }
        }
    }

    IEnumerator DelayCollider(){
        yield return new WaitForSeconds(1);

        music.transform.GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnMouseDown(){
        isRotate = true;
        if(music != null && !isPress){
            Throwing();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Music"){
            other.transform.GetComponent<Rigidbody2D>().AddForce(new Vector3(-isRight * 0.1f, 1, 0) * 500);
            Destroy(other.gameObject, 10);
            isRight = -isRight;
            if(idcheckSide == 0){
                transform.localScale = new Vector3(1, 1 , 1);
            }
            else{
                transform.localScale = new Vector3(-1, 1 , 1);
            }
            speedDuck = 5;
            GameManager.gameManager.CheckUiController();
            transform.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void Throwing(){
        music.transform.GetComponent<Rigidbody2D>().AddForce(new Vector3(isRight * 0.1f, 1, 0) * outForce);
        music.transform.GetComponent<Rigidbody2D>().gravityScale = 1;
        StartCoroutine(DelayCollider());
        isPress = true;
        Destroy(music, 5);
    }

    public static void Test(){
        Debug.Log("Test");
    }

}
