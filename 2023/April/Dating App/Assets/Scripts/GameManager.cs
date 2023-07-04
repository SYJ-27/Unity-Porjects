using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public SuitUpScene suitUpScene;
    public UiController uiController;
    public DateFriend mainDateFriend;
    public GameContinue gameContinue;

    public List<GameObject> listMainGameObjects;
    public bool isLose;
    public int score;

    void Awake(){
        score = 0;
        isLose = false;
        gameManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameLosing(){
        isLose = true;
        for(int i = 0; i < listMainGameObjects.Count; i++){
            listMainGameObjects[i].SetActive(false);
        }
        uiController.GetGameOver();
    }

    public void GetSuitUpScene(){
        score++;
        mainDateFriend.timeQuest = 2;
        gameContinue.GameContinueScene();
        // HarderTimer();
    }

    public void DoSuitUpScene(){
        suitUpScene.gameObject.SetActive(true);
        uiController.DisableGameUI();
        mainDateFriend.ResetDateFriend();
    }

    public void HarderTimer(){
        if(score % 5 == 0 && score != 0){
            mainDateFriend.timeQuest -= 0.5f;
            mainDateFriend.timeQuest = Mathf.Clamp(mainDateFriend.timeQuest ,0.5f, 2f);
        }
    }

}
