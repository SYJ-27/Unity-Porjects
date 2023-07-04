using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public GameOver gameOver;
    public UiController uiController;

    public List<GameObject> mainGameObjectList;

    public List<Map> mapList;
    public Map currentMap;

    public int score;
    public bool isLose;

    
    void Awake(){
        isLose = false;
        score = 0;
        gameManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnMap1();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameLosing(){
        isLose = true;
        DisableMainGameObject();
        if(currentMap != null){
            currentMap.DestroySelf();
        }
        gameOver.GameOverScene();
    }

    public void SpawnMap(){
        // Invoke("CreateRandomMap", 0.1f);
        // CreateRandomMap();
        if(currentMap != null){
            score++;
            uiController.UpdateScoreUI();
            currentMap.DestroySelf();
        }
        currentMap = Instantiate(mapList[Random.Range(1, mapList.Count)], Vector3.zero, Quaternion.identity);
    }

    public void SpawnMap1(){
        if(currentMap != null){
            currentMap.DestroySelf();
        }
        currentMap = Instantiate(mapList[0], Vector3.zero, Quaternion.identity);
    }

    public void DisableMainGameObject(){
        for(int i = 0; i < mainGameObjectList.Count; i++){
            if(mainGameObjectList[i] != null){
                mainGameObjectList[i].SetActive(false);
            }
        }
    }

    public void CreateRandomMap(){
        
    }

}
