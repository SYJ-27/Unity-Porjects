using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text textScore, textCountdown;
    public GameObject imgScore, imgCountdown;
    void Start(){
        // Test.count = 4;
        // Debug.Log(Test.count);
    }
    public void UpdateScore(int score){
        textScore.text = $"{score}";
    }

    public void UpdateCountdown(int countTime){
        textCountdown.text = $"{countTime}";
    }

    public void HideImage(){
        imgScore.SetActive(false);
        imgCountdown.SetActive(false);
    }

    public void ShowImage(){
        imgScore.SetActive(true);
        imgCountdown.SetActive(true);
    }
}
