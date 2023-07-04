using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MessageBox : MonoBehaviour
{
    public UiController uiController;
    public List<Transform> listOrders;
    public List<GameObject> listTypeOrders, listGameOrder;
    public List<ObjectOrder> listObjectOrder;
    public Player mainPlayer;
    List<int> listOrderTypeID;
    List<string> listOrder1, listOrder2, listOrder3, listOrder4;
    List<List<string>> listStringOrders;
    public int score, foodNum, foodGetCount, numberOrder;
    public GameObject checkButton, robberButton, robberExplosion;
    Vector3 robberBtnPos, orgPosTable;
    public bool isGetRobber;
    public Transform baseTable, boxTrans;


    void Awake(){
        orgPosTable = baseTable.position;
        isGetRobber = false;
        checkButton.SetActive(false);
        robberButton.SetActive(false);
        foodNum = 3;
        foodGetCount = 0;
        score = 0;
        listOrderTypeID = new List<int>(){-1, -1, -1};
        listGameOrder = new List<GameObject>(){null, null, null};
        listObjectOrder = new List<ObjectOrder>();
        listOrder1 = new List<string>(){"I want something made of poultry and can be held!",
                                        "I don't have a lover!",
                                        "Is there anything that can help me get out of this heat?",
                                        "It's rice but there's no bowl? I want to try it!",
                                        "What do Americans like to eat? It looks delicious!",
                                        "I'm not hungry but I'm thirsty"};
        listOrder2 = new List<string>(){"Is there something cheaper here than at McDonald's?",
                                        "Neat! I gonna die alone!",
                                        "Oh my goodness! Am i flying around the sun right?",
                                        "I want something like Korean food!",
                                        "I just want a slide of cake!",
                                        "I just finished playing football and I feel like I'm missing something!"};
        listOrder3 = new List<string>(){"I want to eat at KFC but it's too expensive!",
                                        "I want to grab the spoon",
                                        "Can you give me what I need or do I have to go to the Mixue?",
                                        "Hand held rice? That's good!",
                                        "Is that pizza?",
                                        "My friend bought 3 hamburgers and he didn't buy water!"};
        listOrder4 = new List<string>(){"I'm craving for fried food, can I have something to eat?",
                                        "I feel lonely at school, I have no friends.",
                                        "It would be nice to eat something cool right now.",
                                        "The doctor told me to add more starch.",
                                        "I want to eat bread or something like that.",
                                        "My combo is missing a drink!"};
        listStringOrders = new List<List<string>>();
        listStringOrders.Add(listOrder1);
        listStringOrders.Add(listOrder2);
        listStringOrders.Add(listOrder3);
        listStringOrders.Add(listOrder4);
        
        boxTrans.localScale = Vector3.zero;
        mainPlayer.gameObject.transform.localScale = Vector3.zero;

    }

    void Start()
    {
        robberBtnPos = robberButton.transform.position;
        Invoke("GetCustomer", 0.5f);
    }

    void Update()
    {
        
    }

    public void GetOrder(){
        boxTrans.DOScale(new Vector3(1f, 1, 0), 0.1f).OnComplete(() =>{
            if(!GameManager.gameManager.isLose){
                int order1ID = -1, order2ID = -1;
                numberOrder = Random.Range(1, 3);
                for(int i = 0; i < numberOrder; i++){
                    int randomID = Random.Range(0, listTypeOrders.Count);
                    listOrderTypeID[i] = randomID;
                    if(i == 0){
                        order1ID = randomID;
                    }
                    else{
                        order2ID = randomID;
                    }
                }
                string order1Str = listStringOrders[Random.Range(0, listStringOrders.Count)][order1ID];
                string order2Str;
                if(order2ID != -1){
                    order2Str = listStringOrders[Random.Range(0, listStringOrders.Count)][order2ID];
                }
                else{
                    order2Str = "";
                }
                uiController.UpdateTextOrder(order1Str, order2Str);
                checkButton.SetActive(true);
                TimeCountDown.timeCountDown.TimerOn = true;
                foodGetCount = 0;
            }
        });
    }

    public void GetRobber(){
        robberButton.transform.DOMoveY(robberBtnPos.y + 5, 0).OnComplete(() =>{
            isGetRobber = true;
            Debug.Log("Robber");
            boxTrans.DOScale(new Vector3(1f, 1, 0), 0.1f).OnComplete(() =>{
                if(!GameManager.gameManager.isLose){
                    uiController.UpdateTextOrder("I'm robber give me all your money", "");
                    robberButton.SetActive(true);
                    foodGetCount = 0;
                    robberButton.transform.DOMoveY(robberBtnPos.y, 0.5f).OnComplete(() =>{
                        Invoke("GetCustomer", Random.Range(0.7f, 1f));
                    });
                }
            });
        });
    }

    public void DestroyOrder(){
        for(int i = 0; i < listObjectOrder.Count; i++){
            if(listObjectOrder[i] != null){
                Destroy(listObjectOrder[i].gameObject);
            }
        }
        listObjectOrder.Clear();
        for(int i = 0; i < listGameOrder.Count; i++){
            if(listGameOrder[i] != null){
                Destroy(listGameOrder[i]);
            }
            listGameOrder[i] = null;
        }
        uiController.UpdateTextOrder("", "");
    }

    bool IsInListOrder(int id){
        for(int i = 0; i < listOrderTypeID.Count; i++){
            if(id == listOrderTypeID[i]){
                listOrderTypeID[i] = -1;
                return true;
            }
        }
        return false;
    }

    public void CalculateScore(){
        int tempScore = score;
        checkButton.SetActive(false);
        if(listObjectOrder.Count < numberOrder){
            score --;
        }
        for(int i = 0; i < listObjectOrder.Count; i++){
            if(IsInListOrder(listObjectOrder[i].GetComponent<ObjectOrder>().typeID)){
                score++;
            }
            else{
                score--;
            }
        }
        if(tempScore < score){
            TimeCountDown.timeCountDown.ReTimeCount(1);
        }
        GameManager.gameManager.customerNum++;
        
        DestroyOrder();
        boxTrans.localScale = Vector3.zero;
        mainPlayer.NextCustomer();
    }

    public void HandleRobber(){
        CancelInvoke("GetCustomer");
        Destroy(Instantiate(robberExplosion, new Vector3(mainPlayer.gameObject.transform.position.x, mainPlayer.gameObject.transform.position.y + 1, 0), Quaternion.identity), 0.7f);
        mainPlayer.gameObject.SetActive(false);
        robberButton.SetActive(false);
        uiController.UpdateTextOrder("", "");
        score += 3;
        TimeCountDown.timeCountDown.ReTimeCount(2);
        DestroyOrder();
        boxTrans.localScale = Vector3.zero;
        isGetRobber = false;
        Invoke("GetCustomer", 0.5f);
    }

    void GetCustomer(){
        robberButton.SetActive(false);
        if(isGetRobber){
            isGetRobber = false;
            score--;
            Debug.Log("Can't handle Robber");
            DestroyOrder();
            boxTrans.localScale = Vector3.zero;
        }
        mainPlayer.ShowUp();
    }

    public void HideTable(){
        baseTable.DOMoveY(-8f,0.5f);
    }

    public void ShowTable(){
        baseTable.DOMoveY(orgPosTable.y,0.5f);
    }

}
