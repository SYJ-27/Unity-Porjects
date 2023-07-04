using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Text scoreText;
    public int score;
    void Awake(){
        score = 0;
        scoreText.text = $"{score}";
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreUI(){
        score = GameManager.gameManager.score;
        scoreText.text = $"{score}";
    }

}
