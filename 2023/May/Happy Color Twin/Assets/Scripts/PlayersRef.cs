using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersRef : MonoBehaviour
{
    public Transform playerTrans;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    void Rotate(){
        var angle = Mathf.Atan2(playerTrans.position.y - transform.position.y, playerTrans.position.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
