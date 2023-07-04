using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static bool pointerDown = false;
    
    public FixedJoystick mainJoystick;
    private Rigidbody2D playerRigidbody2D;
    Vector2 move;
    public bool shootingState = false, canShoot;
    public float playerSpeed, zAxis;


    public Transform LifeSpquare, healthBar, headGun;


    public List<GameObject> playerStates;
    public GameObject shootButton;
    public Bullet bulletPrefab;
    public List<Bullet> listOfBullet = new List<Bullet>();

    public float life, maxLife = 60, breakNumber;
    
    public Image healthBarImg;

    void Awake()
    {
        life = maxLife;
        breakNumber = 0;
        canShoot = true;
        ChangeState();
    }
    // Start is called before the first frame update
    void Start()
    {
        float changeTime = GameManager.gameManager.changeTime;
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        InvokeRepeating("ChangeState", changeTime, changeTime);
    }

    // Update is called once per frame
    void Update()
    {
        move.x = mainJoystick.Horizontal;
        move.y = mainJoystick.Vertical;
        if(life <= 0){
            ShowTimeLife();
            DestroyAllBullet();
            GameManager.gameManager.GameLosing();
        }
        if(GameManager.gameManager.isLose){
            DestroyAllBullet();

        }
    }

    void FixedUpdate()
    {
        if(pointerDown){
            playerRigidbody2D.velocity = Vector3.zero;
        }
        else if(!pointerDown && !shootingState){
            playerRigidbody2D.MovePosition(playerRigidbody2D.position + move * playerSpeed * Time.fixedDeltaTime);
        }
        PlayerRotating();
        ShowTimeLife();
    }

    private void PlayerRotating(){
        float hAxis = move.x;
        float vAxis = move.y;
        if(move.y == 0){
            zAxis = Mathf.Atan2(vAxis, hAxis) * Mathf.Rad2Deg;
        }
        else{
            zAxis = Mathf.Atan2(vAxis, hAxis) * Mathf.Rad2Deg - 180;
        }
        transform.eulerAngles = new Vector3(0, 0, zAxis);
        LifeSpquare.position = new Vector3(transform.position.x, transform.position.y + 1f, 0);
        healthBar.position = LifeSpquare.position;
    }

    private void ChangeState(){
        DestroyAllBullet();
        if(shootingState){
            shootingState = false;
            shootButton.SetActive(false);
            playerStates[0].SetActive(false);
            playerStates[1].SetActive(true);
        }
        else{
            shootingState = true;
            shootButton.SetActive(true);
            playerStates[0].SetActive(true);
            playerStates[1].SetActive(false);
        }
    }

    public void Shooting(){
        if(canShoot && shootingState && !GameManager.gameManager.isLose){
            // GameManager.gameManager.DestroyTile();
            var bullet = Instantiate(bulletPrefab, headGun.position, Quaternion.identity);
            bullet.InitBullet(zAxis);
            listOfBullet.Add(bullet);
            canShoot = false;
            Invoke("SetCanShoot", 0.5f);
        }
    }

    public void SetCanShoot(){
        canShoot = true;
    }

    void DestroyAllBullet(){
        for(int i = 0; i < listOfBullet.Count; i++){
            if(listOfBullet[i] != null){
                Destroy(listOfBullet[i].gameObject);
            }
        }
        listOfBullet.Clear();
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Bullet" ){
            Destroy(other.gameObject);
            life--;
        }
    }

    private void ShowTimeLife(){
        if(life >= 0){
            healthBarImg.fillAmount = life / maxLife;
        }
    }

}
