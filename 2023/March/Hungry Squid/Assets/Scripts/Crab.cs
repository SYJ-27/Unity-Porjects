using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    public bool canRotate = false;
    public float minX, minY, maxX, maxY, speed;
    public int randomNegative;
    public int countHit =  0;

    void Awake(){
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        minX = -worldPosition.x;
        minY = -worldPosition.y;
        maxX = worldPosition.x;
        maxY = worldPosition.y;
        
        Destroy(gameObject, 13);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(randomNegative == 0){
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else{
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        
        
    }

    public void InitPosition(){
        randomNegative = Random.Range(0,2);
        if(randomNegative == 0){
            transform.position = new Vector3(Random.Range(minX - 1, minX), Random.Range(minY + 0.5f, maxY - 5));
        }
        else{
            transform.position = new Vector3(Random.Range(maxX,maxX + 1), Random.Range(minY + 0.5f, maxY - 5));
        }
    }

}
