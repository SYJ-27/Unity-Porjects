using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public int earthLife, maxLife;
    void Awake(){
        maxLife = 5;
        earthLife = 5;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy"){
            Destroy(other.gameObject);
            earthLife--;
            if(earthLife <= 0){
                earthLife = 0;
            }
        }
    }

}
