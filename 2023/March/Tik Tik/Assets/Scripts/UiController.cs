using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Player mainPlayer;
    public Text textLife, textScore, textPowerful, textDamage;
    public GameObject powerfulBullet, damageBullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGameUI();
    }

    void UpdateGameUI(){
        textLife.text = $"{mainPlayer.lifePlayer}";
        textScore.text = $"{GameManager.gameManager.score}";
        if(GameManager.gameManager.resetDamageTime > 0){
            damageBullet.gameObject.SetActive(true);
            float time1 = (float)Math.Round(GameManager.gameManager.resetDamageTime, 2);
            textDamage.text = $"{time1}";
        }
        else{
            damageBullet.gameObject.SetActive(false);
        }

        if(GameManager.gameManager.resetBulletTime > 0){
            powerfulBullet.gameObject.SetActive(true);
            float time1 = (float)Math.Round(GameManager.gameManager.resetBulletTime, 2);
            textPowerful.text = $"{time1}";
        }
        else{
            powerfulBullet.gameObject.SetActive(false);
        }
        
    }

}
