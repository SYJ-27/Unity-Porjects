using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class Gift : MonoBehaviour
{
    public List<float> listPosY;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitPosition(){
        float x = UnityEngine.Random.Range(-3, 3);
        float y = listPosY[UnityEngine.Random.Range(0, listPosY.Count)];
        var (minValue, minIndex) = listPosY.Select((x, i) => (x, i)).Min();
        if(y > minValue && GameManager.gameManager.GetCurrentScore() < 3){
            Destroy(gameObject);
        }
        else{
            transform.position = new Vector3(x, y, 0);
        }
        
    }



}
