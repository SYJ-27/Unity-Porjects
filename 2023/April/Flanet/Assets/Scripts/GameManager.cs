using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;
    public Earth mainEarth;
    Vector3 worldPoint;
    public bool isLose;

    public List<int> scoreFlowers;
    public List<int> scoreEnemies;

    public Bullet prefabBullet;
    List<Bullet> listBullets;

    public Butterfly prefabButterfly;
    List<Butterfly> listButterflys;

    public List<GameObject> listMainGameObject;
    public List<Flower> listFlowers;

    void Awake(){
        isLose = false;
        listBullets = new List<Bullet>();
        listButterflys = new List<Butterfly>();
        gameManager = this;
        scoreFlowers = new List<int>(){0,0,0,0};
        scoreEnemies = new List<int>(){0,0};
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void SpawnEnemy(){
        int randomEnemy = Random.Range(0, 2);
        if(randomEnemy == 0){
            var sBullet = Instantiate(prefabBullet, RandomPosition(), Quaternion.identity);
            listBullets.Add(sBullet);
        }
        else{
            var sButterfly = Instantiate(prefabButterfly, RandomPosition(), Quaternion.identity);
            listButterflys.Add(sButterfly);
        }
        if(GetTotalScoreFlower() < 20){
            Invoke("SpawnEnemy", Random.Range(2f, 2.5f));
        }
        else{
            Invoke("SpawnEnemy", Random.Range(1f, 2f));
        }
    }

    public int GetTotalScoreFlower(){
        int score = 0;
        for(int i =0;i < scoreFlowers.Count; i++){
            score+=scoreFlowers[i];
        }
        return score;
    }

    public void SetScoreFlower(int idFlower){
        scoreFlowers[idFlower]++;
        if(mainEarth.earthLife < mainEarth.maxLife){
            mainEarth.earthLife++;
        }
        if(GetTotalScoreFlower() == 3){
            Invoke("SpawnEnemy", Random.Range(1f, 1.5f));
        }
    }

    public int GetScoreFlower(int idFlower){
        return scoreFlowers[idFlower];
    }

    int GetTotalScoreEnemy(){
        int score = 0;
        for(int i =0;i < scoreEnemies.Count; i++){
            score+=scoreEnemies[i];
        }
        return score;
    }

    public void SetScoreEnemy(int idFlower){
        scoreEnemies[idFlower]++;
        if(GetTotalScoreEnemy() == 3){
            Invoke("SpawnEnemy", Random.Range(1f, 2f));
        }
    }

    public int GetScoreEnemy(int idFlower){
        return scoreEnemies[idFlower];
    }

    public void GameLosing(){
        isLose = true;
        DisableAllGameObject();
        DestroyAllEnemy();
        listMainGameObject[0].GetComponent<Player>().DestroyAllSeed();
        uiController.GetGameOver();
    }

    void DisableAllGameObject(){
        for(int i = 0; i < listMainGameObject.Count; i++){
            listMainGameObject[i].SetActive(false);
        }
    }

    void DestroyAllEnemy(){
        CancelInvoke("SpawnEnemy");
        for(int i = 0;i < listBullets.Count; i++){
            if(listBullets[i] != null){
                Destroy(listBullets[i].gameObject);
            }
        }
        listBullets.Clear();

        for(int i = 0;i < listButterflys.Count; i++){
            if(listButterflys[i] != null){
                Destroy(listButterflys[i].gameObject);
            }
        }
        listButterflys.Clear();

        for(int i = 0;i < listFlowers.Count; i++){
            if(listFlowers[i] != null){
                Destroy(listFlowers[i].gameObject);
            }
        }
        listFlowers.Clear();

    }

}
