using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDad : MonoBehaviour
{
    public float speed;
    Vector3 worldPoint;
    Vector2 directionMovement;
    int moveId, stateDirection;
    public int life, maxLife;

    public Transform healthBar;

    Glass thisGlass;

    void Awake(){
        maxLife = 30;
        life = maxLife;
        stateDirection = 0;
        moveId = 0;
        directionMovement = -transform.right;
        speed = 7;
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
    // Start is called before the first frame update
    void Start()
    {
        moveId = Random.Range(0, 4);
        if(moveId == 0){
            MoveLeft();
        }
        else if(moveId == 1){
            MoveRight();
        }
        else if(moveId == 2){
            MoveUp();
        }
        else if(moveId == 3){
            MoveDown();
        }
    }

    public void SetPosition(Vector3 pos){
        life = maxLife;
        gameObject.SetActive(true);
        if(GameManager.gameManager.level < 7){
            speed = 5;
        }
        else{
            speed = 7;
        }
        healthBar.gameObject.SetActive(true);

        transform.position = pos;
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
        // if(x == minX && stateDirection == 0){
        //     CancelInvoke("GetNextMove");
        //     MoveRight();
        // }
        // else if(y == maxY  && stateDirection == 1){
        //     CancelInvoke("GetNextMove");
        //     MoveDown();
        // }
        // else if(x == maxX  && stateDirection == 2){
        //     CancelInvoke("GetNextMove");
        //     MoveLeft();
        // }
        // else if(y == minY  && stateDirection == 3){
        //     CancelInvoke("GetNextMove");
        //     MoveUp();
        // }
    }

    void GetNextMove(){
        // CancelInvoke("GetNextMove");
        CancelAllInvoke();
        moveId = Random.Range(0, 4);
        if(moveId == 0){
            Invoke("MoveLeft", Random.Range(0.1f, 0.5f));
        }
        else if(moveId == 1){
            Invoke("MoveRight", Random.Range(0.1f, 0.5f));
        }
        else if(moveId == 2){
            Invoke("MoveUp", Random.Range(0.1f, 0.5f));
        }
        else if(moveId == 3){
            Invoke("MoveDown", Random.Range(0.1f, 0.5f));
        }
    }

    public void MoveUp(){
        CancelAllInvoke();
        if(!GameManager.gameManager.isStart){
            if(thisGlass != null){
                transform.position = new Vector3(thisGlass.transform.position.x - 0, transform.position.y, 0);
            }
            transform.localEulerAngles = new Vector3(0,0, -90);
            directionMovement = -transform.up;
            stateDirection = 1;
        }
        Invoke("GetNextMove", 0.1f);    
    }

    void CancelAllInvoke(){
        CancelInvoke("GetNextMove");
        CancelInvoke("MoveUp");
        CancelInvoke("MoveRight");
        CancelInvoke("MoveDown");
        CancelInvoke("MoveLeft");
    }

    public void MoveDown(){
        CancelAllInvoke();
        if(!GameManager.gameManager.isStart){
            if(thisGlass != null){
                transform.position = new Vector3(thisGlass.transform.position.x + 0, transform.position.y, 0);
            }
            transform.localEulerAngles = new Vector3(0,0, 90);
            directionMovement = transform.up;
            stateDirection = 3;
            
        }
        Invoke("GetNextMove", 0.1f);    
    }

    public void MoveLeft(){
        CancelAllInvoke();
        if(!GameManager.gameManager.isStart){
            if(thisGlass != null){
                transform.position = new Vector3(transform.position.x, thisGlass.transform.position.y - 0, 0);
            }
            transform.localEulerAngles = new Vector3(0, 0, 0);
            directionMovement = -transform.right;
            stateDirection = 0;
        }
        Invoke("GetNextMove", 0.1f);    

    }

    public void MoveRight(){
        CancelAllInvoke();
        if(!GameManager.gameManager.isStart){
            if(thisGlass != null){
                transform.position = new Vector3(transform.position.x, thisGlass.transform.position.y + 0, 0);
            }
            transform.localEulerAngles = new Vector3(0, 0, 180);
            directionMovement = transform.right;
            stateDirection = 2;
        }
        Invoke("GetNextMove", 0.1f);    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Glass"){
            thisGlass = other.GetComponent<Glass>();
        }
        if(other.tag == "Saw"){
            life--;
            if(life <= 0){
                // GameManager.gameManager.GameLosing();
                life = 0;
                gameObject.SetActive(false);
                healthBar.gameObject.SetActive(false);
                CancelInvoke("GetNextMove");
            }
        }
        if(other.tag == "Stone"){
            CancelInvoke("GetNextMove");
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
            Invoke("GetNextMove", 0.1f);   
        }
    }
}
