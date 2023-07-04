using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public TrailRenderer mainTrail;
    public float speed;
    public bool isEatingApple;
    Vector3 worldPoint;

    void Awake(){
        isEatingApple = false;
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
        // transform.position = new Vector3(transform.position.x, transform.position.y, -transform.position.y);
    }

    Vector3 LimitPosition(Vector3 movement){
        float x = Mathf.Clamp(movement.x,-(worldPoint.x + 2), (worldPoint.x + 2));
        float y = Mathf.Clamp(movement.y,-(worldPoint.y + 3), (worldPoint.y + 2));

        return new Vector3(x,y,0);
    }

    public void ResetPosition(){
        transform.position = Vector3.zero;
    }

    public void MoveCrossRight(int numberMove){
        GameManager.gameManager.isStart = false;
        PlayerPrefs.SetInt("Mode", 1);
        Vector3 movement = transform.position + Vector3.right * numberMove /* * Time.deltaTime * speed */ + Vector3.down * numberMove /* * Time.deltaTime * speed */;
        movement = LimitPosition(movement);
        transform.DOMove(movement, 0.3f).SetId("PlayerMove");
        // transform.position = Vector3.MoveTowards(transform.position, movement, speed /* * Time.deltaTime */);
        // LimitPosition();
    }

    public void MoveCrossLeft(int numberMove){
        GameManager.gameManager.isStart = false;
        PlayerPrefs.SetInt("Mode", 1);
        Vector3 movement = transform.position + Vector3.left * numberMove /* * Time.deltaTime * speed */ + Vector3.down * numberMove /* * Time.deltaTime * speed */;
        movement = LimitPosition(movement);
        transform.DOMove(movement, 0.3f).SetId("PlayerMove");
        // transform.position = Vector3.MoveTowards(transform.position, movement, speed /* * Time.deltaTime */);
        // LimitPosition();
    }

    public void MoveLeft(int numberMove){
        GameManager.gameManager.isStart = false;
        PlayerPrefs.SetInt("Mode", 1);
        Vector3 movement = transform.position + Vector3.left * numberMove /* * Time.deltaTime * speed */;
        movement = LimitPosition(movement);
        transform.DOMove(movement, 0.3f).SetId("PlayerMove");
        // transform.position = Vector3.MoveTowards(transform.position, movement, speed /* * Time.deltaTime */);
        // LimitPosition();
    }

    public void MoveRight(int numberMove){
        GameManager.gameManager.isStart = false;
        PlayerPrefs.SetInt("Mode", 1);
        Vector3 movement = transform.position + Vector3.right * numberMove /* * Time.deltaTime * speed */;
        movement = LimitPosition(movement);
        transform.DOMove(movement, 0.3f).SetId("PlayerMove");
        // transform.position = Vector3.MoveTowards(transform.position, movement, speed /* * Time.deltaTime */);
        // LimitPosition();
    }

    public void MoveDown(int numberMove){
        GameManager.gameManager.isStart = false;
        PlayerPrefs.SetInt("Mode", 1);
        Vector3 movement = transform.position + Vector3.down * numberMove /* * Time.deltaTime * speed */;
        movement = LimitPosition(movement);
        transform.DOMove(movement, 0.3f).SetId("PlayerMove");
        // transform.position = Vector3.MoveTowards(transform.position, movement, speed /* * Time.deltaTime */);
        // LimitPosition();
    }

}
