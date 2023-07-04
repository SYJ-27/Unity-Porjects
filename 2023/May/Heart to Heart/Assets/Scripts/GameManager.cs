using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;
    public UiController uiController;
    Vector3 worldPoint;
    public bool isLose, isStart, isPause;
    public List<GameObject> listMainGameObject;

    public Heart prefabHeart;
    public Obstacle prefabObstacle;
    public List<PauseStar> prefabStars;

    public Transform heartHolderTrans;
    public int score, scoreHeart;
    public float timePause;

    public List<Heart> listHearts;
    List<PauseStar> listPauseStars;
    List<Obstacle> listObstacles;

    void Awake(){
        scoreHeart = 0;
        timePause = 0;
        gameManager = this;
        // isStart = true;
        isLose = false;
        isPause = false;
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        // listHearts = new List<Heart>();
        listObstacles = new List<Obstacle>();
        listPauseStars = new List<PauseStar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Invoke("SpawnObstacle", Random.Range(0.5f, 1));
        Invoke("MovingHeart", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        // if(IsAllHeartOutArea() && isStart){
        //     isStart = false;
        // }
    }

    public void SpawningHeart(){
        SpawnHeart();
    }

    public void SpawningStar(){
        Invoke("SpawnStar", Random.Range(2.5f, 3.5f));
    }

    public void SpawningObstacle(){
        Invoke("SpawnObstacle", Random.Range(0.7f, 1f));
    }

    void SpawnStar(){
        int starIdx = Random.Range(0, 4);
        var star = Instantiate(prefabStars[starIdx], RandomStarPosition(starIdx), Quaternion.identity);
        listPauseStars.Add(star);
        Invoke("SpawnStar", Random.Range(2.5f, 3.5f));
    }

    void SpawnObstacle(){
        var obs = Instantiate(prefabObstacle, new Vector3(50, 50, 0), Quaternion.identity);
        listObstacles.Add(obs);
        Invoke("SpawnObstacle", Random.Range(4f, 5f));
    }

    void SpawnHeart(){
        var heartSpawned = Instantiate(prefabHeart, RandomHeartPosition(), Quaternion.identity, heartHolderTrans);
        heartSpawned.ActiveMoving();
        listHearts.Add(heartSpawned);
    }

    void MovingHeart(){
        for(int i = 0;i < listHearts.Count; i++){
            listHearts[i].canMove = true;
        }
    }

    Vector3 RandomHeartPosition(){
        float rotationRadius = Random.Range(0.3f, 0.5f);
        float angle = Random.Range(0 , 2 * Mathf.PI);
        float posX = transform.position.x + Mathf.Cos(angle) * rotationRadius;
        float posY = transform.position.y + Mathf.Sin(angle) * rotationRadius;
        transform.position = new Vector2(posX, posY);
        return new Vector3(posX, posY, 0);
    }

    Vector3 RandomStarPosition(int idx){
        float x, y;
        if(idx == 0){
            x = Random.Range(-worldPoint.x + 1.5f, worldPoint.x - 1.5f);
            y = worldPoint.y + 0.5f;
        }
        else if(idx == 1){
            x = -worldPoint.x - 0.5f;
            y = Random.Range(-worldPoint.y + 1.5f, worldPoint.y - 1.5f);
        }
        else if(idx == 1){
            x = Random.Range(-worldPoint.x + 1.5f, worldPoint.x - 1.5f);
            y = -worldPoint.y - 0.5f;
        }
        else{
            x = worldPoint.x + 0.5f;
            y = Random.Range(-worldPoint.y + 1.5f, worldPoint.y - 1.5f);
        }
        return new Vector3(x,y,0);
    }

    public void GameLosing(){
        isLose = true;
        DisableAllGameObject();
        DestroyAllThings();
        uiController.GameLosingUI();
    }

    void DisableAllGameObject(){
        for(int i = 0; i < listMainGameObject.Count; i++){
            listMainGameObject[i].SetActive(false);
        }
    }

    bool IsAllHeartOutArea(){
        for(int i = 0; i < listHearts.Count; i++){
            if(!listHearts[i].outArea){
                return false;
            }
        }
        return true;
    }

    public void GamePausing(){
        isPause = true;
        CancelInvoke("SpawnObstacle");
        CancelInvoke("SpawnStar");
        // CancelInvoke("SpawnHeart");
        Invoke("GameContinue", timePause);
    }

    public void GameContinue(){
        CancelInvoke("GameContinue");
        isPause = false;
        if(score > 3){
            Invoke("SpawnObstacle", Random.Range(4f, 5f));
        }
        if(score % 7 == 0 && score != 0){
            SpawningHeart();
        }
        Invoke("SpawnStar", Random.Range(2.5f, 3.5f));
    }

    void DestroyAllThings(){
        CancelInvoke("SpawnObstacle");
        CancelInvoke("SpawnStar");
        // CancelInvoke("SpawnHeart");
        for(int i = 0;i < listObstacles.Count; i++){
            if(listObstacles[i] != null){
                Destroy(listObstacles[i].gameObject);
            }
        }
        listObstacles.Clear();

        for(int i = 0;i < listHearts.Count; i++){
            if(listHearts[i] != null){
                Destroy(listHearts[i].gameObject);
            }
        }
        listHearts.Clear();

        for(int i = 0;i < listPauseStars.Count; i++){
            if(listPauseStars[i] != null){
                Destroy(listPauseStars[i].gameObject);
            }
        }
        listPauseStars.Clear();
    }

}
