using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public List<GameObject> prefabHouses;
    public List<Transform> listSpots;
    List<GameObject> listHouse = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        SpawnHouseArea();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnHouseArea(){
        for(int i = 0; i < listSpots.Count; i++){
            listHouse.Add(Instantiate(prefabHouses[Random.Range(0, 2)], listSpots[i].position, Quaternion.identity, listSpots[i]));
        }
    }

    public bool CheckAllHouseDestroyed(){
        for(int i = 0; i < listHouse.Count; i++){
            if(!listHouse[i].GetComponent<House>().isDead){
                return false;
            }
        }
        return true;
    }

    public void AllClearHouse(){
        for(int i = 0; i < listHouse.Count; i++){
            if(listHouse[i] != null){
                listHouse[i].GetComponent<House>().ClearListStatus();
                Destroy(listHouse[i]);
            }
        }
        listHouse.Clear();
    }

    public int GetHouseRemain(){
        int houseCount = 0;
        for(int i = 0; i < listHouse.Count; i++){
            if(!listHouse[i].GetComponent<House>().isDead){
                houseCount++;
            }
        }
        return houseCount;
    }

}
