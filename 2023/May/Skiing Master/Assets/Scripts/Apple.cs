using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Apple : MonoBehaviour
{
    public Transform appleRef;
    int numberScale;
    Vector3 worldPoint;
    bool isScaling;
    void Awake(){
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        isScaling = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        numberScale = 1;
        // Scaling();
        RandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        appleRef.position = transform.position;
        if(!GameManager.gameManager.isStart && !isScaling){
            isScaling = true;
            Scaling();
        }
    }

    void Scaling(){
        if(!GameManager.gameManager.isLose && !GameManager.gameManager.isStart){
            transform.DOScale(new Vector3(1f + numberScale * 0.1f, 1f + numberScale * 0.1f, 1f + numberScale * 0.1f), 0.3f).SetId("NoticeGate").OnComplete(() =>
            {
                if(!GameManager.gameManager.isLose && !GameManager.gameManager.isStart){
                    numberScale = -numberScale;
                    Scaling();
                }
            });
        }
    }

    public void RandomPosition(){
        float x, y;
        int random = UnityEngine.Random.Range(0, 3);
        if(GameManager.gameManager.level < 7){
            if(random == 0){
                x = Random.Range(-2.5f, -1);
                y = Random.Range(-2, 0);
            }
            else if(random == 1){
                x = Random.Range(1, 2.5f);
                y = Random.Range(-2, 0);
            }
            else{
                x = Random.Range(-2, +2);
                y = Random.Range(-2.5f,-2);
            }
        }
        else{
            if(random == 0){
                x = Random.Range(-(worldPoint.x - 2), -2);
                y = Random.Range(-(worldPoint.y - 2), 0);
            }
            else if(random == 1){
                x = Random.Range(+2, (worldPoint.x - 2));
                y = Random.Range(-(worldPoint.y - 2), 0);
            }
            else{
                x = Random.Range(-2, +2);
                y = Random.Range(-(worldPoint.y - 2),-2);
            }
        }
        transform.position = new Vector3(x,y,0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player Ref"){
            GameObject.Find("Player").GetComponent<Player>().isEatingApple = true;
            gameObject.SetActive(false);
        }
    }

    
}
