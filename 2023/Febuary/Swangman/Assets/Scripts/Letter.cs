using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    public SpriteRenderer spriteRender;
    public List<Sprite> listLetters;
    public int letterID;

    public void InitLetter(int id){
        letterID = id;
        spriteRender.sprite = listLetters[letterID];
    }
    

}
