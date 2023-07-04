using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseStar : MonoBehaviour
{
    public int idStar;
    float speed, timePausing;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(1f, 2f);
        timePausing = Random.Range(3f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Moving();
    }

    void Moving(){
        if(idStar == 0){
            transform.Translate(-transform.up * speed * Time.deltaTime);
        }
        else if(idStar == 1){
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else if(idStar == 2){
            transform.Translate(transform.up * speed * Time.deltaTime);
        }
        else{
            transform.Translate(-transform.right * speed * Time.deltaTime);
        }
    }

    public void PausingTime(){
        GameManager.gameManager.timePause = timePausing;
        GameManager.gameManager.GamePausing();
        Destroy(gameObject);
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Wall"){
            Destroy(gameObject);
        }
    }

}
