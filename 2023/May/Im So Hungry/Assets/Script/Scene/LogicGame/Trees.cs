using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        UpPos();
    }

    public void UpPos()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        float rdX = Random.Range(GameObject.Find("posTree1").transform.position.x, GameObject.Find("posTree2").transform.position.x);
        float rdY = Random.Range(GameObject.Find("posTree3").transform.position.y, GameObject.Find("posTree4").transform.position.y);
        transform.position = new Vector2(rdX, rdY);
    }
}
