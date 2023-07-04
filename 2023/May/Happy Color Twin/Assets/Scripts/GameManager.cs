using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;

    public bool isLose, isStart, isPlayerConnecting,isNextFace;
    public int score, faceNumber, level, scoreTime, scoreBean, maxFaces;
    public float time, maxTime;

    Vector3 worldPoint;

    public Item1 prefabItem1;
    public Item2 prefabItem2;
    public Cutter prefabCutter;
    public Player player1, player2;

    List<Item1> listItem1s;
    // List<List<Item1>> listCoupleItem1s;
    List<Item2> listItem2s;
    List<Cutter> listCutters;
    // List<List<Item2>> listCoupleItem2s;


    public List<BigFace> prefabBigFaces;
    public List<GameObject> listMainGameObjects;
    List<List<BigFace>> listCoupleBigFaces;

    void Awake(){
        maxFaces = 0;
        isNextFace = false;
        isPlayerConnecting = true;
        listItem1s = new List<Item1>();
        listItem2s = new List<Item2>();
        listCutters = new List<Cutter>();
        isStart = true;
        faceNumber = 0;
        listCoupleBigFaces = new List<List<BigFace>>();
        gameManager = this;
        isLose = false;
        score = 0;
        scoreBean = 0;
        scoreTime = 0;
        level = 0;
        maxTime = 15f;
        time = maxTime;
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    // Start is called before the first frame update
    void Start()
    {
        // TimeCountDown.timeCountDown.TimeLeft = maxTime;
        SpawnBigFaces();
        SpawnItem1s();
    }

    // Update is called once per frame
    void Update()
    {
        // EnableNextBigFaces();
    }

    void FixedUpdate()
    {
        // if(!isStart){
        //     time -= Time.deltaTime;
        //     // time = (float)Math.Round(time,1);
        //     if(time <= 0){
        //         time = 0;
        //         if(!isLose){
        //             GameLosing();
        //         }
        //     }
        // }
    }

    public void UpdateTime(){
        int timeUp = UnityEngine.Random.Range(4, 7);
        TimeCountDown.timeCountDown.TimeLeft += timeUp;
        uiController.PlusTimeUI(timeUp);
        scoreTime += timeUp;
        if(time > maxTime){
            time = maxTime;
        }
        isStart = true;
    }

    public void UpdateScoreBean(){
        uiController.PlusBeanUI();
        scoreBean++;
    }

    Vector3 RandomInQuadrant1(int idx){
        float x, y;
        if(idx == 1){
            x = UnityEngine.Random.Range(0.5f, (worldPoint.x - 1));
            y = UnityEngine.Random.Range(0.5f, (worldPoint.y - 1));
        }
        else if(idx == 2){
            x = UnityEngine.Random.Range(-0.5f, -(worldPoint.x - 1));
            y = UnityEngine.Random.Range(0.5f, (worldPoint.y - 1));
        }
        else if(idx == 3){
            x = UnityEngine.Random.Range(-0.5f, -(worldPoint.x - 1));
            y = UnityEngine.Random.Range(-0.5f, -(worldPoint.y - 1));
        }
        else{
            x = UnityEngine.Random.Range(0.5f, (worldPoint.x - 1));
            y = UnityEngine.Random.Range(-0.5f, -(worldPoint.y - 1));
        }

        return new Vector3(x,y,0);
    }

    void ResetPlayer(){
        player1.ResetPlayer();
        player2.ResetPlayer();
    }

    Vector3 RandomInQuadrant2(int idx){
        float x, y;
        if(idx == 1){
            x = UnityEngine.Random.Range(0.3f, (worldPoint.x - 0.5f));
            y = UnityEngine.Random.Range(0.3f, (worldPoint.y - 0.5f));
        }
        else if(idx == 2){
            x = UnityEngine.Random.Range(-0.3f, -(worldPoint.x - 0.5f));
            y = UnityEngine.Random.Range(0.3f, (worldPoint.y - 0.5f));
        }
        else if(idx == 3){
            x = UnityEngine.Random.Range(-0.3f, -(worldPoint.x - 0.5f));
            y = UnityEngine.Random.Range(-0.3f, -(worldPoint.y - 0.5f));
        }
        else{
            x = UnityEngine.Random.Range(0.3f, (worldPoint.x - 0.5f));
            y = UnityEngine.Random.Range(-0.3f, -(worldPoint.y - 0.5f));
        }

        return new Vector3(x,y,0);
    }

    Vector3 RandomCutter(){
        float x, y;
        int random = UnityEngine.Random.Range(0, 3);
        y = UnityEngine.Random.Range((worldPoint.y - 2f), (worldPoint.y - 2f));
        if(random == 0){
            x = UnityEngine.Random.Range(1.7f, (worldPoint.x - 2f));
        }
        else if(random == 1){
            x = UnityEngine.Random.Range(-1.7f, -(worldPoint.x - 2f));
        }
        else{
            x = UnityEngine.Random.Range(-1.7f, -(worldPoint.x - 2f));
            random = UnityEngine.Random.Range(0, 2);
            if(random == 0){
                y = UnityEngine.Random.Range(1.7f, (worldPoint.y - 2f));
            }
            else{
                y = UnityEngine.Random.Range(-1.7f, -(worldPoint.y - 2f));
            }
        }

        return new Vector3(x, y, 0);
    }

    public void GameLosing(){
        isLose = true;
        ClearListCoupleFaces();
        ClearListItem1();
        ClearListItem2();
        ClearListCutter();
        DisableMainGameObject();
        uiController.GameLosingUI();
    }

    void SpawnItem1s(){
        int[] randomQuadrants = new int[]{1, 2, 3, 4};
        var rnd = new System.Random();
        randomQuadrants = randomQuadrants.OrderBy(item => rnd.Next()).ToArray();

        int numberItem1 = UnityEngine.Random.Range(1, 3);
        if(level < 2){
            numberItem1 = 0;
        }

        for(int i = 0; i < numberItem1; i++){
            var item1 = Instantiate(prefabItem1, RandomInQuadrant2(randomQuadrants[0]), Quaternion.identity);
            var item2 = Instantiate(prefabItem1, RandomInQuadrant2(randomQuadrants[1]), Quaternion.identity);
            item1.idBean = listItem1s.Count();
            listItem1s.Add(item1);
            item2.idBean = listItem1s.Count();
            listItem1s.Add(item2);
        }
    }

    void SpawnCutters(){
        int numberCutter = UnityEngine.Random.Range(1, 6);
        if(level < 3){
            numberCutter = 1;
        }
        else if(level < 5){
            numberCutter = 2;
        }

        for(int i = 0; i < numberCutter; i++){
            var cutter1 = Instantiate(prefabCutter, RandomCutter(), Quaternion.identity);
            listCutters.Add(cutter1);
        }
    }

    void SpawnItem2s(){
        int[] randomQuadrants = new int[]{1, 2, 3, 4};
        var rnd = new System.Random();
        randomQuadrants = randomQuadrants.OrderBy(item => rnd.Next()).ToArray();

        int numberItem2 = 1;
        if(level < 1){
            numberItem2 = 0;
        }

        for(int i = 0; i < numberItem2; i++){
            var item1 = Instantiate(prefabItem2, RandomInQuadrant2(randomQuadrants[0]), Quaternion.identity);
            var item2 = Instantiate(prefabItem2, RandomInQuadrant2(randomQuadrants[1]), Quaternion.identity);
            item1.idTime = listItem2s.Count;
            listItem2s.Add(item1);
            item2.idTime = listItem2s.Count;
            listItem2s.Add(item2);
        }
    }

    void SpawnBigFaces(){
        int[] randomQuadrants = new int[]{1, 2, 3, 4};
        var rnd = new System.Random();
        randomQuadrants = randomQuadrants.OrderBy(item => rnd.Next()).ToArray();

        int numberFace = UnityEngine.Random.Range(2, 4);
        if(level == 0){
            numberFace = 1;
        }
        maxFaces = numberFace;

        for(int j = 0; j < maxFaces; j++){
            List<BigFace> list1 = new List<BigFace>();
            for(int i = 0; i < prefabBigFaces.Count; i++){
                var face = Instantiate(prefabBigFaces[i], RandomInQuadrant1(randomQuadrants[i]), Quaternion.identity);
                face.gameObject.name = $"Big Face {j}-{i}";
                if(j == 0){
                    face.InitBigFace(i, 1);
                }
                else{
                    face.InitBigFace(i, 0);
                }
                list1.Add(face);
            }
            listCoupleBigFaces.Add(list1);
        }
        UpdateTimeLeft();

    }

    void NextLevel(){
        score++;
        level++;
        isStart = true;
        // time = maxTime;
        faceNumber = 0;
        ClearListCoupleFaces();
        ClearListItem1();
        ClearListItem2();
        ClearListCutter();
        SpawnBigFaces();
        SpawnItem1s();
        SpawnItem2s();
        SpawnCutters();
        ResetPlayer();
    }

    void UpdateTimeLeft(){
        if(score == 0){
            maxTime = 15;
        }
        else if(score < 7){
            maxTime = 13 * maxFaces;
        }
        else if (score < 13){
            maxTime = UnityEngine.Random.Range(7, 14) * maxFaces;
            player1.speed = 5;
            player2.speed = 5;
        }
        else{
            maxTime = UnityEngine.Random.Range(7, 11) * maxFaces;
            player1.speed = 7;
            player2.speed = 7;
        }
        time = maxTime;
        TimeCountDown.timeCountDown.TimeLeft = maxTime;
    }

    void DisableMainGameObject(){
        for(int i = 0; i < listMainGameObjects.Count; i++){
            listMainGameObjects[i].SetActive(false);
        }
    }

    void ClearListCoupleFaces(){
        for(int i = 0; i < listCoupleBigFaces.Count; i++){
            for(int j = 0; j < listCoupleBigFaces[i].Count; j++){
                if(listCoupleBigFaces[i][j] != null){
                    Destroy(listCoupleBigFaces[i][j].gameObject);
                }
            }
            listCoupleBigFaces[i].Clear();
        }
        listCoupleBigFaces.Clear();
    }

    void ClearListItem1(){
        for(int i = 0; i < listItem1s.Count; i++){
            if(listItem1s[i] != null){
                Destroy(listItem1s[i].gameObject);
            }
        }
        listItem1s.Clear();
    }

    void ClearListItem2(){
        for(int i = 0; i < listItem2s.Count; i++){
            if(listItem2s[i] != null){
                Destroy(listItem2s[i].gameObject);
            }
        }
        listItem2s.Clear();
    }

    void ClearListCutter(){
        for(int i = 0; i < listCutters.Count; i++){
            if(listCutters[i] != null){
                Destroy(listCutters[i].gameObject);
            }
        }
        listCutters.Clear();
    }

    public void EnableNextBigFaces(){
        GameManager.gameManager.isNextFace = false;
        faceNumber++;
        if(faceNumber < maxFaces){
            listCoupleBigFaces[faceNumber][0].EnableBigFace();
            listCoupleBigFaces[faceNumber][1].EnableBigFace();
        }
        else{
            NextLevel();
        }
    }


}
