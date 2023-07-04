using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class SeedOfFood : MonoBehaviour
{
    public FoodHarvest food;
    public bool isPlanted, isGrowing;
    public int indexSoil;
    public GameObject seedPlantingHolder;
    public GameObject seedOfFoodExplore;
    void Awake(){
        seedPlantingHolder = GameObject.Find("Seed Planting");
        isPlanted = false;
        isGrowing = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlanted && !isGrowing){
            isGrowing = true;
            Invoke("GrowUp", food.timeGrowUp);
        }
    }

    public void SetPlanted(bool state, int idx){
        isPlanted = state;
        indexSoil = idx;
    }

    public void GrowUp(){
        // transform.DOScale(new Vector3(2f, 2f, 2f), 0.1f).OnComplete(() =>{
            var yourFood = Instantiate(food, transform.position, Quaternion.identity, seedPlantingHolder.transform);
            yourFood.InitIndexSoil(indexSoil);
            Destroy(gameObject);
        // });
        
    }

    void OnMouseDown(){
        CancelInvoke("GrowUp");
        GameManager.gameManager.ResetSoil(indexSoil);
        transform.DOMove(new Vector3(6, -3, 0), 1f);
        // Destroy(gameObject);
    }

    public void ExplosionSeed(){
        Destroy(gameObject);
        Destroy(Instantiate(seedOfFoodExplore, transform.position, Quaternion.identity), 0.3f);
    }

}
