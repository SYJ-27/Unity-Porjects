using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController mainUI;
    public int currentScore;
    public bool isLose = false;
    
    public Enemy enemyPrefabs;
    private List<Enemy> listOfEnemy = new List<Enemy>();
    public List<GameObject> mainObjects = new List<GameObject>();
    public int yourLife;

    void Awake(){
        yourLife = 3;
        gameManager = this;
        
    }
    
    void Start()
    {
        StartGame();
    }

    void Update()
    {

    }

    void StartGame(){
        InvokeRepeating("GenerateEnemy",0,2);
        GameOver.mainGameOver.HideGameOver();
        isLose = false;
        currentScore = -1;
        UpdateScoreUI();
    }

    public void UpdateScoreUI(){
        currentScore++;
        mainUI.UpdateCurrentScore(currentScore);
    }

    public void GameLose(){
        DisableMainObject();
        DestroyAllEnemy();
        isLose = true;
        mainUI.HideCurrentScore();
        GameOver.mainGameOver.ShowGameOver(currentScore);
    }

    public void PlayGame(){
        StartGame();
        mainUI.ShowCurrentScore();
    }

    void GenerateEnemy(){
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        var enemySpawned = Instantiate(enemyPrefabs, new Vector3(worldPosition.x + 5,Player.mainPlayer.gameObject.transform.position.y,0), Quaternion.identity);
        enemySpawned.InitEnemy();
        listOfEnemy.Add(enemySpawned);
    }

    void DestroyAllEnemy(){
        CancelInvoke("GenerateEnemy");
        for(int i = 0; i < listOfEnemy.Count; i++){
            if(listOfEnemy[i] != null){
                Destroy(listOfEnemy[i].gameObject);
            }
        }
    }

    void DisableMainObject(){
        for(int i = 0; i < mainObjects.Count; i++){
            mainObjects[i].SetActive(false);
        }
    }

    public void LoseHeart(){
        yourLife--;
        mainUI.LoseHeart(yourLife);
        if(yourLife == 0){
            GameLose();
        }
    }

}
