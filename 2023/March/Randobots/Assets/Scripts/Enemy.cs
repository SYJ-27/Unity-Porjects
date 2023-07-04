using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public Transform mainPlayer, targetEnemy, headGun, LifeSpquare, healthBar;
    public Vector3 pos;
    public bool shootingState = true;
    public List<GameObject> enemyStates;
    public Bullet bulletPrefab;
    public List<Bullet> listOfBullet = new List<Bullet>();

    public float angle;
    public float speedEnemy;
    public float life, maxLife = 60, breakNumber;
    public Image healthBarImg;

    void Awake()
    {
        life = maxLife;
        breakNumber = 0;
        ChangeState();
    }
    // Start is called before the first frame update
    void Start()
    {
        float changeTime = GameManager.gameManager.changeTime;
        InvokeRepeating("ChangeState", changeTime, changeTime);
        InvokeRepeating("Shooting", 1.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(shootingState){
            pos = mainPlayer.position;
        }
        else{
            pos = targetEnemy.position;
        }
        if(life <= 0){
            ShowTimeLife();
            DestroyAllBullet();
            GameManager.gameManager.GameLosing();
        }
    }

    void FixedUpdate()
    {
        if(shootingState){
            angle = Mathf.Atan2(pos.y - transform.position.y, pos.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else{
            angle = Mathf.Atan2(pos.y - transform.position.y, pos.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.position = Vector2.MoveTowards(transform.position, targetEnemy.position, speedEnemy * Time.deltaTime);
        }
        LifeSpquare.position = new Vector3(transform.position.x, transform.position.y + 1f, 0);
        healthBar.position = LifeSpquare.position;
        ShowTimeLife();
    }

    private void ChangeState(){
        DestroyAllBullet();
        if(shootingState){
            shootingState = false;
            enemyStates[0].SetActive(false);
            enemyStates[1].SetActive(true);
        }
        else{
            shootingState = true;
            enemyStates[0].SetActive(true);
            enemyStates[1].SetActive(false);
        }
    }

    void Shooting(){
        if(!GameManager.gameManager.isLose){
            if(shootingState){
                var bullet = Instantiate(bulletPrefab, headGun.position, Quaternion.identity);
                bullet.InitBullet(angle);
                listOfBullet.Add(bullet);
            }
        }
        else{
            CancelInvoke("Shooting");
            CancelInvoke("ChangeState");
        }
    }

    void DestroyAllBullet(){
        for(int i = 0; i < listOfBullet.Count; i++){
            if(listOfBullet[i] != null){
                Destroy(listOfBullet[i].gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Bullet" ){
            life--;
        }
    }

    private void ShowTimeLife(){
        if(life >= 0){
            healthBarImg.fillAmount = life / maxLife;
        }
    }

}
