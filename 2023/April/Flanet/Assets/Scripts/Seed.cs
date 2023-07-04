using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Seed : MonoBehaviour
{
    Transform earthTransform;
    public Transform topPos, top;
    public BoxCollider2D  firstCollider;
    public List<Transform> listTopPos;
    public List<Flower> listFlowers;
    public LineRenderer lineSeed;
    Flower mainFlower;
    Vector3 centerPos;
    public float angularSpeed, rotationRadius;

    private float posX, posY, angle = 0;
    int direction = 1;
    public bool canHarvest, isGrowing;

    private void Awake(){
        isGrowing = false;
        canHarvest = false;
        top = listTopPos[Random.Range(0, listTopPos.Count)];
        centerPos = Vector3.zero;
        earthTransform = GameObject.Find("Earth").transform;
        rotationRadius = 0.4f;
    }

    void Update(){
        lineSeed.SetPosition(0, transform.position);
        lineSeed.SetPosition(1, topPos.position);
        // if(canHarvest){
        //     secondCollider.enabled = true;
        // }
    }

    void FixedUpdate(){
        var angleTemp = Mathf.Atan2(earthTransform.position.y - transform.position.y, earthTransform.position.x - transform.position.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.AngleAxis(angleTemp, Vector3.forward);
        if(earthTransform != null && centerPos != Vector3.zero){
            posX = centerPos.x + Mathf.Cos(angle) * rotationRadius;
            posY = centerPos.y + Mathf.Sin(angle) * rotationRadius;
            transform.position = new Vector2(posX, posY);
            angle = angle - Time.deltaTime * angularSpeed * direction;

            if(angle >= 360){
                angle = 0;
            }
        }
    }

    public void InitSeed(Vector3 center, float angleDirection, int playerDirection){
        centerPos = center;
        angle = angleDirection;
        direction = playerDirection;
        angularSpeed = Random.Range(3f, 4f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Earth" && !isGrowing){
            angularSpeed = 0;
            firstCollider.enabled = false;
            Growing();
        }
        if(other.tag == "Player" && canHarvest){
            mainFlower.gameObject.transform.DOMove(transform.position, 0.3f).OnComplete(() =>{
                Destroy(gameObject);
            });
        }
    }

    void Growing(){
        isGrowing = true;
        topPos.DOMove(top.position, Random.Range(0.5f, 0.7f)).OnComplete(() =>{
            GetFlower();
        });
    }

    void GetFlower(){
        mainFlower = Instantiate(listFlowers[Random.Range(0,listFlowers.Count)], top.position, Quaternion.identity);
        mainFlower.GrowFlower(this);
        GameManager.gameManager.listFlowers.Add(mainFlower);
    }

    public void DestroySeed(){
        if(mainFlower != null){
            Destroy(mainFlower.gameObject);
        }
        Destroy(gameObject);
    }

}
