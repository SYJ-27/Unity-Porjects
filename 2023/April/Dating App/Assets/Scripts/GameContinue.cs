using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameContinue : MonoBehaviour
{
    public Text dateText;
    public GameObject continueButton;
    public List<GameObject> listGameObject;

    void Awake(){
        transform.localScale = new Vector3(0,0,0);
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameContinueScene(){
        DisableGameObject();
        gameObject.SetActive(true);
        dateText.text = "Your Date Now\n" + $"{GameManager.gameManager.score}";
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
    }

    void DisableGameObject(){
        for(int i = 0; i < listGameObject.Count; i++){
            listGameObject[i].SetActive(false);
        }
    }

    public void ContinuePlay(){
        continueButton.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f).OnComplete(() =>{
            continueButton.transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f).OnComplete(() =>{
                gameObject.SetActive(false);
                listGameObject[0].SetActive(true);
                GameManager.gameManager.DoSuitUpScene();
            });
        });
    }
}
