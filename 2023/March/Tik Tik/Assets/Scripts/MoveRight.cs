using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public Player mainPlayer;
    // public Transform mainClock;
    // public bool isRight;
    // void Awake(){
    //     isRight = true;
    // }
    // void Update(){
    //     if(!isRight){
    //         if((mainClock.localRotation.z > 0.01f || mainClock.localRotation.z < -0.01f)){
    //             mainPlayer.RotateLeft();
    //         }
    //         else{
    //             mainPlayer.ResetButtons();
    //         }
    //     }
    // }
    void OnMouseDrag(){
        // isRight = true;
        mainPlayer.RotateRight();
    }

    // void OnMouseUp(){
    //     isRight = false;
    // }
}
