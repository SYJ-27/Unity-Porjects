using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Bomb1 : MonoBehaviour
{
    Rigidbody2D bombRigid;
    int negX, negY;
    float orgX, orgY;
    public GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {
        orgX = transform.position.x;
        orgY = transform.position.y;
        bombRigid = GetComponent<Rigidbody2D>();
        bombRigid.bodyType = RigidbodyType2D.Static;
        Invoke("Move", 1f);
        Invoke("BombExplore", 15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move(){
        bombRigid.bodyType = RigidbodyType2D.Dynamic;
        if(orgY > 0.5f){
            if(orgX> 0){
                negX = 1;
            }
            else{
                negX = -1;
            }
            bombRigid.AddForce(new Vector3(-negX * 3,0, 0) * 22);
        }
        else if(orgY < 0.5f && orgY > -0.5f){
            if(orgX > 0){
                negX = 1;
            }
            else{
                negX = -1;
            }
            bombRigid.AddForce(new Vector3(-negX * 3,3, 0) * 22);
        }
        else{
            bombRigid.AddForce(new Vector3(-transform.position.x,10, 0) * 22);
        }
        Invoke("Stop", 0.3f);
    }

    void Stop(){
        bombRigid.bodyType = RigidbodyType2D.Static;
        if(CanJump()){
            Invoke("Move", 0.1f);
        }
        else{
            CancelInvoke("BombExplore");
            Invoke("BombExplore", Random.Range(2f, 4f));
        }
    }

    bool CanJump(){
        if(isInPlayArea()){
            if(orgY > 0.5f){
                if(transform.position.y > 0.5f){
                    return true;
                }
                else{
                    return false;
                }
            }
            else if(orgY < 0.5f && orgY > -0.5f){
                if(transform.position.x > 0f){
                    return true;
                }
                else{
                    return false;
                }
            }
            else{
                if(transform.position.y < 0.5f){
                    return true;
                }
                else{
                    return false;
                }
            }
        }
        return true;
    }

    bool isInPlayArea(){
        if(transform.position.x < -GameManager.gameManager.limitHorizontal.position.x - 0.3f || transform.position.x > GameManager.gameManager.limitHorizontal.position.x + 0.3f){
            return false;
        }
        if(transform.position.y < -3 || transform.position.y > 3){
            return false;
        }
        return true;
    }

    void BombExplore(){
        var explore = Instantiate(explosion, transform.position, Quaternion.identity);
        explore.gameObject.transform.localScale = new Vector3(0,0,0);
        explore.gameObject.transform.DOScale(new Vector3(3, 3, 1), 0.3f).OnComplete(() =>{
            Destroy(explore);
            Destroy(gameObject);
        });
    }

}
