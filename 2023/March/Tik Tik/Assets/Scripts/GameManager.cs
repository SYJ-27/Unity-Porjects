using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;
    public GameOver gameOver;
    public GameBonus gameBonus;

    public EnemyLight prefabEnemyLight;
    List<EnemyLight> listEnemyLights = new List<EnemyLight>();

    public EnemyTrain prefabEnemyTrain;
    List<EnemyTrain> listEnemyTrains = new List<EnemyTrain>();

    public List<GameObject> listMainGameObject;
    public List<GameObject> listGameButtons;
    public List<GameObject> listExtraGameObject = new List<GameObject>();

    public int numberTrains, numberLights, score, clearScore, bulletDamage;
    public bool isLose, isPause, canDestroyBullet2;
    public float resetBulletTime, resetDamageTime;

    void Awake(){
        resetBulletTime = 0;
        resetDamageTime = 0;
        canDestroyBullet2 = false;
        bulletDamage = 1;
        isPause = false;
        isLose = false;
        score = 0;
        clearScore = 0;
        gameManager  = this;
        numberLights = 0;
        numberTrains = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        // InvokeRepeating("SpawnEnemyLight", 0, 1);
        // InvokeRepeating("SpawnEnemyTrain", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPause){
            if(resetBulletTime > 0){
                resetBulletTime -= Time.deltaTime;
            }
            else{
                resetBulletTime = 0;
            }

            if(resetDamageTime > 0){
                resetDamageTime -= Time.deltaTime;
            }
            else{
                resetDamageTime = 0;
            }
        }
    }

    void SpawnEnemyLight(){
        for(int i = 0; i < numberLights; i++){
            var enemyLightT = Instantiate(prefabEnemyLight, Vector3.zero, new Quaternion(0,0,Random.Range(7,9) * 0.1f, Random.Range(7,9) * 0.1f));
            
            listEnemyLights.Add(enemyLightT);
        }
    }

    void SpawnEnemyTrain(){
        for(int i = 0; i < numberTrains; i++){
            var enemyTrainT = Instantiate(prefabEnemyTrain, Vector3.zero, new Quaternion(0,0,Random.Range(7,9) * 0.1f, Random.Range(7,9) * 0.1f));
            listEnemyTrains.Add(enemyTrainT);
        }
    }

    public bool CheckIsAllEnemyNull(){
        for(int i = 0; i < listEnemyLights.Count; i++){
            if(listEnemyLights[i] != null){
                return false;
            }
        }

        for(int i = 0; i < listEnemyTrains.Count; i++){
            if(listEnemyTrains[i] != null){
                return false;
            }
        }

        return true;
    }

    public void UpdateScore(){
        score++;
    }

    public void UpdateClearScore(){
        clearScore++;
    }

    public void GameLosing(){
        isLose = true;
        ClearListEnemy();
        ClearListExtra();
        DisableMainGameObject();
        gameOver.GameOverScene();
    }

    public void DisableMainGameObject(){
        for(int i= 0; i < listMainGameObject.Count; i++){
            if(listMainGameObject[i] != null){
                listMainGameObject[i].SetActive(false);
            }
        }
    }

    public void ClearListEnemy(){
        for(int i = 0; i < listEnemyLights.Count; i++){
            if(listEnemyLights[i] != null){
                if(isLose){
                    listEnemyLights[i].DestroyAllBullet2();
                }
                Destroy(listEnemyLights[i].gameObject);
            }
        }
        listEnemyLights.Clear();

        for(int i = 0; i < listEnemyTrains.Count; i++){
            if(listEnemyTrains[i] != null){
                Destroy(listEnemyTrains[i].gameObject);
            }
        }
        listEnemyTrains.Clear();
    }

    public void ClearListExtra(){
        for(int i = 0; i < listExtraGameObject.Count; i++){
            if(listExtraGameObject[i] != null){
                Destroy(listExtraGameObject[i].gameObject);
            }
        }
        listExtraGameObject.Clear();
    }

    public void BonusScene(){
        isPause = true;
        SetButtons(false);
        gameBonus.GetBonusScene();
    }

    public void NewLevel(){
        isPause = false;
        SetButtons(true);
        if(!isLose){
            if(score == 0){
                ClearListEnemy();
                SpawnEnemyLight();
                SpawnEnemyTrain();
            }
            else{
                numberLights = Random.Range(1, 4);
                numberTrains = 5 - numberLights;
                ClearListEnemy();
                SpawnEnemyLight();
                SpawnEnemyTrain();
            }
        }
        else{
            ClearListEnemy();
        }
        // Invoke("ResetBonus", 15);
        Invoke("ResetBullet", resetBulletTime);
        Invoke("ResetDamage", resetDamageTime);
    }

    public void SetButtons(bool state){
        for(int i = 0; i < listGameButtons.Count; i++){
            if(listGameButtons[i] != null){
                listGameButtons[i].SetActive(state);
            }
        }
    }

    public void ResetBullet(){
        canDestroyBullet2 = false;
    }

    public void ResetDamage(){
        bulletDamage = 1;
    }
    public void PauseResetBonus(){
        CancelInvoke("ResetBullet");
        CancelInvoke("ResetDamage");
    }

}
