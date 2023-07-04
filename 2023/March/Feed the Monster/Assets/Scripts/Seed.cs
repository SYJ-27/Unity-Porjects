using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Seed : MonoBehaviour
{
    public bool isUnPick;
    public float orgY;
    public int seedType;
    void Awake(){
        isUnPick = true;
        orgY = transform.position.y;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isUnPick){
            SetUnPick();
        }
    }

    void OnMouseDown(){
        GameManager.gameManager.SetAllUnPick();
        isUnPick = false;
        transform.DOMoveY(orgY + 0.5f, 0.1f).OnComplete(() =>{
            // GameManager.gameManager.AddList(transform.position.x);
            // Destroy(gameObject);
            return;
        });
    }

    public void InitSeed(int typeS){
        seedType = typeS;
    }

    // void OnMouseEnter(){
    //     transform.DOMoveY(orgY + 0.5f, 0.1f).OnComplete(() =>{
    //         return;
    //     });
    // }

    // void OnMouseExit(){
    //     transform.DOMoveY(orgY, 0.1f).OnComplete(() =>{
    //         return;
    //     });
    // }

    private void SetUnPick(){
        transform.DOMoveY(orgY, 0.1f).OnComplete(() =>{
            return;
        });
    }

    public void SetIsUnPick(bool state){
        isUnPick = state;
    }

    public void ChoosingSeed(){
        GameManager.gameManager.SpawnSeedOfFood(seedType, new Vector3(transform.position.x, transform.position.y, 0));
        GameManager.gameManager.AddList(transform.position.x);
        Destroy(gameObject);
    }

}
