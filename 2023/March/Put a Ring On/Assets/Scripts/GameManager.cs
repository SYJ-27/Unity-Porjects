using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;
    public bool isLose = false, isPlaying;

    public List<Gift> giftPrefabs;
    public Balloon balloonPrefab;
    
    public List<Gift> listOfGift = new List<Gift>();
    public List<Balloon> listOfBalloon  = new List<Balloon>();

    public int maxGift, maxBalloon;

    public GameObject mainTable;
    public HandRing mainHandRing;
    public Hand mainHand;
    public Transform fingerHand, answerImg;
    public List<GameObject> listOfGameObject;
    
    void Awake(){
        isPlaying = false;
        mainTable.SetActive(false);
        UnSetLose();
        gameManager = this;
        maxGift = 5;
        maxBalloon = 3;
    }
    
    void Start()
    {
        
        StartGame();
    }

    void Update()
    {
        answerImg.position = fingerHand.position;
        if(Input.GetMouseButtonDown(0)){
            mainHandRing.SetIsMove(true);
            isPlaying = true;
            uiController.HideTutorialText();
        }
        if(uiController.GetCurrentScore() > 10){
            maxGift = 7;
            maxBalloon = 5;
        }
        if(!mainHandRing.isMove){
            isPlaying = false;
        }
    }

    void StartGame(){
        NewLevel();
    }

    public void UpdateScoreUI(){
        uiController.UpdateScoreUI();
        Invoke("NewLevel", 1);
    }

    public void GameLosing(){
        mainHandRing.SetCanMove(false);
        ClearListGiftBalloon();
        VisibleGameOjects();
        SetLose();
        uiController.HideScoreUI();
    }

    public void SetLose(){
        isLose = false;
    }

    public void UnSetLose(){
        isLose = true;
    }

    private void SpawnBalloon(){
        var balloonSpawned = Instantiate(balloonPrefab, transform.position, Quaternion.identity);
        balloonSpawned.InitPosition();
        listOfBalloon.Add(balloonSpawned);
    }

    private void BalloonSpawning(){
        if(uiController.GetCurrentScore() > 0 ){
            int randomNumber = Random.Range(3, maxBalloon);
            for(int i = 0; i < randomNumber; i++){
                SpawnBalloon();
            }
        }
    }

    private void SpawnGift(){
        var giftSpawned = Instantiate(giftPrefabs[Random.Range(0, giftPrefabs.Count)], transform.position, Quaternion.identity);
        giftSpawned.InitPosition();
        listOfGift.Add(giftSpawned);
    }

    private void GiftSpawning(){
        if(uiController.GetCurrentScore() > -1 && uiController.GetCurrentScore() != 1){
            int randomNumber = Random.Range(3, maxGift);
            for(int i = 0; i < randomNumber; i++){
                SpawnGift();
            }
        }
    }

    private void ClearListGiftBalloon(){
        for(int i = 0; i < listOfBalloon.Count; i++){
            if(listOfBalloon[i] != null){
                Destroy(listOfBalloon[i].gameObject);
            }
        }
        listOfBalloon.Clear();

        for(int i = 0; i < listOfGift.Count; i++){
            if(listOfGift[i] != null){
                Destroy(listOfGift[i].gameObject);
            }
        }
        listOfGift.Clear();


    }

    public void NewLevel(){
        mainHand.ResetHand();
        uiController.ResetAnswerBox();
        ClearListGiftBalloon();
        if(uiController.GetCurrentScore() == 3){
            SetActiveTable();
        }
        GiftSpawning();
        BalloonSpawning();
        mainHandRing.SetCanMove(true);
    }

    private void SetActiveTable(){
        mainTable.SetActive(true);
    }

    private void VisibleGameOjects(){
        for(int i = 0; i < listOfGameObject.Count; i++){
            if(listOfGameObject[i] != null){
                listOfGameObject[i].SetActive(false);
            }
        }
    }

    public int GetCurrentScore(){
        return uiController.GetCurrentScore();
    }

}
