using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameBonus gameBonus;

    public Transform attackMode, defenseMode;
    public Image attackImage;
    public float attackTime, timeToAttack, speed, immortalTime;
    public int health, score, healthByEnemy, botNumber;
    bool isStartDragging;

    public Shield prefabShield;
    Shield playerShield;

    public List<GameObject> listBotPlayer;

    void Awake(){
        botNumber = 0;
        healthByEnemy = 0;
        immortalTime = 5;
        health = 100;
        speed = 3;
        timeToAttack = 10;
        attackTime = 0;
        score = 0;
        isStartDragging = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetBot();
    }

    void FixedUpdate(){
        if(!isStartDragging){
            CountAttackTime();
        }
        if (Input.GetMouseButton(0) && EventSystem.current.currentSelectedGameObject == null && !GameManager.gameManager.isBonusTime)
        {
            if(isStartDragging){
                isStartDragging = false;
                GameManager.gameManager.SpawnEnemies();
            }
            Moving();
        }
        attackMode.position = defenseMode.position;
    }

    void Moving(){
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePos, speed * Time.deltaTime);
    }

    void CountAttackTime(){
        if(!GameManager.gameManager.isAttackTime && !GameManager.gameManager.isBonusTime){
            attackTime += 0.02f;
        }
        if(attackTime <= timeToAttack){
            attackImage.fillAmount = attackTime/timeToAttack;
        }
        else{
            attackTime = timeToAttack;
            GameManager.gameManager.isAttackTime = true;
            GameManager.gameManager.StopSpawnEnemies();
            Invoke("NonAttackTime", immortalTime);
        }
    }

    void NonAttackTime(){
        GameManager.gameManager.isAttackTime = false;
        if(GameManager.gameManager.IsClearEnemies() || score % 13 == 0){
            if(score % 13 == 0){
                GameManager.gameManager.DestroyAllEnemy();
            }
            GameManager.gameManager.isBonusTime = true;
            transform.position = Vector3.zero;
            GameManager.gameManager.StopSpawnEnemies();
            gameBonus.GetBonusScene();
            isStartDragging = true;
        }
        else{
            GameManager.gameManager.SpawnEnemies();
        }
        attackTime = 0;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Enemy"){
            if(!GameManager.gameManager.isAttackTime){
                health -= 5;
                if(health <= 0){
                    health = 0;
                    GameManager.gameManager.GameLosing();
                }
            }
            else{
                health += healthByEnemy;
                if(health >= 100){
                    health = 100;
                }
            }
        }

        if(other.tag == "Bomb" && !GameManager.gameManager.isAttackTime){
            health -= 5;
            if(health <= 0){
                health = 0;
                GameManager.gameManager.GameLosing();
            }
        }

        if(other.tag == "Laser" && !GameManager.gameManager.isAttackTime){
            health -= 5;
            if(health <= 0){
                health = 0;
                GameManager.gameManager.GameLosing();
            }
        }

        if(other.tag == "Safe Mode"){
            Destroy(other.gameObject);
        }
    }

    public void ResetBonus(){
        healthByEnemy = 0;
        if(botNumber == 3){
            botNumber = 0;
        }
    }

    public void GetShield(){
        if(playerShield == null){
            playerShield = Instantiate(prefabShield, transform.position, Quaternion.identity, transform);
        }
    }

    void GetBot(){
        for(int i = 0; i < listBotPlayer.Count; i++){
            if(i < botNumber){
                listBotPlayer[i].SetActive(true);
            }
            else{
                listBotPlayer[i].SetActive(false);
            }
            listBotPlayer[i].transform.Rotate(0, 0, 80 * Time.deltaTime);
        }
    }

}
