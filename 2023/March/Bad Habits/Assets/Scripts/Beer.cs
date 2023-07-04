using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Beer : MonoBehaviour
{
    public Color orgColor;
    public bool canEat;
    public int stateSize;
    void Awake(){
        canEat = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!canEat && !GameManager.gameManager.isStart){
            gameObject.GetComponent<SpriteRenderer>().color = orgColor;
        }
    }

    public void InitBeer(){
        stateSize = Random.Range(2, 6);
        transform.localScale += new Vector3(stateSize * 0.1f, stateSize * 0.1f, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Beer"){
            Destroy(other.gameObject);
            GameManager.gameManager.SpawnBeer();
        }
    }

    public void SetCanEat(){
        canEat = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        ScaleUpDown();
    }

    public void ScaleUpDown(){
        transform.DOScale(0.65f + stateSize*0.1f + 0.1f, 0.5f).OnComplete(() =>{
            transform.DOScale(0.65f + stateSize*0.1f, 0.5f).OnComplete(() =>{
                ScaleUpDown();
            });
        });
    }

}
