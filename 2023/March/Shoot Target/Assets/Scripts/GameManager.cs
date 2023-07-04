using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;
    public GameBonus gameBonus;

    public Vector2 worldPoint;

    public Enemy enemyPrefabs;
    public List<Enemy> listEnemy;

    public Enemy2 enemy2Prefabs;
    public List<Enemy2> listEnemy2;
    public List<GameObject> listMainGameObject = new List<GameObject>();

    public int score;
    public float enemyTimeSpawn;

    public bool isLose, isPause;
    public Player mainPlayer;


    void Awake(){
        enemyTimeSpawn = 2;
        isLose = false;
        score = 0;
        gameManager = this;
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0 , enemyTimeSpawn);
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    void SpawnEnemy(){
        SpawnEnemy1();
        if(listEnemy.Count == 3 || IsNullEnemy2()){
            SpawnEnemy2();
        }
    }

    public void SpawnEnemy2(){
        float x = Random.Range(-worldPoint.x + 1, worldPoint.x - 1);
        float y = Random.Range(-worldPoint.y + 1, 3);
        var enemy2 = Instantiate(enemy2Prefabs, new Vector3(x,y, 0), Quaternion.identity);

        listEnemy2.Add(enemy2);
    }

    public void SpawnEnemy1(){
        float x = Random.Range(-worldPoint.x + 1, worldPoint.x - 1);
        float y = Random.Range(-worldPoint.y + 1, worldPoint.y - 1);
        var enemy = Instantiate(enemyPrefabs, new Vector3(x,y, 0), Quaternion.identity);
        enemy.InitSpeed();
        listEnemy.Add(enemy);
    }

    public bool IsNullEnemy2(){
        if(listEnemy2.Count == 0){
            return false;
        }
        else{
            for(int i = 0; i < listEnemy2.Count; i++){
                if(listEnemy2[i] != null){
                    return false;
                }
            }
            return true;
        }
    }

    public void UpdateScore(){
        score++;
        if((score % 10 == 0 && mainPlayer.lifePlayer > 0) || score == 1){
            GameBonusing();
        }
        uiController.UpdateScoreUI(score);
    }

    public void GameLosing(){
        DestroyAllEnemy();
        DisableGamePlay();
        isLose = true;
    }

    public void GamePausing(){
        if(!isPause){
            isPause = true;
            mainPlayer.CancelResetBullet();
            CancelInvoke("SpawnEnemy");
        }
        else{
            mainPlayer.ResetBullet();
            GameContinue();
        }
    }

    public void GameBonusing(){
        GameLosing();
        Invoke("GetBonus", 0.35f);
    }

    void GetBonus(){
        CancelInvoke("SpawnEnemy");

        gameBonus.GetBonusScene();
    }

    public void GameContinue(){
        isPause = false;
        InvokeRepeating("SpawnEnemy", enemyTimeSpawn , enemyTimeSpawn);
    }

    void DestroyAllEnemy(){
        CancelInvoke("SpawnEnemy");
        for(int i = 0; i < listEnemy.Count; i++){
            if(listEnemy[i] != null){
                Destroy(listEnemy[i].gameObject);
            }
        }
        listEnemy.Clear();

        for(int i = 0; i < listEnemy2.Count; i++){
            if(listEnemy2[i] != null){
                Destroy(listEnemy2[i].gameObject);
            }
        }
        listEnemy2.Clear();
    }

    void DisableGamePlay(){
        for(int i = 0; i < listMainGameObject.Count; i++){
            if(listMainGameObject[i] != null){
                listMainGameObject[i].SetActive(false);
            }
        }
    }

    public void ResetEnemyTimeSpawn(){
        CancelInvoke("SpawnEnemy");
        InvokeRepeating("SpawnEnemy", enemyTimeSpawn , enemyTimeSpawn);
    }

}
