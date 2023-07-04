using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed, direction;
    public bool canJump, isHitting, canBoots;
    Rigidbody2D playerRigidbody;
    public Animator mainAnimator;
    public int numCup, numTeapot, numPlate;
    public int[] keyDownLogList;
    void Awake(){
        ResetPlayer();
        speed = 8;
        playerRigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(direction * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)){
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

    }

    public void MoveRight(){
        direction = 1;
        playerRigidbody.AddForce(Vector2.right * speed );
        LimitForce();
        LoggingKey(3);
    }

    public void MoveUp(){
        if(keyDownLogList[2] != 2 && canJump){
            canJump = false;
            // playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(Vector2.up * speed * 30);
            LimitForce();
            LoggingKey(2);
        }
    }

    public void MoveDown(){
        if(keyDownLogList[2] != 4){
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            LimitForce();
            LoggingKey(4);
        }
    }

    public void MoveLeft(){
        direction = -1;
        playerRigidbody.AddForce(Vector2.left * speed );
        LimitForce();
        LoggingKey(1);
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Base"){
            playerRigidbody.velocity = Vector2.zero;
            LoggingKey(0);
            canBoots = true;
            canJump = true;
        }
        if(other.gameObject.tag == "Cup" || other.gameObject.tag == "Teapot" || other.gameObject.tag == "Plate"){
            if(!isHitting){
                // ResetPlayer();
                GameManager.gameManager.GameLosing();
            }
            else{
                if(other.gameObject.tag == "Cup"){
                    numCup++;
                }
                else if(other.gameObject.tag == "Teapot"){
                    numTeapot++;
                }
                else if(other.gameObject.tag == "Plate"){
                    numPlate++;
                }
                CancelHit();
                Destroy(other.gameObject);
            }
        }
        if(other.gameObject.tag == "Wall" ||  other.gameObject.tag == "Teadrop" || other.gameObject.tag == "Saw"){
            GameManager.gameManager.GameLosing();
        }
    }

    void LimitForce(int x = 4){
        float xForce = Mathf.Clamp(playerRigidbody.velocity.x, -x, x);
        float yForce = Mathf.Clamp(playerRigidbody.velocity.y, -x, x);
        playerRigidbody.velocity = new Vector2(xForce, yForce);
    }

    public void Hitting(){
        if(keyDownLogList[2] == 2 && canBoots && !canJump){
            canBoots = false;
            playerRigidbody.AddForce(Vector2.up * speed * 30);
            LimitForce();
            LoggingKey(5);
        }
        isHitting = true;
        mainAnimator.SetBool("isKick", true);
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.2f,1.4f);
        Invoke("CancelHit", 1f);
    }

    void CancelHit(){
        isHitting = false;
        mainAnimator.SetBool("isKick", false);

        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.83f,1.4f);
    }

    void ResetPlayer(){
        numCup = 0;
        numPlate = 0;
        numTeapot = 0;
        canBoots = true;
        canJump = true;
        keyDownLogList = new int[3]{0,0,0};
        direction = 1;
        isHitting = false;
        transform.position = Vector3.zero;
    }

    void LoggingKey(int key){
        if(key != 0 && keyDownLogList[2] == 2){
            keyDownLogList[0] = keyDownLogList[1];
            keyDownLogList[1] = key;
        }
        else{
            keyDownLogList[0] = keyDownLogList[1];
            keyDownLogList[1] = keyDownLogList[2];
            keyDownLogList[2] = key;
        }
    }

}
