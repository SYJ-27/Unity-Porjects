using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLight : MonoBehaviour
{
    public int direction, lifeCount;
    public Bullet2 prefabBullet2;
    List<Bullet2> listBullet2s = new List<Bullet2>();

    public GameObject explosion, explosionPos;

    void Awake(){
        lifeCount = 2;
    }
    // Start is called before the first frame update
    void Start()
    {

        direction = Random.Range(0,2);
        if(direction == 0){
            direction = -1;
        }
        transform.localScale = new Vector3(direction, 1, 1);
        InvokeRepeating("Shooting", 2, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,-50f * direction * Time.deltaTime);
    }

    void Shooting(){
        var bullet2T = Instantiate(prefabBullet2, Vector3.zero, transform.localRotation);
        bullet2T.InitBullet(direction);
        listBullet2s.Add(bullet2T);
    }

    public void DestroyAllBullet2(){
        for(int i = 0; i < listBullet2s.Count; i++){
            if(listBullet2s[i] != null){
                Destroy(listBullet2s[i].gameObject);
            }
        }
        listBullet2s.Clear();
    }

    // void OnCollisionEnter2D(Collision2D other){
    //     if(other.gameObject.tag == "Bullet" || other.gameObject.tag == "Player"){
    //         if(other.gameObject.tag == "Bullet"){
    //             lifeCount -= other.gameObject.GetComponent<Bullet>().damage;
    //         }
    //         else{
    //             lifeCount --;
    //         }
    //         if(lifeCount <= 0){
    //             GameManager.gameManager.UpdateClearScore();
    //             Destroy(Instantiate(explosion, explosionPos.transform.position, Quaternion.identity), 0.3f);
    //             Destroy(gameObject);
    //         }
    //     }
    // }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Bullet" || other.tag == "Player Ref"){
            if(other.tag == "Bullet"){
                lifeCount -= other.GetComponent<Bullet>().damage;
            }
            else{
                lifeCount --;
            }
            if(lifeCount <= 0){
                GameManager.gameManager.UpdateClearScore();
                Destroy(Instantiate(explosion, explosionPos.transform.position, Quaternion.identity), 0.3f);
                Destroy(gameObject);
            }
        }
    }
}
