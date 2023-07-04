using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public List<Sprite> listSpriteCustomer;
    public MessageBox messageBox;
    public Image mainBase;
    public Transform playerPos, boxPos;
    Vector3 orgPos;
    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = playerPos.position;
        messageBox.gameObject.transform.position = boxPos.position;
        orgPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        boxPos.position = messageBox.gameObject.transform.position;
    }

    public void NextCustomer()
    {
        transform.DOMoveX(-12, 0.3f).OnComplete(() =>
        {
            ShowUp();
        });
    }

    public void ShowUp()
    {
        int robber = 0;
        if(GameManager.gameManager.customerNum > 3){
            robber = Random.Range(0, 2);
        }
        if(robber == 0){
            transform.localScale = Vector3.zero;
            gameObject.SetActive(true);
            transform.position = orgPos;
            GetComponent<SpriteRenderer>().sprite = listSpriteCustomer[Random.Range(0, listSpriteCustomer.Count - 1)];
            transform.DOScale(Vector3.one, 0.3f).OnComplete(() =>
            {
                messageBox.GetOrder();
                messageBox.ShowTable();
            });
        }
        else{
            transform.localScale = Vector3.zero;
            gameObject.SetActive(true);
            transform.position = orgPos;
            GetComponent<SpriteRenderer>().sprite = listSpriteCustomer[listSpriteCustomer.Count - 1];
            transform.DOScale(Vector3.one, 0.3f).OnComplete(() =>
            {
                messageBox.GetRobber();
                messageBox.HideTable();
            });
        }
    }
}
