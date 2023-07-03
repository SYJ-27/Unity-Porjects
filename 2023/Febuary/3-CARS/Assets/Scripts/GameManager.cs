using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;
    public GameOver gameOver;
    public int currentScore, highScore = 0, score = 0;
    public bool isLose = false;

    public Obstacle obstaclePrefabs;
    private List<Obstacle> listOfObstacle = new List<Obstacle>();
    public Canvas canvasBG;
    public Car purpleCar, yellowCar, greenCar;
    public Transform line1, line2;
    
    void Awake(){
        gameManager = this;
    }
    
    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            // Debug.Log(Screen.width);
            if(worldPosition.y > line1.position.y){
                purpleCar.Move();
            }
            else if(worldPosition.y > line2.position.y && worldPosition.y < line1.position.y){
                yellowCar.Move();
            }
            else if(worldPosition.y < line2.position.y){
                greenCar.Move();
            }
        }
    }

    void StartGame(){
        gameOver.HideGameOver();
        isLose = false;
        currentScore = -1;
        UpdateScoreUI();
        EnableCar();
        InvokeRepeating("SpawnObstacle",0,3f);
    }

    void SpawnObstacle(){
        for(int i = 0; i < 3; i++){
            var obstacleSpawned = Instantiate(obstaclePrefabs, new Vector3(-10, 0, 0), Quaternion.identity);
            if(i == 0){
                obstacleSpawned.InitObstacle(2.35f, 4.35f);
            }
            else if(i == 1){
                obstacleSpawned.InitObstacle(0.99f, -0.99f);
            }
            else{
                obstacleSpawned.InitObstacle(-4.35f, -2.35f);
            }
            listOfObstacle.Add(obstacleSpawned);
        }
    }

    public void UpdateScoreUI(){
        currentScore++;
        if(currentScore > highScore){
            highScore = currentScore;
        }
        score = currentScore;
        uiController.UpdateCurrentScore(currentScore);
    }

    public void GameLose(){
        DisableCar();
        CancelInvoke("SpawnObstacle");
        DestroyAllObstacle();
        isLose = true;
        uiController.HideCurrentScore();
        gameOver.ShowGameOver(score, highScore);
    }

    void DestroyAllObstacle(){
        for(int i = 0; i < listOfObstacle.Count; i++){
            if(listOfObstacle[i] != null){
                Destroy(listOfObstacle[i].gameObject);
            }
        }
        listOfObstacle.Clear();
    }

    public void PlayGame(){
        StartGame();
        uiController.ShowCurrentScore();
    }

    void DisableCar(){
        line1.gameObject.SetActive(false);
        line2.gameObject.SetActive(false);
        purpleCar.gameObject.SetActive(false);
        yellowCar.gameObject.SetActive(false);
        greenCar.gameObject.SetActive(false);
    }

    void EnableCar(){
        line1.gameObject.SetActive(true);
        line2.gameObject.SetActive(true);
        purpleCar.gameObject.SetActive(true);
        yellowCar.gameObject.SetActive(true);
        greenCar.gameObject.SetActive(true);
    }

}
