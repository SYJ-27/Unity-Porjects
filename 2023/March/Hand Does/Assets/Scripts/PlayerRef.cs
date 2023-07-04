using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRef : MonoBehaviour
{
    public Ball yourBall;
    public Hand yourHand;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EndingLine"){
            yourHand.DisableDrag();
            yourBall.DisablePhysics();
            GameManager.gameManager.UpdateScoreUI();
            GameManager.gameManager.DestroyAllRoadLine();
            GameManager.gameManager.CreatRoad();
        }

        if(other.tag == "BorderRoad"){
            yourBall.Explore();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "BorderRoad"){
            yourBall.ReduceProtectNumber();
        }
    }
}
