using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text gameOverText, levelText, bestLevelText, timeText, askText, loserText, reasonDiedText;
    public Text winText, winLevelText, winTimeText, breakLevelText;
    public GameObject replayButton;
    int loserNumber, winNumber;
    void Awake()
    {
        // LOSE
        loserNumber = 1;
        gameOverText.transform.localScale = Vector3.zero;
        levelText.transform.localScale = Vector3.zero;
        bestLevelText.transform.localScale = Vector3.zero;
        timeText.transform.localScale = Vector3.zero;
        askText.transform.localScale = Vector3.zero;
        reasonDiedText.transform.localScale = Vector3.zero;

        // WIN
        winNumber = 1;
        winText.transform.localScale = Vector3.zero;
        breakLevelText.transform.localScale = Vector3.zero;
        winTimeText.transform.localScale = Vector3.zero;

        replayButton.transform.localScale = Vector3.zero;
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

    public void GameOverScene(float timeEndOver, int levelOve, string reasonDied)
    {
        PlayerPrefs.SetInt("Level Current", 0);;
        float time = timeEndOver;
        // float time = GameManager.gameManager.time;
        time = (float)Math.Round(time, 2);
        int level = levelOve;
        // int level = GameManager.gameManager.level;
        int bestLevel = PlayerPrefs.GetInt("Best Level");
        if (bestLevel < level)
        {
            bestLevel = level;
            PlayerPrefs.SetInt("Best Level", bestLevel);
        }

        reasonDiedText.text = reasonDied;
        levelText.text = $"ON LEVEL: {level}";
        bestLevelText.text = $"BEST LEVEL: {bestLevel}";
        timeText.text = $"Time Playing:\n{time}s";

        gameObject.SetActive(true);
        gameOverText.transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
        {
            reasonDiedText.transform.DOScale(Vector3.one, 0.4f).OnComplete(() =>
            {
                levelText.transform.DOScale(Vector3.one, 0.4f).OnComplete(() =>
                {
                    timeText.transform.DOScale(Vector3.one, 0.4f).OnComplete(() =>
                    {
                        // askText.transform.DOScale(Vector3.one, 0.4f).OnComplete(() =>
                        bestLevelText.transform.DOScale(Vector3.one, 0.4f).OnComplete(() =>
                        {
                            ScaleLoserText();
                            replayButton.transform.DOScale(Vector3.one, 0.2f);
                        });
                    });
                });
            });
        });
    }

    void ScaleLoserText()
    {
        if(GameManager.gameManager.isLose){
            reasonDiedText.gameObject.transform.DOScale(new Vector3(1f + loserNumber * 0.1f, 1f + loserNumber * 0.1f, 1f + loserNumber * 0.1f), 0.3f).SetId("ReasonDiedScale").OnComplete(() =>
            {
                if(GameManager.gameManager.isLose){
                    loserNumber = -loserNumber;
                    ScaleLoserText();
                }
            });
        }
    }

    void ScaleWinnerText()
    {
        winText.gameObject.transform.DOScale(new Vector3(1f + winNumber * 0.1f, 1f + winNumber * 0.1f, 1f + winNumber * 0.1f), 0.3f).OnComplete(() =>
        {
            winNumber = -winNumber;
            ScaleWinnerText();
        });
    }

    public void GameWinScene(float timeEndWin, int levelWin)
    {
        float time = timeEndWin;
        // float time = GameManager.gameManager.time;
        time = (float)Math.Round(time, 2);
        int level = levelWin;
        // int level = GameManager.gameManager.level;

        winLevelText.text = $"{level}";
        winTimeText.text = $"In: {time}";

        gameObject.SetActive(true);
        winTimeText.transform.DOScale(Vector3.one, 0.4f).OnComplete(() =>
        {
            breakLevelText.transform.DOScale(Vector3.one, 0.4f).OnComplete(() =>
            {
                winText.transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
                {
                    ScaleWinnerText();
                    replayButton.transform.DOScale(Vector3.one, 0.2f);
                });
            });
        });

    }

    public void ReplayGame()
    {
        replayButton.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.15f).OnComplete(() =>
        {
            replayButton.transform.DOScale(new Vector3(1f, 1f, 1f), 0.15f).OnComplete(() =>
            {
                PlayerPrefs.SetInt("Level Current", 0);
                DOTween.Kill("ReasonDiedScale");
                SceneManager.LoadScene("GamePlay");
            });
        });
    }
}
