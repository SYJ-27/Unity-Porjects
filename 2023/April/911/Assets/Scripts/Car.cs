using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public static bool isPointerDown = true;
    public FixedJoystick mainJoystick;
    Vector2 move;
    Rigidbody2D playerRigidbody;
    float orgScaleX;
    public int direction, orgRotationZ, orgRotationX;
    void Awake(){
        orgScaleX = transform.localScale.x;
        playerRigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        PlayerMoving();
    }

    void PlayerMoving(){
        move.x = mainJoystick.Horizontal;
        move.y = mainJoystick.Vertical;
        float zAxis = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;
        if(move.x < 0){
            transform.localScale = new Vector3(direction * orgScaleX, transform.localScale.y, 1);
            // transform.localEulerAngles = new Vector3(orgRotationX, 0, orgRotationZ + zAxis + 180);
            transform.localEulerAngles = new Vector3(0, 0, zAxis + 180);
        }
        else{
            transform.localScale = new Vector3(-direction * orgScaleX, transform.localScale.y, 1);
            // transform.localEulerAngles = new Vector3(orgRotationX, 0, orgRotationZ + zAxis);
            transform.localEulerAngles = new Vector3(0, 0, zAxis);
        }
        Vector2 vecDirection  = transform.up * move.y + transform.right * move.x;
        if(isPointerDown){
            playerRigidbody.velocity = Vector2.zero;
        }
        else{
            // playerRigidbody.MovePosition(playerRigidbody.position + vecDirection * 3 * Time.fixedDeltaTime);
            playerRigidbody.MovePosition(playerRigidbody.position + move * 3 * Time.fixedDeltaTime);
        }
    }

}
