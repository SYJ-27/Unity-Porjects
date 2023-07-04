using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    bool canJump;
    public Transform stateTrans, limitHorizontal, verticalPlayerTrans;

    public Vector3 orgPos;
    public float speed;

    void Awake(){
        canJump = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        // Debug.Log(limitHorizontal.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            MoveUp();
        }
        if(Input.GetKey(KeyCode.A)){
            MoveLeft();
        }
        if(Input.GetKey(KeyCode.S)){
            MoveDown();
        }
        if(Input.GetKey(KeyCode.D)){
            MoveRight();
        }
        if(Input.GetKey(KeyCode.Space)){
            Jump();
        }
        orgPos.y = verticalPlayerTrans.position.y;
        if(transform.position.y < -3.3f){
            float y = Mathf.Clamp(transform.position.y, -3.3f, 3.8f);
            transform.position = new Vector3(transform.position.x,y,0);

            y = Mathf.Clamp(verticalPlayerTrans.position.y, -3.3f, 3.8f);
            verticalPlayerTrans.position = new Vector3(verticalPlayerTrans.position.x,y,0);
        }
        if(playerRigidbody.velocity.y == 0){
            stateTrans.localScale = new Vector3(0.8f, 0.3f, 1);
        }
        else if(playerRigidbody.velocity.y > 0){
            stateTrans.localScale -= new Vector3(0.01f, 0.01f, 0);
        }
        else{
            stateTrans.localScale += new Vector3(0.01f, 0.01f, 0);
        }
        // transform.position = new Vector3(stateTrans.position.x, stateTrans.position.y, 0);
    }

    public void Jump(){
        if(canJump){
            canJump = false;
            orgPos = transform.position;
            playerRigidbody.bodyType = RigidbodyType2D.Dynamic;
            playerRigidbody.AddForce(Vector2.up * 200);
            Debug.Log(playerRigidbody.velocity);
        }
    }

    public void MoveLeft(){
        // playerRigidbody.bodyType = RigidbodyType2D.Static;
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        orgPos.x = transform.position.x;
        LimitPlayAreaHorizontal();

    }

    public void MoveRight(){
        // playerRigidbody.bodyType = RigidbodyType2D.Static;
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        orgPos.x = transform.position.x;
        LimitPlayAreaHorizontal();

    }

    public void MoveUp(){
        // playerRigidbody.bodyType = RigidbodyType2D.Static;
        if(transform.position.y < 3.8f){
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            verticalPlayerTrans.Translate(Vector2.up * speed * Time.deltaTime);
            LimitPlayAreaVeritcal();
        }

    }

    public void MoveDown(){
        // playerRigidbody.bodyType = RigidbodyType2D.Static;
        if(transform.position.y > -3.3f){
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            verticalPlayerTrans.Translate(Vector2.down * speed * Time.deltaTime);
            LimitPlayAreaVeritcal();
        }

    }

    public void ButtonLeft(){
        InvokeRepeating("MoveLeft", 0, 0.01f);
    }

    public void ButtonRight(){
        InvokeRepeating("MoveRight", 0, 0.01f);
    }

    public void ButtonUp(){
        InvokeRepeating("MoveUp", 0, 0.01f);
    }

    public void ButtonDown(){
        InvokeRepeating("MoveDown", 0, 0.01f);
    }

    public void ButtonRelease(){
        CancelInvoke("MoveLeft");
        CancelInvoke("MoveRight");
        CancelInvoke("MoveUp");
        CancelInvoke("MoveDown");
        // orgPos = transform.position;
    }

    private void LimitPlayArea(){
        float x = Mathf.Clamp(transform.position.x, -limitHorizontal.position.x, limitHorizontal.position.x);
        float y = Mathf.Clamp(transform.position.y, -3.3f, 3.8f);
        transform.position = new Vector3(x,y,0);

        x = Mathf.Clamp(verticalPlayerTrans.position.x, -limitHorizontal.position.x, limitHorizontal.position.x);
        y = Mathf.Clamp(verticalPlayerTrans.position.y, -3.3f, 3.8f);
        verticalPlayerTrans.position = new Vector3(x,y,0);
    }

    private void LimitPlayAreaHorizontal(){
        float x = Mathf.Clamp(transform.position.x, -limitHorizontal.position.x, limitHorizontal.position.x);
        transform.position = new Vector3(x,transform.position.y,0);

        x = Mathf.Clamp(verticalPlayerTrans.position.x, -limitHorizontal.position.x, limitHorizontal.position.x);
        verticalPlayerTrans.position = new Vector3(x,verticalPlayerTrans.position.y,0);
    }

    private void LimitPlayAreaVeritcal(){
        float y = Mathf.Clamp(transform.position.y, -3.3f, 3.8f);
        transform.position = new Vector3(transform.position.x,y,0);

        y = Mathf.Clamp(verticalPlayerTrans.position.y, -3.3f, 3.8f);
        verticalPlayerTrans.position = new Vector3(verticalPlayerTrans.position.x,y,0);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "State"){
            playerRigidbody.bodyType = RigidbodyType2D.Kinematic;
            playerRigidbody.velocity = Vector2.zero;
            transform.position = new Vector3(transform.position.x,orgPos.y, 0);
            stateTrans.position = new Vector3(transform.position.x,orgPos.y - 0.43f, 0);
            canJump = true;
        }
        if(other.tag == "Obstacle"){
            if(canJump){
                Debug.Log("Lose");
                GameManager.gameManager.GameLosing("Collision with obstacles");
            }
        }
        if(other.tag == "Bomb"){
            GameManager.gameManager.GameLosing("Affected by the explosion");
            Debug.Log("Lose Bomb");
        }
        if(other.tag == "Lazer" && other.gameObject.GetComponent<Snipper>().isActiveLazer == true){
            GameManager.gameManager.GameLosing("Burned by laser");
            Debug.Log("Lose Bomb");
        }
    }

    void PlayerExplore(){
        
    }

}
