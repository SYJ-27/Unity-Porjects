using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public GameOver gameOver;
    public Earth mainEarth;
    public Image healthBar;
    public List<Text> listTextFlowerStatus;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTextFlowerStatus();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.gameManager.isLose){
            UpdateTextFlowerStatus();
            UpdateHealthEarth();
        }
    }

    void UpdateTextFlowerStatus(){
        for(int i = 0; i < listTextFlowerStatus.Count; i++){
            listTextFlowerStatus[i].text = $"{GameManager.gameManager.scoreFlowers[i]}";
        }
    }

    void UpdateHealthEarth(){
        float currentHealth = (float)mainEarth.earthLife;
        if(currentHealth > 0){
            healthBar.fillAmount = currentHealth/mainEarth.maxLife;
        }
        else{
            currentHealth = 0;
            healthBar.fillAmount = currentHealth/mainEarth.maxLife;
            GameManager.gameManager.GameLosing();
        }
    }

    public void GetGameOver(){
        gameOver.GameOverScene();
    }
}
