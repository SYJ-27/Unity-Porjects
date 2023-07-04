using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State5 : MonoBehaviour
{
    private GameObject mainPlayer;
    public List<Transform> transformDirections;
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
        var bullet1 = Instantiate(bulletPrefab, transformDirections[0].position, transformDirections[0].localRotation);
        bullet1.InitBullet(1);
        var bullet2 = Instantiate(bulletPrefab, transformDirections[1].position, transformDirections[1].localRotation);
        bullet2.InitBullet(1);
        var bullet3 = Instantiate(bulletPrefab, transformDirections[2].position, transformDirections[2].localRotation);
        bullet3.InitBullet(1);
        Destroy(gameObject);
        mainPlayer.GetComponent<Player>().GetRandomState();

    }

    private void Rotating(){
        transformDirections[0].Rotate(0,0,-0.5f);
        transformDirections[1].Rotate(0,0,-0.5f);
        transformDirections[2].Rotate(0,0,-0.5f);
    }

    private void InitPosition(){
        transform.parent = mainPlayer.transform;
        transform.position = mainPlayer.transform.position;
    }
}
