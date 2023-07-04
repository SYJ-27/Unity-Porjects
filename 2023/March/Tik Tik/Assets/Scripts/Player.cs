using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Transform clockTrans;
    public Bullet prefabBullet;
    List<Bullet> listBullets = new List<Bullet>();

    public int direction, lifePlayer;
    public bool isJumping;
    // public MoveRight buttonRight;
    // public MoveLeft buttonLeft;
    void Awake(){
        lifePlayer = 8;
        isJumping = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(direction, 1, 1);
        if(Input.GetKey(KeyCode.A)){
            RotateRight();
        }
        if(Input.GetKey(KeyCode.D)){
            RotateLeft();
        }
        if(Input.GetKey(KeyCode.W)){
            Jumping();
        }
        // Debug.Log(transform.position.y);
        // LimitPosition();
    }

    public void RotateRight(){
        direction = 1;
        clockTrans.Rotate(0,0,70f * Time.deltaTime);
    }

    public void RotateLeft(){
        direction = -1;
        clockTrans.Rotate(0,0,-70f * Time.deltaTime);
    }

    public void Shooting(){
        if(!isJumping){
            var bulletT = Instantiate(prefabBullet, Vector3.zero, Quaternion.identity);
            bulletT.InitBullet(direction);
            listBullets.Add(bulletT);
        }
    }

    public void Jumping(){
        if(!isJumping){
            isJumping = true;
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300);
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Clock"){
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            isJumping = false;
        }
        // if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Enemy Bullet" || other.gameObject.tag == "Bullet"){
        //     ResetPlayer();
        // }
        // if(other.gameObject.tag == "Goal"){
        //     lifePlayer++;
        //     ResetPlayer();
        //     other.gameObject.GetComponent<LevelGoal>().ResetGoal();
        // }
    }

    public void NewLevel(){
        lifePlayer++;
        ResetPlayer();
        // other.gameObject.GetComponent<LevelGoal>().ResetGoal();
    }

    void DestroyAllBullet(){
        for(int i = 0; i < listBullets.Count; i++){
            if(listBullets[i] != null){
                Destroy(listBullets[i].gameObject);
            }
        }
        listBullets.Clear();
    }

    public void ResetPlayer(){
        MinusLife();
        isJumping = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = Vector3.zero;
    }

    public void MinusLife(){
        lifePlayer--;
        if(lifePlayer <= 0){
            DestroyAllBullet();
            GameManager.gameManager.GameLosing();
        }
    }

    public void AddLife(){
        if(lifePlayer <= 7){
            lifePlayer++;
        }
        else{
            lifePlayer = 8;
        }
    }

    // public void ResetButtons(){
    //     clockTrans.localRotation = new Quaternion(0,0,0,0);
    //     buttonLeft.isLeft = true;
    //     buttonRight.isRight = true;
    // }

    // public void LimitPosition(){
    //     float x = Mathf.Clamp(transform.position.x, -1.3f, 1.3f);
    //     float y = transform.position.y;
    //     if(x == -1.3f){
    //         y = -3.5f;
    //     }
    //     transform.position = new Vector3(x, y, transform.position.z);    
    // }
}
