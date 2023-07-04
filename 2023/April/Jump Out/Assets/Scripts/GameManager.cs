using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;

    public Bomb prefabsBomb;
    List<Bomb> listBombs = new List<Bomb>();

    public Bomb1 prefabsBomb1;
    List<Bomb1> listBomb1s = new List<Bomb1>();

    public Obstacle prefabsObstacle;
    List<Obstacle> listObstacles = new List<Obstacle>();

    public Snipper prefabsSnipper;
    List<Snipper> listSnippers = new List<Snipper>();

    public Food mainFood;

    public List<GameObject> listMainGameObject;
    
    public Transform limitHorizontal;
    Vector3 worldPoint;


    void Awake(){
        gameManager = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        // InvokeRepeating("SpawnSnipper", 0, 1);
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetRandomPosition(){
        float minX = -limitHorizontal.position.x;
        float maxX = limitHorizontal.position.x;
        float minY = -3.3f;
        float maxY = 3.8f;
        return new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
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

    public void SpawnBomb(){
        // if(mainFood.score == 3 && listBombs.Count  < 1){
        int rand = Random.Range(0, 2);
        if(rand == 0){
            listBombs.Add(Instantiate(prefabsBomb, GetRandomPosition(), Quaternion.identity));
        }
        else{
            listBomb1s.Add(Instantiate(prefabsBomb1, RandomPosition(), Quaternion.identity));
        }
        Invoke("SpawnBomb", Random.Range(2f, 4f));
        // }
    }

    public void SpawnSnipper(){
        listSnippers.Add(Instantiate(prefabsSnipper, Vector3.zero, Quaternion.identity));
    }

    void SpawnObstacle(){
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        int randomPos = Random.Range(0, 4);
        float x,y,z;
        if(randomPos == 0){
            x = 0;
            y = -worldPoint.y - 1;
        }
        else if(randomPos == 1){
            x = -worldPoint.x - 1;
            y = 0;
        }
        else if(randomPos == 2){
            x = 0;
            y = worldPoint.y + 1;
        }
        else{
            x = worldPoint.x + 1;
            y = 0;
        }
        uiController.UpdateNoticeObstacle(randomPos);
        var obstacleSpawn = Instantiate(prefabsObstacle, new Vector3(x, y, 0), Quaternion.identity);
        if(randomPos == 0){
            x = 2;
            y = 1;
            z = 0;
        }
        else if(randomPos == 1){
            x = 1;
            y = 1;
            z = 90;
        }
        else if(randomPos == 2){
            x = 2;
            y = 1;
            z = 0;
        }
        else{
            x = 1;
            y = 1;
            z = 90;
        }
        obstacleSpawn.InitObstacle(x, y, z, randomPos);
        listObstacles.Add(obstacleSpawn);
    }

    public void GameLosing(string reasonLose){
        CancelInvoke("SpawnObstacle");
        CancelInvoke("SpawnBomb");
        CancelInvoke("SpawnSnipper");
        DestroyAllEnemy();
        DisableMainGameObject();
        uiController.GameOverUI(reasonLose);
    }

    void DestroyAllEnemy(){
        for(int i = 0; i < listBombs.Count; i++){
            if(listBombs[i] != null){
                Destroy(listBombs[i].gameObject);
            }
        }
        listBombs.Clear();

        for(int i = 0; i < listBomb1s.Count; i++){
            if(listBomb1s[i] != null){
                Destroy(listBomb1s[i].gameObject);
            }
        }
        listBomb1s.Clear();

        for(int i = 0; i < listSnippers.Count; i++){
            if(listSnippers[i] != null){
                Destroy(listSnippers[i].gameObject);
            }
        }
        listSnippers.Clear();

        for(int i = 0; i < listObstacles.Count; i++){
            if(listObstacles[i] != null){
                Destroy(listObstacles[i].gameObject);
            }
        }
        listObstacles.Clear();

    }

    void DisableMainGameObject(){
        for(int i = 0; i < listMainGameObject.Count; i++){
            if(listMainGameObject[i] != null){
                listMainGameObject[i].SetActive(false);
            }
        }
    }

    public void SpawningEnemy(int score){
        if(score == 1){
            InvokeRepeating("SpawnObstacle", 5, 5);
        }
        if(score == 3){
            Invoke("SpawnBomb", Random.Range(2f, 4f));
        }
        if(score == 7){
            InvokeRepeating("SpawnSnipper", 0, 5);
        }

    }

}
