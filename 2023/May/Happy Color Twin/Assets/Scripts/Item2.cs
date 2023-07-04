using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2 : MonoBehaviour
{
    public GameObject timeExpl;
        public int idTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyItem2(){
        Destroy(gameObject);
        Destroy(Instantiate(timeExpl, transform.position, Quaternion.identity), 0.3f);
    }
}
