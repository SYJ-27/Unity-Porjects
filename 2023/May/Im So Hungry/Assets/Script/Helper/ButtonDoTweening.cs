using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ButtonDoTweening : MonoBehaviour
{
    public static ButtonDoTweening btnScale;

    private Vector3 _originalScale;
    private Vector3 _scaleTo;
    private float time = 0.2f;

    // Start is called before the first frame update
    void Awake()
    {
    }
    void Start()
    {
        btnScale = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void test()
    {
        Debug.Log("ok");
    }

    // Khong co hanh dong ben trong
    public void OnScaleBtnUiInaction(GameObject btn, float scaleBtn)
    {
        _originalScale = transform.localScale;
        _scaleTo = _originalScale * scaleBtn;
        btn.transform.DOScale(_scaleTo, time)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                btn.transform.DOScale(_originalScale, 0.1f);
                /* .SetEase(Ease.OutBounce)*/
                /*      .SetDelay(1.0f);*/
                /*  .OnComplete(OnScale);*/
            });
    }

    //btn Load Scene
    public void OnScaleBtnLoadScene(GameObject btn, float scaleBtn, bool bl)
    {
        _originalScale = transform.localScale;
        _scaleTo = _originalScale * scaleBtn;
        btn.transform.DOScale(_scaleTo, time)
            .OnComplete(() =>
            {
                btn.transform.DOScale(_originalScale, 0.1f)
                 .OnComplete(() =>
                 {
                     if (bl == true)
                     {
                         SceneManager.LoadScene("GameScene");
                     }
                     else if (bl == false)
                     {
                         SceneManager.LoadScene("HomeScene");
                     }
                 });
            });
    }


}
