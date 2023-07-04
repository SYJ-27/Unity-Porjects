using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitRight : MonoBehaviour
{
    public bool canHit;
    public Base yourBase;
    void Awake(){
        canHit = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDrag(){
        if(canHit){
            canHit = false;
            yourBase.HitRight();
            Invoke("EnableHit", 0.2f);
        }

    }

    void EnableHit(){
        canHit = true;
    }
}
