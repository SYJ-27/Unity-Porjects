using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public GameObject playerRevive;
    public float speed = 3;
    public int shootingMode;
    public List<Bullet> prefabBullets;
    public Bomb prefabBomb;
    bool canMove, isChangeShootingMode;

    List<GameObject> listBulletObjects;
    Vector3 worldPoint;

    public int maxMoveScale;

    public bool canRevive;

    void Awake(){
        maxMoveScale = 3;
        canRevive = false;
        isChangeShootingMode = false;
        listBulletObjects = new List<GameObject>();
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        shootingMode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.gameManager.isStart && !GameManager.gameManager.isPause){
            // PlayerAbsorbing();
            canMove = true;
        }
        else{
            canMove = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        // LimitPosition();
        // speed = GetVeloc();

        // if(Input.GetKey(KeyCode.Space)){
        //     Shooting(); 
        // }
    
    }

    float GetVeloc(){
        float velocX = GetComponent<Rigidbody2D>().velocity.x;
        float velocY = GetComponent<Rigidbody2D>().velocity.y;
        return Mathf.Sqrt((velocX * velocX) + (velocY * velocY));
    }

    void LimitPosition(){
        float x = Mathf.Clamp(transform.position.x, -worldPoint.x, worldPoint.x);
        float y = Mathf.Clamp(transform.position.y, -worldPoint.y, worldPoint.y);
        transform.position = new Vector3(x,y,0);
    }

    void MoveUp()
    {
        if(canMove){
            speed = GetVeloc() * 7f;
            if(speed > maxMoveScale){
                speed = maxMoveScale;
            }
            // GameManager.gameManager.isStart = false;
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            LimitPosition();
        }
        // GetComponent<Rigidbody2D>().AddForce(Vector2.up * 30);
    }

    void MoveDown()
    {
        if(canMove){
            speed = GetVeloc() * 7f;
            if(speed > maxMoveScale){
                speed = maxMoveScale;
            }
            // GameManager.gameManager.isStart = false;
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            LimitPosition();
        }
        // GetComponent<Rigidbody2D>().AddForce(Vector2.down * 30);
    }

    void MoveLeft()
    {
        if(canMove){
            speed = GetVeloc() * 7f;
            if(speed > maxMoveScale){
                speed = maxMoveScale;
            }
            // GameManager.gameManager.isStart = false;
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            LimitPosition();
        }
        // GetComponent<Rigidbody2D>().AddForce(Vector2.left * 30);
    }

    void MoveRight()
    {
        if(canMove){
            speed = GetVeloc() * 7f;
            if(speed > maxMoveScale){
                speed = maxMoveScale;
            }
            // GameManager.gameManager.isStart = false;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            LimitPosition();
        }
        // GetComponent<Rigidbody2D>().AddForce(Vector2.right * 30);
    }

    public void ButtonUp(){
        GameManager.gameManager.isStart = false;
        InvokeRepeating("MoveUp", 0, 0.01f);
    }

    public void ButtonDown(){
        GameManager.gameManager.isStart = false;
        InvokeRepeating("MoveDown", 0, 0.01f);
    }

    public void ButtonLeft(){
        GameManager.gameManager.isStart = false;
        InvokeRepeating("MoveLeft", 0, 0.01f);
    }

    public void ButtonRight(){
        GameManager.gameManager.isStart = false;
        InvokeRepeating("MoveRight", 0, 0.01f);
    }

    public void ButtonUpRelease(){
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        CancelInvoke("MoveUp");
    }

    public void ButtonDownRelease(){
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        CancelInvoke("MoveDown");
    }

    public void ButtonLeftRelease(){
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        CancelInvoke("MoveLeft");
    }

    public void ButtonRightRelease(){
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        CancelInvoke("MoveRight");
    }

    public void Shooting(){
        if(!GameManager.gameManager.isPause){
            if(isChangeShootingMode){
                isChangeShootingMode = false;
                Invoke("ResetShootingMode", 7);
            }
            GameManager.gameManager.isStart = false;
            if(shootingMode == 0){
                Debug.Log(shootingMode);
                listBulletObjects.Add(Instantiate(prefabBullets[0], transform.position, Quaternion.identity).gameObject);
            }
            if(shootingMode == 1){
                Debug.Log(shootingMode);

                listBulletObjects.Add(Instantiate(prefabBullets[1], transform.position, Quaternion.identity).gameObject);
            }
            if(shootingMode == 2){
                Debug.Log(shootingMode);

                var bullet1 = Instantiate(prefabBullets[0], transform.position, Quaternion.identity);
                bullet1.Rotate(30);
                var bullet2 = Instantiate(prefabBullets[0], transform.position, Quaternion.identity);
                var bullet3 = Instantiate(prefabBullets[0], transform.position, Quaternion.identity);
                bullet3.Rotate(-30);
                listBulletObjects.Add(bullet1.gameObject);
                listBulletObjects.Add(bullet2.gameObject);
                listBulletObjects.Add(bullet3.gameObject);

            }
            if(shootingMode == 3){
                Debug.Log(shootingMode);

                listBulletObjects.Add(Instantiate(prefabBomb, transform.position, Quaternion.identity).gameObject);
            }
        }
    }

    public void ClearListBullets(){
        for(int i = 0 ; i < listBulletObjects.Count; i++){
            if(listBulletObjects[i] != null){
                Destroy(listBulletObjects[i]);
            }
        }
        listBulletObjects.Clear();
    }

    public void SetShootingMode(int id){
        CancelInvoke("ResetShootingMode");
        if(id == 4){
            shootingMode = 0;
            canRevive = true;
        }
        else{
            shootingMode = id;
            isChangeShootingMode = true;
        }
    }

    public void ResetPlayerMovement(){
        CancelInvoke("MoveUp");
        CancelInvoke("MoveDown");
        CancelInvoke("MoveRight");
        CancelInvoke("MoveLeft");
    }

    void ResetShootingMode(){
        shootingMode = 0;
    }

    public void ResetPosition(){
        ClearListBullets();
        transform.position = Vector3.zero;
    }

    public void Reviving(){
        ClearListBullets();
        transform.localScale = Vector3.zero;
        canRevive = false;
        ResetPlayerMovement();
        transform.position = Vector3.zero;
        Destroy(Instantiate(playerRevive, Vector3.zero, Quaternion.identity),10f);
        transform.DOScale(Vector3.one, 1f).OnComplete(() =>{
            // GameManager.gameManager.isStart = true;
            canMove = true;
        });
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Planet"){
            if(!GameManager.gameManager.isPause){
                transform.position = new Vector3(-worldPoint.x + 5, 0, 0);
                ResetPlayerMovement();
            }
        }
        if(other.tag == "Sun"){
            if(!canRevive){
                GameManager.gameManager.GameLosing();
            }
            else{
                canMove = false;
                GameManager.gameManager.PlayerRevived();
            }
        }
    }



}
