using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DateFriend : MonoBehaviour
{
    public Player mainPlayer;
    public List<Sprite> dateFriendSprites;
    public List<GameObject> listDateFriendObject;
    public List<GameObject> listQuestions;
    SpriteRenderer objectSpriteRenderer;

    int randomQuest, numberQuest, currentQuest;
    public float timeQuest;
    bool isClick, isGettingQuestion;
    void Awake(){
        currentQuest = 0;
        numberQuest = 10;
        isGettingQuestion = true;
        randomQuest = -1;
        timeQuest = 2;
        objectSpriteRenderer = GetComponent<SpriteRenderer>();
        DisableDateFriendObject();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisableDateFriendObject(){
        for(int i = 0; i < listDateFriendObject.Count; i++){
            listDateFriendObject[i].SetActive(false);
        }
    }

    public void ShowDateFriend(int idx){
        objectSpriteRenderer.sprite = dateFriendSprites[idx];
        listDateFriendObject[0].SetActive(true);
        Invoke("GetQuestion", 1.5f);
    }

    void ScaleUpQuest(GameObject quest){
        isGettingQuestion = true;
        // quest.transform.DOScale(Vector3.zero, 0f).OnComplete(() =>{
        quest.transform.DOScale(Vector3.one, 0.3f).OnComplete(() =>{
            isGettingQuestion = false;
        });
    }

    void ScaleDownQuest(GameObject quest){
        quest.transform.DOScale(Vector3.zero, 0.3f).OnComplete(() =>{
            quest.transform.localScale = Vector3.one;
            quest.SetActive(false);
        });
    }

    void GetQuestion(){
        bool isGettingQuestionTemp = isGettingQuestion;
        isGettingQuestion = true;
        if(!GameManager.gameManager.isLose){
            currentQuest++;

            if(currentQuest <= numberQuest){
                if(randomQuest != -1){
                    listQuestions[randomQuest].transform.DOScale(Vector3.zero, 0.3f).OnComplete(() =>{
                        if(!isGettingQuestionTemp){
                            mainPlayer.MinusHeart();
                            timeQuest -= 0.1f;
                            timeQuest = Mathf.Clamp(timeQuest ,0.5f, 2f);
                        }
                        else{
                            if(currentQuest % 2 == 0 && currentQuest != 0){
                                timeQuest -= 0.15f;
                                timeQuest = Mathf.Clamp(timeQuest ,0.5f, 2f);
                            }
                        }
                        // listQuestions[randomQuest].transform.localScale = Vector3.one;
                        listQuestions[randomQuest].SetActive(false);
                        listDateFriendObject[1].SetActive(true);
                        randomQuest = Random.Range(0, listQuestions.Count);
                        for(int i =0; i < listQuestions.Count; i++){
                            if(i == randomQuest){
                                listQuestions[i].SetActive(true);
                                ScaleUpQuest(listQuestions[i]);
                            }
                            else{
                                listQuestions[i].SetActive(false);
                            }
                        }
                        Invoke("GetQuestion", timeQuest);
                    });
                }
                else{
                    listDateFriendObject[1].SetActive(true);
                    randomQuest = Random.Range(0, listQuestions.Count);
                    for(int i =0; i < listQuestions.Count; i++){
                        if(i == randomQuest){
                            listQuestions[i].SetActive(true);
                            ScaleUpQuest(listQuestions[i]);
                        }
                        else{
                            listQuestions[i].SetActive(false);
                        }
                    }
                    Invoke("GetQuestion", timeQuest);
                }
            }
            else{
                listQuestions[randomQuest].transform.DOScale(Vector3.zero, 0.3f).OnComplete(() =>{
                    for(int i =0; i < listQuestions.Count; i++){
                        listQuestions[i].SetActive(false);
                    }
                    GameManager.gameManager.GetSuitUpScene();
                });
            }
        }
    }

    public void ResetDateFriend(){
        listDateFriendObject[1].SetActive(false);
        gameObject.SetActive(false);
        currentQuest = 0;
        randomQuest = -1;
    }

    public void ButtonUp(){
        if(!isGettingQuestion){
            isGettingQuestion = true;
            if(randomQuest == 0){
                Debug.Log("True 0");
                mainPlayer.MoveUp();

                // ScaleUpQuest(listQuestions[randomQuest]);
            }
            else{
                Debug.Log("False 0");
                mainPlayer.MinusHeart();
            }
            CancelInvoke("GetQuestion");
            GetQuestion();
        }
    }

    public void ButtonDown(){
        if(!isGettingQuestion){
            isGettingQuestion = true;
            if(randomQuest == 1){
                Debug.Log("True 1");
                mainPlayer.MoveDown();
                // ScaleUpQuest(listQuestions[randomQuest]);
            }
            else{
                Debug.Log("False 1");
                mainPlayer.MinusHeart();
            }
            CancelInvoke("GetQuestion");
            GetQuestion();
        }
    }

    public void ButtonLeft(){
        if(!isGettingQuestion){
            isGettingQuestion = true;
            if(randomQuest == 2){
                Debug.Log("True 2");
                mainPlayer.MoveLeft();

                // ScaleUpQuest(listQuestions[randomQuest]);
            }
            else{
                Debug.Log("False 2");
                mainPlayer.MinusHeart();
            }
            CancelInvoke("GetQuestion");
            GetQuestion();
        }
    }

    public void ButtonRight(){
        if(!isGettingQuestion){
            isGettingQuestion = true;
            if(randomQuest == 3){
                Debug.Log("True 3");
                mainPlayer.MoveRight();

                // ScaleUpQuest(listQuestions[randomQuest]);
            }
            else{
                Debug.Log("False 3");
                mainPlayer.MinusHeart();
            }
            CancelInvoke("GetQuestion");
            GetQuestion();
        }
    }

    public void ButtonTap(){
        if(!isGettingQuestion){
            isGettingQuestion = true;
            if(randomQuest == 4){
                Debug.Log("True 4");
                mainPlayer.Taping();

                // ScaleUpQuest(listQuestions[randomQuest]);
            }
            else{
                Debug.Log("False 4");
                mainPlayer.MinusHeart();
            }
            CancelInvoke("GetQuestion");
            GetQuestion();
        }
    }


}
