using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;
    public GameOver gameOver;
    public int currentScore;
    public bool isLose = false;

    public List<FishingRod> fishingRodType;
    public List<FishingRod> listOfRod = new List<FishingRod>();
    public int maxRod, maxFood, maxCrab;

    public List<Food> foodType;
    public List<Food> listOfFood = new List<Food>();

    public Crab crabPrefabs;
    public List<Crab> listOfCrab = new List<Crab>();

    public float minX, minY, maxX, maxY;

    public Image timeBar;
    public float maxTime, timeMin;

    public List<GameObject> gamePlayObject;
    
    
    void Awake(){
        timeMin = maxTime;
        gameManager = this;
        maxRod = 2;
        maxFood = 9;
        maxCrab = 4;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        minX = -worldPosition.x;
        minY = -worldPosition.y;
        maxX = worldPosition.x;
        maxY = worldPosition.y;
    }
    
    void Start()
    {
        StartGame();
        
        Invoke("SpawnFood", 0);
        
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        showTime();
    }


    void StartGame(){
        gameOver.HideGameOver();
        isLose = false;
        currentScore = -1;
        UpdateScoreUI();
    }

    public void UpdateScoreUI(){
        currentScore++;
        
        uiController.UpdateCurrentScore(currentScore);
        if(currentScore == 10){
            Invoke("SpawnCrab", 7);
        }
        else if(currentScore == 5){
            InvokeRepeating("SpawnRod", 5, 3);
        }
    }

    public void GameLose(){
        DestroyAllFCR();
        DisableAllGamePlayObject();
        isLose = true;
        uiController.HideCurrentScore();
        gameOver.ShowGameOver(currentScore);
    }

    public void PlayGame(){
        StartGame();
        uiController.ShowCurrentScore();
    }

    private void SpawnRod(){
        if(CountRodInScene() <= maxRod){
            int random = Random.Range(0,fishingRodType.Count);
            int randomX = Random.Range((int)(minX + 2), (int)(maxX - 2));
            
            var rodSpawned = Instantiate(fishingRodType[random], new Vector3(randomX, 10, 0), Quaternion.identity);
            if(random < fishingRodType.Count - 1){
                rodSpawned.InitPosition(1);
            }
            else{
                rodSpawned.InitPosition(0);
            }
            listOfRod.Add(rodSpawned);
        }
    }

    private void SpawnFood(){
        if(CountFoodInScene() <= maxFood){
            int random = Random.Range(0,foodType.Count);
            int randomX = Random.Range((int)(minX + 2), (int)(maxX - 2));
            var foodSpawned = Instantiate(foodType[random], new Vector3(randomX, 10, 0), Quaternion.identity);
            foodSpawned.InitPosition();
            listOfFood.Add(foodSpawned);
        }
        Invoke("SpawnFood", Random.Range(2, 4));
    }

    private void SpawnCrab(){
        if(CountCrabInScene() <= maxCrab){
            int randomX = Random.Range((int)(minX + 2), (int)(maxX - 2));
            var crabSpawned = Instantiate(crabPrefabs, new Vector3(randomX, -20, 0), Quaternion.identity);
            crabSpawned.InitPosition();
            listOfCrab.Add(crabSpawned);
        }
        Invoke("SpawnCrab", Random.Range(3, 7));
    }

    private int CountRodInScene(){
        int rodCount = 0;
        for(int i = 0; i < listOfRod.Count; i++){
            if(listOfRod[i] != null){
                rodCount++;
            }
        }
        return rodCount;
    }

    private int CountFoodInScene(){
        int foodCount = 0;
        for(int i = 0; i < listOfFood.Count; i++){
            if(listOfFood[i] != null){
                foodCount++;
            }
        }
        return foodCount;
    }

    private int CountCrabInScene(){
        int crabCount = 0;
        for(int i = 0; i < listOfCrab.Count; i++){
            if(listOfCrab[i] != null){
                crabCount++;
            }
        }
        return crabCount;
    }

    public void updateTime(float time)
    {
        timeMin = Mathf.Clamp(time, 0, maxTime);
        timeBar.fillAmount = timeMin / maxTime;
    }
    public void showTime()
    {
        updateTime(timeMin - Time.deltaTime);
        if (timeMin == 0)
        {
           GameLose();
        }
    }

    public void DestroyAllFCR(){
        CancelInvoke("SpawnCrab");
        CancelInvoke("SpawnFood");
        CancelInvoke("SpawnRod");
        for(int i = 0; i < listOfCrab.Count; i++){
            if(listOfCrab[i] != null){
                Destroy(listOfCrab[i].gameObject);
            }
        }
        listOfCrab.Clear();

        for(int i = 0; i < listOfFood.Count; i++){
            if(listOfFood[i] != null){
                Destroy(listOfFood[i].gameObject);
            }
        }
        listOfFood.Clear();

        for(int i = 0; i < listOfRod.Count; i++){
            if(listOfRod[i] != null){
                Destroy(listOfRod[i].gameObject);
            }
        }
        listOfRod.Clear();
    }

    public void DisableAllGamePlayObject(){
        for(int i = 0; i < gamePlayObject.Count; i++){
            gamePlayObject[i].SetActive(false);
        }
    }

}
