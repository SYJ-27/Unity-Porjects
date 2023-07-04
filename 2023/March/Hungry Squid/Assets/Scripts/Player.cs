using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public static Player mainPlayer;
    public bool canRotate, hitRod, hitCrab;
    public float minXMove, minYMove, maxXMove, maxYMove;
    public float speed, speedRotate;
    public Animator playerAnimation;

    void Awake(){
        mainPlayer = this;
        canRotate = true;
        hitRod = false;
        hitCrab = false;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        minXMove = -worldPosition.x;
        minYMove = -worldPosition.y;
        maxXMove = worldPosition.x;
        maxYMove = worldPosition.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();   
        if(canRotate && !hitRod){
            transform.Rotate(0,0,-1 * speedRotate);
        }
        KeepPlayerInRange();
    }

    private void PlayerMovement(){
        if(Input.GetMouseButton(0)){
            Move();
        }
        if(Input.GetMouseButtonUp(0)){
            IdleAnimation();
            ActiveRotate();
        }
    }

    private void KeepPlayerInRange(){
        if(!hitRod && !hitCrab){
            float x = Mathf.Clamp(transform.position.x, minXMove, maxXMove);
            float y = Mathf.Clamp(transform.position.y, minYMove, maxYMove);
            transform.position = new Vector3(x,y,0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Rod"){
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            FishingRod rod = other.gameObject.GetComponent<FishingRod>();
            if(rod.rodType == 1){
                GameManager.gameManager.UpdateScoreUI();
            }
            canRotate = false;
            hitRod = true;
            gameObject.GetComponent<SpriteRenderer>().DOColor(Color.red, 0.1f).OnComplete(() =>{
                gameObject.GetComponent<SpriteRenderer>().DOColor(Color.white, 0.1f).OnComplete(() =>{
                    MoveFollowRod(other.gameObject.transform.position.y - transform.position.y);
                    rod.MoveUp();
                });
            });
            
        }
        if(other.tag  == "Food"){
            Destroy(other.gameObject);
            GameManager.gameManager.UpdateScoreUI();
        }
        if(other.tag == "Crab"){
            hitCrab = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().DOColor(Color.red, 0.1f).OnComplete(() =>{
                gameObject.GetComponent<SpriteRenderer>().DOColor(Color.white, 0.1f).OnComplete(() =>{
                    gameObject.GetComponent<SpriteRenderer>().DOColor(Color.red, 0.1f).OnComplete(() =>{
                        gameObject.GetComponent<SpriteRenderer>().DOColor(Color.white, 0.1f).OnComplete(() =>{
                            
                            gameObject.GetComponent<BoxCollider2D>().enabled = false;
                            Destroy(other.gameObject);
                            Explose();
                        });
                    });
                });
            });
            
        }
    }

    public void Move(){
        if(!hitRod && !hitCrab){
            MoveAnimation();
            canRotate = false;
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

    public void ActiveRotate(){
        canRotate = true;
    }

    public void ReFalling(){
        IdleAnimation();
        hitRod = false;
        hitCrab = false;
        transform.position = new Vector3(0, 20, 0);
        transform.DOMoveY(0, 0.4f).OnComplete(() => {
            canRotate = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        });
        
    }

    public void MoveFollowRod(float yPos){
        transform.DOMoveY(10 - yPos, 0.2f).OnComplete(() =>{
            Invoke("ReFalling",1);
        });
    }

    public void Explose(){
        transform.DOMoveY(10, 0f).OnComplete(() =>{
            Invoke("ReFalling",1);
            
        });
    }

    public void MoveAnimation(){
        playerAnimation.SetBool("IsMove", true);
    }

    public void IdleAnimation(){
        playerAnimation.SetBool("IsMove", false);
    }

}
