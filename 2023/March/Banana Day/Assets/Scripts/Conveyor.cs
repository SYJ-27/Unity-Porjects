using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Conveyor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
    }

    public void MoveConveyor(){
        if(transform.position.x <= -19.8f){
            transform.position = new Vector3(29.4f, transform.position.y, transform.position.z);
        }
        transform.DOMoveX(transform.position.x - GameManager.gameManager.foodStep, 0.1f);
    }
}
