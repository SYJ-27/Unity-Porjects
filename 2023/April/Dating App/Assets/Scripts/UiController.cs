using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public GameObject gameUI;
    public GameOver gameOver;
    public List<GameObject> listHearts;
    public Player mainPlayer;
    // Start is called before the first frame update
    void Start()
    {
        gameUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHeartUI();
    }

    void UpdateHeartUI(){
        for(int i = 0; i < listHearts.Count; i++){
            if(i < mainPlayer.heartNumber){
                listHearts[i].SetActive(true);
            }
            else{
                listHearts[i].SetActive(false);
            }
        }
    }

    public void GetGameOver(){
        gameOver.GameOverScene();
    }

    public void ActiveGameUI(){
        gameUI.SetActive(true);
    }

    public void DisableGameUI(){
        gameUI.SetActive(false);
    }
}
