using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;

    public bool isLose, isStart, isWin, isNextLevel;
    public int level;
    public float time, maxTime;

    Vector3 worldPoint;

    public Glass prefabGlass;
    public Transform mapTrans;
    public Player mainPlayer;
    public PlayerDad mainPlayerDad;
    public Saw prefabSaw;
    public Stone prefabStone;


    List<Glass> listGlasses;
    List<Stone> listStones;
    List<Saw> listSaws;
    Vector2 glassSize;

    public List<GameObject> listMainGameObjects;
    public List<Transform> listWallTrans;

    void Awake(){
        listGlasses = new List<Glass>();
        listSaws = new List<Saw>();
        listStones = new List<Stone>();
        isStart = true;
        gameManager = this;
        isLose = false;
        isWin = false;
        isNextLevel = false;
        level = 0;
        maxTime = 15f;
        time = 0;
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        glassSize.x = prefabGlass.GetComponent<SpriteRenderer>().bounds.size.x;
        glassSize.y = prefabGlass.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateMap();
        // Invoke("SpawnSaw", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckWin() && !isNextLevel && !isLose && !isWin){
            // GameWin();
            NextLevel();
        }
        if(!isStart){
            time += Time.deltaTime;
            TimeCountDown.timeCountDown.TimeLeft = time;
        }
    }

    void FixedUpdate()
    {

    }

    bool CheckWin(){
        for(int i = 0; i < listGlasses.Count; i++){
            if(!listGlasses[i].isClean()){
                return false;
            }
        }
        return true;
    }

    Vector3 RandomInQuadrant1(int idx){
        float x, y;
        if(idx == 1){
            x = UnityEngine.Random.Range(0.5f, (worldPoint.x - 1));
            y = UnityEngine.Random.Range(0.5f, (worldPoint.y - 1));
        }
        else if(idx == 2){
            x = UnityEngine.Random.Range(-0.5f, -(worldPoint.x - 1));
            y = UnityEngine.Random.Range(0.5f, (worldPoint.y - 1));
        }
        else if(idx == 3){
            x = UnityEngine.Random.Range(-0.5f, -(worldPoint.x - 1));
            y = UnityEngine.Random.Range(-0.5f, -(worldPoint.y - 1));
        }
        else{
            x = UnityEngine.Random.Range(0.5f, (worldPoint.x - 1));
            y = UnityEngine.Random.Range(-0.5f, -(worldPoint.y - 1));
        }

        return new Vector3(x,y,0);
    }

    Vector3 RandomInQuadrant2(int idx){
        float x, y;
        if(idx == 1){
            x = UnityEngine.Random.Range(0.3f, (worldPoint.x - 0.5f));
            y = UnityEngine.Random.Range(0.3f, (worldPoint.y - 0.5f));
        }
        else if(idx == 2){
            x = UnityEngine.Random.Range(-0.3f, -(worldPoint.x - 0.5f));
            y = UnityEngine.Random.Range(0.3f, (worldPoint.y - 0.5f));
        }
        else if(idx == 3){
            x = UnityEngine.Random.Range(-0.3f, -(worldPoint.x - 0.5f));
            y = UnityEngine.Random.Range(-0.3f, -(worldPoint.y - 0.5f));
        }
        else{
            x = UnityEngine.Random.Range(0.3f, (worldPoint.x - 0.5f));
            y = UnityEngine.Random.Range(-0.3f, -(worldPoint.y - 0.5f));
        }

        return new Vector3(x,y,0);
    }

    Vector3 RandomCutter(){
        float x, y;
        int random = UnityEngine.Random.Range(0, 3);
        y = UnityEngine.Random.Range((worldPoint.y - 2f), (worldPoint.y - 2f));
        if(random == 0){
            x = UnityEngine.Random.Range(1.7f, (worldPoint.x - 2f));
        }
        else if(random == 1){
            x = UnityEngine.Random.Range(-1.7f, -(worldPoint.x - 2f));
        }
        else{
            x = UnityEngine.Random.Range(-1.7f, -(worldPoint.x - 2f));
            random = UnityEngine.Random.Range(0, 2);
            if(random == 0){
                y = UnityEngine.Random.Range(1.7f, (worldPoint.y - 2f));
            }
            else{
                y = UnityEngine.Random.Range(-1.7f, -(worldPoint.y - 2f));
            }
        }

        return new Vector3(x, y, 0);
    }

    Vector3 GetRandomPositionInMap(){
        float x = UnityEngine.Random.Range(GetMaxLeft() + 2, GetMaxRight() - 2);
        float y = UnityEngine.Random.Range(GetMaxBottom() + 2, GetMaxTop() - 2);

        return new Vector3(x,y,0);
    }

    void SpawnSaw(){
        if(!isStart){
            listSaws.Add(Instantiate(prefabSaw, GetRandomPositionInMap(), Quaternion.identity));
        }
        if(level != 0){
            if(level <= 5 && listSaws.Count < 3){
                Invoke("SpawnSaw", UnityEngine.Random.Range(3, 4f));
            }
            else if(level > 5 && level <= 13 && listSaws.Count < 5){
                Invoke("SpawnSaw", UnityEngine.Random.Range(3, 4f));
            }
            else if(level > 13 && listSaws.Count < 10){
                Invoke("SpawnSaw", UnityEngine.Random.Range(3, 4f));
            }
        }
    }

    public void GameLosing(){
        CancelInvoke("SpawnSaw");
        ClearGrassList();
        ClearSawList();
        ClearStoneList();
        DisableMainGameObject();
        int highLevel = PlayerPrefs.GetInt("Best Level");
        if(level > highLevel){
            PlayerPrefs.SetInt("Best Level", level);
            GameWin();
        }
        else{
            isLose = true;
            uiController.GameLosingUI();
        }
    }

    public void GameWin(){
        isWin = true;
        uiController.GameWinUI();
    }

    void ClearGrassList(){
        for(int i = 0; i < listGlasses.Count; i++){
            // if(listGlasses[i] != null)
            Destroy(listGlasses[i].gameObject);
        }
        listGlasses.Clear();
    }

    void ClearSawList(){
        for(int i = 0; i < listSaws.Count; i++){
            if(listSaws[i] != null)
                Destroy(listSaws[i].gameObject);
        }
        listSaws.Clear();
    }

    void ClearStoneList(){
        for(int i = 0; i < listStones.Count; i++){
            // if(listStones[i] != null)
            Destroy(listStones[i].gameObject);
        }
        listStones.Clear();
    }

    public void ResetPlayerPosition(){
        var rnd = new System.Random();
        List<Glass> listGlassTemp = listGlasses.OrderBy(item => rnd.Next()).ToList();
    
        mainPlayer.SetPosition(listGlassTemp[0].transform.position);
        mainPlayerDad.SetPosition(listGlassTemp[1].transform.position);
        int idxStone = 2;
        int numberStone = 0;
        if(level < 5){
            numberStone = UnityEngine.Random.Range(3, 5);
        }
        else if(level >= 5 && level < 10){
            numberStone = UnityEngine.Random.Range(3, 10);
        }
        else if(level >= 10){
            numberStone = UnityEngine.Random.Range(5, 10);
        }
        for(int i = 0; i < numberStone; i++){
            listGlassTemp[idxStone].DisableGrass();
            listStones.Add(Instantiate(prefabStone, listGlassTemp[idxStone].transform.position, Quaternion.identity));
            idxStone++;
        }
    }

    void CreateMap(){        
        int numHeight, numWidth;
        float startX = -Mathf.Round(worldPoint.x) + glassSize.x * 2;
        float startY = Mathf.Round(worldPoint.y) - glassSize.y * 2;
        numHeight = 12;
        numWidth = (int)Mathf.Floor((worldPoint.x + 3.5f) / (glassSize.y));
        int numberGlass = 0;
        for(int i = 0; i < numHeight; i++){
            for(int j = 0; j < numWidth; j++){
                numberGlass++;
                listGlasses.Add(Instantiate(prefabGlass, new Vector3((float)Math.Round(startX + j * glassSize.y, 2), (float)Math.Round(startY - i * glassSize.x, 2), 0),Quaternion.identity, mapTrans));
            }
        }
        // Debug.Log(listGlasses[0].transform.position + " == " + listGlasses[listGlasses.Count - 1].transform.position);

        ResetPlayerPosition();
        SetWallPlaying();
        SpawnSaw();
    }

    public float GetMaxTop(){
        return listGlasses[0].transform.position.y;
    }

    public float GetMaxLeft(){
        return listGlasses[0].transform.position.x;
    }

    public float GetMaxRight(){
        return listGlasses[listGlasses.Count - 1].transform.position.x;
    }

    public float GetMaxBottom(){
        return listGlasses[listGlasses.Count - 1].transform.position.y;
    }

    public float GetBoxSize(){
        return glassSize.x;
    }

    public void SetWallPlaying(){
        listWallTrans[0].position = new Vector3(0, GetMaxTop() + GetBoxSize(), 0);
        listWallTrans[1].position = new Vector3(GetMaxLeft() - GetBoxSize(), 0, 0);
        listWallTrans[2].position = new Vector3(GetMaxRight() + GetBoxSize(), 0, 0);
        listWallTrans[3].position = new Vector3(0, GetMaxBottom() - GetBoxSize(), 0);
    }

    void NextLevel(){
        CancelInvoke("SpawnSaw");
        ClearStoneList();
        ClearGrassList();
        ClearSawList();
        isNextLevel = false;
        level++;
        isStart = true;
        CreateMap();
    }

    void DisableMainGameObject(){
        for(int i = 0; i < listMainGameObjects.Count; i++){
            listMainGameObjects[i].SetActive(false);
        }
    }


}
