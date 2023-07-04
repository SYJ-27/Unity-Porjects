using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public Player mainPlayer;
    // public Transform mainClock;
    // public bool isLeft;
    // void Awake(){
    //     isLeft = true;
    // }
    // void Update(){
    //     if(!isLeft){
    //         if((mainClock.localRotation.z > 0.01f || mainClock.localRotation.z < -0.01f)){
    //             mainPlayer.RotateRight();
    //         }
    //         else{
    //             mainPlayer.ResetButtons();
    //         }
    //     }
    // }
    void OnMouseDrag(){
        // isLeft = true;
        mainPlayer.RotateLeft();
    }

    // void OnMouseUp(){
    //     isLeft = false;
    // }
}
