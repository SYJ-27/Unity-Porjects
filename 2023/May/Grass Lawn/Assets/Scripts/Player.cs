using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public ConnectingLine mainLine;
    // public Player otherPlayer;
    public float speed;
    Vector3 worldPoint;
    Vector2 directionMovement;
    int stateDirection;
    public int life, maxLife;
    public Transform healthBar, sawButton;
    public bool canCleanAll;

    Glass thisGlass;

    void Awake(){
        canCleanAll = false;
        maxLife = 100;
        life = maxLife;
        stateDirection = 0;
        directionMovement = -transform.right;
        speed = 6;
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
    }

    void FixedUpdate(){
        if(!GameManager.gameManager.isStart){
            Moving();
        }
    }

    void Moving(){
        transform.Translate(directionMovement * speed * Time.fixedDeltaTime);
        LimitPosition();
    }

    void LimitPosition(){
        float minX = GameManager.gameManager.GetMaxLeft();
        float minY = GameManager.gameManager.GetMaxBottom();
        float maxX = GameManager.gameManager.GetMaxRight();
        float maxY = GameManager.gameManager.GetMaxTop();
        float x = Mathf.Clamp(transform.position.x, minX, maxX);
        float y = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(x,y,0);
    }

    public void MoveUp(){
        GameManager.gameManager.isStart = false;
        if(thisGlass != null){
            transform.position = new Vector3(thisGlass.transform.position.x - 0, transform.position.y, 0);
        }
        transform.localEulerAngles = new Vector3(0,0, -90);
        directionMovement = -transform.up;
        stateDirection = 1;
    }

    public void MoveDown(){
        GameManager.gameManager.isStart = false;
        if(thisGlass != null){
            transform.position = new Vector3(thisGlass.transform.position.x + 0, transform.position.y, 0);
        }
        transform.localEulerAngles = new Vector3(0,0, 90);
        directionMovement = transform.up;
        stateDirection = 3;
    }

    public void MoveLeft(){
        GameManager.gameManager.isStart = false;
        if(thisGlass != null){
            transform.position = new Vector3(transform.position.x, thisGlass.transform.position.y - 0, 0);
        }
        transform.localEulerAngles = new Vector3(0, 0, 0);
        directionMovement = -transform.right;
        stateDirection = 0;
    }

    public void MoveRight(){
        GameManager.gameManager.isStart = false;
        if(thisGlass != null){
            transform.position = new Vector3(transform.position.x, thisGlass.transform.position.y + 0, 0);
        }
        transform.localEulerAngles = new Vector3(0, 0, 180);
        directionMovement = transform.right;
        stateDirection = 2;
    }

    void RotateSaw(){
        sawButton.Rotate(0,0, 300 * Time.deltaTime);
    }

    public void ButtonCleaningDown(){
        canCleanAll = true;
        InvokeRepeating("RotateSaw",0 ,0.01f);
    }

    public void ButtonCleaningUp(){
        canCleanAll = false;
        CancelInvoke("RotateSaw");
    }

    public void SetPosition(Vector3 pos){
        if(GameManager.gameManager.level < 7){
            speed = 5;
        }
        else{
            speed = 7;
        }
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Glass"){
            if(thisGlass != null){
                thisGlass.SetColor(Color.white);
            }
            thisGlass = other.GetComponent<Glass>();
            thisGlass.SetColor(Color.red);
        }
        if(other.tag == "Saw"){
            if(GameManager.gameManager.level > -1){
                life--;
                if(life <= 0){
                    GameManager.gameManager.GameLosing();
                }
            }
        }
        if(other.tag == "Stone"){
            if(stateDirection == 0){
                stateDirection = 2;
                transform.localEulerAngles = new Vector3(0, 0, 180);
                directionMovement = transform.right;
            }
            else if(stateDirection == 3){
                stateDirection = 1;
                transform.localEulerAngles = new Vector3(0, 0, -90);
                directionMovement = -transform.up;
            }
            else if(stateDirection == 2){
                stateDirection = 0;
                transform.localEulerAngles = new Vector3(0, 0, 0);
                directionMovement = -transform.right;
            }
            else if(stateDirection == 1){
                stateDirection = 3;
                transform.localEulerAngles = new Vector3(0, 0, 90);
                directionMovement = transform.up;
            }
        }
    }

}
