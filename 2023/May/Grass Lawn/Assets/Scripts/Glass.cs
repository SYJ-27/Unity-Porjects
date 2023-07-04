using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    public List<Sprite> listStatusGlass;
    int idGlass = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        SetGlassStatus(idGlass);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isClean(){
        if(idGlass >= listStatusGlass.Count - 1){
            return true;
        }
        return false;
    }

    public void SetColor(Color thisColor){
        // GetComponent<SpriteRenderer>().color = thisColor;
    }

    void SetGlassStatus(int id){
        GetComponent<SpriteRenderer>().sprite = listStatusGlass[id];
    }

    public void DisableGrass(){
        GetComponent<BoxCollider2D>().enabled = false;
        idGlass = listStatusGlass.Count - 1;
        SetGlassStatus(idGlass);
    }

    void IdUp(){
        if(idGlass >= listStatusGlass.Count){
            CancelInvoke("IdUp");
        }
        else{
            if(idGlass < listStatusGlass.Count){
                idGlass++;
            }
        }
        if(idGlass < listStatusGlass.Count){
            SetGlassStatus(idGlass);
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            Player playerTemp = other.gameObject.GetComponent<Player>();
            if(playerTemp != null && playerTemp.canCleanAll){
                idGlass = listStatusGlass.Count - 1;
                SetGlassStatus(idGlass);
            }
            else{
                InvokeRepeating("IdUp", 0, 1f);
            }
        }
        if(other.tag == "Saw"){
            if(other.gameObject.GetComponent<Saw>().canCleanGrass){
                IdUp();
            }
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player"){
            CancelInvoke("IdUp");
        }
        // if(other.tag == "Stone"){
        //     GetComponent<BoxCollider2D>().enabled = false;
        // }
    }

}
