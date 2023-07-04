using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FoodHarvest : MonoBehaviour
{
    public float timeGrowUp;
    public int indexSoil, foodType;
    public GameObject foodExplore;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f).OnComplete(() =>{
            transform.DOScale(new Vector3(1, 1, 1), 0.3f).OnComplete(() =>{
                return;
            });
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown(){
        GameManager.gameManager.ResetSoil(indexSoil);
        transform.DOMove(new Vector3(6, -3, 0), 1f);
        // Destroy(gameObject);
    }

    public void InitIndexSoil(int idx){
        indexSoil = idx;
    }

    public void ExplosionFood(){
        Destroy(gameObject);
        Destroy(Instantiate(foodExplore, transform.position, Quaternion.identity), 0.3f);
    }

}
