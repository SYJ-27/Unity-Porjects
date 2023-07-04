using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    
    public Sprite[] sprite;
    public BoxCollider2D box;
    
    public int id;
    bool isPass;
    public Vector3 orgGatePos;
    void Awake(){
        isPass = false;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(id == 0 && transform.position.y > GameManager.gameManager.playerFood.position.y && !isPass && !GameManager.gameManager.isStart && !GameManager.gameManager.isLose){
            GameManager.gameManager.GameLosing("Ignored the Gate!");
            // Debug.Log("Inorge the Gate");
        }
        if(id != 0 && id != 3 && id != 4){
            if(transform.position.y > GameManager.gameManager.playerFood.position.y){
                spriteRenderer.sortingOrder = 1;
            }
            else{
                spriteRenderer.sortingOrder = 3;
            }
        }
    }

    public void InitObstacle(int idObstacle){
        if(idObstacle != 0){
            id = Random.Range(1, 3);
        }
        else{
            id = idObstacle;
        }
        
        spriteRenderer.sprite = null;
        spriteRenderer.sprite = sprite[id];
        // box.size = spriteRenderer.size;
        if(id == 0){
            box.offset = new Vector2(0, 0.2f);
            box.size = new Vector2(2.3f, 0.4f);
        }
        else if(id == 2){
            box.offset = new Vector2(0, 0.15f);
            box.size = new Vector2(0.84f, 0.3f);
        }
        else{
            box.offset = new Vector2(0, 0.25f);
            box.size = new Vector2(0.9f, 0.5f);
        }

        RadomPosition();
    }

    public void InitObstacle0(){
        id = 0;
        spriteRenderer.sprite = null;
        spriteRenderer.sprite = sprite[id];
        box.offset = new Vector2(0, 0.2f);
        box.size = new Vector2(2.3f, 0.4f);
    }

    // public void 

    public void RadomPosition()
    {
        float x, y;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector3 playerPos = GameManager.gameManager.playerTrans.position;

        if(id != 0){
            int random = UnityEngine.Random.Range(0, 3);
            if(random == 0){
                x = Random.Range(/* playerPos.x */ - (worldPoint.x + 2), /* playerPos.x */ - 2);
                y = Random.Range(/* playerPos.y  */- (worldPoint.y + 2), /* playerPos.y  */+ 3);
            }
            else if(random == 1){
                x = Random.Range(/* playerPos.x */ + 2, /* playerPos.x */ + (worldPoint.x + 2));
                y = Random.Range(/* playerPos.y  */- (worldPoint.y + 2), /* playerPos.y  */+ 3);
            }
            else{
                x = Random.Range(/* playerPos.x */ - 2, /* playerPos.x */ + 2);
                y = Random.Range(/* playerPos.y  */- (worldPoint.y + 2), /* playerPos.y  */- 2);
            }

            x = Mathf.Ceil(x);
            y = Mathf.Ceil(y);
            // Debug.Log(playerPos + " -- " + worldPoint);
            transform.position = new Vector3(x,y,4-id);

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && id == 0 && !isPass){
            DOTween.Kill("PlayerMove");
            isPass = true;
            if(other.gameObject.GetComponent<Player>().isEatingApple){
                GameManager.gameManager.NextLevel();
                spriteRenderer.color = Color.blue;
            }
            else{
                GameManager.gameManager.GameLosing("Missing the Apple");
            }
        }
        if(other.tag == "Player" && id != 0){
            if(!GameManager.gameManager.isStart){
                DOTween.Kill("PlayerMove");
                GameManager.gameManager.GameLosing("Collided with Obstacle!");
            }
            else{
                Destroy(gameObject);
            }
        }
        if((other.tag == "Gate" || other.tag == "Apple Ref") && id != 0){
            Destroy(gameObject);
            GameManager.gameManager.SpawnObstacleE();
        }
    }
}
