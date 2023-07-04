using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOrder : MonoBehaviour
{
    public int typeID;
    Transform plateOrder;
    public bool isOrderObject;
    // public int 
    void Awake(){
        isOrderObject = false;
        plateOrder = GameObject.Find("Tray Order").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
