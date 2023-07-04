using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    Color eggColor;
    Transform playerTrans;
    Rigidbody2D playerBody, eggBody;
    float xVeloc, yVeloc;
    void Awake(){
        // canvasUI = GameObject.Find("Canvas UI").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        eggColor = new Color((float)Random.Range(0,256)/255, (float)Random.Range(0,256)/255, (float)Random.Range(0,256)/255, 255);
        GetComponent<SpriteRenderer>().color = eggColor;
        playerBody = GameObject.Find("Penguin Player").GetComponent<Rigidbody2D>();
        playerTrans = GameObject.Find("Penguin Player").transform;
        eggBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameManager.isLose){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            AddForceEgg();
        }
        if(other.tag == "Ground"){
            Destroy(gameObject);
            GameManager.gameManager.eggBroked++;
            if(!GameManager.gameManager.isLose){
                GameManager.gameManager.SpawnEgg();
            }
        }
        if(other.tag == "Egg Holder"){
            GameManager.gameManager.PlusTimeScore();
            Destroy(gameObject);
            other.gameObject.GetComponent<SpriteRenderer>().color = eggColor;
            GameManager.gameManager.score++;
            if(!GameManager.gameManager.isLose){
                GameManager.gameManager.NextLevel();
            }
        }
        if(other.tag == "Wall V"){
            WallVelocity(1);
            AddingForce();
        }
        if(other.tag == "Wall H"){
            WallVelocity(-1);
            AddingForce();
        }
    }

    void AddForceEgg(){
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Vector3 distance = (transform.position - playerTrans.position);
        if(Mathf.Abs(distance.x) < 0.7f){
            distance.x = distance.x / Mathf.Abs(distance.x) * 0.7f;
        }
        if(Mathf.Abs(distance.y) < 0.7f){
            distance.y = distance.y / Mathf.Abs(distance.y) * 0.7f;
        }
        Vector3 pullForce = distance * 500;
        pullForce += new Vector3(playerBody.velocity.x, playerBody.velocity.y);
        GetComponent<Rigidbody2D>().AddForce(pullForce, ForceMode2D.Force);
    }

    private void WallVelocity(int direction){
        xVeloc = direction * eggBody.velocity.x * Random.Range(1,3);
        yVeloc = -direction * eggBody.velocity.y * Random.Range(1,3);
        LimitVeloc();
    }

    public void LimitVeloc(){
        xVeloc = Mathf.Clamp(xVeloc, -3, 3);
        yVeloc = Mathf.Clamp(yVeloc, -3, 3);
        eggBody.velocity = new Vector2(xVeloc, yVeloc);
    }

    public void AddingForce(){
        eggBody.AddForce(new Vector2(xVeloc * 30, yVeloc * 30));
    }

}
