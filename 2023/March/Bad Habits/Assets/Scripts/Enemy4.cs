using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy4 : MonoBehaviour
{
    public Transform mainPlayer;
    public int stateSize;
    public Color orgColor;
    public bool canEat;
    public Bullet4 bullet4Prefab;
    public List<Bullet4> listOfBullet4 = new List<Bullet4>();
    public float yScale, zScale, xScale;
    void Awake(){
        mainPlayer = GameObject.Find("Player").transform;
        canEat = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootBullet4", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(!canEat && !GameManager.gameManager.isStart){
            gameObject.GetComponent<SpriteRenderer>().color = orgColor;
        }
    }

    void FixedUpdate(){
        RotateToPlayer();
    }

    public void InitEnemy4(){
        stateSize = Random.Range(2, 4);
        transform.localScale += new Vector3(stateSize * 0.1f, stateSize * 0.1f, 0);
        yScale = transform.localScale.y;
        xScale = transform.localScale.x;
        zScale = transform.localScale.z;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Match Box" || other.tag == "Beer" || other.tag == "Enemy3" || other.tag == "Enemy4"){
            Destroy(gameObject);
            GameManager.gameManager.SpawnEnemy4();
        }

    }

    public void SetCanEat(){
        canEat = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void ShootBullet4(){
        if(!GameManager.gameManager.isStart){
            RotateToPlayer();
            CancelInvoke("ShootBullet4");
            var bullet4 = Instantiate(bullet4Prefab, transform.position, Quaternion.identity);
            listOfBullet4.Add(bullet4);
            Invoke("ShootBullet4", 20);
        }
    }

    public void ClearBullet4ShootList(){
        CancelInvoke("ShootBullet4");
        for(int i = 0; i < listOfBullet4.Count; i++){
            if(listOfBullet4[i] != null){
                Destroy(listOfBullet4[i].gameObject);
            }
        }
        listOfBullet4.Clear();
    }

    public void RotateToPlayer(){
        var angle = Mathf.Atan2(mainPlayer.position.y - transform.position.y, mainPlayer.position.x - transform.position.x) * Mathf.Rad2Deg;
        float y = (angle > 90 || angle < -90) ? -yScale : yScale;
        transform.localScale = new Vector3(xScale, y, zScale);
        transform.eulerAngles = new Vector3(0, 0, angle);

    }

}
