using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public bool isLose;
    public List<GameObject> listMainGameObject;

    public Cow prefabCow;
    List<Cow> listCows;

    public ShootingCow prefabShootingCow;
    List<ShootingCow> listShootingCows;

    public Bread prefabBread;
    List<Bread> listBreads;

    public Milk prefabMilk;
    List<Milk> listMilks;

    Vector3 worldPoint;
    int idCow;
    public int score, breadScore, milkScore;

    void Awake(){
        isLose = false;
        score = 0;
        breadScore = 0;
        milkScore = 0;
        gameManager = this;
        idCow = 0;
        listCows = new List<Cow>();
        listShootingCows = new List<ShootingCow>();
        listBreads = new List<Bread>();
        listMilks = new List<Milk>();
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnCowLine", 0);
        Invoke("SpawnShootingCow", 3);
        Invoke("SpawnMilk", Random.Range(0f, 1f));
        Invoke("SpawnBread", Random.Range(0f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float RandomX(){
        return Random.Range(-worldPoint.x + 1, worldPoint.x - 1);
    }

    float RandomY(){
        return Random.Range(1, worldPoint.y - 1);
    }

    void SpawnCowLine(){
        idCow++;
        int randomNum = Random.Range(20, 25);
        float distance = (25 - randomNum) * 0.7f;
        float randY = RandomY();
        for(int i = 0; i < randomNum; i++){
            Cow cowLine = Instantiate(prefabCow, new Vector3(-worldPoint.x - 1.5f + i * distance, 7, 0), Quaternion.identity);
            cowLine.InitCow(idCow);
            listCows.Add(cowLine);
        }
        Invoke("SpawnCowLine", Random.Range(10f, 11f));
    }

    public void GameLosing(){
        isLose = true;
        DestroyAllEnemy();
        DisableGamePlay();
    }

    public void DisableGamePlay(){
        for(int i = 0; i < listMainGameObject.Count; i++){
            listMainGameObject[i].SetActive(false);
        }
    }

    public void DestroyAllEnemy(){
        CancelInvoke("SpawnCowLine");
        CancelInvoke("SpawnShootingCow");
        CancelInvoke("SpawnBread");
        CancelInvoke("SpawnMilk");
        for(int i = 0; i < listCows.Count; i++){
            if(listCows[i] != null){
                Destroy(listCows[i].gameObject);
            }
        }
        listCows.Clear();

        for(int i = 0; i < listShootingCows.Count; i++){
            if(listShootingCows[i] != null){
                Destroy(listShootingCows[i].gameObject);
            }
        }
        listShootingCows.Clear();

        for(int i = 0; i < listBreads.Count; i++){
            if(listBreads[i] != null){
                Destroy(listBreads[i].gameObject);
            }
        }
        listBreads.Clear();

        for(int i = 0; i < listMilks.Count; i++){
            if(listMilks[i] != null){
                Destroy(listMilks[i].gameObject);
            }
        }
        listMilks.Clear();

    }

    public void ClearLineId(int id){
        for(int i = 0; i < listCows.Count; i++){
            if(listCows[i] != null && listCows[i].idCow == id){
                Destroy(listCows[i].gameObject);
            }
        }
    }

    public void ChangeCowLineDirection(int id, int direction){
        for(int i = 0; i < listCows.Count; i++){
            if(listCows[i] != null && listCows[i].idCow == id){
                listCows[i].directionHorizontal = direction;
            }
        }
    }

    void SpawnShootingCow(){
        ShootingCow cow;
        int dirH = Random.Range(0, 2);
        if(dirH == 0){
            dirH = -1;
            cow = Instantiate(prefabShootingCow, new Vector3(worldPoint.x - 1.5f, RandomY(), 0), Quaternion.identity);
        }
        else{
            cow = Instantiate(prefabShootingCow, new Vector3(-worldPoint.x + 1.5f, RandomY(), 0), Quaternion.identity);
        }
        cow.InitShootingCow(dirH);
        listShootingCows.Add(cow);
        Invoke("SpawnShootingCow", Random.Range(6f, 7f));

    }

    void SpawnBread(){
        listBreads.Add(Instantiate(prefabBread, new Vector3(Random.Range(-worldPoint.x + 1, worldPoint.x - 1), 7, 0), Quaternion.identity));
        Invoke("SpawnMilk", Random.Range(2f, 3f));

    }

    void SpawnMilk(){
        listMilks.Add(Instantiate(prefabMilk, new Vector3(Random.Range(-worldPoint.x + 1, worldPoint.x - 1), 7, 0), Quaternion.identity));
        Invoke("SpawnBread", Random.Range(3f, 4f));

    }

}
