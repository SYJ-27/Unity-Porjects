using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    public Text textTime, textHighTime;
    public float time, highTime;
    public GameObject replayButton;
    public bool isClickReplay;
    public int direction;
    void Awake(){
        direction = 2;
        isClickReplay = false;
        transform.DOScale(Vector3.zero, 0);
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isClickReplay){
            replayButton.transform.Rotate(0,0,direction);
        }
    }

    public void GameOverScene(float timeT){
        time = timeT;
        highTime = PlayerPrefs.GetFloat("HighTime");
        if(time > highTime){
            highTime = time;
            PlayerPrefs.SetFloat("HighTime", highTime);
        }
        textTime.text = $"Time Alived: {time}s";
        textHighTime.text = $"Longest Time: {highTime}s";

        gameObject.SetActive(true);
        transform.DOScale(new Vector3(1,1,1), 0.15f);
    }

    public void Replay(){
        isClickReplay = true;
        replayButton.transform.DOScale(new Vector3(1.5f,1.5f,1.5f), 0.2f).OnComplete(() =>{
            direction = -2;
            replayButton.transform.DOScale(new Vector3(1f,1f,1f), 0.2f).OnComplete(() =>{
                isClickReplay = false;
                direction = 2;
                SceneManager.LoadScene("GamePlay");
            });
        });
    }

}
