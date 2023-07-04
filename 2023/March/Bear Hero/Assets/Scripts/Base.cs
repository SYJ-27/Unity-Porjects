using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Base : MonoBehaviour
{
    public Transform mainPlayer;
    public float xOrg;
    public bool isRightSide = false;

    void Awake(){
        xOrg = transform.position.x;
        DisableCollider();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitLeft(){
        isRightSide = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        float playerX = Player.mainPlayer.gameObject.transform.position.x;
        transform.position = new Vector3(playerX - xOrg, transform.position.y,0);
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        Hit(180);
    }

    public void HitRight(){
        isRightSide = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        float playerX = Player.mainPlayer.gameObject.transform.position.x;
        transform.position = new Vector3(playerX + xOrg, transform.position.y,0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        Hit(0);
    }

    public void Hit(float rotationY){
            transform.DORotate(new Vector3(0, rotationY, -180), 0.15f).OnComplete(() => {
                transform.DORotate(new Vector3(0, rotationY, 0), 0).OnComplete(() => {
                    DisableCollider();
                });
            });
    }

    public void DisableCollider(){
        gameObject.GetComponent<CircleCollider2D>().enabled = false;

    }

}
