using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Flower : MonoBehaviour
{
    Transform earthTransform;
    Seed mainSeed;
    public int idFlower;
    public Transform flowerStatus;
    void Awake(){
        flowerStatus = GameObject.Find($"Flower {idFlower + 1}").transform;
        transform.localScale = Vector3.zero;
        earthTransform = GameObject.Find("Earth").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var angleTemp = Mathf.Atan2(earthTransform.position.y - transform.position.y, earthTransform.position.x - transform.position.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.AngleAxis(angleTemp, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !other.gameObject.GetComponent<Player>().isJumping){
            if(mainSeed != null){
                Destroy(mainSeed.gameObject);
            }
            GetComponent<CircleCollider2D>().enabled = false;

            transform.DOMove(flowerStatus.position, 0.5f).OnComplete(() =>{
                GameManager.gameManager.SetScoreFlower(idFlower);
                Destroy(gameObject);
            });
        }
    }

    public void GrowFlower(Seed yourSeed){
        mainSeed = yourSeed;
        transform.DOScale(new Vector3(1,1,1), 0.15f).OnComplete(() =>{
            yourSeed.canHarvest = true;
            yourSeed.firstCollider.enabled = true;
        });
    }
}
