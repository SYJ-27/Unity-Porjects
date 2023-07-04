using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bomb : MonoBehaviour
{
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("BombExplore", Random.Range(2f, 4f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BombExplore(){
        var explore = Instantiate(explosion, transform.position, Quaternion.identity);
        explore.gameObject.transform.localScale = new Vector3(0,0,0);
        explore.gameObject.transform.DOScale(new Vector3(3, 3, 1), 0.3f).OnComplete(() =>{
            Destroy(explore);
            Destroy(gameObject);
        });
    }

}
