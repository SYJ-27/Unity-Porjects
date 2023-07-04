using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Player mainPlayer;
    public Ball mainBall;
    public Transform predictBallTrans;
    public Obstacle prefabsObstacle;
    List<Obstacle> listObstacles;
    List<int> listSafeObstacles, listUnSafeObstaclesID, listPlayingObstacles;
    int widthMap, heightMap, playerFirstPos, middleX, middleY;
    Vector3 worldPoint;

    // Start is called before the first frame update
    void Awake()
    {
        listObstacles = new List<Obstacle>();
        listSafeObstacles = new List<int>();
        listPlayingObstacles = new List<int>();
        listUnSafeObstaclesID = new List<int>();
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        widthMap = 14;
        heightMap = 9;
        middleX = widthMap / 2;
        middleY = heightMap / 2;
        playerFirstPos = widthMap + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitObstacleInMap(){
        RandomQuadrant1();
        RandomQuadrant2();
        RandomQuadrant3();
        RandomQuadrant4();
    }

    public int GetFirstPlayerPosition(){
        var rnd = new System.Random();
        List<int> listObstacle2 = listPlayingObstacles.OrderBy(item => rnd.Next()).ToList();
        return listObstacle2[0];
    }

    public void InitGamePlay(){
        CreateMap();
        mainPlayer.SetPosition(listObstacles[GetFirstPlayerPosition()].gameObject.transform.position);
        PredictBallPosition();
        RePositionBall();
    }

    void CreateMap(){
        InitObstacleInMap();
        int idx = 0, safeCount = 1;
        for(int i = 0; i < heightMap; i++){
            for(int j = 0; j < widthMap; j++){
                var obs = Instantiate(prefabsObstacle, new Vector3(j * 0.8f - Mathf.Round(worldPoint.x) + 1,i * 0.8f - 3.2f,0), Quaternion.identity, transform);
                obs.gameObject.name = $"Obstacle {widthMap * (i) + (j)}";
                if(i == 0 || j == 0 || i == heightMap-1 || j == widthMap - 1){
                    obs.InitObstacle(0);
                }
                else{
                    if(!IsUnSafe(widthMap * i + j)){
                        if(safeCount > 0){
                            obs.InitObstacle(0);
                            listUnSafeObstaclesID.Add(widthMap * i + j);
                            safeCount--;
                        }
                        else{
                            obs.InitObstacle(1);
                            listSafeObstacles.Add(idx);
                        }
                    }
                    else{
                        obs.InitObstacle(0);
                    }
                    listPlayingObstacles.Add(idx);
                }
                listObstacles.Add(obs);
                idx++;
            }
        }
    }

    public bool CanMove(Vector3 pos){
        for(int i = 0; i < listObstacles.Count; i++){
            if(listObstacles[i].CheckPos(pos)){
                if(listObstacles[i].idObstacle != 0){
                    return true;
                }
                else{
                    return false;
                }
            }
        }
        return false;
    }

    public void PredictBallPosition(){
        var rnd = new System.Random();
        listSafeObstacles = listSafeObstacles.OrderBy(item => rnd.Next()).ToList();
        Vector3 thisPos = listObstacles[listSafeObstacles[0]].gameObject.transform.position;
        if(mainPlayer.CheckPos(thisPos) || mainBall.CheckPos(thisPos)){
            if(mainPlayer.CheckPos(thisPos) || mainBall.CheckPos(thisPos)){
                thisPos = listObstacles[listSafeObstacles[2]].gameObject.transform.position;
            }
            else{
                thisPos = listObstacles[listSafeObstacles[1]].gameObject.transform.position;
            }
        }
        predictBallTrans.position = thisPos;
        // return thisPos;
    }

    public void RePositionBall(){
        // var rnd = new System.Random();
        // listSafeObstacles = listSafeObstacles.OrderBy(item => rnd.Next()).ToList();
        // Vector3 thisPos = listObstacles[listSafeObstacles[0]].gameObject.transform.position;
        // if(mainPlayer.CheckPos(thisPos)){
        //     thisPos = listObstacles[listSafeObstacles[1]].gameObject.transform.position;
        // }
        Vector3 thisPos = predictBallTrans.position;
        mainBall.SetPos(thisPos);
        ResetPlus(thisPos, 0, 0);
        PredictBallPosition();
    }

    public void ResetPlus(Vector3 pos, int range, int idPlayer){
        if(range == 0){
            Vector3 pos1 = new Vector3(pos.x + 0.8f, pos.y, 0);
            Vector3 pos2 = new Vector3(pos.x, pos.y + 0.8f, 0);
            Vector3 pos3 = new Vector3(pos.x - 0.8f, pos.y, 0);
            Vector3 pos4 = new Vector3(pos.x, pos.y - 0.8f, 0);
            for(int i = 0; i < listObstacles.Count; i++){
                if(listObstacles[i].idObstacle != 0){
                    if(listObstacles[i].CheckPos(pos)){
                        listObstacles[i].SetObstacleID(1);
                    }
                    if(listObstacles[i].CheckPos(pos1)){
                        int rnd = UnityEngine.Random.Range(0,2);
                        if(rnd == 0){
                            listObstacles[i].SetObstacleID(1);
                        }
                    }   
                    if(listObstacles[i].CheckPos(pos2)){
                        int rnd = UnityEngine.Random.Range(0,2);
                        if(rnd == 0){
                            listObstacles[i].SetObstacleID(1);
                        }
                    }   
                    if(listObstacles[i].CheckPos(pos3)){
                        int rnd = UnityEngine.Random.Range(0,2);
                        if(rnd == 0){
                            listObstacles[i].SetObstacleID(1);
                        }
                    }   
                    if(listObstacles[i].CheckPos(pos4)){
                        int rnd = UnityEngine.Random.Range(0,2);
                        if(rnd == 0){
                            listObstacles[i].SetObstacleID(1);
                        }
                    }   
                }
            }
        }
        else{
            for(int i = 0; i < listObstacles.Count; i++){
                if((listObstacles[i].CheckPosX(pos) || listObstacles[i].CheckPosY(pos)) && listObstacles[i].idObstacle != 0 && listObstacles[i].idObstacle != idPlayer){
                    listObstacles[i].SetObstacleID(1);
                }
            }
        }
    }

    bool IsUnSafe(int idx){
        for(int i = 0; i < listUnSafeObstaclesID.Count; i++){
            if(idx == listUnSafeObstaclesID[i]){
                return true;
            }
        }
        return false;
    }

    void RandomQuadrant1(){
        List<int> listQuatdrant = new List<int>();
        for(int i = middleY; i < heightMap - 1; i++){
            for(int j = middleX; j < widthMap - 1; j++){
                Debug.Log(widthMap * (i) + (j));
                listQuatdrant.Add((widthMap * i + j));
            }
        }
        var rnd = new System.Random();
        listQuatdrant = listQuatdrant.OrderBy(item => rnd.Next()).ToList();
        listUnSafeObstaclesID.Add(listQuatdrant[0]);
    }

    void RandomQuadrant2(){
        List<int> listQuatdrant = new List<int>();
        for(int i = middleY; i < heightMap - 1; i++){
            for(int j = 1; j < middleX; j++){
                Debug.Log(widthMap * (i) + (j));
                listQuatdrant.Add((widthMap * i + j));
            }
        }
        var rnd = new System.Random();
        listQuatdrant = listQuatdrant.OrderBy(item => rnd.Next()).ToList();
        listUnSafeObstaclesID.Add(listQuatdrant[0]);
    }

    void RandomQuadrant3(){
        List<int> listQuatdrant = new List<int>();
        for(int i = 1; i < middleY; i++){
            for(int j = 1; j < middleX; j++){
                Debug.Log(widthMap * (i) + (j));
                listQuatdrant.Add((widthMap * i + j));
            }
        }
        var rnd = new System.Random();
        listQuatdrant = listQuatdrant.OrderBy(item => rnd.Next()).ToList();
        listUnSafeObstaclesID.Add(listQuatdrant[0]);
    }

    void RandomQuadrant4(){
        List<int> listQuatdrant = new List<int>();
        for(int i = 1; i < middleY; i++){
            for(int j = middleX; j < widthMap - 1; j++){
                Debug.Log(widthMap * (i) + (j));
                listQuatdrant.Add((widthMap * i + j));
            }
        }
        var rnd = new System.Random();
        listQuatdrant = listQuatdrant.OrderBy(item => rnd.Next()).ToList();
        listUnSafeObstaclesID.Add(listQuatdrant[0]);
    }

}
