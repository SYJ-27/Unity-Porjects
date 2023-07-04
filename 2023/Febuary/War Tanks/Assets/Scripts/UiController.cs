using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    // public static UiController mainUi;
    public Text currentScore;
    public GameObject imgCurrentScore;

    public List<GameObject> yourHeart;
    void Awake(){
        // mainUi = this;
    }
    public void UpdateCurrentScore(int score){
        currentScore.text = $"{score}";
    }

    public void ShowCurrentScore(){
        imgCurrentScore.SetActive(true);
    }

    public void HideCurrentScore(){
        imgCurrentScore.SetActive(false);
    }

    public void LoseHeart(int yourLife){
        yourHeart[yourLife].SetActive(false);
    }
}
