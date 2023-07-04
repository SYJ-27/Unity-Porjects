using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Apple : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RandomApplePosition();
        ScaleUpDown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomApplePosition(){
        float randomX = Random.Range((int)GameManager.gameManager.minX + 1, (int)GameManager.gameManager.maxX - 1);
        float randomY = Random.Range((int)GameManager.gameManager.minY + 0.5f, (int)GameManager.gameManager.maxY - 0.5f);

        transform.position = new Vector3(randomX, randomY, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Match Box" || other.tag == "Beer" || other.tag == "Enemy3" || other.tag == "Enemy4"){
            RandomApplePosition();
        }
    }

    public void ScaleUpDown(){
        transform.DOScale(1.1f, 0.5f).OnComplete(() =>{
            transform.DOScale(1f, 0.5f).OnComplete(() =>{
                Invoke("ScaleUpDown", 1);
            });
        });
    }

}
