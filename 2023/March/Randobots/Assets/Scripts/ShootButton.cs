using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootButton : MonoBehaviour
{
    public Player mainPlayer;
    void OnMouseDrag(){
        mainPlayer.Shooting();
    }


}
