using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Food : MonoBehaviour
{
    public bool canRotate = false;
    public float minX, minY, maxX, maxY;
    public float rotateAngle;
    public int countHit =  0;

    void Awake(){
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        minX = -worldPosition.x;
        minY = -worldPosition.y;
        maxX = worldPosition.x;
        maxY = worldPosition.y;
        int randomNegative = Random.Range(0,2);
        if(randomNegative == 0){
            rotateAngle = -Random.Range(0.3f, 0.6f);
        }
        else{
            rotateAngle = Random.Range(0.3f, 0.6f);
        }
        Destroy(gameObject, Random.Range(11, 13));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canRotate){
            transform.Rotate(0,0,rotateAngle);
        }
    }

    public void InitPosition(){
        float y = Random.Range(minY + 2, maxY - 2);
        transform.DOMoveY(y, Random.Range(0.2f, 0.4f)).OnComplete(() =>{
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            canRotate = true;
        });
    }

}
