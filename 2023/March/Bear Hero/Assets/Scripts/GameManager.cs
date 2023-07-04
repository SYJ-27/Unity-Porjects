using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;
    public GameOver gameOver;
    public float currentScore;
    public bool isLose = false;
    public Saw sawPrefabs;
    public List<Saw> listOfSaw = new List<Saw>();
    public int maxSaw;
    public List<GameObject> mainPlayer;
    
    void Awake(){
        gameManager = this;
        maxSaw = 2;
    }
    
    void Start()
    {
        StartGame();
    }

    void Update()
    {
        UpdateScoreUI();
    }

    void StartGame(){
        gameOver.HideGameOver();
        isLose = false;
        currentScore = 0;
        UpdateScoreUI();
        InvokeRepeating("SpawningSaw", 1.5f, 2);
    }

    public void UpdateScoreUI(){
        currentScore += Time.deltaTime;
        float currentScoreRounded = (float)Math.Round(currentScore);
        uiController.UpdateCurrentScore(currentScoreRounded);
        if(currentScore > 22)
            maxSaw = 3;
    }

    public void GameLose(){
        DestroyAllSaw();
        DisableMainObject();
        isLose = true;
        uiController.HideCurrentScore();
        float currentScoreRounded = (float)Math.Round(currentScore);
        gameOver.ShowGameOver(currentScoreRounded);
    }

    public void PlayGame(){
        StartGame();
        uiController.ShowCurrentScore();
    }

    void SpawningSaw(){
        if(CountExistSaw() <= maxSaw){
            var spawnedSaw = Instantiate(sawPrefabs, new  Vector3(0,0,0), Quaternion.identity);
            listOfSaw.Add(spawnedSaw);
        }
    }

    void DestroyAllSaw(){
        CancelInvoke("SpawningSaw");
        for(int i = 0; i < listOfSaw.Count; i++){
            if(listOfSaw[i] != null){
                Destroy(listOfSaw[i].gameObject);
            }
        }
        listOfSaw.Clear();
    }

    int CountExistSaw(){
        int countSaw = 0;
        for(int i = 0; i < listOfSaw.Count; i++){
            if(listOfSaw[i] != null){
                countSaw++;
            }
        }
        return countSaw;
    }

    void DisableMainObject(){
        for(int i = 0 ; i < mainPlayer.Count; i++){
            mainPlayer[i].SetActive(false);
        }
    }

}
