using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public GameOver gameOver;
    public Text scoreText, orderText;
    public MessageBox messageBox;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreUI();
    }

    public void UpdateTextOrder(string order1, string order2){
        if(order1 == "I'm robber give me all your money"){
            orderText.color = Color.red;
        }
        else{
            orderText.color = Color.black;
        }
        orderText.text = order1 + "\n" + order2;
    }

    void UpdateScoreUI(){
        int score = messageBox.score;
        scoreText.text = $"{score}";
    }

    public void GetGameOver(){
        gameOver.GameOverScene();
    }
}
