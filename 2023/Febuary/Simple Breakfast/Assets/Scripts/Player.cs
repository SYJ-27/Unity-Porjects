using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player mainPlayer;
    public float minX, maxX, minY, maxY;
    public float[] rangeX = {-3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7};
    public float[] rangeY = {-3.5f, -2.5f, -1.5f, -0.5f, 0.5f, 1.5f, 2.5f};
    public bool isNewLevel;
    void Awake(){
        isNewLevel = true;
        mainPlayer = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        minX = rangeX[4];
        maxX = rangeX[6];
        minY = rangeY[2];
        maxY = rangeY[4];
    }

    // Update is called once per frame
    void Update()
    {
        float mainX = Mathf.Clamp(transform.position.x, minX, maxX);
        float mainY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(mainX, mainY, 0);
    }

    // Limit Area Playing
    public void SetRangePlayer(int x, int y){
        isNewLevel = true;
        minX = rangeX[5 - x];
        maxX = rangeX[5 + x];
        minY = rangeY[3 - y];
        maxY = rangeY[3 + y];
    }

    // Player Movement
    public void MoveUp(){
        isNewLevel = false;
        transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
    }

    public void MoveDown(){
        isNewLevel = false;
        transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
    }

    public void MoveLeft(){
        isNewLevel = false;
        transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
    }

    public void MoveRight(){
        isNewLevel = false;
        transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
    }
}
