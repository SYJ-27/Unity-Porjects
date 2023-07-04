using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    public Text textScore, textClear;
    public int score, clear;
    public Transform replayButton;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(new Vector3(0f,0f,0f), 0);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverScene(){
        gameObject.SetActive(true);
        score = GameManager.gameManager.score;
        clear = GameManager.gameManager.clearScore;
        textScore.text = $"Score: {score}";
        textClear.text = $"Clear: {clear}";
        transform.DOScale(new Vector3(1f,1f,1f), 0.15f);
    }

    public void Replay(){
        replayButton.DOScale(new Vector3(1.3f,1.3f,1.3f), 0.15f).OnComplete(() =>{
            replayButton.DOScale(new Vector3(1f,1f,1f), 0f).OnComplete(() =>{
                SceneManager.LoadScene("GamePlay");
            });
        });

    }
}
