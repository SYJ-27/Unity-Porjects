using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UIController uiControl;
    public GameOver gameOverUI;
    public GameObject duck;
    public List<Transform> duckSpawners = new List<Transform>();
    public int coin = 0, countTime = 30, highScore = 0;
    public float randomSpawn;
    private List<GameObject> duckSpawns = new List<GameObject>();

    void Awake()
    {
        gameManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        // Test.count = 2;
        // Debug.Log(Test.count);
        uiControl.UpdateCountdown(countTime);
        // StartCoroutine(SpawnDuck());
        InvokeRepeating("CountDown", 1, 1);
        randomSpawn = Random.Range(2.0f, 4.0f);
        InvokeRepeating("SpawnDuck", 0, randomSpawn);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnDuck()
    {
        randomSpawn = Random.Range(2.0f, 4.0f);
        int randSide = Random.Range(0, duckSpawners.Count);
        float randY = Random.Range(duckSpawners[0].position.y, duckSpawners[1].position.y);
        var duckSpawned = Instantiate(duck, new Vector3(duckSpawners[randSide].position.x, randY, 0), Quaternion.identity);
        duckSpawns.Add(duckSpawned);
        if (randSide == 0)
        {
            duckSpawned.GetComponent<Duck>().isRight = -1;
            duckSpawned.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            duckSpawned.GetComponent<Duck>().isRight = 1;
            duckSpawned.transform.localScale = new Vector3(1, 1, 1);
        }
        duckSpawned.GetComponent<Duck>().idcheckSide = randSide;

        Destroy(duckSpawned, 50);
        // while (true)
        // {
        //     // yield return new WaitForSeconds(Random.Range(2,4));

        // }
    }

    public void CountDown()
    {
        countTime--;
        uiControl.UpdateCountdown(countTime);
        if (countTime == 0)
        {
            Over();
        }
    }

    void Over()
    {
        DestroyAllDuck();
        CancelInvoke("CountDown");
        CancelInvoke("SpawnDuck");
        uiControl.HideImage();
        gameOverUI.ShowImage(coin, highScore);
    }

    public void CheckUiController()
    {
        coin++;
        if(coin > highScore){
            highScore = coin;
        }
        uiControl.UpdateScore(coin);
    }

    public void Play(){
        gameOverUI.HideImage();
        coin = 0;
        countTime = 30;
        uiControl.UpdateScore(coin);
        uiControl.UpdateCountdown(countTime);
        InvokeRepeating("CountDown", 1, 1);
        randomSpawn = Random.Range(2.0f, 4.0f);
        InvokeRepeating("SpawnDuck", 0, randomSpawn);
        uiControl.ShowImage();
    }

    public void DestroyAllDuck(){
        for(int i = 0; i < duckSpawns.Count; i++){
            if(duckSpawns[i] != null){
                Destroy(duckSpawns[i]);
            }
        }
        duckSpawns.Clear();
    }
}
