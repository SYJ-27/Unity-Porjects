using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public GameObject planetExplore;
    public int id;
    public List<Sprite> listSpritePlanet;
    public float speed = 3;
    public int life;
    bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.gameManager.score < 13){
            speed = Random.Range(3f, 4f);
        }
        else{
            speed = Random.Range(3.5f, 5f);
        }
        canMove = true;
        SetIdPlanet(Random.Range(0, listSpritePlanet.Count));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GameManager.gameManager.isStart && !GameManager.gameManager.isPause && canMove){
            // PlayerAbsorbing();
            // canMove = true;
            Moving();
        }
    }

    void Moving(){
        transform.Translate(-transform.right * speed * Time.deltaTime);
    }

    void SetIdPlanet(int idPlanet){
        id = idPlanet;
        GetComponent<SpriteRenderer>().sprite = listSpritePlanet[id];
        life = id + 3;
    }

    void DestroyPlanet(){
        Destroy(Instantiate(planetExplore, transform.position, Quaternion.identity), 0.3f);
        Destroy(gameObject);
    }

 
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bullet"){
            int dameBullet = other.gameObject.GetComponent<Bullet>().dameBullet;
            life -= dameBullet;
            GameManager.gameManager.UpdatePlanetLifeUI(transform.position, dameBullet);
            canMove = false;
            Invoke("EnableMoving", 0.2f);
            if(life <= 0){
                GameManager.gameManager.UpdateScoreUI(transform.position);
                DestroyPlanet();
            }
            Destroy(other.gameObject);
        }
        if(other.tag == "Bomb"){
            GameManager.gameManager.UpdateScoreUI(transform.position);
            DestroyPlanet();
            Destroy(other.gameObject);
        }
        if(other.tag == "Player"){
            GameManager.gameManager.UpdateScoreUI(transform.position);
            DestroyPlanet();
            // Destroy(gameObject);
        }
    }

    void EnableMoving(){
        canMove = true;
    }

}
