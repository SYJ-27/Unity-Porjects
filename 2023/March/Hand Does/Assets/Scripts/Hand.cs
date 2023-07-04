using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public SpringJoint2D handJoint;
    public LineRenderer lineConnected;
    public Transform balloonTrans;
    public Ball yourBall;
    public bool isDraging, isPassed, isNewLevel;

    void Awake(){
        isNewLevel = false;
        isPassed = false;
        isDraging = false;
    }
    
    void Update()
    {
        if(!GameManager.gameManager.isLose){
            
            if(Input.GetMouseButtonDown(0)){
                yourBall.EnablePhysics();
                UpdateJointState();
            }
            if(Input.GetButtonUp("Fire1")){
                ResetLineConnected();
            }
            if(isDraging){
                UpdateLineConnected();
            }
        }
        else{
            gameObject.SetActive(false);
        }
        
    }

    void ResetLineConnected()
    {
        HandDraging(false);
        lineConnected.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
        lineConnected.SetPosition(1, new Vector3(transform.position.x, transform.position.y, 0));
    }

    void UpdateLineConnected()
    {
        lineConnected.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
        lineConnected.SetPosition(1, new Vector3(balloonTrans.position.x, balloonTrans.position.y, 0));
    }

    public void EnableDrag(){
        yourBall.EnablePhysics();
        UpdateJointState();

        gameObject.SetActive(true);
    }

    public void DisableDrag(){
        lineConnected.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
        lineConnected.SetPosition(1, new Vector3(transform.position.x, transform.position.y, 0));

        gameObject.SetActive(false);
    }

    public void HandDraging(bool state){
        isDraging = state;
        handJoint.enabled = state;
        gameObject.GetComponent<SpriteRenderer>().enabled = state;
    }

    public void UpdateJointState(){
        HandDraging(true);

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPos.x, worldPos.y, 0);
        handJoint.connectedAnchor=new Vector2(transform.position.x, transform.position.y);
    }

}
