using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDown : MonoBehaviour
{
    public Transform halfCircle;
    
    void OnMouseDrag(){
        if(CircleMain.mainCircle.gameObject.transform.localScale.x < 13){
            CircleMain.mainCircle.gameObject.transform.localScale -= new Vector3(-0.1f,-0.1f,0);
            halfCircle.localScale -= new Vector3(-0.1f,-0.1f,0);
        }
    }
}
