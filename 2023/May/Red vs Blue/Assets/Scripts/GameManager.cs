using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;
    public Map mainMap;

    public int ballCount;
    public List<GameObject> listMainGameObjects;
    public bool isLose;


    void Awake(){
        gameManager = this;
        ballCount = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        // mainMap.RePositionBall();
        mainMap.InitGamePlay();
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
}
