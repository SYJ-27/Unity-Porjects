using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;

    public Transform holderLeafs, holeTrans;

    public Leaf prefabLeaf;
    private List<Leaf> listOfLeafs;
    public Stone prefabStone;
    private List<Stone> listOfStones;

    public List<GameObject> listMainGameObject;
    public bool isLose;
    private Vector3 worldPoint;
    public int score;
    public float intensityLeaf, speedStone;


    void Awake(){
        speedStone = 13;
        intensityLeaf = 13;
        score = 0;
        isLose = false;
        gameManager = this;
        listOfLeafs = new List<Leaf>();
        listOfStones = new List<Stone>();
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnLeafs();
        ResetHolePosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearListLeafs(){
        for(int i = 0; i < listOfLeafs.Count; i++){
            if(listOfLeafs[i] != null){
                Destroy(listOfLeafs[i].gameObject);
            }
        }
        listOfLeafs.Clear();
    }

    public void ClearListStones(){
        for(int i = 0; i < listOfStones.Count; i++){
            if(listOfStones[i] != null){
                Destroy(listOfStones[i].gameObject);
            }
        }
        listOfStones.Clear();
    }

    Vector3 RandomPositionOutScreen(){
        float x, y;
        int pos = Random.Range(0, 3);
        if(pos == 0){
            x = -worldPoint.x - 1;
            y = Random.Range(-worldPoint.y - 1, worldPoint.y + 1.1f);
        }
        else if(pos == 1){
            x = Random.Range(-worldPoint.x - 1, worldPoint.x + 1.1f);
            y = Random.Range(0, 2);
            if(y == 0){
                y = worldPoint.y + 1;
            }
            else{
                y = -worldPoint.y - 1;
            }
        }
        else{
            x = worldPoint.x + 1;
            y = Random.Range(-worldPoint.y - 1, worldPoint.y + 1.1f );
        }
        return new Vector3(x,y,0);
    }

    Vector3 RandomPositionInScreen(){
        float x, y;
        x = Mathf.Round(Random.Range(-worldPoint.x + 1f, worldPoint.x - 1f));
        y = Mathf.Round(Random.Range(-worldPoint.y + 1f, worldPoint.y - 1f));

        return new Vector3(x,y,0);
    }

    Vector3 RandomPositionInScreen2(){
        float x, y;
        int pos = Random.Range(0, 3);
        Debug.Log("Pos: " + pos);
        // int pos = 0;
        if(pos == 0){
            x = Random.Range(-worldPoint.x + 1f, -2);
            y = Random.Range(-worldPoint.y + 1, worldPoint.y - 1.1f);
        }
        else if(pos == 1){
            x = Random.Range(-2, 2);
            y = Random.Range(0, 2);
            if(y == 0){
                y = Random.Range(2, worldPoint.y - 1.1f);
            }
            else{
                y = Random.Range(-worldPoint.y + 1f, -2);
            }
        }
        else{
            x = Random.Range(2, worldPoint.x - 1f);
            y = Random.Range(-worldPoint.y + 1, worldPoint.y - 1.1f);
        }
        return new Vector3(x,y,0);
    }

    public void GameLosing(string dieReason){
        CancelSpawnStone();
        isLose = true;
        ClearListLeafs();
        ClearListStones();
        holderLeafs.gameObject.SetActive(false);
        DisableGamePlay();
        uiController.GameLosingUI(dieReason);
    }

    void DisableGamePlay(){
        for(int i = 0; i < listMainGameObject.Count; i++){
            listMainGameObject[i].SetActive(false);
        }
    }

    public void SpawnLeafs(){
        int numLeafs = Random.Range(4, 7);
        for(int i = 0; i < numLeafs; i++){
            var leafSpawned = Instantiate(prefabLeaf, RandomPositionInScreen(), Quaternion.identity, holderLeafs);
            listOfLeafs.Add(leafSpawned);
        }
    }

    public void SpawnStones(){
        // int numStones = Random.Range(1, 7);
        // for(int i = 0; i < numStones; i++){
            var stoneSpawned = Instantiate(prefabStone, RandomPositionOutScreen(), Quaternion.identity);
            listOfStones.Add(stoneSpawned);
            if(score > 22){
                Invoke("SpawnStones", Random.Range(1f, 1.5f));
            }
            else{
                Invoke("SpawnStones", Random.Range(2f, 3f));
            }
        // }
    }

    void ResetHolePosition(){
        holeTrans.position = RandomPositionInScreen2();
    }

    public void NextLevel(){
        if(score == 22){
            intensityLeaf = 22;
            speedStone = 22;
        }
        CancelSpawnStone();
        // score++;
        uiController.ResetTimeBarUI();
        ClearListLeafs();
        SpawnLeafs();
        ResetHolePosition();
    }

    public void CancelSpawnStone(){
        CancelInvoke("SpawnStones");
        ClearListStones();
    }

    public void SpawningStones(){
        if(score >= 3){
            Invoke("SpawnStones", Random.Range(1f, 2f));
        }
    }

}
