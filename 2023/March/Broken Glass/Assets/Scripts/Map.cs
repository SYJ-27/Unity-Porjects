using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<GameObject> mapObjects;
    public bool isCreateMap;
    void Awake(){
        isCreateMap = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckObjectNull() && !isCreateMap){
            isCreateMap = true;
            Invoke("CreateMap", 0.5f);
            // CreateMap();
        }
    }

    bool CheckObjectNull(){
        for(int i = 0; i < mapObjects.Count; i++){
            if(mapObjects[i] != null){
                return false;
            }
        }
        return true;
    }

    public void DestroySelf(){
        Destroy(gameObject);
    }

    public void CreateMap(){
        GameManager.gameManager.SpawnMap();
    }
}
