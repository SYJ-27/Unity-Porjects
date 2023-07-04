using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int life;
    Vector3 mousePoint;
    void Awake(){
        life = 3;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLife(){
        life++;
        if(life > 3){
            life = 3;
        }
    }

    public void MinusLife(){
        life--;
        if(life <= 0){
            life = 0;
        }
    }

}
