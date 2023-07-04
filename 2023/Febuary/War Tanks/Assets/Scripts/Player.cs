using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public static Player mainPlayer;
    public Transform headGun;
    public Bullet bulletPrefabs;
    
    
    public GameObject tankExplosion, headGunExplosion;

    private float orgX;

    // public int shootedBullet;
    // public bool canShoot;
    // public List<GameObject> bulletRemains;
    void Awake(){
        mainPlayer = this;
        // yourLife = 3;
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        transform.position = new Vector3(-worldPosition.x + 2.3f, transform.position.y, 0);
        orgX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Enemy"){
            GameManager.gameManager.isLose = true;
            gameObject.SetActive(false);
            var explosion = Instantiate(tankExplosion, transform.position, Quaternion.identity);
            explosion.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            Destroy(explosion, 1);
            Invoke("GameOver", 1);
        }
    }

    void GameOver(){
        GameManager.gameManager.GameLose();
    }

    public void ShootRedBuller(){
        var bulletShooted = Instantiate(bulletPrefabs, new Vector3(headGun.position.x, headGun.position.y, 0), Quaternion.identity);
        bulletShooted.InitBullet(0);
        var explosion = Instantiate(headGunExplosion, headGun.position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        Destroy(explosion, 1);
        ShakeTank();
    }

    public void ShootBlueBuller(){
        var bulletShooted = Instantiate(bulletPrefabs, new Vector3(headGun.position.x, headGun.position.y, 0), Quaternion.identity);
        bulletShooted.InitBullet(1);
        var explosion = Instantiate(headGunExplosion, headGun.position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        Destroy(explosion, 1);
        ShakeTank();
    }

    

    void ShakeTank(){
        transform.DOMoveX(transform.position.x - 1, 0.1f).OnComplete(() =>{
            transform.DOMoveX(orgX, 0.1f);
        });
    }

}
