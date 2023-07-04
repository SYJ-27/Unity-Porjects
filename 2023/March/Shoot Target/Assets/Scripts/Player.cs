using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject targetObject;

    public Bullet bulletPlayer;
    public Laze bulletLaze;
    public List<Bullet> listBulletPlayers;
    public List<Laze> listBulletLazes;

    public List<Color> listColorBullets;

    public Vector2 worldPoint;

    public int lifePlayer, bonusType;

    public bool isCountingChangeTime;
    public float changeTime;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        changeTime = 10;
        isCountingChangeTime = false;
        bonusType = -1;
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        lifePlayer = 3;
        // InvokeRepeating("Shooting", 0.1f, 0.1f);
        Invoke("Shooting", 0.1f);
    }

    void Update(){
        if(isCountingChangeTime){
            if(changeTime >= 0){
                changeTime -= Time.deltaTime;
            }
            else{
                changeTime = 0;
            }
        }
        LimitPositionPlayer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(-targetObject.transform.position.x, -targetObject.transform.position.y, 0);
        // Shooting();
    }

    void Shooting(){
        if(!GameManager.gameManager.isLose && !GameManager.gameManager.isPause){
            if(bonusType == -1){
                // CancelInvoke("Shooting");
                var bulletSpawned = Instantiate(bulletPlayer, transform.position, Quaternion.identity);
                bulletSpawned.Initbullet(0, listColorBullets[0]);
                listBulletPlayers.Add(bulletSpawned);
                Invoke("Shooting", 0.1f);
            }
            else if(bonusType == 1){
                // CancelInvoke("Shooting");
                for(int i = 0; i < 3; i++){
                    var bulletSpawned = Instantiate(bulletPlayer, transform.position, Quaternion.identity);
                    bulletSpawned.Initbullet(-5 + i * 5, listColorBullets[1]);
                    listBulletPlayers.Add(bulletSpawned);
                }
                Invoke("Shooting", 0.1f);
            }
            else if(bonusType == 2){
                // CancelInvoke("Shooting");
                for(int i = 0; i < 2; i++){
                    var bulletSpawned = Instantiate(bulletPlayer, transform.position, Quaternion.identity);
                    bulletSpawned.Initbullet(180 - i * 180, listColorBullets[2]);
                    listBulletPlayers.Add(bulletSpawned);
                }
                Invoke("Shooting", 0.1f);
            }
            else if(bonusType == 3){
                // CancelInvoke("Shooting");
                var bulletSpawned = Instantiate(bulletLaze, transform.position, Quaternion.identity, transform);
                bulletSpawned.Initbullet(0);
                listBulletLazes.Add(bulletSpawned);
                Invoke("Shooting", 1.5f);
            }
        }
    }

    void LimitPositionPlayer(){
        float x = Mathf.Clamp(transform.position.x, -worldPoint.x, worldPoint.x);
        float y = Mathf.Clamp(transform.position.y, -worldPoint.y, worldPoint.y);
        transform.position = new Vector3(x,y,0);
    }

    public void MinusLife(){
        lifePlayer--;
    }

    public void AddLife(){
        if(lifePlayer + 1 <= 3){
            lifePlayer++;
        }
    }

    public void CancelResetBullet(){
        isCountingChangeTime = false;
        CancelInvoke("BackBulletType");
    }

    public void ResetBullet(){
        isCountingChangeTime = true;
        Invoke("BackBulletType", changeTime);
    }

    void BackBulletType(){
        bonusType = -1;
        GameManager.gameManager.enemyTimeSpawn = 2;
        GameManager.gameManager.ResetEnemyTimeSpawn();
    }

    public void DestroyPlayer(){
        Destroy(Instantiate(explosion, transform.position, Quaternion.identity),0.3f);
        gameObject.SetActive(false);

    }
    
    public void EnableShooting(){
        Invoke("Shooting", 0.1f);
    }

}
