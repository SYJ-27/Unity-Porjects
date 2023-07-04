using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiController : MonoBehaviour
{
    public GameOver gameOver;
    public Food mainFood;
    public Text textScore;
    public List<GameObject> listNotices;
    int score;

    void Awake(){
        score = 0;
        textScore.text = $"{score}";
        for(int i = 0; i < listNotices.Count; i++){
            listNotices[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreUI();
    }

    void UpdateScoreUI(){
        score = mainFood.score;
        textScore.text = $"{score}";
    }

    public void GameOverUI(string reasonLose){
        score = mainFood.score;
        gameOver.GameOverScene(reasonLose ,score);
    }

    public void UpdateNoticeObstacle(int x){
        for(int i = 0; i < listNotices.Count; i++){
            if(i == x){
                listNotices[i].SetActive(true);
                ScaleNotice(listNotices[i]);
            }
            else{
                listNotices[i].SetActive(false);
            }
        }
    }

    void ScaleNotice(GameObject notice){
        notice.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.15f).OnComplete(() =>{
            notice.transform.DOScale(new Vector3(1f, 1f, 1f), 0.15f).OnComplete(() =>{
                notice.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.15f).OnComplete(() =>{
                    notice.transform.DOScale(new Vector3(1f, 1f, 1f), 0.15f).OnComplete(() =>{
                        notice.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.15f).OnComplete(() =>{
                            notice.transform.DOScale(new Vector3(1f, 1f, 1f), 0.15f).OnComplete(() =>{
                                notice.SetActive(false);
                            });
                        });
                    });
                });
            });
        });
    }

}
