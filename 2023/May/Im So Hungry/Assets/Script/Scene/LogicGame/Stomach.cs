using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stomach : MonoBehaviour
{
    public Image full, stomach;
    public Sprite[] spritesFull, spritesStomach;
    public float min, max;
    public bool isCheck;
    // Start is called before the first frame update
    void Start()
    {
        isCheck = false;
        ShowSpritesFull(0);
        ShowStomach(0);
    }

    private void FixedUpdate()
    {
        if (isCheck)
        {
            Check();
        }
    }

    private void Update()
    {
        if (min >= max / 4)
        {
            ShowStomach(2);
        }

        if (min >= max / 2)
        {
            ShowSpritesFull(0);
            ShowStomach(1);
        }

        if (min >= (max / 2 + max / 4))
        {
            ShowStomach(0);
        }
    }

    public void updateFull(float time)
    {
        min = Mathf.Clamp(time, 0, max);
        full.fillAmount = min / max;
    }
    public void Check()
    {
        updateFull(min - 0.1f);
        if (min < max / 4)
        {
            ShowStomach(3);
        }
        else if (min < max / 2)
        {
            ShowSpritesFull(1);
            ShowStomach(2);
        }
        else if (min < (max / 2 + max / 4))
        {
            ShowStomach(1);
        }

        if (min == 0)
        {
            GameScene.game.checkOver();
            isCheck = false;
        }
    }

    public void CheckUpFull()
    {
        updateFull(min + 10.0f);
    }

    public void ShowSpritesFull(int index)
    {
        full.sprite = spritesFull[index];
        full.SetNativeSize();
    }

    public void ShowStomach(int index)
    {
        stomach.sprite = spritesStomach[index];
        stomach.SetNativeSize();
    }
}
