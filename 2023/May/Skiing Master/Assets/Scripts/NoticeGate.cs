using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NoticeGate : MonoBehaviour
{
    int numberScale;
    bool isScaling;
    // bool isScaling();

    void Awake(){
        isScaling = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        numberScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.gameManager.isLose && !GameManager.gameManager.isStart && !isScaling){
            isScaling = true;
            Scaling();
        }
    }

    void Scaling(){
        if(!GameManager.gameManager.isLose && !GameManager.gameManager.isStart){
            transform.DOScale(new Vector3(1f + numberScale * 0.1f, 1f + numberScale * 0.1f, 1f + numberScale * 0.1f), 0.3f).SetId("NoticeGate").OnComplete(() =>
            {
                if(!GameManager.gameManager.isLose && !GameManager.gameManager.isStart){
                    numberScale = -numberScale;
                    Scaling();
                }
            });
        }
    }
}
