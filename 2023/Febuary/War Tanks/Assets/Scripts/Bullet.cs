using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int colorID;
    public List<Sprite> typeOfBullet;
    public List<GameObject> bullerExplosion;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * 8, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy"){
            if(other.gameObject.GetComponent<Enemy>().colorID == colorID){
                GameManager.gameManager.UpdateScoreUI();
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            else{
                var explosion = Instantiate(bullerExplosion[1 - colorID], transform.position, Quaternion.identity);
                Destroy(gameObject);
                explosion.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
                Destroy(explosion, 1);
                GameManager.gameManager.LoseHeart();
            }
        }
    }

    public void InitBullet(int color){
        colorID = color;
        gameObject.GetComponent<SpriteRenderer>().sprite = typeOfBullet[colorID];
    }

}
