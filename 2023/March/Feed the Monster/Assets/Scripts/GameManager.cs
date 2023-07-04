using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;
    public bool isLose = false;
    
    public List<Seed> seedPrefabs;
    public List<Seed> seedSpawnList = new List<Seed>();
    public List<float> seedPosXList = new List<float>();
    public List<float> seedPosXEmptyList = new List<float>();

    public List<SeedOfFood> seedOfFoodPrefabs;
    public SeedOfFood seedFood;

    public List<Soil> listOfSoil;

    public Transform seedsHolder, seedPlantingHolder;

    public List<GameObject> mainObjects;
    
    void Awake(){
        UpdateSeedEmptyList();
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
        for(int i = 0; i < 3; i++){
            SpawnSeed();
        }
        
    }

    public void UpdateScoreUI(){
        uiController.UpdateScoreUI();
    }

    public void GameLosing(){
        DisableAllObjects();
        SetLose();
        uiController.HideScoreUI();
    }

    public void SetLose(){
        isLose = false;
    }

    public void UnSetLose(){
        isLose = true;
    }

    private void SpawnSeed(){
        if(seedPosXEmptyList.Count > 0){
            int typeSeed = Random.Range(0, seedPrefabs.Count);
            var seed = Instantiate(seedPrefabs[typeSeed], new Vector3(seedPosXEmptyList[seedPosXEmptyList.Count - 1] + seedsHolder.position.x,seedsHolder.position.y,0), Quaternion.identity, seedsHolder);
            seedPosXEmptyList.RemoveAt(seedPosXEmptyList.Count - 1);
            seed.InitSeed(typeSeed);
            seedSpawnList.Add(seed);
        }
    }

    private void UpdateSeedEmptyList(){
        for(int i = 0; i < seedPosXList.Count; i++){
            if(!isExistEmptyList(seedPosXList[i])){
                seedPosXEmptyList.Add(seedPosXList[i]);
            }
        }
    }

    private bool isExistEmptyList(float x){
        for(int i = 0; i < seedPosXEmptyList.Count; i++){
            if(x == seedPosXEmptyList[i]){
                return true;
            }
        }
        return false;
    }

    public void AddList(float x){
        seedPosXEmptyList.Add(x - seedsHolder.position.x);
        SpawnSeed();
    }

    public void SetAllUnPick(){
        for(int i = 0; i < seedSpawnList.Count; i++){
            if(seedSpawnList[i] != null){
                seedSpawnList[i].SetIsUnPick(true);
            }
        }
    }

    public void SpawnSeedOfFood(int typeSeed, Vector3 seedPos){
       seedFood = Instantiate(seedOfFoodPrefabs[typeSeed], seedPos, Quaternion.identity, seedPlantingHolder);
       seedFood.transform.DOMove(new Vector3(0, 0, 0), 0.1f);
    }

    public void Choosing(){
        if(seedFood == null){
            for(int i = 0; i < seedSpawnList.Count; i++){
                if(seedSpawnList[i] != null){
                    if(!seedSpawnList[i].isUnPick){
                        seedSpawnList[i].ChoosingSeed();
                    }
                }
            }
        }
    }

    public void UnPickAllSoil(){
        for(int i = 0; i < listOfSoil.Count; i++){
            if(listOfSoil[i] != null){
                listOfSoil[i].SetIsPick(false);
            }
        }
    }

    public void Planting(){
        for(int i = 0; i < listOfSoil.Count; i++){
            if(listOfSoil[i] != null && listOfSoil[i].isPick && seedFood != null && !listOfSoil[i].isPlanted){
                listOfSoil[i].SetIsPlanted(true);
                seedFood.transform.position = listOfSoil[i].transform.position;
                seedFood.SetPlanted(true, i);
                seedFood = null;
            }
        }
    }

    public void ResetSoil(int idx){
        listOfSoil[idx].SetIsPick(true);
        listOfSoil[idx].SetIsPlanted(false);
    }

    public void DisableAllObjects(){
        for(int i = 0; i < mainObjects.Count; i++){
            if(mainObjects[i] != null){
                mainObjects[i].SetActive(false);
            }
        }
    }
    

}
