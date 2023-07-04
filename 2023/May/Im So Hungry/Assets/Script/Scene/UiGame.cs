using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UiGame : MonoBehaviour
{
    public static UiGame Ui;

    public GameObject blackScreen, btnHome, btnReplay, boxCoin;
    [SerializeField]
    private Text textCoin;
    [SerializeField]
    private GameObject[] heart;

    public Over over;
    private Vector3 _originalScale;
    private Vector3 _scaleTo;

    void Awake()
    {
        Ui = this;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpHeart(int index, bool bl)
    {
        if (index >= 0)
        {
            heart[index].SetActive(bl);
        }

    }

    public void OnClickHome()
    {
        _originalScale = transform.localScale;
        _scaleTo = _originalScale * 1.2f;
        btnHome.transform.DOScale(_scaleTo, 0.2f)
            .OnComplete(() =>
            {
                btnHome.transform.DOScale(_originalScale, 0.1f)
                 .OnComplete(() =>
                 {
                     SceneManager.LoadScene("HomeScene");
                 });
            });
    }
    public void OnClickReplay()
    {
        _originalScale = transform.localScale;
        _scaleTo = _originalScale * 1.2f;
        btnReplay.transform.DOScale(_scaleTo, 0.2f)
            .OnComplete(() =>
            {
                btnReplay.transform.DOScale(_originalScale, 0.1f)
                 .OnComplete(() =>
                 {
                     SceneManager.LoadScene("GameScene");
                 });
            });
    }
    public void UpdateTextCoin(int coin)
    {
        textCoin.text = $"{coin}";
        _originalScale = transform.localScale;
        _scaleTo = _originalScale * 1.5f;
        textCoin.transform.DOScale(_scaleTo, 0.2f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                textCoin.transform.DOScale(_originalScale, 0.1f);
            });
    }
    public void showPuOver(int coin)
    {
        btnHome.SetActive(false);
        btnReplay.SetActive(false);
        boxCoin.SetActive(false);
        over.UpdateTextCoin(coin);
        over.transform.localScale = new Vector3(0, 0, 0);
        blackScreen.SetActive(true);
        over.transform.gameObject.SetActive(true);
        _originalScale = transform.localScale;
        _scaleTo = _originalScale * 1;
        over.transform.gameObject.transform.DOScale(_scaleTo, 0.5f);
    }
}
