using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;

    public Transform eggBasket, eggHolder, canvasUI;
    public GameObject plusEggText, plusTimeText;

    public Egg prefabEgg;
    public Basket mainBasket;

    public bool isStart, isLose, isPlaying;
    public int score, eggBroked;
    public List<GameObject> listMainGameObject;
    List<Egg> listEggs;
    Vector3 worldPoint;


    void Awake(){
        isPlaying = false;
        isLose = false;
        score = 0;
        eggBroked = 0;
        isStart = true;
        gameManager = this;
        listEggs = new List<Egg>();
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
    // Start is called before the first frame update
    void Start()
    {
        // SpawnEgg();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEgg(){
        listEggs.Add(Instantiate(prefabEgg, eggBasket.position, Quaternion.identity));
    }

    Vector3 RandomPositionBasket1(){
        float x = Random.Range(0.5f, worldPoint.x - 1);
        float y = Random.Range(0.5f, worldPoint.y - 1);
        
        return new Vector3(x,y,0);
    }

    Vector3 RandomPositionBasket2(){
        float x = Random.Range(-worldPoint.x + 1, -0.5f);
        float y = Random.Range(0.5f, worldPoint.y - 1);
        
        return new Vector3(x,y,0);
    }

    public void PlusTimeScore(){
        Vector3 posHolder = eggHolder.transform.position;
        var eggNum = Instantiate(plusEggText, posHolder, Quaternion.identity, canvasUI);
        eggNum.transform.DOMoveY(posHolder.y + 1, 0.5f).OnComplete(() =>{
            Destroy(eggNum);
        });
        var timeNum = Instantiate(plusTimeText, new Vector3(0, 5, 0), Quaternion.identity, canvasUI);
        timeNum.transform.DOMoveY(4, 0.5f).OnComplete(() =>{
            Destroy(timeNum);
        });
    }

    Vector3 RandomPositionHolder(float x1, float y1){
        int randomX = Random.Range(0, 2);
        float x;
        if(randomX == 0){
            x = Random.Range(-worldPoint.x + 1, x1 - 1);
        }
        else{
            x = Random.Range(x1 + 1, worldPoint.x - 1);
        }
        float y;
        if(score >= 10){
            y = Random.Range(-3.5f, worldPoint.y - 2);
        }
        else{
            y = Random.Range(-1f, worldPoint.y - 2);
        }

        return new Vector3(x,y,0);
    }

    void ResetPositionHolderAndBasket(){
        eggBasket.position = PositionAt(Random.Range(0, 1));
        eggHolder.position = RandomPositionHolder(eggBasket.position.x, eggBasket.position.y);
    }

    Vector3 PositionAt(int id){
        if(id == 0){
            return RandomPositionBasket1();
        }
        else{
            return RandomPositionBasket2();
        }
    }

    public void NextLevel(){
        isPlaying = false;
        mainBasket.direction = 0;
        if(score > 13){
            mainBasket.speed = Random.Range(4,4.5f);
        }
        uiController.PlusTime(5);
        ResetPositionHolderAndBasket();
        isStart = true;
    }

    public void MoveBasket(){
        if(score > 3){
            mainBasket.direction = Random.Range(0, 2);
            if(mainBasket.direction == 0){
                mainBasket.direction = -1;
            }
        }
    }

    void DisableGamePlay(){
        for(int i = 0; i < listMainGameObject.Count; i++){
            listMainGameObject[i].SetActive(false);
        }
    }

    public void GameLosing(){
        isLose = true;
        isPlaying = false;
        DisableGamePlay();
        uiController.GameLoseUI();
    }

}
