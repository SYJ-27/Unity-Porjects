using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;

    public SmallSaw prefabSmallSaw;
    List<SmallSaw> listSmallSaws = new List<SmallSaw>();
    Vector3 worldPoint;

    public List<GameObject> listMainGameObject;
    public bool isLose;

    void Awake(){
        gameManager = this;
        isLose = false;
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnSmallSaw", 0, 1);
        // Invoke("ExtremeSpawnSaw", 15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 RandomPosition(){
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

    void SpawnSmallSaw(){
        var ssaw = Instantiate(prefabSmallSaw, RandomPosition(), Quaternion.identity);
        listSmallSaws.Add(ssaw);
    }

    void DestroyAllSmallSaw(){
        for(int i = 0; i < listSmallSaws.Count; i++){
            if(listSmallSaws[i] != null){
                Destroy(listSmallSaws[i].gameObject);
            }
        }
        listSmallSaws.Clear();
    }

    void DisableMainGameObject(){
        for(int i = 0; i < listMainGameObject.Count; i++){
            if(listMainGameObject[i] != null){
                listMainGameObject[i].SetActive(false);
            }
        }
    }

    public void GameLosing(){
        isLose = true;
        CancelInvoke("ExtremeSpawnSaw");
        CancelInvoke("NormalSpawnSaw");
        CancelInvoke("SpawnSmallSaw");
        DestroyAllSmallSaw();
        DisableMainGameObject();
        uiController.GameLosingUI();
    }

    public void ExtremeSpawnSaw(){
        uiController.DangerNotice();
        CancelInvoke("SpawnSmallSaw");
        InvokeRepeating("SpawnSmallSaw", 0, 0.4f);
        Invoke("NormalSpawnSaw", 7);
    }

    public void NormalSpawnSaw(){
        uiController.CancelNotice();
        CancelInvoke("SpawnSmallSaw");
        InvokeRepeating("SpawnSmallSaw", 0, 1f);
    }

}
