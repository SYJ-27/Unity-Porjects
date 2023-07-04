using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;


public class MainSaw : MonoBehaviour
{
    private Camera myCam;
    private Vector3 screenPos;
    private float angleOffSet;

    void Start(){
        myCam = Camera.main;
    }

    void Update(){
        Vector3 mousePos = myCam.ScreenToWorldPoint(Input.mousePosition);
        // if(!mainPlayer.isClickMove || !uiController.isDangerTime){
            if(Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject == null){
                Debug.Log("Touch 1");
                screenPos = myCam.WorldToScreenPoint(transform.position);
                Vector3 vec3 = Input.mousePosition - screenPos;
                angleOffSet = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) * Mathf.Rad2Deg;
            }
            if(Input.GetMouseButton(0) && EventSystem.current.currentSelectedGameObject == null){
                Debug.Log("Touch 2");
                Vector3 vec3 = Input.mousePosition - screenPos;
                float angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, 0, angle + angleOffSet);
            }
        // }
    }

}
