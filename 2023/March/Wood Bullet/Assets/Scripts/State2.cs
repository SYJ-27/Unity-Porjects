using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class State2 : MonoBehaviour
{
    private GameObject mainPlayer;

    public float speed;
    public bool canMove;
    void Awake(){
        mainPlayer = GameObject.Find("Player");
    }
    // Start is called before the first frame update
    void Start()
    {

        canMove = true;
        speed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && EventSystem.current.currentSelectedGameObject == null && canMove)
        {
            Move();
        }
        if(GameManager.gameManager.isLose){
            Destroy(gameObject);
        }
    }

    private void Move(){
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePos, speed * Time.deltaTime);
        LimitPosition();
    }

    private void LimitPosition(){
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float x = Mathf.Clamp(transform.position.x, -mousePos.x + 2f, mousePos.x - 2f);
        float y = Mathf.Clamp(transform.position.y, -3.5f, 3.5f);
        transform.position = new Vector3(x,y,0);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(!mainPlayer.GetComponent<Player>().hasShield){
                canMove = false;
                transform.localScale = new Vector3(1, 1, 1);
                transform.parent = other.gameObject.transform;
                transform.position = other.gameObject.transform.position;
                mainPlayer.GetComponent<Player>().SetHadShield(true);
            }
            else{
                Destroy(gameObject);
            }
            mainPlayer.GetComponent<Player>().GetRandomState();
        }
        if(other.tag == "Enemy" && !canMove){
            mainPlayer.GetComponent<Player>().score++;
            mainPlayer.GetComponent<Player>().DisableSheild();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }


}
