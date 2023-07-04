using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    public Text numberText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitNumberText(int num){
        if(num == 0){
            gameObject.SetActive(false);
        }
        else{
            gameObject.SetActive(true);
        }
        numberText.text = $"{num}";
    }
}
