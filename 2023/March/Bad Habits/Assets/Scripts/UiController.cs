using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Text textCurrentScore;
    public GameObject imgCurrentScore;
    public int currentScore;
    public GameOver gameOver;
    void Awake(){
        currentScore = 0;
        textCurrentScore.text = $"{currentScore}";
        gameOver.HideGameOver();
    }
    public void UpdateCurrentScore(){
        currentScore++;
        textCurrentScore.text = $"{currentScore}";
    }
    public void HideCurrentScore(){
        imgCurrentScore.SetActive(false);
        gameOver.ShowGameOver(currentScore);
    }
}
