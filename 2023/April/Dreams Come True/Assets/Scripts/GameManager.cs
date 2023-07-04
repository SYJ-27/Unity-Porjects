using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;
    public bool isLose;
    public List<GameObject> listMainGameObject;
    public int customerNum;
    void Awake(){
        customerNum = 0;
        gameManager = this;
        isLose = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isLose){
            DisableGameObject();
        }
    }

    public void GameLosing(){
        isLose = true;
        DisableGameObject();
        uiController.GetGameOver();
    }

    void DisableGameObject(){
        listMainGameObject[1].GetComponent<MessageBox>().DestroyOrder();
        for(int i = 0; i < listMainGameObject.Count; i++){
            listMainGameObject[i].SetActive(false);
        }
    }

}
