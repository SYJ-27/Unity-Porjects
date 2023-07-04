using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public float speed, scaleNum;
    public int stateSize;
    public Apple yourApple;
    public bool canDrag;
    public GameObject explosion;
    public int appleNum;
    void Awake(){
        appleNum = 0;
        canDrag = true;
        // gameObject.GetComponent<CircleCollider2D>().enabled = false;
        stateSize = 1;
        speed = 4;
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("EnableDraging", 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && EventSystem.current.currentSelectedGameObject == null)
        {
            if(canDrag){
                Move();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Apple"){
            if(!GameManager.gameManager.isStart){
                ResizePlayer();
                appleNum++;
                yourApple.RandomApplePosition();
            }
            else{
                yourApple.RandomApplePosition();
            }
        }
        if(other.tag == "Beer"){
            if(!GameManager.gameManager.isStart){
                Beer tempBeer = other.gameObject.GetComponent<Beer>();
                if(tempBeer.stateSize > stateSize){
                    PlayerExplore();
                    GameManager.gameManager.GameLose();
                }
                else{
                    GameManager.gameManager.countEnemy++;
                    ResizePlayer();
                    Destroy(other.gameObject);
                }
            }
            else{
                Destroy(other.gameObject);
                GameManager.gameManager.SpawnBeer();
            }
        }
        if(other.tag == "Match Box"){
            if(!GameManager.gameManager.isStart){
                MatchBox tempMatchBox = other.gameObject.GetComponent<MatchBox>();
                if(tempMatchBox.stateSize > stateSize){
                    PlayerExplore();
                    GameManager.gameManager.GameLose();
                }
                else{
                    tempMatchBox.ClearMatchShootList();
                    GameManager.gameManager.countEnemy++;
                    ResizePlayer();
                    Destroy(other.gameObject);
                }
            }
            else{
                Destroy(other.gameObject);
                GameManager.gameManager.SpawnMatchBox();
            }
        }
        if(other.tag == "Match"){
            PlayerExplore();
            GameManager.gameManager.GameLose();
        }
        if(other.tag == "Enemy3"){
            if(!GameManager.gameManager.isStart){
                Enemy3 tempEnemy3 = other.gameObject.GetComponent<Enemy3>();
                if(tempEnemy3.stateSize > stateSize){
                    PlayerExplore();
                    GameManager.gameManager.GameLose();
                }
                else{
                    tempEnemy3.ClearBullet3ShootList();
                    GameManager.gameManager.countEnemy++;
                    ResizePlayer();
                    Destroy(other.gameObject);
                }
            }
            else{
                Destroy(other.gameObject);
                GameManager.gameManager.SpawnEnemy3();
            }
        }
        if(other.tag == "Enemy4"){
            if(!GameManager.gameManager.isStart){
                Enemy4 tempEnemy4 = other.gameObject.GetComponent<Enemy4>();
                if(tempEnemy4.stateSize > stateSize){
                    PlayerExplore();
                    GameManager.gameManager.GameLose();
                }
                else{
                    tempEnemy4.ClearBullet4ShootList();
                    GameManager.gameManager.countEnemy++;
                    ResizePlayer();
                    Destroy(other.gameObject);
                }
            }
            else{
                Destroy(other.gameObject);
                GameManager.gameManager.SpawnEnemy4();
            }
        }
    }

    private void ResizePlayer(){
        stateSize++;
        transform.localScale += new Vector3(scaleNum, scaleNum, 0);
    }

    public void ResetPlayer(){
        stateSize =1;
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void Move(){
        GameManager.gameManager.isStart = false;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePos, speed * Time.deltaTime);
    }

    public void EnableDraging(){
        canDrag = true;
    }

    public void DisableDraging(){
        canDrag = false;
        Invoke("EnableDraging", 1f);
    }

    public void PlayerExplore(){
        var eplosionPlayer = Instantiate(explosion, transform.position, Quaternion.identity);
        eplosionPlayer.transform.localScale += new Vector3(stateSize * 0.1f, stateSize * 0.1f, 0);
        Destroy(eplosionPlayer, 0.3f);
    }

}
