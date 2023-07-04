using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy3 : MonoBehaviour
{
    public Transform mainPlayer;

    public int stateSize;
    public Color orgColor;
    public bool canEat;
    public Bullet3 bullet3Prefab;
    public List<Bullet3> listOfBullet3 = new List<Bullet3>();
    public float yScale, xScale, zScale;

    void Awake(){
        mainPlayer = GameObject.Find("Player").transform;
        canEat = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootBullet3", 2f,2f);
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

    public void InitEnemy3(){
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
            GameManager.gameManager.SpawnEnemy3();
        }

    }

    public void SetCanEat(){
        canEat = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void ShootBullet3(){
        if(!GameManager.gameManager.isStart){
            var bullet3 = Instantiate(bullet3Prefab, transform.position, Quaternion.identity);
            listOfBullet3.Add(bullet3);
        }
    }

    public void ClearBullet3ShootList(){
        CancelInvoke("ShootBullet3");
        for(int i = 0; i < listOfBullet3.Count; i++){
            if(listOfBullet3[i] != null){
                Destroy(listOfBullet3[i].gameObject);
            }
        }
        listOfBullet3.Clear();
    }

    public void RotateToPlayer(){
        var angle = Mathf.Atan2(mainPlayer.position.y - transform.position.y, mainPlayer.position.x - transform.position.x) * Mathf.Rad2Deg;
        float y = angle > 90 || angle < -90 ? -yScale : yScale;
        transform.localScale = new Vector3(yScale, y, 1);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
