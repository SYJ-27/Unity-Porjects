using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public GameObject prefabObjectOrder;
    Transform plateOrder;
    public List<Transform> posOrders;
    MessageBox messageBox;
    bool canMove;
    void Awake(){
        messageBox = GameObject.Find("Message Box").GetComponent<MessageBox>();
        plateOrder = GameObject.Find("Tray Order").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           
    }


    void OnMouseDown()
    {
        if(messageBox.foodGetCount < messageBox.foodNum){
            messageBox.foodGetCount++;
            var objectOrder = Instantiate(prefabObjectOrder, posOrders[messageBox.foodGetCount-1].position, Quaternion.identity);
            objectOrder.GetComponent<ObjectOrder>().isOrderObject = true;
            messageBox.listObjectOrder.Add(objectOrder.GetComponent<ObjectOrder>());
        }
    }
}
