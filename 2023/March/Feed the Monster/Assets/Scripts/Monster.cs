using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int life;
    public List<Order> listOfOrders;
    public List<int> listOfNumberOrders;
    public List<int> listOfNumberTemp;
    public List<int> listOfNumberOver;
    void Awake(){
        life = 3;
    }
    // Start is called before the first frame update
    void Start()
    {
        GenerateOrder();
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckEmptyOrder()){
            UpdateOverOrder();
            GenerateOrder();
        }
        if(life == 0){
            UpdateOverOrder();
            GameManager.gameManager.GameLosing();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Food"){
            var food = other.gameObject.GetComponent<FoodHarvest>();
            if(listOfNumberOrders[food.foodType] - 1 < 0){
                life--;
                food.ExplosionFood();
            }
            else{
                listOfNumberOrders[food.foodType] -= 1;
                listOfOrders[food.foodType].InitNumberText(listOfNumberOrders[food.foodType]);
                Destroy(other.gameObject);
            }
            
        }
        else{
            life--;
            var seed = other.gameObject.GetComponent<SeedOfFood>();
            seed.ExplosionSeed();
        }
    }

    public void GenerateOrder(){
        GameManager.gameManager.UpdateScoreUI();
        if(listOfNumberOver.Count == 0){
            for(int i = 0; i < listOfOrders.Count; i++){
                listOfNumberOver.Add(0);
            }
        }
        listOfNumberOrders.Clear();
        listOfNumberTemp.Clear();
        for(int i = 0; i < listOfOrders.Count; i++){
            listOfNumberOrders.Add(Random.Range(0,5));
            listOfNumberTemp.Add(listOfNumberOrders[i]);
            listOfOrders[i].InitNumberText(listOfNumberOrders[i]);
        }
    }

    public bool CheckEmptyOrder(){
        for(int i = 0; i < listOfNumberOrders.Count; i++){
            if(listOfNumberOrders[i] > 0){
                return false;
            }
        }
        return true;
    }
    
    private void UpdateOverOrder(){
        for(int i = 0; i < listOfNumberTemp.Count; i++){
            listOfNumberOver[i] += listOfNumberTemp[i] - listOfNumberOrders[i];
        }
    }

}
