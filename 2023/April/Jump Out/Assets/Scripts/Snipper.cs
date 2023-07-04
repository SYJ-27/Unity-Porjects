using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snipper : MonoBehaviour
{
    public LineRenderer predictLine;
    public bool isActiveLazer;
    public List<Transform> listSnippers;
    public LineCollision lineCollisionController;
    Vector3 worldPoint;
    // Start is called before the first frame update
    void Start()
    {
        isActiveLazer = false;
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        SetPositionSnippers();
        predictLine.SetPositions(listSnippers.ConvertAll(n => n.position - new Vector3(0,0,5)).ToArray());
        Invoke("ActiveLazer", 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetPositionSnippers(){
        listSnippers[0].position = RandomPosition();
        listSnippers[1].position = new Vector3(-listSnippers[0].position.x, -listSnippers[0].position.y, 0);
        RotateSnippers();
    }

    Vector3 RandomPosition(){
        float x, y;
        int pos = Random.Range(0, 3);
        if(pos == 0){
            x = -worldPoint.x + 1;
            y = Random.Range(-worldPoint.y + 1, worldPoint.y - 1);
        }
        else if(pos == 1){
            x = Random.Range(-worldPoint.x + 1, worldPoint.x - 1);
            y = Random.Range(0, 2);
            if(y == 0){
                y = worldPoint.y - 1;
            }
            else{
                y = -worldPoint.y + 1;
            }
        }
        else{
            x = worldPoint.x - 1;
            y = Random.Range(-worldPoint.y + 1, worldPoint.y - 1);
        }
        return new Vector3(x,y,0);
    }

    void ActiveLazer(){
        isActiveLazer = true;
        lineCollisionController.ActiveLineCollision();
        predictLine.gameObject.SetActive(false);
        Destroy(gameObject, 1.3f);
    }

    void RotateSnippers(){
        for(int i = 0; i < listSnippers.Count; i++){
            var angle = Mathf.Atan2(- listSnippers[i].position.y, - listSnippers[i].position.x) * Mathf.Rad2Deg - 90;
            listSnippers[i].rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

}
