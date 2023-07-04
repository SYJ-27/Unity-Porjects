using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public List<GameObject> playerStates;
    private Rigidbody2D playerRigidbody;
    public int indexState;
    public bool hasShield;
    public float xVeloc, yVeloc;

    public Transform healthBar;
    public Image healthBarUI;
    public float minHealth, maxHealth;

    public int score;
    public float time;

    void Awake(){
        time = 0;
        score = 0;
        minHealth = maxHealth;
        hasShield = false;
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        GetRandomState();
        InitVeloc();
        AddingForce();
    }

    void Update()
    {
        if(!GameManager.gameManager.isLose){
            time += Time.deltaTime;
        }
        healthBar.position = new Vector3(transform.position.x, transform.position.y + 0.65f, transform.position.z);
        
        // LimitHumanPosition();
    }

    void FixedUpdate(){
        UpdateHealthBar();
        if(minHealth == 0){
            GameManager.gameManager.GameLosing(score, time);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Wall V"){
            WallVelocity(1);
            AddingForce();
        }
        else if(other.tag == "Wall H"){
            WallVelocity(-1);
            AddingForce();
        }
        if(other.tag == "Enemy" && !hasShield){
            Destroy(other.gameObject);
            minHealth -= 1;
        }
    }

    public void GetRandomState(){
        if(!GameManager.gameManager.isLose){
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            indexState = Random.Range(0, playerStates.Count);
            if(GameManager.gameManager.isStart){
                indexState = 0;
            }
            Instantiate(playerStates[indexState], new Vector3(Random.Range(-2,2), Random.Range(-2,2), 0), Quaternion.identity);
        }
    }

    public void SetHadShield(bool state){
        hasShield = state;
    }
    public void OffShield(){
        hasShield = false;
    }

    public void DisableSheild(){
        Invoke("OffShield", 0.5f);
    }

    private void InitVeloc(){
        int direction = Random.Range(0,2);
        if(direction == 0){
            direction = -1;
        }
        xVeloc = direction * Random.Range(2f, 3f);

        direction = Random.Range(0,2);
        if(direction == 0){
            direction = -1;
        }
        yVeloc = direction * Random.Range(2f, 3f);
        LimitVeloc();
    }

    public void AddingForce(){
        playerRigidbody.AddForce(new Vector2(xVeloc * 30, yVeloc * 30));
    }

    private void WallVelocity(int direction){
        xVeloc = direction * playerRigidbody.velocity.x * Random.Range(1,3);
        yVeloc = -direction * playerRigidbody.velocity.y * Random.Range(1,3);
        LimitVeloc();
    }

    public void LimitVeloc(){
        xVeloc = Mathf.Clamp(xVeloc, -3, 3);
        yVeloc = Mathf.Clamp(yVeloc, -3, 3);
        playerRigidbody.velocity = new Vector2(xVeloc, yVeloc);
    }

    void UpdateHealthBar(){
        if(minHealth >= 0){
            healthBarUI.fillAmount = minHealth/maxHealth;
        }
    }

}
