using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;
    public Player mainPlayer;

    public EnemyShip prefabEnemyShip;
    List<EnemyShip> listEnemyShips = new List<EnemyShip>();

    public Bomb prefabBomb;
    List<Bomb> listBombs = new List<Bomb>();

    public Shooter prefabShooter;
    List<Shooter> listShooters = new List<Shooter>();

    Vector3 worldPoint;
    public List<GameObject> listMainGameObject;
    public bool isLose, isAttackTime, isBonusTime;
    public float timeEnemyShip, timeBomb, timeShooter;
    // Start is called before the first frame update
    void Start()
    {
        isBonusTime = false;
        gameManager = this;
        isAttackTime = false;
        isLose = false;
        timeBomb = 4;
        timeEnemyShip = 1;
        timeShooter = 8;
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnEnemies(){
        StopSpawnEnemies();
        InvokeRepeating("SpawnEnemyShip", 0, timeEnemyShip);
        InvokeRepeating("SpawnBomb", 5, timeBomb);
        InvokeRepeating("SpawnShooter", 0, timeShooter);
    }

    public void StopSpawnEnemies(){
        CancelInvoke("SpawnEnemyShip");
        CancelInvoke("SpawnBomb");
        CancelInvoke("SpawnShooter");
    }

    Vector3 RandomPosition(){
        float x, y;
        int pos = Random.Range(0, 3);
        if(pos == 0){
            x = -worldPoint.x - 1;
            y = Random.Range(-worldPoint.y - 1, worldPoint.y + 1.1f);
        }
        else if(pos == 1){
            x = Random.Range(-worldPoint.x - 1, worldPoint.x + 1.1f);
            y = Random.Range(0, 2);
            if(y == 0){
                y = worldPoint.y + 1;
            }
            else{
                y = -worldPoint.y - 1;
            }
        }
        else{
            x = worldPoint.x + 1;
            y = Random.Range(-worldPoint.y - 1, worldPoint.y + 1.1f );
        }
        return new Vector3(x,y,0);

    }

    public void GameLosing(){
        StopSpawnEnemies();
        DestroyAllEnemy();
        DisableMainGameObject();
        uiController.GameOverUI();
    }

    public bool IsClearEnemies(){
        for(int i = 0 ; i < listEnemyShips.Count; i++){
            if(listEnemyShips[i] != null){
                return false;
            }
        }

        for(int i = 0 ; i < listBombs.Count; i++){
            if(listBombs[i] != null){
                return false;
            }
        }

        for(int i = 0 ; i < listShooters.Count; i++){
            if(listShooters[i] != null){
                return false;
            }
        }
        return true;
    }

    public void DestroyAllEnemy(){
        for(int i = 0 ; i < listEnemyShips.Count; i++){
            if(listEnemyShips[i] != null){
                Destroy(listEnemyShips[i].gameObject);
            }
        }
        listEnemyShips.Clear();

        for(int i = 0 ; i < listBombs.Count; i++){
            if(listBombs[i] != null){
                Destroy(listBombs[i].gameObject);
            }
        }
        listBombs.Clear();

        for(int i = 0 ; i < listShooters.Count; i++){
            if(listShooters[i] != null){
                Destroy(listShooters[i].gameObject);
            }
        }
        listShooters.Clear();
    }

    public void AllDestroyBonus(){
        // StopSpawnEnemies();
        for(int i = 0 ; i < listEnemyShips.Count; i++){
            if(listEnemyShips[i] != null){
                // Destroy(listEnemyShips[i].gameObject);
                listEnemyShips[i].GetComponent<EnemyShip>().ExploreEnemy();
                mainPlayer.score++;
            }
        }
        listEnemyShips.Clear();

        for(int i = 0 ; i < listBombs.Count; i++){
            if(listBombs[i] != null){
                // Destroy(listBombs[i].gameObject);
                listBombs[i].GetComponent<Bomb>().ExploreEnemy();
                mainPlayer.score++;
            }
        }
        listBombs.Clear();

        for(int i = 0 ; i < listShooters.Count; i++){
            if(listShooters[i] != null){
                // Destroy(listShooters[i].gameObject);
                listShooters[i].GetComponent<Shooter>().ExploreEnemy();
                mainPlayer.score++;
            }
        }
        listShooters.Clear();
        // SpawnEnemies();
    }

    void DisableMainGameObject(){
        for(int i = 0; i < listMainGameObject.Count; i++){
            listMainGameObject[i].SetActive(false);
        }
    }

    void SpawnEnemyShip(){
        if(!isAttackTime){
            var sEnemyShip = Instantiate(prefabEnemyShip, RandomPosition(), Quaternion.identity);
            listEnemyShips.Add(sEnemyShip);
        }
    }

    void SpawnBomb(){
        if(!isAttackTime){
            var sBomb = Instantiate(prefabBomb, RandomPosition(), Quaternion.identity);
            listBombs.Add(sBomb);
        }
    }

    void SpawnShooter(){
        if(!isAttackTime || listShooters.Count <= 3){
            var sShooter = Instantiate(prefabShooter, RandomPosition(), Quaternion.identity);
            listShooters.Add(sShooter);
        }
    }


}
