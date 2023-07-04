using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    Player mainHeart;
    GameObject lineCatching;
    public int direction, orgDirection;
    public bool outArea, canMove;
    Vector3 distanceHeart;
    int life;
    void Awake()
    {
        life = 3;
        canMove = false;
        outArea = false;
        // GetComponent<PolygonCollider2D>().enabled = false;
        mainHeart = GameObject.Find("Main Heart").GetComponent<Player>();
        lineCatching = GameObject.Find("Catching Line");
    }
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.gameManager.score < 10){
            orgDirection = Random.Range(1, 3);
        }
        else{
            orgDirection = Random.Range(1, 4);
        }
        direction = orgDirection;
        distanceHeart = (transform.position - mainHeart.gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove && (!GameManager.gameManager.isPause || (GameManager.gameManager.isPause && direction < 0))){
            Moving();
        }
    }

    void Moving(){
        transform.Translate(new Vector3(distanceHeart.x, distanceHeart.y, 0) * direction * Time.deltaTime);
    }

    public void ActiveMoving(){
        Invoke("SetCanMove", (Random.Range(1.5f, 2f)));
    }

    public void SetCanMove(){
        canMove = true;
    }

    public void ResetDirection(){
        direction = orgDirection;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Area"){
            if(outArea){
                GameManager.gameManager.score++;
                life--;
                if(GameManager.gameManager.score == 3){
                    GameManager.gameManager.SpawningObstacle();
                }
                if(GameManager.gameManager.score % 7 == 0 && GameManager.gameManager.score != 0){
                    GameManager.gameManager.SpawningHeart();
                }
                if(GameManager.gameManager.score == 2){
                    GameManager.gameManager.SpawningStar();
                }
            }
            outArea = false;
            direction = orgDirection;
            lineCatching.GetComponent<CatchingLine>().ResetLine();
            if(life <= 0){
                Destroy(gameObject);
                GameManager.gameManager.score++;
                GameManager.gameManager.scoreHeart++;
                int scoreHeart = GameManager.gameManager.scoreHeart;
                if(scoreHeart % 3 == 0 && scoreHeart != 0){
                    mainHeart.UpdateLife();
                }
                GameManager.gameManager.SpawningHeart();
            }
        }
        if(other.tag == "Wall"){
            mainHeart.MinusLife();
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Area"){
            outArea = true;
        }
    }

}
