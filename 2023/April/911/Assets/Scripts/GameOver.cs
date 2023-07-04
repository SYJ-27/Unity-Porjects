using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class GameOver : MonoBehaviour
{
    public Text loseText, fireText, heartText, sosText, timeText, houseText;
    public GameObject buttonReplay;
    void Awake(){
        transform.DOScale(0, 0f);
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverScene(string reasonLose ,int fire, int heart, int sos, int house, float time){
        loseText.text = reasonLose;
        houseText.text = $"Houses remain: {house}";
        fireText.text = $"{fire}";
        heartText.text = $"{heart}";
        sosText.text = $"{sos}";
        timeText.text = $"in: {time}s";
        gameObject.SetActive(true);
        transform.DOScale(1, 0.3f);
    }

    public void ReplayGame(){
        buttonReplay.transform.DOScale(1.5f, 0.2f).OnComplete(() => {
            buttonReplay.transform.DOScale(1, 0.2f).OnComplete(() => {
                SceneManager.LoadScene("GamePlay");
            });
        });
    }
}
