using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishingRod : MonoBehaviour
{
    public float yMax = 6f, yMin = 3f;
    public int rodType;

    void Awake(){
        Invoke("MoveUpNoSquid", Random.Range(7, 9));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitPosition(int id){
        rodType = id;
        float randomY = Random.Range(yMin, yMax);
        transform.DOMoveY(randomY, Random.Range(0.2f, 0.4f));
    }

    public void MoveUp(){
        transform.DOMoveY(10, 0.2f);
        Destroy(gameObject, 1);
    }

    public void MoveUpNoSquid(){
        transform.DOMoveY(10, 0.2f).OnComplete(() =>{
            Destroy(gameObject);
        });
        
    }

    public void RenewSquid(){
        
    }

}
