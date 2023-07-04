using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BigFace : MonoBehaviour
{
    public GameObject faceInCharge;
    public Player mainPlayer;
    public int id;
    public int idStatus;
    public bool isInCharge;
    public List<Sprite> listStatusSprites;
    SpriteRenderer faceSprite;
    // public bool isStopIncharge;
    // sbool isKill;
    Sequence mySequence;

    void Awake(){
        // isInCharge = false;
        // isStopIncharge = false;
        mainPlayer = GameObject.Find($"Player {id + 1}").GetComponent<Player>();
        faceInCharge.transform.localScale = Vector3.zero;
        faceSprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // if(GameManager.gameManager.isCancelInCharge){
        //     // isStopIncharge = false;
        //     Debug.Log("Stop in charge");
        //     DOTween.Kill("InCharge", true);
        //     faceInCharge.transform.localScale = Vector3.zero;
        // }
    }

    public bool CheckIdPlayer(int idPlayer){
        if(idPlayer == id){
            if(idStatus == 1){
                return true;
            }
        }
        return false;
    }

    public void EnableBigFace(){
        GetComponent<PolygonCollider2D>().enabled = true;
        faceSprite.sprite = listStatusSprites[1];
        idStatus = 1;
    }

    public void StartInCharge(){
        faceInCharge.transform.DOScale(Vector3.one, 2f).SetId("InCharge").OnComplete(() =>{
            mainPlayer.isInBigFace = false;
            Destroy(gameObject);
        });
    }

    public void StopInCharge(){
        // Debug.Log("Stop in charge");
        // DOTween.Kill("InCharge", true);
        // faceInCharge.transform.localScale = Vector3.zero;
        // DOTween.Kill("InCharge", true);
        if(!GameManager.gameManager.isNextFace){
            DOTween.Kill("InCharge");
            mySequence.Kill();
            mySequence = null;
        }
        faceInCharge.transform.localScale = Vector3.zero;
        // isStopIncharge = true;
    }

    public void InitBigFace(int idFace, int idStatusSprite){
        id = idFace;
        idStatus = idStatusSprite;
        faceSprite.sprite = listStatusSprites[idStatusSprite];
        if(idStatus == 0){
            GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

    public void Destroying(int x){
        mainPlayer.isInBigFace = false;
        mainPlayer.isCharging = false;
        if(mySequence == null){
            mySequence = DOTween.Sequence();
            mySequence.Append(
                faceInCharge.transform.DOScale(Vector3.one, 2f).SetLoops(1, LoopType.Yoyo).SetId("InCharge").OnComplete(() =>{
                    GameManager.gameManager.isNextFace = true;
                    mainPlayer.isInBigFace = false;
                    if(x == 0){
                        GameManager.gameManager.EnableNextBigFaces();
                    }
                    Destroy(gameObject);
                })
            );
            mySequence.Play();  
        }
        
    }

}
