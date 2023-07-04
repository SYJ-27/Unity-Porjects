using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animals : MonoBehaviour
{
    public Vector2 targetPos;
    public float speed;
    public bool isDown, isCheckMover;

    // Start is called before the first frame update
    void Start()
    {
        isDown = true;
        isCheckMover = true;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(GameObject.Find("Players").transform.position, transform.position);
        if (distance < 2)
        {
            isDown = false;
            if (transform.position.x < GameObject.Find("Players").transform.position.x)
            {
                Check(0);
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, targetPos) < 0.1f)
                {
                    isDown = true;
                }
            }
            else if (transform.position.x > GameObject.Find("Players").transform.position.x)
            {
                Check(1);
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, targetPos) < 0.1f)
                {
                    isDown = true;
                }
            }
        }
        else
        {
            isDown = true;
            isCheckMover = true;
        }
    }

    private void FixedUpdate()
    {
        if (isDown)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }

    public void Check(int index)
    {
        if (isCheckMover)
        {
            if (index == 0)
            {
                targetPos = new Vector2(Random.Range(GameObject.Find("PosAnimals1").transform.position.x, transform.position.x), transform.position.y);
            }
            else if (index == 1)
            {
                targetPos = new Vector2(Random.Range(transform.position.x, GameObject.Find("PosAnimals2").transform.position.x), transform.position.y);
            }
            isCheckMover = false;
        }
    }

}
