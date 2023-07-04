using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public Player mainPlayer;
    public Transform conveyor;
    public UiController uiController;
    public GameOver gameOver;
    
    public List<GameObject> prefabFoods;
    public List<GameObject> mainGameObjects;
    public List<GameObject> listFoods = new List<GameObject>();
    public Transform foodSpawner;
    public float foodStep;
    public int numCoin, numBanana,numFood;

    public bool isLose, canEat, canCatch;

    public List<int> foodListType = new List<int>(){};

    public List<Conveyor> listConveyors;

    // Start is called before the first frame update
    void Start()
    {
        isLose = false;
        canEat = false;
        canCatch = false;
        gameManager  = this;
        foodStep = foodSpawner.position.x / 6;
        InitFoodList();
        for(int i = 5; i > -1; i--){
            var foodVar = Instantiate(prefabFoods[foodListType[5 - i]], new Vector3(foodSpawner.position.x - i * foodStep, conveyor.position.y, foodSpawner.position.z), Quaternion.identity);
            listFoods.Add(foodVar);
        }
        numFood = 5;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveQuery(){
        MoveConveyor();
        canEat = true;
        canCatch = true;
        for(int i = 0; i < listFoods.Count; i++){
            if(listFoods[i] != null){
                if(foodListType[i] == 2 && listFoods[i].transform.position.x > -0.001f && listFoods[i].transform.position.x < 0.001f){
                    Debug.Log($"{listFoods[i].transform.position.x} - {i} Losing");
                    isLose = false;
                    Debug.Log(1);
                    GameLosing("You Miss the Coin!");
                    break;
                }
                if(listFoods[i].transform.position.x > -0.001f && listFoods[i].transform.position.x < 0.001f && i == listFoods.Count - 1){
                    Debug.Log(2 + " " + i);
                    GameLosing("");
                    break;
                }
                listFoods[i].transform.DOMoveX(listFoods[i].transform.position.x - foodStep, 0.1f);
            }
            else{
                if(i == listFoods.Count - 1){
                    GameLosing("");
                    break;
                }
            }
            
        }
        if(!isLose){
            SpawningFood();
        }
    }

    public void EatFood(){
        if(canEat ){
            canEat = false;
            for(int i = 0; i < listFoods.Count; i++){
                if(listFoods[i] != null && listFoods[i].transform.position.x < 0.001f && listFoods[i].transform.position.x > -0.001f){
                    mainPlayer.RightHandEat(listFoods[i]);
                    listFoods[i] = null;
                    if(foodListType[i] == 0){
                        mainPlayer.UpdateBanana();
                    }
                    else if(foodListType[i] == 2){
                        GameLosing("You Eat the Coin???");
                    }
                    else{
                        mainPlayer.UpdateElse();
                    }
                    break;
                }
            }
            Invoke("MoveQuery", 0.1f);
        }
    }

    public void CatchCoin(){
        if(canCatch){
            canCatch = false;
            int playerCoin = mainPlayer.countCoin;
            for(int i = 0; i < listFoods.Count; i++){
                if(listFoods[i] != null && listFoods[i].transform.position.x < 0.001f && listFoods[i].transform.position.x > -0.001f){
                    listFoods[i].GetComponent<BoxCollider2D>().enabled = false;
                    if(foodListType[i] == 2){
                        playerCoin++;
                        mainPlayer.UpdateCoin();
                    }
                    else{
                        mainPlayer.UpdateElse();
                    }
                    mainPlayer.RightHandCatch(listFoods[i]);
                    listFoods[i] = null;
                }
            }
            Invoke("MoveQuery", 0.1f);
        }

    }

    public void SpawningFood(){
        numFood++;
        if(listFoods.Count < foodListType.Count){
            var foodVar = Instantiate(prefabFoods[foodListType[numFood]], new Vector3(foodSpawner.position.x, conveyor.position.y, foodSpawner.position.z), Quaternion.identity);
            listFoods.Add(foodVar);
        }
    }

    private void InitFoodList(){
        int randomFoodNum = UnityEngine.Random.Range(20, 25);
        int randomCoinNum = UnityEngine.Random.Range(5, 7);
        for(int i = 0; i < randomFoodNum; i++){
            if(i < randomCoinNum){
                foodListType.Add(2);
                numCoin++;
            }
            else if(i > randomCoinNum - 1 && i < randomCoinNum + 3){
                foodListType.Add(0);
                numBanana++;
            }
            else{
                int ranX = UnityEngine.Random.Range(0, prefabFoods.Count);
                if(ranX == 2){
                    ranX = UnityEngine.Random.Range(0, 2);
                    if(ranX == 0){
                        ranX = 1;
                    }
                    else{
                        ranX = 3;
                    }
                }
                foodListType.Add(ranX);
                if(foodListType[i] == 0){
                    numBanana++;
                }
            }
        }
        var rnd = new System.Random();
        foodListType = foodListType.OrderBy(item => rnd.Next()).ToList();
        
    }

    public void GameLosing(string status){
        isLose = true;
        DisableGameObject();
        if(status == ""){
            if(mainPlayer.countBanana < numBanana){
                gameOver.GameOverScene("Not Enough Banana!", mainPlayer.countBanana, mainPlayer.countCoin, mainPlayer.countElse);
            }
            else{
                gameOver.GameOverScene("Win", mainPlayer.countBanana, mainPlayer.countCoin, mainPlayer.countElse);
            }
        }
        else{
            gameOver.GameOverScene(status, mainPlayer.countBanana, mainPlayer.countCoin, mainPlayer.countElse);
        }
        DestroyListFood();
    }

    private void DisableGameObject(){
        for(int i = 0; i < mainGameObjects.Count; i++){
            mainGameObjects[i].SetActive(false);
        }
    }

    private void DestroyListFood(){
        for(int i = 0; i < listFoods.Count; i++){
            if(listFoods[i] != null){
                Destroy(listFoods[i].gameObject);
            }
        }
        listFoods.Clear();
    }

    private void MoveConveyor(){
        for(int i = 0; i < listConveyors.Count; i++){
            listConveyors[i].MoveConveyor();
        }
    }

}
