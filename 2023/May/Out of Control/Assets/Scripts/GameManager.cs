using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;

    public bool isLose, isStart, isPause, isTemp;
    public int score;
    public float time, maxTime;

    Vector3 worldPoint;

    public Player mainPlayer;
    public Sun mainSun;

    public Planet prefabPlanet;
    List<Planet> listPlanets;

    public List<GameObject> listMainGameObjects;

    void Awake(){
        listPlanets = new List<Planet>();
        // maxFaces = 0;
        isTemp = false;
        isPause = false;
        isStart = true;
        // faceNumber = 0;
        gameManager = this;
        isLose = false;
        score = 0;
        // scoreBean = 0;
        // scoreTime = 0;
        // level = 0;
        maxTime = 15f;
        time = maxTime;
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!isStart && !isTemp){
            isTemp = true;
            // Invoke("GameBonus", 3);
            Invoke("SpawnPlanet", Random.Range(0f, 1f));
        }
    }

    void FixedUpdate()
    {
        // if(!isStart){
        //     time -= 0.01f;
        //     if(time <= 0){
        //         time = 0;
        //         if(!isLose){
        //             GameLosing();
        //         }
        //     }
        // }
    }

    // Vector3 RandomInQuadrant1(int idx){
    //     float x, y;
    //     if(idx == 1){
    //         x = UnityEngine.Random.Range(0.5f, (worldPoint.x - 1));
    //         y = UnityEngine.Random.Range(0.5f, (worldPoint.y - 1));
    //     }
    //     else if(idx == 2){
    //         x = UnityEngine.Random.Range(-0.5f, -(worldPoint.x - 1));
    //         y = UnityEngine.Random.Range(0.5f, (worldPoint.y - 1));
    //     }
    //     else if(idx == 3){
    //         x = UnityEngine.Random.Range(-0.5f, -(worldPoint.x - 1));
    //         y = UnityEngine.Random.Range(-0.5f, -(worldPoint.y - 1));
    //     }
    //     else{
    //         x = UnityEngine.Random.Range(0.5f, (worldPoint.x - 1));
    //         y = UnityEngine.Random.Range(-0.5f, -(worldPoint.y - 1));
    //     }

    //     return new Vector3(x,y,0);
    // }

    // Vector3 RandomInQuadrant2(int idx){
    //     float x, y;
    //     if(idx == 1){
    //         x = UnityEngine.Random.Range(0.3f, (worldPoint.x - 0.5f));
    //         y = UnityEngine.Random.Range(0.3f, (worldPoint.y - 0.5f));
    //     }
    //     else if(idx == 2){
    //         x = UnityEngine.Random.Range(-0.3f, -(worldPoint.x - 0.5f));
    //         y = UnityEngine.Random.Range(0.3f, (worldPoint.y - 0.5f));
    //     }
    //     else if(idx == 3){
    //         x = UnityEngine.Random.Range(-0.3f, -(worldPoint.x - 0.5f));
    //         y = UnityEngine.Random.Range(-0.3f, -(worldPoint.y - 0.5f));
    //     }
    //     else{
    //         x = UnityEngine.Random.Range(0.3f, (worldPoint.x - 0.5f));
    //         y = UnityEngine.Random.Range(-0.3f, -(worldPoint.y - 0.5f));
    //     }

    //     return new Vector3(x,y,0);
    // }

    // Vector3 RandomCutter(){
    //     float x, y;
    //     int random = UnityEngine.Random.Range(0, 3);
    //     y = UnityEngine.Random.Range((worldPoint.y - 2f), (worldPoint.y - 2f));
    //     if(random == 0){
    //         x = UnityEngine.Random.Range(1.7f, (worldPoint.x - 2f));
    //     }
    //     else if(random == 1){
    //         x = UnityEngine.Random.Range(-1.7f, -(worldPoint.x - 2f));
    //     }
    //     else{
    //         x = UnityEngine.Random.Range(-1.7f, -(worldPoint.x - 2f));
    //         random = UnityEngine.Random.Range(0, 2);
    //         if(random == 0){
    //             y = UnityEngine.Random.Range(1.7f, (worldPoint.y - 2f));
    //         }
    //         else{
    //             y = UnityEngine.Random.Range(-1.7f, -(worldPoint.y - 2f));
    //         }
    //     }

    //     return new Vector3(x, y, 0);
    // }

    Vector3 RandomPositionPlanet(){
        float x = worldPoint.x + 0.5f;
        float y = Random.Range(-worldPoint.y + 1, worldPoint.y - 1);

        return new Vector3(x,y,0);
    }

    public void GameLosing(){
        CancelInvoke("SpawnPlanet");
        isLose = true;
        ClearListPlanet();
        DisableMainGameObject();
        uiController.GameLosingUI();
    }

    void GameBonus(){
        CancelInvoke("SpawnPlanet");
        isPause = true;
        ClearListPlanet();
        mainPlayer.ResetPosition();
        uiController.BonusTimeUI();
    }

    public void PlayerRevived(){
        isStart = true;
        isTemp = false;
        CancelInvoke("SpawnPlanet");
        ClearListPlanet();
        mainPlayer.Reviving();
    }

    public void ContinueGame(){
        isPause = false;
        uiController.ResetGameUI();
        Invoke("SpawnPlanet", Random.Range(1.5f, 2f));
    }

    void SpawnPlanet(){
        listPlanets.Add(Instantiate(prefabPlanet, RandomPositionPlanet(), Quaternion.identity));
        Invoke("SpawnPlanet", Random.Range(1.5f, 2f));
    }

    void ClearListPlanet(){
        for(int i = 0; i < listPlanets.Count; i++){
            if(listPlanets[i] != null){
                Destroy(listPlanets[i].gameObject);
            }
        }
        listPlanets.Clear();
    }

    public void UpdateScoreUI(Vector3 pos){
        score++;
        uiController.PlusScoreUI(pos);
        if(score % 7 == 0 && score != 0 && Random.Range(0, 2) == 1){
            GameBonus();
        }
        if(score > 7 && score < 13){
            mainSun.absrobForce = 7;
            // mainPlayer.maxMoveScale = 5;
        }
        else if(score > 13 && score < 22){
            mainSun.absrobForce = 13;
            // mainPlayer.maxMoveScale = 7;
        }
        else if(score > 22 && score < 31){
            mainSun.absrobForce = 22;
            // mainPlayer.maxMoveScale = 3;
        }
        else if(score > 31){
            mainSun.absrobForce = 47;
            mainPlayer.maxMoveScale = 5;
        }
    }

    
    public void UpdatePlanetLifeUI(Vector3 pos, int dameBullet){
        // score++;
        uiController.MinusPlanetLifeUI(pos, dameBullet);
        // if(score % 7 == 0 && score != 0){
        //     GameBonus();
        // }
        // if(score == 7){
        //     mainSun.absrobForce = 7;
        // }
    }

    // void NextLevel(){
    //     score++;
    //     level++;
    //     isStart = true;
    //     time = maxTime;
    //     faceNumber = 0;
    // }


    void DisableMainGameObject(){
        for(int i = 0; i < listMainGameObjects.Count; i++){
            listMainGameObjects[i].SetActive(false);
        }
        listMainGameObjects[0].GetComponent<Player>().ClearListBullets();
    }

}
