using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CatchingLine : MonoBehaviour
{
    bool isCatching, isGetting;
    Transform heartTrans;
    SpriteRenderer lineSprite;
    Vector3 mousePoint;
    public float initScaleNumber;

    void Awake(){
        lineSprite = GetComponent<SpriteRenderer>();
        lineSprite.enabled = false;
    }

    void Start()
    {
        initScaleNumber = 0.05f;
        isCatching = false;
        isGetting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isGetting){
            lineSprite.enabled = true;
            mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Rotate(mousePoint);
            isCatching = true;
        }
        if(Input.GetMouseButtonUp(0)){
            // lineSprite.enabled = false;
            // if(heartTrans != null){
            //     heartTrans.gameObject.GetComponent<Heart>().ResetDirection();
            // }
            // isCatching = false;
            // transform.localScale = Vector3.one * initScaleNumber;
            // heartTrans = null;
            ResetLine();
        }
        if(isCatching){
            Catching();
        }
        if(isGetting){
            Getting();
        }
    }

    void Rotate(Vector3 pos){
        var angleTemp = Mathf.Atan2(pos.y - transform.position.y, pos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angleTemp, Vector3.forward);
    }

    void Catching(){
        transform.localScale += new Vector3(7 * Time.deltaTime, 0, 0);
    }

    void Getting(){
        if(heartTrans != null){
            float xScale = (float)Math.Round(Mathf.Sqrt((heartTrans.position.x * heartTrans.position.x) + (heartTrans.position.y * heartTrans.position.y)), 1);
            transform.localScale = new Vector3(xScale, initScaleNumber, initScaleNumber);
        }
        else{
            isGetting = false;
            transform.localScale = Vector3.one * initScaleNumber;
        }
    }

    public void ResetLine(){
        lineSprite.enabled = false;;
        if(heartTrans != null){
            heartTrans.gameObject.GetComponent<Heart>().ResetDirection();
        }
        isCatching = false;
        transform.localScale = Vector3.one * initScaleNumber;
        heartTrans = null;
        isGetting = false;
    }

    void EnableCollider2D(){
        GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Heart"){
            Heart tempHeart = other.gameObject.GetComponent<Heart>();
            if(tempHeart.outArea){
                Rotate(other.gameObject.transform.position);
                isGetting = true;
                isCatching = false;
                tempHeart.direction = -30;
                heartTrans = other.gameObject.transform;
            }
        }
        if(other.tag == "Obstacle"){
            ResetLine();
        }
        if(other.tag == "Star"){
            ResetLine();
            other.gameObject.GetComponent<PauseStar>().PausingTime();
        }
    }

}
