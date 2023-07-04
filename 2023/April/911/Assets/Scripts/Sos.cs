using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sos : MonoBehaviour
{
    House atHouse;

    float timeRemain;
    bool isPause;
    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        timeRemain = 4;
        Invoke("EndStatusItSelf", timeRemain);
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(timeRemain > 0 && !isPause){
        //     timeRemain -= Time.deltaTime;
        // }
        // else{
        //     timeRemain = 0;
        // }
        if(atHouse != null && GameManager.gameManager.isLose){
            Destroy(gameObject);
        }
    }

    public void InitStatus(House thisHouse){
        atHouse = thisHouse;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "PCircle"){
            isPause = true;
            CancelInvoke("EndStatusItSelf");
            Invoke("DestroyStatus", 1f);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "PCircle"){
            isPause = false;
            CancelInvoke("DestroyStatus");
            Invoke("EndStatusItSelf", timeRemain);
        }
    }

    void DestroyStatus(){
        Destroy(gameObject);
        GameManager.gameManager.sosSaved++;
        atHouse.RandomStatus();
    }

    void EndStatusItSelf(){
        Destroy(gameObject);
        if(atHouse != null){
            atHouse.MinusLifeHouse();
        }
    }
}
