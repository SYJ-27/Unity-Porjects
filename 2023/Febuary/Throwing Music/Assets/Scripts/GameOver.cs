using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text textFinalScore, textHighScore;
    public GameObject imgOver;

    // void Start(){
        
    // }
    
    public void ShowImage(int fScore, int hScore){
        imgOver.SetActive(true);
        UpdateFinalScore(fScore);
        UpdateHighScore(hScore);
    }

    public void HideImage(){
        imgOver.SetActive(false);
    }

    public void UpdateFinalScore(int score){
        textFinalScore.text = $"{score}";
    }

    public void UpdateHighScore(int hscore){
        textHighScore.text = $"{hscore}";
    }

}
