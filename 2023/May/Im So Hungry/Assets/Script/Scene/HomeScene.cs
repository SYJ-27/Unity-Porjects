using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour
{
    [SerializeField]
    private GameObject blackScreen, boxHowToPlay, btnPL, btnHTPL, btnClosHTPL, btnHome;

    private Vector3 _originalScale;
    private Vector3 _scaleTo;
    private float time = 0.5f;

    // E:\Unityphancung\2020.3.39f1\Editor\Data\PlaybackEngines\AndroidPlayer\OpenJDK
    public void OnClickPlay()
    {
        _originalScale = transform.localScale;
        _scaleTo = _originalScale * 1.2f;
        btnPL.transform.DOScale(_scaleTo, 0.2f)
            .OnComplete(() =>
            {
                btnPL.transform.DOScale(_originalScale, 0.1f)
                 .OnComplete(() =>
                 {
                     PlayerPrefs.SetInt("IndexLevel", 0);
                     SceneManager.LoadScene("GameScene");
                 });
            });
    }
    public void OnClickHowToPlay()
    {
        _originalScale = transform.localScale;
        _scaleTo = _originalScale * 1.2f;
        btnHTPL.transform.DOScale(_scaleTo, 0.2f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                btnHome.SetActive(true);
                btnHTPL.transform.DOScale(_originalScale, 0.1f);
                boxHowToPlay.transform.localScale = new Vector3(0, 0, 0);
                blackScreen.SetActive(true);
                boxHowToPlay.SetActive(true);
                _originalScale = transform.localScale;
                _scaleTo = _originalScale * 1;
                boxHowToPlay.gameObject.transform.DOScale(_scaleTo, time);
            });

    }
    public void OnClickCloseHowToPlay()
    {
        _originalScale = transform.localScale;
        _scaleTo = _originalScale * 1.2f;
        btnClosHTPL.transform.DOScale(_scaleTo, 0.2f)
            .OnComplete(() =>
            {
                btnClosHTPL.transform.DOScale(_originalScale, 0.1f)
                 .OnComplete(() =>
                 {
                     _originalScale = transform.localScale;
                     _scaleTo = _originalScale * 0;
                     boxHowToPlay.gameObject.transform.DOScale(_scaleTo, time).OnComplete(() =>
                     {
                         blackScreen.SetActive(false);
                         boxHowToPlay.SetActive(false);
                     });
                 });
            });
    }
    public void OnClickCloseBtnHome()
    {
        _originalScale = transform.localScale;
        _scaleTo = _originalScale * 1.2f;
        btnHome.transform.DOScale(_scaleTo, 0.2f)
            .OnComplete(() =>
            {
                btnHome.transform.DOScale(_originalScale, 0.1f)
                 .OnComplete(() =>
                 {
                     btnHome.SetActive(false);
                     _originalScale = transform.localScale;
                     _scaleTo = _originalScale * 0;
                     boxHowToPlay.gameObject.transform.DOScale(_scaleTo, time).OnComplete(() =>
                     {
                         blackScreen.SetActive(false);
                         boxHowToPlay.SetActive(false);
                     });
                 });
            });
    }

}
