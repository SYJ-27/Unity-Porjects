using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Seed prefabSeed;
    List<Seed> listSeed;
    public Transform earthTransform, jumpPos;
    public List<Transform> listCenter;
    public float angularSpeed, rotationRadius;
    int direction;
    public int numberSeed;

    List<float> listAngleSeed;

    private float posX, posY, angle;
    public bool isJumping;

    private void Awake(){
        listSeed = new List<Seed>();
        numberSeed = 5;
        listAngleSeed = new List<float>(){0, Mathf.PI};
        direction = 1;
        rotationRadius = jumpPos.position.y;
        isJumping = false;
        angle = Mathf.PI / 2;
        InvokeRepeating("GetSeed", 0, 2);
    }

    void Update(){
        if(Input.GetKey(KeyCode.D)){
            MoveRight();
        }
        if(Input.GetKey(KeyCode.A)){
            MoveLeft();
        }
        if(Input.GetKey(KeyCode.W)){
            MoveUp();
        }
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)){
            angularSpeed = 0;
        }
    }
    
    void FixedUpdate(){
        rotationRadius = jumpPos.position.y;
        var angleTemp = Mathf.Atan2(earthTransform.position.y - transform.position.y, earthTransform.position.x - transform.position.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.AngleAxis(angleTemp, Vector3.forward);

        posX = earthTransform.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = earthTransform.position.y + Mathf.Sin(angle) * rotationRadius;
        transform.position = new Vector2(posX, posY);
        angle = angle - Time.deltaTime * angularSpeed;
        
    }

    public void ButtonLeft(){
        Invoke("MoveLeft", Time.deltaTime);
    }
    public void ButtonRight(){
        Invoke("MoveRight", Time.deltaTime);
    }

    public void ButtonRightRelease(){
        angularSpeed = 0;
        CancelInvoke("MoveRight");
    }

    public void ButtonLeftRelease(){
        angularSpeed = 0;
        CancelInvoke("MoveLeft");
    }

    void MoveRight(){
        direction = 1;
        angularSpeed = 2;
    }

    void MoveLeft(){
        direction = -1;
        angularSpeed = -2;
    }

    public void MoveUp(){
        if(!isJumping){
            isJumping = true;
            jumpPos.DOMoveY(4, 0.15f).SetId("Player Jumping").OnComplete(() =>{
                jumpPos.DOMoveY(2, 0.1f).OnComplete(() =>{
                    isJumping = false;
                });
            });
        }
    }

    void GetSeed(){
        if(numberSeed < 5){
            numberSeed++;
        }
    }

    public void Seeding(){
        if(numberSeed > 0 && !isJumping){
            numberSeed--;
            var sed = Instantiate(prefabSeed, transform.position, Quaternion.identity);
            float angleSeed;
            int idx = direction;
            if(idx == -1){
                idx = 0;
                angleSeed = Mathf.Atan2(transform.position.y, transform.position.x) - Mathf.PI / 2;
            }
            else{
                angleSeed = Mathf.PI / 2 + Mathf.Atan2(transform.position.y, transform.position.x);
            }
            sed.InitSeed(listCenter[idx].position, angleSeed, direction);
            listSeed.Add(sed);
        }
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy"){
            if(isJumping){
                DOTween.Kill("Player Jumping");
                jumpPos.DOMoveY(2, 0.1f).OnComplete(() =>{
                    isJumping = false;
                });
            }
        }
    }

    public void DestroyAllSeed(){
        for(int i = 0; i < listSeed.Count; i++){
            if(listSeed[i] != null){
                listSeed[i].DestroySeed();
            }
        }
    }

}
