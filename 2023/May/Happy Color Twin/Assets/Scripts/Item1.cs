using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item1 : MonoBehaviour
{
    public GameObject beanExpl;

    bool isWarning;
    int negativeNumber;
    public int idBean;
    // Start is called before the first frame update
    void Start()
    {
        isWarning = false;
        negativeNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.gameManager.isPlayerConnecting && !isWarning){
            isWarning = true;
            WarningItem1();
        }
    }

    void WarningItem1(){
        float xScale = transform.localScale.x;
        transform.DOScale(Vector3.one * xScale + Vector3.one * negativeNumber * 0.5f, 0.2f).OnComplete(() =>{
            if(!GameManager.gameManager.isPlayerConnecting){
                negativeNumber = -negativeNumber;
                WarningItem1();
            }
            else{
                transform.localScale = Vector3.one;
                negativeNumber = 1;
            }
        });
    }

    public void DestroyItem1(){
        Destroy(gameObject);
        Destroy(Instantiate(beanExpl, transform.position, Quaternion.identity), 0.3f);
    }

}
