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
    
    float[] boxScaleX = {1, 1.5f, 2, 2.5f, 3};
    float[] boxScaleY = {1, 1.5f, 2};
    public float[] rangeX = {-3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7};
    public float[] rangeY = {-3.5f, -2.5f, -1.5f, -0.5f, 0.5f, 1.5f, 2.5f};
    public Transform boxPlaying;
    public Egg eggPrefabs;
    private List<Egg> listOfEgg = new List<Egg>();

    public Apple applePrefabs;
    private List<Apple> listOfApple = new List<Apple>();
    int randomY, randomX;
    public List<GameObject> mainGameObjects = new List<GameObject>();
    
    void Awake(){
        // PlayerPrefs.SetInt("HighScore", highScore);
        gameManager = this;
        boxPlaying.localScale = new Vector3(boxScaleX[0], boxScaleY[0]);
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
        gameOver.HideGameOver();
        isLose = false;
        currentScore = -1;
        UpdateScoreUI();
    }

    public void UpdateScoreUI(){
        currentScore++;
        if(currentScore > highScore){
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        score = currentScore;
        uiController.UpdateCurrentScore(currentScore);
    }

    public void GameLose(){
        DisableMainObject();
        DestroyAllEnenmy();
        isLose = true;
        uiController.HideCurrentScore();
        gameOver.ShowGameOver(score, highScore);
    }

    public void PlayGame(){
        StartGame();
        uiController.ShowCurrentScore();
    }

    void DisableMainObject(){
        for(int i = 0; i < mainGameObjects.Count; i++){
            mainGameObjects[i].SetActive(false);
        }
    }

    public void NewLevel(){
        if(!isLose){
            DestroyAllEnenmy();
            UpdateScoreUI();
            randomY = Random.Range(1, 4);
            randomX = Random.Range(1, 6);
            boxPlaying.localScale = new Vector3(boxScaleX[randomX-1], boxScaleY[randomY-1]);
            Player.mainPlayer.SetRangePlayer(randomX, randomY);
            Bread.mainBread.RandomGenerateBread(randomX, randomY);
            if(currentScore >= 3){
                int randomNumber = Random.Range(1,randomX);
                for(int i = 0; i < randomNumber; i++){
                    var eggSpawned = Instantiate(eggPrefabs, new Vector3(-10, -10, 0), Quaternion.identity);
                    eggSpawned.RandomGenerateEgg(randomX, randomY);
                    listOfEgg.Add(eggSpawned);
                }
                InvokeRepeating("SpawnApple", 0, randomNumber);
            }
        }

    }

    void SpawnApple(){
        CancelInvoke("SpawnApple");
        int randomNumber = Random.Range(1,3);
        for(int i = 0; i < randomNumber; i++){
            var appleSpawned = Instantiate(applePrefabs, new Vector3(-10, -10, 0), Quaternion.identity);
            appleSpawned.RandomGenerateApple(randomX, randomY);
            listOfApple.Add(appleSpawned);
        }
        randomNumber = Random.Range(2,4);
        InvokeRepeating("SpawnApple", randomNumber, randomNumber);
    }

    void DestroyAllEnenmy(){
        CancelInvoke("SpawnApple");
        for(int i = 0; i < listOfEgg.Count; i++){
            if(listOfEgg[i] != null){
                Destroy(listOfEgg[i].gameObject);
            }
        }
        listOfEgg.Clear();

        for(int i = 0; i < listOfApple.Count; i++){
            if(listOfApple[i] != null){
                Destroy(listOfApple[i].gameObject);
            }
        }
        listOfApple.Clear();
    }

}
