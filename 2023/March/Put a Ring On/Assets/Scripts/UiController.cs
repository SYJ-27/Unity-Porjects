using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Text textCurrentScore, answerText;
    public GameObject scoreUI, tutorialText, answerBox;
    public GameOver gameOver;
    private int currentScore;
    public float waitTime = 1;
    public bool showMessage = false;
    public string yourMessage;

    // public Transform fingerHand, answerImg;


    void Awake(){
        ResetAnswerBox();
        currentScore = 0;
        textCurrentScore.text = $"{currentScore}";
    }

    void Start(){
        // answerImg.position = fingerHand.position;
    }

    void FixedUpdate()
    {
        if(showMessage){
            ShowMessage();
        }
    }
    public void UpdateScoreUI(){
        // answerText.text = "I DO";
        // InvokeRepeating("ShowMessage", 0, 0.1f);
        yourMessage = "I DO";
        showMessage = true;
        currentScore++;
        textCurrentScore.text = $"{currentScore}";
    }

    public void HideScoreUI(){
        yourMessage = "I DO NOT";
        showMessage = true;
        scoreUI.SetActive(false);
        gameOver.GameOverScene(currentScore);
    }

    public int GetCurrentScore(){
        return currentScore;
    }

    public void HideTutorialText(){
        tutorialText.SetActive(false);
    }

    public void ShowMessage(){
        // answerText.text = message;
        // Debug.Log('1');
        if(answerBox.GetComponent<Image>().fillAmount < 1){
            answerBox.GetComponent<Image>().fillAmount += 0.1f / waitTime;
        }
        else{
            answerText.text = yourMessage;
        }
    }

    public void ResetAnswerBox(){
        showMessage = false;
        yourMessage = "";
        answerText.text = yourMessage;
        answerBox.GetComponent<Image>().fillAmount = 0;
    }

}
