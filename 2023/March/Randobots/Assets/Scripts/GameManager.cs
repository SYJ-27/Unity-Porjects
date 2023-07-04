using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;
    public bool isLose = false;

    public Transform gridMap;
    public GameObject stonePrefabs;
    public List<List<Vector3>> listMapRow = new List<List<Vector3>>();
    public float changeTime = 10;

    public List<GameObject> listGameObject;
    public List<GameObject> listStone;

    
    void Awake(){
        UnSetLose();
        gameManager = this;
    }
    
    void Start()
    {
        StartGame();
    }

    void Update()
    {
    }

    void StartGame(){
        InvokeRepeating("SpawnMap", 0, changeTime);
    }

    // public void UpdateScoreUI(){
    //     uiController.UpdateScoreUI();
    // }

    public void GameLosing(){
        StopTheGamePlay();
        SetLose();
        uiController.HideScoreUI();
    }

    public void SetLose(){
        isLose = true;
    }

    public void UnSetLose(){
        isLose = false;
    }

    public void SpawnMap(){
        ClearStonePosition();
        InitMapVector3();
        int randomStoneNumber = Random.Range(30, 40);
        int count = 0;
        for(int i = 0; i < listMapRow.Count; i++){
            for(int j = 0; j < listMapRow[i].Count; j++){
                int random = Random.Range(0, 10);
                if(random > 7)
                {
                    count++;
                    listStone.Add(Instantiate(stonePrefabs, listMapRow[i][j], Quaternion.identity, gridMap));
                    if(count == randomStoneNumber){
                        return;
                    }
                }
                
            }
        }
    }

    public void InitMapVector3(){
        ClearMapPostion();
        float x, y = 3.2f;
        for(int i = 0;  i < 9; i++){
            x = -6f;
            List<Vector3> listMapColumn = new List<Vector3>();
            listMapRow.Add(listMapColumn);
            for(int j = 0;  j  < 16; j++){
                listMapColumn.Add(new Vector3(x, y, 0));
                x += 0.8f;
            }
            y -= 0.8f;
        }
    }

    public void ClearMapPostion(){
        for(int i = 0; i < listMapRow.Count; i++){
            listMapRow[i].Clear();
        }
        listMapRow.Clear();
    }

    public void StopTheGamePlay(){
        CancelInvoke("SpawnMap");
        for(int i = 0; i < listGameObject.Count; i++){
            listGameObject[i].SetActive(false);
        }
    }

    public void ClearStonePosition(){
        for(int i = 0; i < listStone.Count; i++){
            Destroy(listStone[i]);
        }
        listStone.Clear();
    }


}
