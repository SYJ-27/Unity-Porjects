using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectDatingScene : MonoBehaviour
{
    public UiController uiController;
    public List<Sprite> listDatingSprites;
    public List<Button> listButtons;
    public DateFriend mainDateFriend;
    int[] listDating;
    void Awake(){
        listDating = new int[5]{0, 1, 2, 3, 4};
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        // ShowDatingFriends();        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowDatingFriends(){
        var rnd = new System.Random();
        listDating = listDating.OrderBy(item => rnd.Next()).ToArray();

        for(int i = 0; i < listButtons.Count; i++){
            listButtons[i].gameObject.GetComponent<Image>().sprite = listDatingSprites[listDating[i]];
            listButtons[i].gameObject.GetComponent<Image>().SetNativeSize();
        }
        gameObject.SetActive(true);
    }

    public void PlayGame(){
        gameObject.SetActive(false);
        uiController.ActiveGameUI();
    }

    public void DateChoice1(){
        GetDateFriend(0);
    }

    public void DateChoice2(){
        GetDateFriend(1);
    }

    public void DateChoice3(){
        GetDateFriend(2);
    }

    void GetDateFriend(int idx){
        listButtons[idx].transform.DOScale(new Vector3(1.5f,1.5f,1.5f), 0.15f).OnComplete(() =>{
            listButtons[idx].transform.DOScale(new Vector3(1f,1f,1f), 0.15f).OnComplete(() =>{
                mainDateFriend.ShowDateFriend(listDating[idx]);
                PlayGame();
            });
        });
    }

    // void LateUpdate(){
    //     listButtons[0].gameObject.GetComponent<Image>().SetNativeSize();
    // }
}
