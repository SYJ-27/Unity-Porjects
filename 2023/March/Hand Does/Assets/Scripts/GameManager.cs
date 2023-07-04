using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;
    public GameOver gameOver;
    public int currentScore, highScore = 0, score = 0;
    public bool isLose = false;

    public List<Road> listOfRoadLevel;
    public List<EndingLine> listOfEndingLineLevel;

    public List<Road> oldRoad = new List<Road>();
    public List<EndingLine> oldEndingLine = new List<EndingLine>();

    public List<int> listOfPassedRoad = new List<int>();
    public int randomLevel;

    public Ball yourBall;
    public Hand yourHand;
    
    
    void Awake(){
        gameManager = this;
    }
    
    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            yourHand.EnableDrag();
        }
    }

    void StartGame(){
        UnSetLose();
        CreatRoad();
        gameOver.HideGameOver();
        currentScore = -1;
        UpdateScoreUI();
    }

    public void UpdateScoreUI(){
        currentScore++;
        uiController.UpdateCurrentScore(currentScore);
    }

    public void GameLose(){
        PlayerPrefs.SetInt("LastLevel", randomLevel);
        isLose = true;
        uiController.HideCurrentScore();
        gameOver.ShowGameOver(currentScore);
    }

    public void CreatRoad(){
        if(currentScore > 0){
            listOfPassedRoad.Add(randomLevel);
            if(currentScore % 5 == 0 && currentScore != 0){
                randomLevel = 0;
            }
            else{
                randomLevel = NewRoadNumber();
            }
        }
        else{
            randomLevel = PlayerPrefs.GetInt("LastLevel");
        }
        
        var yourRoad = Instantiate(listOfRoadLevel[randomLevel], new Vector3(0 , 0, 0), Quaternion.identity);
        yourBall.SetPositionLevel(yourRoad);
        oldRoad.Add(yourRoad);

        var yourEndingLine = Instantiate(listOfEndingLineLevel[randomLevel], new Vector3(0 , 0, 0), Quaternion.identity);
        yourEndingLine.InitPosition();
        oldEndingLine.Add(yourEndingLine);
    }

    public void DestroyAllRoadLine(){
        for(int i= 0; i< oldRoad.Count; i++){
            if(oldRoad[i] != null){
                Destroy(oldRoad[i].gameObject);
            }
        }
        oldRoad.Clear();

        for(int i= 0; i< oldEndingLine.Count; i++){
            if(oldEndingLine[i] != null){
                Destroy(oldEndingLine[i].gameObject);
            }
        }
        oldEndingLine.Clear();
    }

    public int NewRoadNumber(){
        if(listOfPassedRoad.Count == listOfRoadLevel.Count){
            listOfPassedRoad.Clear();
        }
        int random = Random.Range(1, listOfRoadLevel.Count);
        if(IsPassed(random)){
            return NewRoadNumber();
        }
        else{
            return random;
        }
    }

    public bool IsPassed(int x){
        
        for(int i = 0; i < listOfPassedRoad.Count; i++){
            if(x == listOfPassedRoad[i]){
                return true;
            }
        }
        return false;
    }

    public void SetLose(){
        isLose = true;
    }

    public void UnSetLose(){
        isLose = false;
    }

}
