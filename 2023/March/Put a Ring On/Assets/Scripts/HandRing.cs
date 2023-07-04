using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRing : MonoBehaviour
{
    public float speed = 3, orgX;
    public bool isMove, canMove;
    void Awake(){
        orgX = transform.position.x;
    }
    // Start is called before the first frame update
    void Start()
    {
        isMove = false;
        canMove = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isMove && canMove){
            transform.Translate(Vector2.left * Time.deltaTime * speed);
            LimitPosition();
        }
    }

    public void RotateUp(){
        transform.Rotate(0, 0, -1);
    }

    public void RotateDown(){
        transform.Rotate(0, 0, 1);
    }

    void LimitPosition(){
        Vector2 ballPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float x = Mathf.Clamp(transform.position.x, -ballPos.x + 1, ballPos.x);
        float y = Mathf.Clamp(transform.position.y, -ballPos.y + 0.5f, ballPos.y - 1);
        // if(x == -ballPos.x + 1 || x == ballPos.x || y == -ballPos.y + 0.5 || y == ballPos.y - 1){
        //     
        //     GameManager.gameManager.GameLosing();
        // }
        transform.position = new Vector3(x,y,0);
    }

    public void SetCanMove(bool state){
        if(state == true){
            ResetHandRingPos();
        }
        else{
            isMove = state;
        }
        canMove = state;
        
    }

    public void SetIsMove(bool state){
        isMove = state;
    }

    public void ResetHandRingPos(){
        transform.localRotation = new Quaternion(0, 0, 0, 0);
        Vector2 handRingPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        transform.position = new Vector3(handRingPos.x + (orgX - 8.88f), Random.Range(-1f, 2.5f), 0);
        gameObject.GetComponent<TrailRenderer>().Clear();
    }

}
