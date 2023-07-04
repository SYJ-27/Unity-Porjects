using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public List<Sprite> balloonSprites;
    public bool canFly;
    Vector2 ballPos;

    void Awake(){
        ballPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        gameObject.GetComponent<SpriteRenderer>().sprite = balloonSprites[Random.Range(0, balloonSprites.Count)];
    }

    void Start(){
        canFly = false;
        
    }
    void Update(){
        if(GameManager.gameManager.isPlaying){
            EnableFly();
        }
        if(canFly){
            transform.Translate(Vector2.up * Time.deltaTime);
        }
        LimitPosition();
    }
    void EnableFly(){
        canFly = true;
    }

    public void InitPosition(){

        float x = Random.Range(-ballPos.x + 3, ballPos.x - 3);
        float y = Random.Range(-ballPos.y + 1, 1);

        transform.position = new Vector3(x, y, 0);

    }

    void LimitPosition(){
        
        float x = Mathf.Clamp(transform.position.x, -ballPos.x, ballPos.x);
        float y = Mathf.Clamp(transform.position.y, -ballPos.y, ballPos.y);
        transform.position = new Vector3(x,y,0);
    }
}
