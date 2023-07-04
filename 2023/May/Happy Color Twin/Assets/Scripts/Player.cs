using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ConnectingLine mainLine;
    public Player otherPlayer;
    public float speed = 3;
    public int id;
    public bool isInBigFace, isInItem1, isInItem2, isCharging;
    public GameObject thisBigFace, thisItem1, thisItem2;
    Vector3 worldPoint;

    void Awake(){
        speed = 3;
        isCharging = false;
        isInBigFace = false;
        isInItem1 = false;
        isInItem2 = false;
        worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // LimitPosition();
    }

    void LimitPosition(){
        float x = Mathf.Clamp(transform.position.x, -worldPoint.x, worldPoint.x);
        float y = Mathf.Clamp(transform.position.y, -worldPoint.y, worldPoint.y);
        transform.position = new Vector3(x,y,0);
    }

    void MoveUp(){
        // GameManager.gameManager.isStart = false;
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        LimitPosition();
    }

    void MoveDown(){
        // GameManager.gameManager.isStart = false;
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        LimitPosition();
    }

    void MoveLeft(){
        // GameManager.gameManager.isStart = false;
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        LimitPosition();
    }

    void MoveRight(){
        // GameManager.gameManager.isStart = false;
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        LimitPosition();
    }

    public void ButtonUp(){
        GameManager.gameManager.isStart = false;
        InvokeRepeating("MoveUp", 0, 0.01f);
    }

    public void ButtonDown(){
        GameManager.gameManager.isStart = false;
        InvokeRepeating("MoveDown", 0, 0.01f);
    }

    public void ButtonLeft(){
        GameManager.gameManager.isStart = false;
        InvokeRepeating("MoveLeft", 0, 0.01f);
    }

    public void ButtonRight(){
        GameManager.gameManager.isStart = false;
        InvokeRepeating("MoveRight", 0, 0.01f);
    }

    public void ButtonUpRelease(){
        CancelInvoke("MoveUp");
    }

    public void ButtonDownRelease(){
        CancelInvoke("MoveDown");
    }

    public void ButtonLeftRelease(){
        CancelInvoke("MoveLeft");
    }

    public void ButtonRightRelease(){
        CancelInvoke("MoveRight");
    }

    public void DestroyBigFace(int x){
        // Destroy(thisBigFace);
        // isInBigFace = false;
        thisBigFace.GetComponent<BigFace>().Destroying(x);
        thisBigFace = null;
    }

    public void ResetPlayer(){
        mainLine.EnableLine();
        transform.position = new Vector3(1 - 2 * id, 0, 0);
        CancelInvoke("MoveUp");
        CancelInvoke("MoveDown");
        CancelInvoke("MoveLeft");
        CancelInvoke("MoveRight");

    }

    public void DestroyItem1(){
        // Destroy(thisItem1);
        thisItem1.GetComponent<Item1>().DestroyItem1();
        isInItem1 = false;
    }

    public void DestroyItem2(){
        // Destroy(thisItem2);
        thisItem2.GetComponent<Item2>().DestroyItem2();
        isInItem2 = false;
    }

    public int GetItem1ID(){
        if(thisItem1 != null){
            return thisItem1.GetComponent<Item1>().idBean;
        }
        return -1;
    }

    public int GetItem2ID(){
        if(thisItem2 != null){
            return thisItem2.GetComponent<Item2>().idTime;
        }
        return -1;
    }

    void NextFace(){
        Debug.Log("Im In 1372");
        GameManager.gameManager.EnableNextBigFaces();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Big Face"){
            BigFace tempFace = other.gameObject.GetComponent<BigFace>();
            if(tempFace.CheckIdPlayer(id) && mainLine.isActive){
                isInBigFace = true;
                thisBigFace = tempFace.gameObject;
                if(otherPlayer.isInBigFace && !isCharging){
                    // GameManager.gameManager.isCancelInCharge = false;
                    isCharging = true;
                    otherPlayer.isCharging = true;
                    // thisBigFace.GetComponent<BigFace>().StartInCharge();
                    DestroyBigFace(1);
                    otherPlayer.DestroyBigFace(0);
                    // Invoke("NextFace", 2);
                    // NextFace();
                    // GameManager.gameManager.EnableNextBigFaces();

                }
            }
        }

        if(other.tag == "Item1"){
            Item1 tempItem = other.gameObject.GetComponent<Item1>();
            isInItem1 = true;
            thisItem1 = tempItem.gameObject;
            if(otherPlayer.isInItem1 && !CheckIdItems(1, otherPlayer)){
                DestroyItem1();
                otherPlayer.DestroyItem1();
                GameManager.gameManager.score++;
                GameManager.gameManager.UpdateScoreBean();
                mainLine.EnableLine();
            }
        }

        if(other.tag == "Item2"){
            Item2 tempItem = other.gameObject.GetComponent<Item2>();
            isInItem2 = true;
            thisItem2 = tempItem.gameObject;
            if(otherPlayer.isInItem2 && mainLine.isActive && !CheckIdItems(2, otherPlayer)){
                DestroyItem2();
                otherPlayer.DestroyItem2();
                GameManager.gameManager.UpdateTime();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Big Face"){
            // GameManager.gameManager.isCancelInCharge = true;
            thisBigFace = other.gameObject;
            isCharging = false;
            isInBigFace = false;
            StopNext();
            otherPlayer.StopNext();
            DisableCollider();
            otherPlayer.DisableCollider();
            // CancelInvoke("NextFace");
            // if(thisBigFace != null){
            //     thisBigFace = null;
            // }
        }
        if(other.tag == "Item1"){
            isInItem1 = false;
        }
        if(other.tag == "Item2"){
            isInItem2 = false;
        }
    }

    bool CheckIdItems(int itemNum,Player other){
        if(itemNum == 1){
            if(GetItem1ID() == other.GetItem1ID() || GetItem1ID() == -1 || other.GetItem1ID() == -1){
                return true;
            }
            return false;
        }
        else{
            if(GetItem2ID() == other.GetItem2ID() || GetItem2ID() == -1 || other.GetItem2ID() == -1){
                return true;
            }
            return false;
        }
    }

    public void StopNext(){
        if(thisBigFace != null){
            thisBigFace.GetComponent<BigFace>().StopInCharge();
        }
    }

    public void DisableCollider(){
        GetComponent<PolygonCollider2D>().enabled = false;
        Invoke("EnableCollider", 0.01f);
    }

    public void EnableCollider(){
        GetComponent<PolygonCollider2D>().enabled = true;
    }


}
