using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour
{
    [SerializeField]
    private float minY, maxY;
    int carSide;
    float xOrg;
    public GameObject carExplosion;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        // Debug.Log(worldPosition.x);
        carSide = Random.Range(0,2);
        if(carSide == 0){
            transform.position = new Vector3(-worldPosition.x + 1.5f, minY, 0);
        }
        else{
            transform.position = new Vector3(-worldPosition.x + 1.5f, maxY, 0);
        }
        xOrg = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GameManager.gameManager.isLose){
            transform.DOMoveX(transform.position.x + Time.fixedDeltaTime/2 * 1.3f, Time.fixedDeltaTime/2).OnComplete(() =>{
                transform.DOMoveX(xOrg, Time.fixedDeltaTime/2);
            }
            );
        }
    }

    public void Move(){
        carSide = 1 - carSide;
        if(carSide == 0){
            transform.position = new Vector3(xOrg, minY, 0);
        }
        else{
            transform.position = new Vector3(xOrg, maxY, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Obstacle"){
            GameManager.gameManager.isLose = true;
            gameObject.SetActive(false);
            var explose = Instantiate(carExplosion, transform.position, Quaternion.identity);
            Destroy(explose,0.95f);
        }
    }


}
