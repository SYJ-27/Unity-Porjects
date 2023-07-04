using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;
    public Transform holderTrans, playerTrans, playerFood;

    public bool isLose, isStart, isWin, isNextLevel, isFocus;
    public int level, playMode;
    public float time;
    // public Camera playCamera;
    public Vector3 worldPoint;
    public List<Button> listButtonMoves;

    public Obstacle prefabObstacle;
    public Transform obstacleTrans;
    public Player mainPlayer;


    List<Obstacle> listObstacles;

    public List<GameObject> listMainGameObjects;
    public Transform  wallBottom, GatePos, gateArea;

    void Awake(){
        isStart = true;
        listObstacles = new List<Obstacle>();
        gameManager = this;
        isLose = false;
        isNextLevel = false;
        level = 0;
        time = 0;
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        level = PlayerPrefs.GetInt("Level Current");
        playMode = PlayerPrefs.GetInt("Mode");
    }

    // // Start is called before the first frame update
    void Start()
    {
        SpawnObstacle();
    }

    // // Update is called once per frame
    void Update()
    {
        if(listObstacles.Count > 0 && listObstacles[0] != null){
            if(GatePos.position.y > listObstacles[0].transform.position.y && !isStart && !isLose){
                GatePos.gameObject.SetActive(true);
                // worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
                float x = Mathf.Clamp(listObstacles[0].transform.position.x, playerTrans.position.x -worldPoint.x, playerTrans.position.x + worldPoint.x);
                GatePos.position = new Vector3(x, wallBottom.position.y , 0);
            }
            else{
                GatePos.gameObject.SetActive(false);
                // worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
                float x = Mathf.Clamp(listObstacles[0].transform.position.x, playerTrans.position.x -worldPoint.x, playerTrans.position.x + worldPoint.x);
                GatePos.position = new Vector3(x, wallBottom.position.y , 0);
            }
            gateArea.position = listObstacles[0].transform.position;
            }
        if(!isStart){
            time += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        // if(!isLose){
        //     MoveObstacle();
        // }
    }

    public void SpawnObstacleE(){
        var obstacle = Instantiate(prefabObstacle, RandomInScene(), Quaternion.identity, obstacleTrans);
        obstacle.InitObstacle(UnityEngine.Random.Range(1, 3));
        listObstacles.Add(obstacle);
    }

    void SpawnObstacle(){
        int numberStone = UnityEngine.Random.Range(20, 26);
        for(int i = 0; i < numberStone * 2; i++){
            Obstacle obstacle;
            if(i == 0){
                obstacle = Instantiate(prefabObstacle, RandomInScene(), Quaternion.identity);
                obstacle.name = "Gate";
                obstacle.InitObstacle0();
            }
            else{
                obstacle = Instantiate(prefabObstacle, RandomInScene(), Quaternion.identity, obstacleTrans);
                obstacle.InitObstacle(i);
            }
            listObstacles.Add(obstacle);
        }
        isNextLevel = false;
    }

    void MoveObstacle(){
        float distanceToPlayer = Vector3.Distance(obstacleTrans.position, mainPlayer.transform.position);
        if(distanceToPlayer > 20){
            obstacleTrans.position = mainPlayer.transform.position;
            if(!isNextLevel){
                for(int i = 1; i < listObstacles.Count; i++){
                    if(listObstacles[i] != null){
                        listObstacles[i].RadomPosition();
                    }
                }
            }
            else{
                for(int i = 0; i < listObstacles.Count; i++){
                    if(listObstacles[i] != null){
                        listObstacles[i].RadomPosition();
                    }
                }
            }
        }
        isNextLevel = false;
    }

    void ClearObstacleList(){
        for(int i = 0 ; i < listObstacles.Count; i++){
            if(listObstacles[i] != null){
                Destroy(listObstacles[i].gameObject);
            }
        }
        listObstacles.Clear();
    }

    void AutoMoveObstacles(){
        for(int i = 1; i < listObstacles.Count; i++){
            if(listObstacles[i] != null){
                listObstacles[i].RadomPosition();
            }
        }
    }

    public void NextLevel(){
        if(!isLose){
            DOTween.Kill("NoticeGate");
            DOTween.Kill("MoveButton1");
            DOTween.Kill("MoveButton2");
            DOTween.Kill("MoveButton3");
            // isStart = true;
            // level++;
            // isNextLevel = true;
            // mainPlayer.ResetPosition();
            // obstacleTrans.position = mainPlayer.transform.position;
            // ClearObstacleList();
            // SpawnObstacle();
            level++;
            PlayerPrefs.SetInt("Level Current", level);
            SceneManager.LoadScene("GamePlay");
        }
    }

    Vector3 RandomInScene(){
        float x, y;

        x = UnityEngine.Random.Range(wallBottom.position.x - 3, wallBottom.position.x + 3);
        y = UnityEngine.Random.Range(wallBottom.position.y - 1.5f, wallBottom.position.y + 1.5f);
        x = Mathf.Ceil(x);   
        y = Mathf.Ceil(y);

        return new Vector3(x, y, 0);
    }

    public void GameLosing(string reasonDied){
        isLose = true;
        DOTween.Kill("NoticeGate");
        DOTween.Kill("MoveButton1");
        DOTween.Kill("MoveButton2");
        DOTween.Kill("MoveButton3");
        
        DisableMainGameObject();
        
        uiController.GameLosingUI(time, PlayerPrefs.GetInt("Level Current"), reasonDied);
    }

    void DisableMainGameObject(){
        for(int i = 0; i < listMainGameObjects.Count; i++){
            listMainGameObjects[i].SetActive(false);
        }
        listObstacles[0].gameObject.SetActive(false);
    }

    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        // PlayerPrefs.SetInt("Level Current", 0);
    }

    void OnApplicationFocus(bool focusStatus)
    {
        isFocus = true;
        // playMode = PlayerPrefs.GetInt("Mode");
        if(!isStart){
            PlayerPrefs.SetInt("Level Current", level);
        }
        // PlayerPrefs.SetInt("Level Current")
    }

    /// <summary>
    /// Callback sent to all game objects when the player pauses.
    /// </summary>
    /// <param name="pauseStatus">The pause state of the application.</param>
    void OnApplicationPause(bool pauseStatus)
    {
        isFocus = false;

        if(isStart){
            PlayerPrefs.SetInt("Level Current", 0);
            level = 0;
        }
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    // void OnDestroy()
    // {
    //     PlayerPrefs.SetInt("Level Current", 0);
        
    // }

}
