using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;
    public bool isLose = false, isStart;
    
    public float minX, maxX, minY, maxY;
    
    public Beer beerPrefabs;
    public List<Beer> listOfBeer = new List<Beer>();
    
    public MatchBox matchBoxPrefabs;
    public List<MatchBox> listOfMatchBox = new List<MatchBox>();

    public Enemy3 enemy3Prefabs;
    public List<Enemy3> listOfEnemy3 = new List<Enemy3>();

    public Enemy4 enemy4Prefabs;
    public List<Enemy4> listOfEnemy4 = new List<Enemy4>();

    public Player mainPlayer;
    public int countEnemy;

    public GameObject lightBorder;
    public List<GameObject> listOfObject;

    public int maxMatchBox, maxEnemy3, maxEnemy4, maxBeer;
    public int numMatchBox, numEnemy3, numEnemy4, numBeer;
    
    
    void Awake(){
        LimitObject();
        maxBeer = 4;
        maxMatchBox = 2;
        maxEnemy3 = 2;
        maxEnemy4 = 3;
        lightBorder.SetActive(false);
        countEnemy = 0;
        gameManager = this;
        isStart = true;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        minX = -worldPosition.x;
        maxX = worldPosition.x;
        minY = -worldPosition.y;
        maxY = worldPosition.y;
    }
    
    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if(!isStart && !isLose){
            lightBorder.SetActive(true);
        }
        else{
            lightBorder.SetActive(false);
        }
        CheckCanEat();
        CheckAllNullEnemy();
    }

    void StartGame(){
        
        isLose = false;
        
        NewLevel();
    }

    public void UpdateScoreUI(){
        uiController.UpdateCurrentScore();
    }

    public void GameLose(){
        isLose = true;
        DisableAllPlayArea();
        ClearListEnemy();
        
        uiController.HideCurrentScore();
    }

    public void SpawnBeer(){
        float randomX = Random.Range(minX + 1, maxX - 1);
        float randomY = Random.Range(minY + 1, maxY - 1);
        var beerSpawner = Instantiate(beerPrefabs, new Vector3(randomX, randomY, 0), Quaternion.identity);
        beerSpawner.InitBeer();
        listOfBeer.Add(beerSpawner);
    }

    public void SpawningBeer(){
        LimitObject();
        for(int i = 0; i < numBeer; i++){
            SpawnBeer();
        }
    }

    public void SpawnMatchBox(){
        float randomX = Random.Range(minX + 1, maxX - 1);
        float randomY = Random.Range(minY + 1, maxY - 1);
        var matchBoxSpawner = Instantiate(matchBoxPrefabs, new Vector3(randomX, randomY, 0), Quaternion.identity);
        matchBoxSpawner.InitMatchBox();
        listOfMatchBox.Add(matchBoxSpawner);
    }

    public void SpawningMatchBox(){
        LimitObject();

        for(int i = 0; i < numMatchBox; i++){
            SpawnMatchBox();
        }
    }

    public void SpawnEnemy3(){
        float randomX = Random.Range(minX + 1, maxX - 1);
        float randomY = Random.Range(minY + 1, maxY - 1);
        var enemy3Spawner = Instantiate(enemy3Prefabs, new Vector3(randomX, randomY, 0), Quaternion.identity);
        enemy3Spawner.InitEnemy3();
        listOfEnemy3.Add(enemy3Spawner);
    }

    public void SpawningEnemy3(){
        LimitObject();
        for(int i = 0; i < numEnemy3; i++){
            SpawnEnemy3();
        }
    }

    public void SpawnEnemy4(){
        float randomX = Random.Range(minX + 1, maxX - 1);
        float randomY = Random.Range(minY + 1, maxY - 1);
        var enemy4Spawner = Instantiate(enemy4Prefabs, new Vector3(randomX, randomY, 0), Quaternion.identity);
        enemy4Spawner.InitEnemy4();
        listOfEnemy4.Add(enemy4Spawner);
    }

    public void SpawningEnemy4(){
        LimitObject();
        for(int i = 0; i < numEnemy4; i++){
            SpawnEnemy4();
        }
    }

    public void CheckCanEat(){
        for(int i = 0; i < listOfBeer.Count; i++){
            if(listOfBeer[i] != null){
                if(mainPlayer.stateSize >= listOfBeer[i].stateSize && !listOfBeer[i].canEat){
                    listOfBeer[i].SetCanEat();
                }
            }
        }

        for(int i = 0; i < listOfMatchBox.Count; i++){
            if(listOfMatchBox[i] != null){
                if(mainPlayer.stateSize >= listOfMatchBox[i].stateSize && !listOfMatchBox[i].canEat){
                    listOfMatchBox[i].SetCanEat();
                }
            }
        }

        for(int i = 0; i < listOfEnemy3.Count; i++){
            if(listOfEnemy3[i] != null){
                if(mainPlayer.stateSize >= listOfEnemy3[i].stateSize && !listOfEnemy3[i].canEat){
                    listOfEnemy3[i].SetCanEat();
                }
            }
        }

        for(int i = 0; i < listOfEnemy4.Count; i++){
            if(listOfEnemy4[i] != null){
                if(mainPlayer.stateSize >= listOfEnemy4[i].stateSize && !listOfEnemy4[i].canEat){
                    listOfEnemy4[i].SetCanEat();
                }
            }
        }
    }

    public void CheckAllNullEnemy(){
        int countEnemy1 = 0;
        for(int i = 0; i < listOfBeer.Count; i++){
            if(listOfBeer[i] == null){
                countEnemy1++;
            }
        }
        for(int i = 0; i < listOfMatchBox.Count; i++){
            if(listOfMatchBox[i] == null){
                countEnemy1++;
            }
        }
        for(int i = 0; i < listOfEnemy3.Count; i++){
            if(listOfEnemy3[i] == null){
                countEnemy1++;
            }
        }
        for(int i = 0; i < listOfEnemy4.Count; i++){
            if(listOfEnemy4[i] == null){
                countEnemy1++;
            }
        }
        
        if(countEnemy1 >= listOfBeer.Count + listOfMatchBox.Count + listOfEnemy3.Count + listOfEnemy4.Count && !isLose){
            UpdateScoreUI();
            NewLevel();
        }
    }

    public void ClearListEnemy(){
        for(int i = 0; i < listOfBeer.Count; i++){
            if(listOfBeer[i] != null){
                Destroy(listOfBeer[i].gameObject);
            }
        }
        listOfBeer.Clear();

        for(int i = 0; i < listOfMatchBox.Count; i++){
            if(listOfMatchBox[i] != null){
                listOfMatchBox[i].ClearMatchShootList();
                Destroy(listOfMatchBox[i].gameObject);
            }
        }
        listOfMatchBox.Clear();

        for(int i = 0; i < listOfEnemy3.Count; i++){
            if(listOfEnemy3[i] != null){
                listOfEnemy3[i].ClearBullet3ShootList();
                Destroy(listOfEnemy3[i].gameObject);
            }
        }
        listOfEnemy3.Clear();

        for(int i = 0; i < listOfEnemy4.Count; i++){
            if(listOfEnemy4[i] != null){
                listOfEnemy4[i].ClearBullet4ShootList();
                Destroy(listOfEnemy4[i].gameObject);
            }
        }
        listOfEnemy4.Clear();
    }

    public void NewLevel(){
        isStart = true;
        mainPlayer.ResetPlayer();
        mainPlayer.DisableDraging();
        ClearListEnemy();
        SpawningBeer();
        SpawningMatchBox();
        SpawningEnemy3();
        SpawningEnemy4();
    }

    public void DisableAllPlayArea(){
        for(int i = 0; i< listOfObject.Count; i++){
            if(listOfObject[i] != null){
                listOfObject[i].SetActive(false);
            }
        }
    }

    private void LimitObject(){
        int currentScore = uiController.currentScore;
        if(currentScore == 0){
            numBeer = 3;
            numEnemy3 = 0;
            numEnemy4 = 0;
            numMatchBox = 0;
        }
        else if(currentScore > 0 && currentScore < 3){
            numBeer = 2;
            numEnemy3 = 0;
            numEnemy4 = 1;
            numMatchBox = 1;
        }
        else if(currentScore > 3 && currentScore < 4){
            numBeer = 2;
            numEnemy3 = 1;
            numEnemy4 = 0;
            numMatchBox = 1;
        }
        else if(currentScore > 4 && currentScore < 7){
            numBeer = 2;
            numEnemy3 = 1;
            numEnemy4 = 1;
            numMatchBox = 0;
        }
        else{
            numBeer = Random.Range(0, maxBeer);
            numMatchBox = Random.Range(0, numMatchBox);
            numEnemy3 = Random.Range(0, maxEnemy3);
            numEnemy4 = Random.Range(0, maxEnemy4);
            if(numBeer == numMatchBox && numBeer == numEnemy3 && numBeer == numEnemy4 && numBeer == 0){
                numBeer = 4;
            }
        }
    }

}
