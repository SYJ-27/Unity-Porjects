using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public int negativeX, negativeY;
    public Transform stateBall;
    void Awake(){
        SetSizeToScreen();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D other){
        
    }

    private void SetSizeToScreen(){
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float width = GetComponent<SpriteRenderer>().bounds.size.x;
        float height = GetComponent<SpriteRenderer>().bounds.size.y;

        transform.localScale = new Vector3(negativeX*worldPos.x/width*2, negativeY*worldPos.y/height*2, 0);
    }


}
