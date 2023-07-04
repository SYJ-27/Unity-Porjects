using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speedPlayer;
    public Bullet prefabsBullet;
    public List<Bullet> listBullets;

    public int lifePlayer, maxLife, maxBullet, numBullet;
    Vector3 worldPoint;
    void Awake(){
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        maxBullet = 20;
        numBullet = maxBullet;
        maxLife = 5;
        lifePlayer = maxLife;
        speedPlayer = 5;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A)){
            MoveLeft();
        }
        if(Input.GetKey(KeyCode.D)){
            MoveRight();
        }
        LimitHorizontalPlayer();
    }

    void LimitHorizontalPlayer(){
        if(transform.position.x < -worldPoint.x){
            transform.position = new Vector3(-worldPoint.x, transform.position.y, 0);
        }
        if(transform.position.x > worldPoint.x){
            transform.position = new Vector3(worldPoint.x, transform.position.y, 0);
        }
    }

    public void ButtonRightEnter(){
        InvokeRepeating("MoveRight", 0, 0.01f);
    }

    public void ButtonRightRelease(){
        CancelInvoke("MoveRight");
    }

    public void MoveRight(){
        transform.Translate(transform.right * speedPlayer * Time.deltaTime);
    }

    public void ButtonLeftEnter(){
        InvokeRepeating("MoveLeft", 0, 0.01f);
    }

    public void ButtonLeftRelease(){
        CancelInvoke("MoveLeft");
    }

    public void MoveLeft(){
        transform.Translate(-transform.right * speedPlayer * Time.deltaTime);
    }

    public void ButtonShootEnter(){
        InvokeRepeating("Shooting", 0, 0.1f);
    }

    public void ButtonShootRelease(){
        CancelInvoke("Shooting");
    }

    public void Shooting(){
        if(numBullet > 0){
            numBullet--;
            listBullets.Add(Instantiate(prefabsBullet, transform.position, Quaternion.identity));
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Carrot" || other.tag == "Cow"){
            lifePlayer--;
        }
        if(other.tag == "Bread"){
            numBullet += 4;
            if(numBullet > maxBullet){
                numBullet = maxBullet;
            }
        }
        if(other.tag == "Milk"){
            numBullet += 5;
            if(numBullet > maxBullet){
                numBullet = maxBullet;
            }
        }
    }
}
