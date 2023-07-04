using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teapot : MonoBehaviour
{
    public GameObject explosion;
    public Teadrop prefabsTeaDrop;
    List<Teadrop> listTeaDrop = new List<Teadrop>();
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootTeadrop", 2, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameManager.isLose){
            CancelInvoke("ShootTeadrop");
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            Player playerTemp = other.gameObject.GetComponent<Player>();
            if(playerTemp.isHitting){
                Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 0.3f);
                DestroyAllTeadrop();
            }
        }
    }

    void ShootTeadrop(){
        listTeaDrop.Add(Instantiate(prefabsTeaDrop, transform.position, Quaternion.identity));
    }

    public void DestroyAllTeadrop(){
        for(int i = 0; i < listTeaDrop.Count; i++){
            if(listTeaDrop[i] != null){
                Destroy(listTeaDrop[i].gameObject);
            }
        }
    }
}
