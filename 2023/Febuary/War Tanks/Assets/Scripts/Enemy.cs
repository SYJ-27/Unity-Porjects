using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int colorID;
    public List<Sprite> typeOfTank;
    public List<GameObject> tankExplosions;
    public void InitEnemy(){
        colorID = Random.Range(0,2);
        gameObject.GetComponent<SpriteRenderer>().sprite = typeOfTank[colorID];
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 13);
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.gameManager.isLose)
            transform.Translate(-Time.deltaTime * Random.Range(2f,2.5f), 0, 0);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Bullet"){
            if(other.gameObject.GetComponent<Bullet>().colorID == colorID){
                var explosion = Instantiate(tankExplosions[colorID], transform.position, Quaternion.identity);
                explosion.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
                Destroy(explosion, 1);
            }
        }
    }
}
