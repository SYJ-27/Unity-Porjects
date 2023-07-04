using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingLine : MonoBehaviour
{
    EdgeCollider2D egdeCollider;
    LineRenderer lineRender;
    SpriteRenderer lineSprite;
    public List<Transform> listPlayerTrans;
    public List<Transform> listPlayerPointTrains;
    public bool isActive;

    void Awake(){
        egdeCollider = GetComponent<EdgeCollider2D>();
        lineRender = GetComponent<LineRenderer>();
        lineSprite = GetComponent<SpriteRenderer>();
        isActive = true;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SetLineRenderer();
        SetEdgeCollider();
    }

    void SetLineRenderer(){
        for(int i = 0; i < listPlayerTrans.Count; i++){
            lineRender.SetPosition(i, listPlayerTrans[i].position);
        }
    }

    void SetEdgeCollider(){
        List<Vector2> edges = new List<Vector2>();
        // egdeCollider.SetPoints(edges);

        for(int i = 0; i < listPlayerPointTrains.Count + 1; i++){
            Vector2 point = new Vector2(listPlayerPointTrains[i % (listPlayerPointTrains.Count)].position.x, listPlayerPointTrains[i % (listPlayerPointTrains.Count)].position.y);
            edges.Add(point);
        }

        egdeCollider.SetPoints(edges);
    }

    public void DisableLine(){
        GameManager.gameManager.isPlayerConnecting = false;
        gameObject.SetActive(false);
        isActive = false;
    }

    public void EnableLine(){
        gameObject.SetActive(true);
        isActive = true;
        GameManager.gameManager.isPlayerConnecting = true;
    }

}
