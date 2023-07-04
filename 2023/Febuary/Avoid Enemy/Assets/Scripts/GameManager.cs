using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;
    public GameOver gameOver;
    public int currentScore, highScore = 0, score = 0;
    public bool isLose = false;
    
    public Collider2D topSpawner, bottomSpawner, leftSpawner, rightSpawner;
    public Enemy enemyPrefabs;
    private List<Enemy> listOfEnemy = new List<Enemy>();
    public float RandomTimeSpawn;
    
    void Awake(){
        // PlayerPrefs.SetInt("HighScore",highScore);
        gameManager = this;
        // DontDestroyOnLoad(this.gameObject);
        highScore = PlayerPrefs.GetInt("HighScore");
    }
    
    void Start()
    {
        StartGame();

    }

    void Update()
    {

    }

    void StartGame(){
        Player.mainPlayer.UnStopPlayer();
        RandomTimeSpawn = 1;
        gameOver.HideGameOver();
        isLose = false;
        currentScore = -1;
        UpdateScoreUI();
        InvokeRepeating("SpawnEnemy", 0, RandomTimeSpawn);
        // InvokeRepeating("SpawnBomb", 0, RandomTimeSpawn);
    }

    public void SpawnEnemy(){
        CancelInvoke("SpawnEnemy");
        RandomTimeSpawn = Random.Range(3,5);
        InvokeRepeating("SpawnEnemy", RandomTimeSpawn, RandomTimeSpawn);
        int randomBomb = Random.Range(0,2);
        if(listOfEnemy.Count < 2){
            randomBomb = 0;
        }
        if(randomBomb == 0){
            float randX = RandomXInRangeBound();
            float randY = RandomYInRangeBound();
            var enemySpawned = Instantiate(enemyPrefabs,new Vector3(randX, randY, 0), Quaternion.identity);
            enemySpawned.InitBomb(false);
            listOfEnemy.Add(enemySpawned);
        }
        else{
            float randX = Random.Range(-2.7f, 2.7f);
            float randY = Random.Range(-2.7f, 2.7f);
            var enemySpawned = Instantiate(enemyPrefabs,new Vector3(randX, randY, 0), Quaternion.identity);
            enemySpawned.InitBomb(true);
            listOfEnemy.Add(enemySpawned);
        }
        
        
    }

    public void SpawnBomb(){
        CancelInvoke("SpawnBomb");
        float RandomTimeSpawnBomb = Random.Range(3,5);
        InvokeRepeating("SpawnBomb", RandomTimeSpawnBomb, RandomTimeSpawnBomb);
        float randX = Random.Range(-2.7f, 2.7f);
        float randY = Random.Range(-2.7f, 2.7f);
        var enemySpawned = Instantiate(enemyPrefabs,new Vector3(randX, randY, 0), Quaternion.identity);
        enemySpawned.InitBomb(true);
        listOfEnemy.Add(enemySpawned);
    }

    public float RandomXInRangeBound(){
        float leftSide = Random.Range(0,2);
        if(leftSide == 0){
            return Random.Range(rightSpawner.bounds.min.x, rightSpawner.bounds.max.x);
        }
        else{
            return Random.Range(leftSpawner.bounds.min.x, leftSpawner.bounds.max.x);
        }
    }

    public float RandomYInRangeBound(){
        float topSide = Random.Range(0,2);
        if(topSide == 0){
            return Random.Range(bottomSpawner.bounds.min.y, bottomSpawner.bounds.max.y);
        }
        else{
            return Random.Range(topSpawner.bounds.min.y, topSpawner.bounds.max.y);
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
        Player.mainPlayer.StopPlayer();
        CancelInvoke("SpawnEnemy");
        // CancelInvoke("SpawnBomb");
        DestroyAllEnemy();
        isLose = true;
        uiController.HideCurrentScore();
        PlayerPrefs.SetInt("HighScore",highScore);
        gameOver.ShowGameOver(score, highScore);
    }

    public void PlayGame(){
        StartGame();
        uiController.ShowCurrentScore();
    }

    void DestroyAllEnemy(){
        for(int i = 0; i < listOfEnemy.Count; i++){
            if(listOfEnemy[i] != null){
                Destroy(listOfEnemy[i].gameObject);
            }
        }
        listOfEnemy.Clear();
    }

}
