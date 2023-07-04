using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State1 : MonoBehaviour
{
    private GameObject mainPlayer;
    public Bullet bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        mainPlayer = GameObject.Find("Player");

        InitPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Rotating();
        if(Input.GetMouseButtonDown(0)){
            Shooting();
        }
    }

    private void Shooting(){
        var bullet1 = Instantiate(bulletPrefab, transform.position, transform.localRotation);
        bullet1.InitBullet(-1);
        var bullet2 = Instantiate(bulletPrefab, transform.position, transform.localRotation);
        bullet2.InitBullet(1);
        Destroy(gameObject);
        mainPlayer.GetComponent<Player>().GetRandomState();

    }

    private void Rotating(){
        transform.Rotate(0,0,-0.5f);
    }

    private void InitPosition(){
        transform.parent = mainPlayer.transform;
        transform.position = mainPlayer.transform.position;
    }
}
