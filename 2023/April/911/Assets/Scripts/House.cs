using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public int[] listStatus;
    public List<GameObject> prefabStatus;
    List<GameObject> listStatusGot = new List<GameObject>();
    public Sprite dieHouse;
    public bool isDead;
    public int i = 0, life = 3;
    void Awake(){
        isDead = false;
        life = 3;
        listStatus = new int[3]{0, 1, 2};
        
    }
    // Start is called before the first frame update
    void Start()
    {
        // transform.localScale = new Vector3(6,6,1);
        RandomStatus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomStatus(){
        
        Invoke("GetStatus", UnityEngine.Random.Range(2f, 4f));
    }

    public void GetStatus(){
        if(i == 0){
            var rnd = new System.Random();
            listStatus = listStatus.OrderBy(item => rnd.Next()).ToArray();
        }
        var statusSpawn = Instantiate(prefabStatus[listStatus[i] % 3], transform.position, Quaternion.identity);
        if(listStatus[i] % 3 == 0){
            statusSpawn.GetComponent<Fire>().InitStatus(this);
        }
        else if(listStatus[i] % 3 == 1){
            statusSpawn.GetComponent<Heart>().InitStatus(this);
        }
        else{
            statusSpawn.GetComponent<Sos>().InitStatus(this);
        }
        listStatusGot.Add(statusSpawn);
        i++;
        if(i > 2){
            var rnd = new System.Random();
            listStatus = listStatus.OrderBy(item => rnd.Next()).ToArray();
            i = 0;
        }
    }

    public void MinusLifeHouse(){
        life--;
        if(life <= 0){
            CancelInvoke("GetStatus");
            GetComponent<SpriteRenderer>().sprite = dieHouse;
            isDead = true;
            ClearListStatus();
            // Destroy(gameObject);
        }
        else{
            RandomStatus();
        }
    }

    public void ClearListStatus(){
        for(int i = 0; i < listStatusGot.Count; i++){
            if(listStatusGot[i] != null){
                Destroy(listStatusGot[i]);
            }
        }
        listStatusGot.Clear();
    }

}
