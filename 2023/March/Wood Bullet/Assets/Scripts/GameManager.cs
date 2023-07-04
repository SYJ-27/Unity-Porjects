using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public GameOver gameOver;

    public bool isLose, isStart;

    public Bat batPrefabs;
    public List<Bat> batList = new List<Bat>();
    public RockEnemy rockenemyPrefabs;
    public List<RockEnemy> rockenemyList = new List<RockEnemy>();
    private Vector3 worldPoint;
    public float minY, maxY, minX, maxX;

    public List<GameObject> mainGameObjectList;
    void Awake(){
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        minY = -worldPoint.y;
        maxY = worldPoint.y;
        minX = -worldPoint.x;
        maxX = worldPoint.x;

        gameManager = this;
        isLose = false;
        isStart = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnBat", 1);
        Invoke("SpawnRockEnemy", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            isStart = false;
        }
    }

    public void GameLosing(int score, float time){
        isLose = true;
        CancelInvoke("SpawnBat");
        CancelInvoke("SpawnRockEnemy");
        ClearEnemy();
        InvisibleGameObjects();
        gameOver.GameOverScene(score, time);
    }

    private void SpawnBat(){
        var batSpawned = Instantiate(batPrefabs, new Vector3(-100, 0, 0), Quaternion.identity);
        batSpawned.InitPosition(worldPoint);
        batList.Add(batSpawned);
        Invoke("SpawnBat", Random.Range(2, 4));
    }

    private void SpawnRockEnemy(){
        var rockenemySpawned = Instantiate(rockenemyPrefabs, new Vector3(-100, 0, 0), Quaternion.identity);
        rockenemySpawned.InitPosition(worldPoint);
        rockenemyList.Add(rockenemySpawned);
        Invoke("SpawnRockEnemy", Random.Range(2, 4));
    }

    private void ClearEnemy(){
        for(int i = 0; i < batList.Count; i++){
            if(batList[i] != null){
                Destroy(batList[i].gameObject);
            }
        }
        batList.Clear();
        for(int i = 0; i < rockenemyList.Count; i++){
            if(rockenemyList[i] != null){
                Destroy(rockenemyList[i].gameObject);
            }
        }
        rockenemyList.Clear();
    }

    private void InvisibleGameObjects(){
        for(int i =0; i< mainGameObjectList.Count; i++){
            mainGameObjectList[i].SetActive(false);
        }
    }


}
