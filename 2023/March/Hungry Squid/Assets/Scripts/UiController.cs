using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Text currentScore;
    public GameObject imgCurrentScore;
    public void UpdateCurrentScore(int score){
        currentScore.text = $"{score}";
    }

    public void ShowCurrentScore(){
        imgCurrentScore.SetActive(true);
    }

    public void HideCurrentScore(){
        imgCurrentScore.SetActive(false);
    }
}
