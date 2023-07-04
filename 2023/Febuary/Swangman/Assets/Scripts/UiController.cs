using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Text currentScore, answerText, hintText;
    public GameObject imgCurrentScore;

    public void UpdateQuest(string hint, string answer){
        answerText.text = "";
        for(int i = 0; i < answer.Length; i++){
            answerText.text += "_";
        }
        hintText.text = "Hint: " + hint;
    }

    public void UpdateAnswer(string ans){
        answerText.text = ans;
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
    
}
