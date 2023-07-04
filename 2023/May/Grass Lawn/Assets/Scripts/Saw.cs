using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public int rotateDirection;
    public float speed;
    public float numberScale;
    public bool canCleanGrass;
    Rigidbody2D sawBody;
    Vector2 direction;
    float xVeloc, yVeloc;
    // Start is called before the first frame update
    void Start()
    {
        sawBody = GetComponent<Rigidbody2D>();
        rotateDirection = UnityEngine.Random.Range(0, 2);
        if(rotateDirection == 0){
            rotateDirection = -1;
        }
        if(GameManager.gameManager.level < 3){
            numberScale = (float)Math.Round(UnityEngine.Random.Range(0.5f, 1f), 1);
        }
        else if(GameManager.gameManager.level < 13){
            numberScale = (float)Math.Round(UnityEngine.Random.Range(0.3f, 0.7f), 1);
        }
        else{
            numberScale = (float)Math.Round(UnityEngine.Random.Range(0.2f, 0.55f), 1);
        }
        // numberScale = 1f;
        // if(numberScale < 0.6f){
        //     canCleanGrass = true;
        // }
        // else{
        //     canCleanGrass = false;
        // }
        canCleanGrass = true;
        transform.localScale = new Vector3(numberScale, numberScale, 0);
        speed = 3 * (1.5f - numberScale) * 200;
        Bouncing();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Bouncing();
        Rotate();
    }

    void Rotate(){
        transform.Rotate(0, 0, rotateDirection * 137 * Time.deltaTime);
    }

    void Bouncing(){
        float forceX, forceY;
        int negativeX = UnityEngine.Random.Range(0, 2);
        if(negativeX == 0){
            forceX = -1 * UnityEngine.Random.Range(0.5f, 1f);
        }
        else{
            forceX = UnityEngine.Random.Range(0.5f, 1f);
        }
        int negativeY = UnityEngine.Random.Range(0, 2);
        if(negativeY == 0){
            forceY = -1 * UnityEngine.Random.Range(0.5f, 1f);
        }
        else{
            forceY = UnityEngine.Random.Range(0.5f, 1f);
        }
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * forceX * speed  + Vector2.right * forceY * speed);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Wall V"){
            WallVelocity(1);
            AddingForce();
        }
        if(other.tag == "Wall H"){
            WallVelocity(-1);
            AddingForce();
        }
        if(other.tag == "Wall"){
            Destroy(gameObject);
        }
        if(other.tag == "Stone"){
            AddForceSaw(other.transform);
        }
    }

    void AddForceSaw(Transform other){
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Vector3 distance = (transform.position - other.position);
        if(Mathf.Abs(distance.x) < 0.7f){
            distance.x = distance.x / Mathf.Abs(distance.x) * 0.7f;
        }
        if(Mathf.Abs(distance.y) < 0.7f){
            distance.y = distance.y / Mathf.Abs(distance.y) * 0.7f;
        }
        Vector3 pullForce = distance * 500;
        pullForce += new Vector3(UnityEngine.Random.Range(0f,2f), UnityEngine.Random.Range(0f,2f));
        GetComponent<Rigidbody2D>().AddForce(pullForce, ForceMode2D.Force);
    }

    private void WallVelocity(int direction){
        xVeloc = direction * sawBody.velocity.x * UnityEngine.Random.Range(1,3);
        yVeloc = -direction * sawBody.velocity.y * UnityEngine.Random.Range(1,3);
        LimitVeloc();
    }

    public void AddingForce(){
        // sawBody.velocity = Vector2.zero;
        sawBody.AddForce(new Vector2(xVeloc * 30, yVeloc * 30));
    }

    public void LimitVeloc(){
        if(numberScale < 0.5f){
            xVeloc = Mathf.Clamp(xVeloc, -(4 + 4 * (1 - numberScale)), (4 + 4 * (1 - numberScale)));
            yVeloc = Mathf.Clamp(yVeloc, -(4 + 4 * (1 - numberScale)), (4 + 4 * (1 - numberScale)));
        }
        else{
            xVeloc = Mathf.Clamp(xVeloc, -(3), (3));
            yVeloc = Mathf.Clamp(yVeloc, -(3), (3));
        }
        sawBody.velocity = new Vector2(xVeloc, yVeloc);
    }

}
