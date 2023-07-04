using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public GameObject heartExplore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyItSelf(){
        Destroy(Instantiate(heartExplore, transform.position, Quaternion.identity), 0.3f);
        Destroy(gameObject);
    }

}
