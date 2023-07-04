using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;

    public List<Area> listAreas;
    public List<GameObject> listMainGameObjects;
    public bool isLose;
    public int fireSaved, heartSaved, sosSaved;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = this;
        fireSaved = 0;
        heartSaved = 0;
        sosSaved = 0;
        isLose = false;        
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckAreaAllDestroyed() && !isLose){
            GameLosing("All House Destroyed");
        }
    }

    public void GameLosing(string reasonLose){
        isLose = true;
        int houseRemain = HouseRemain();
        DisableMainGameObject();
        uiController.GetGameOverScene(reasonLose, fireSaved, heartSaved, sosSaved, houseRemain);
        ClearArea();
    }

    int HouseRemain(){
        int houseCount = 0;
        for(int i = 0; i < listAreas.Count; i++){
            houseCount += listAreas[i].GetHouseRemain();
        }
        return houseCount;
    }

    public bool CheckAreaAllDestroyed(){
        for(int i = 0; i < listAreas.Count; i++){
            if(!listAreas[i].CheckAllHouseDestroyed()){
                return false;
            }
        }
        return true;
    }

    public void DisableMainGameObject(){
        for(int i = 0; i < listMainGameObjects.Count; i++){
            if(listMainGameObjects[i] != null){
                listMainGameObjects[i].SetActive(false);
            }
        }
    }

    public void ClearArea(){
        for(int i = 0; i < listAreas.Count; i++){
            if(listAreas[i] != null){
                listAreas[i].AllClearHouse();
            }
        }
    }

}
