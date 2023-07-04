using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SuitUpScene : MonoBehaviour
{
    public Player mainPlayer;
    public SelectDatingScene mainSelectScene;
    public List<GameObject> listEyes, listAccessories;
    public int idxEyes, idxAccessory;
    public GameObject buttonSelectDating;

    void Awake(){
        idxAccessory = 0;
        idxEyes = 0;
        gameObject.SetActive(true);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSelectedItem();
    }

    void UpdateSelectedItem(){
        for(int i = 0; i < listAccessories.Count; i++){
            if(i == idxAccessory){
                listAccessories[i].SetActive(true);
            }
            else{
                listAccessories[i].SetActive(false);
            }
        }

        for(int i = 0; i < listEyes.Count; i++){
            if(i == idxEyes){
                listEyes[i].SetActive(true);
            }
            else{
                listEyes[i].SetActive(false);
            }
        }
        mainPlayer.UpdateItem(idxAccessory, idxEyes);
    }

    public void GetAccessoryIncreasing(){
        idxAccessory++;
        if(idxAccessory >= listAccessories.Count){
            idxAccessory = 0;
        }
    }

    public void GetAccessoryDecreasing(){
        idxAccessory--;
        if(idxAccessory < 0){
            idxAccessory = listAccessories.Count - 1;
        }
    }

    public void GetEyesIncreasing(){
        idxEyes++;
        if(idxEyes == listEyes.Count){
            idxEyes = 0;
        }
    }

    public void GetEyesDecreasing(){
        idxEyes--;
        if(idxEyes < 0){
            idxEyes = listEyes.Count - 1;
        }
    }

    public void SelectDating(){
        buttonSelectDating.transform.DOScale(new Vector3(2f,2f,2f), 0.15f).OnComplete(() =>{
            buttonSelectDating.transform.DOScale(new Vector3(1.5f,1.5f,1.5f), 0.15f).OnComplete(() =>{
                gameObject.SetActive(false);
                mainSelectScene.ShowDatingFriends();
            });
        });
    }
}
