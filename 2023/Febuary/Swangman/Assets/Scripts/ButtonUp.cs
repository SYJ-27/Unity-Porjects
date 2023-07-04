using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonUp : MonoBehaviour
{
    public Transform halfCircle;
    
    void OnMouseDrag(){
        if(CircleMain.mainCircle.gameObject.transform.localScale.x > 5.4f)
        {
            CircleMain.mainCircle.gameObject.transform.localScale += new Vector3(-0.1f,-0.1f,0);
            halfCircle.localScale += new Vector3(-0.1f,-0.1f,0);
        }
    }
}
