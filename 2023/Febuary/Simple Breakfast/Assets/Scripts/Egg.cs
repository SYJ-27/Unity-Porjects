using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public float[] rangeX = {-3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7};
    public float[] rangeY = {-3.5f, -2.5f, -1.5f, -0.5f, 0.5f, 1.5f, 2.5f};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomGenerateEgg(int x, int y){
        int Xpos = Random.Range(5-x, 6+x), Ypos = Random.Range(3-y, 4+y);
        transform.position = new Vector3(rangeX[Xpos], rangeY[Ypos], 0);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player" || other.tag == "Bread"){
            if(Player.mainPlayer.isNewLevel){
                Destroy(gameObject);
            }
            else{
                if(other.tag == "Player"){
                    GameManager.gameManager.GameLose();
                }
            }
        }
    }
}
