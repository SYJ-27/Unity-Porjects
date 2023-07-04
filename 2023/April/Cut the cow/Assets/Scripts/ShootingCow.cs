using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingCow : MonoBehaviour
{
    public Carrot prefabCarrot;
    List<Carrot> listCarrots;
    public int lifeShootingCow, directionHorizontal;
    void Awake(){
        listCarrots = new List<Carrot>();
        lifeShootingCow = 1;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shooting", 1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    public void InitShootingCow(int negative){
        directionHorizontal = negative;
    }

    void Moving(){
        transform.Translate(transform.right * directionHorizontal * 3 * Time.deltaTime);
    }

    void Shooting(){
        listCarrots.Add(Instantiate(prefabCarrot, transform.position, Quaternion.identity));
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Wall Right" || other.tag == "Wall Left"){
            Destroy(gameObject);
        }
        if(other.tag == "Bullet"){
            lifeShootingCow--;
            if(lifeShootingCow <= 0){
                GameManager.gameManager.score += 3;
                Destroy(gameObject);
            }
            Destroy(other.gameObject);
        }
        if(other.tag == "Player"){
            GameManager.gameManager.score++;
            Destroy(gameObject);
        }
    }
}
