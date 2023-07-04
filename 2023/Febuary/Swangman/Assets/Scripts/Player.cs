using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform face;
    public static Player mainPlayer;
    public LineRenderer lineRender;
    public List<GameObject> bodyPart;
    int count = 0;
    // Start is called before the first frame update
    void Awake(){
        mainPlayer = this;
    }

    void Start()
    {
        for(int i = 0; i < bodyPart.Count; i++){
            bodyPart[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = face.position;
        lineRender.SetPosition(0, new Vector3(0,5,0));
        lineRender.SetPosition(1, transform.position);
        if(count >= 5){
            GameManager.gameManager.GameLose();
        }
    }

    public void DisableBody(){
        for(int i = 0; i < bodyPart.Count; i++){
            bodyPart[i].SetActive(false);
        }
        count = 0;
    }

    public void SetActiveBody(){
        // count++;
        // for(int i = 0; i < count; i++){
        //     if(i < 5){
        //         bodyPart[i].SetActive(true);
        //     }
        // }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Letter"){
            GameManager.gameManager.RemoveLetterCorrect(other.GetComponent<Letter>().letterID);
            Destroy(other.gameObject);
        }
    }
}
