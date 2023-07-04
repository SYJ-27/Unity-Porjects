using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public LineRenderer line;
    public Collider2D firstCollider;
    public Transform holeTrans, leftBox, rightBox, topBox, bottomBox, healthBase;
    private bool isLevelUp, canMove;
    public bool isMoving;
    public int life, maxLife;
    private Vector3 worldPoint;


    // Start is called before the first frame update
    void Start()
    {
        maxLife = 3;
        life = maxLife;
        firstCollider.enabled = false;
        isLevelUp = false;
        canMove = true;
        isMoving = false;
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthBase.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
        if(isLevelUp){
            transform.position = Vector3.MoveTowards(transform.position, holeTrans.position, 0.5f);
        }
        if(transform.position.x == holeTrans.position.x && transform.position.y == holeTrans.position.y && isLevelUp){
            GameManager.gameManager.score++;
            isLevelUp = false;
            transform.position = Vector3.zero;
            holeTrans.gameObject.SetActive(false);
            transform.localRotation = new Quaternion(0,0,0,0);
            GameManager.gameManager.ClearListLeafs();
            firstCollider.enabled = false;
            Invoke("LevelUp", 0.5f);
        }
        // LimitPlayer();
        if(transform.position.x > -worldPoint.x && transform.position.x < worldPoint.x && transform.position.y > -worldPoint.y && transform.position.y < worldPoint.y){
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);
        }
        else{
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, transform.position);
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    // void FixedUpdate()
    // {
    // }

    void LimitPlayer(){
        float x = Mathf.Clamp(transform.position.x, leftBox.position.x, rightBox.position.x);
        float y = Mathf.Clamp(transform.position.y, bottomBox.position.y, topBox.position.y);
        transform.position = new Vector3(x,y,0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Hole"){
            GameManager.gameManager.CancelSpawnStone();
            Debug.Log("Level Up");
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            isLevelUp = true;
            canMove = false;
            isMoving = false;
        }
        if(other.tag == "Stone"){
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Destroy(other.gameObject);
            life--;
            if(life <= 0){
                Debug.Log("Death: " + life);
                GameManager.gameManager.GameLosing("YOU DIED!");
            }
            else{
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    void LevelUp(){
        CancelInvoke("LevelUp");
        GameManager.gameManager.NextLevel();
        canMove = true;
        holeTrans.gameObject.SetActive(true);
    }

    void MoveUp(){
        if(canMove){
            if(!isMoving){
                GameManager.gameManager.SpawningStones();
                isMoving = true;
                firstCollider.enabled = true;
            }
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 7);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    void MoveLeft(){
        if(canMove){
            if(!isMoving){
                GameManager.gameManager.SpawningStones();
                isMoving = true;
                firstCollider.enabled = true;
            }
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7);
        }
    }

    void MoveDown(){
        if(canMove){
            if(!isMoving){
                GameManager.gameManager.SpawningStones();
                isMoving = true;
                firstCollider.enabled = true;
            }
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7);
        }
    }

    void MoveRight(){
        if(canMove){
            if(!isMoving){
                GameManager.gameManager.SpawningStones();
                isMoving = true;
                firstCollider.enabled = true;
            }
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7);
        }
    }

    public void ButtonUp(){
        InvokeRepeating("MoveUp", 0, 0.01f);
    }

    public void ButtonLeft(){
        InvokeRepeating("MoveLeft", 0, 0.01f);
    }

    public void ButtonDown(){
        InvokeRepeating("MoveDown", 0, 0.01f);
    }

    public void ButtonRight(){
        InvokeRepeating("MoveRight", 0, 0.01f);
    }

    public void ButtonUpRelease(){
        CancelInvoke("MoveUp");
    }

    public void ButtonLeftRelease(){
        CancelInvoke("MoveLeft");
    }

    public void ButtonDownRelease(){
        CancelInvoke("MoveDown");
    }

    public void ButtonRightRelease(){
        CancelInvoke("MoveRight");
    }

}
