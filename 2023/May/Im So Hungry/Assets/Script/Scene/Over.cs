using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Over : MonoBehaviour
{
    [SerializeField]
    private Text textCoin, textHighScore;
    [SerializeField]
    private GameObject btnHome, btnReplay, puWinOb;

    private Vector3 _originalScale;
    private Vector3 _scaleTo;

    public bool isCheck;
    // Start is called before the first frame update
    void Start()
    {
        isCheck = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateTextCoin(int coin)
    {
        textCoin.text = $"{coin}";
        int highScore = PlayerPrefs.GetInt(" HighScore");
        if (coin > highScore)
        {
            // gameObject.GetComponent<Image>().enabled = false;
            PlayerPrefs.SetInt(" HighScore", coin);
            textHighScore.text = $"{coin}";
            puWinOb.SetActive(true);
        }
        else if (coin <= highScore)
        {
            textHighScore.text = $"{highScore}";
            // gameObject.GetComponent<Image>().enabled = true;
            puWinOb.SetActive(false);
        }
    }

    public void OnClickHome()
    {
        if (isCheck)
        {
            _originalScale = transform.localScale;
            _scaleTo = _originalScale * 1.2f;
            btnHome.transform.DOScale(_scaleTo, 0.2f)
                .OnComplete(() =>
                {
                    btnHome.transform.DOScale(_originalScale, 0.1f)
                     .OnComplete(() =>
                     {
                         isCheck = true;
                         SceneManager.LoadScene("HomeScene");
                     });
                });
            isCheck = false;
        }
    }
    public void OnClickReplay()
    {
        if (isCheck)
        {
            _originalScale = transform.localScale;
            _scaleTo = _originalScale * 1.2f;
            btnReplay.transform.DOScale(_scaleTo, 0.2f)
                .OnComplete(() =>
                {
                    btnReplay.transform.DOScale(_originalScale, 0.1f)
                     .OnComplete(() =>
                     {
                         isCheck = true;
                         SceneManager.LoadScene("GameScene");
                     });
                });
            isCheck = false;
        }

    }
}
