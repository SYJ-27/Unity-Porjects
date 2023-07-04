using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUp : MonoBehaviour
{
    public HandRing mainHandRing;
    void OnMouseDrag(){
        if(mainHandRing.canMove){
            mainHandRing.RotateUp();
        }
    }
}
