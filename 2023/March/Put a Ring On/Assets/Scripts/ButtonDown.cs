using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDown : MonoBehaviour
{
    public HandRing mainHandRing;
    void OnMouseDrag(){
        if(mainHandRing.canMove){
            mainHandRing.RotateDown();
        }
    }
}
