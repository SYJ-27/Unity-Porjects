using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public GameOver gameOver;
    public Player mainPlayer;
    public List<Image> listLifeCountImg, listBallCountImg;
    public List<Color> listStatusColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGameUI();
    }

    void UpdateGameUI(){
        int ballCount = GameManager.gameManager.ballCount;
        int lifeCount = mainPlayer.lifeCount;
        // for(int i = 0; i < listBallCountImg.Count; i++){
            // if(i < (ballCount % 3)){
            //     listBallCountImg[i].color = listStatusColor[0];
            // }
            // else{
            //     listBallCountImg[i].color = listStatusColor[1];
            // }
            if(ballCount % 3 == 0){
                if(ballCount == 0){
                    listBallCountImg[0].color = listStatusColor[1];
                    listBallCountImg[1].color = listStatusColor[1];
                    listBallCountImg[2].color = listStatusColor[1];
                }
                else{
                    listBallCountImg[0].color = listStatusColor[0];
                    listBallCountImg[1].color = listStatusColor[0];
                    listBallCountImg[2].color = listStatusColor[0];
                }
            }
            if(ballCount % 3 == 1){
                listBallCountImg[0].color = listStatusColor[0];
                listBallCountImg[1].color = listStatusColor[1];
                listBallCountImg[2].color = listStatusColor[1];
            }
            if(ballCount % 3 == 2){
                listBallCountImg[0].color = listStatusColor[0];
                listBallCountImg[1].color = listStatusColor[0];
                listBallCountImg[2].color = listStatusColor[1];
            }
        // }
        for(int i = 0; i < listLifeCountImg.Count; i++){
            if(i < lifeCount){
                listLifeCountImg[i].color = listStatusColor[0];
            }
            else{
                listLifeCountImg[i].color = listStatusColor[1];
            }
        }
        if(lifeCount <= 0 && !GameManager.gameManager.isLose){
            GameManager.gameManager.GameLosing();
        }
    }

    public void GetGameOver(){
        gameOver.GameOverScene();
    }

}
