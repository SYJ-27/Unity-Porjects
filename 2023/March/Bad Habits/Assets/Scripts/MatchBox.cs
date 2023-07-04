using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MatchBox : MonoBehaviour
{
    public int stateSize;
    public bool canEat;
    public Match matchPrefab;
    public List<Match> listOfMatch = new List<Match>();
    void Awake(){
        canEat = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootMatch", 1,1);
    }

    // Update is called once per frame
    void Update()
    {
        if(!canEat && !GameManager.gameManager.isStart){
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void InitMatchBox(){
        stateSize = Random.Range(2, 5);
        transform.localScale += new Vector3(stateSize * 0.1f, stateSize * 0.1f, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Match Box" || other.tag == "Beer" || other.tag == "Enemy3" || other.tag == "Enemy4"){
            Destroy(gameObject);
            GameManager.gameManager.SpawnMatchBox();
        }

    }

    public void SetCanEat(){
        canEat = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        ScaleUpDown();
    }

    public void ScaleUpDown(){
        transform.DOScale(1 + stateSize*0.1f + 0.1f, 0.5f).OnComplete(() =>{
            transform.DOScale(1 + stateSize*0.1f, 0.5f).OnComplete(() =>{
                ScaleUpDown();
            });
        });
    }

    public void ShootMatch(){
        if(!GameManager.gameManager.isStart){
            var matchBullet = Instantiate(matchPrefab, transform.position, Quaternion.identity);
            matchBullet.InitSpeed(Random.Range(1f, 2f));
            listOfMatch.Add(matchBullet);
        }
    }

    public void ClearMatchShootList(){
        CancelInvoke("ShootMatch");
        for(int i = 0; i < listOfMatch.Count; i++){
            if(listOfMatch[i] != null){
                Destroy(listOfMatch[i].gameObject);
            }
        }
        listOfMatch.Clear();
    }


}
