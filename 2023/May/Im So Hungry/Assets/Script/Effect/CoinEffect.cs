using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinEffect : MonoBehaviour
{

    public void CheckMove()
    {
        transform.GetComponent<Animator>().enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).DOMove(GameObject.Find("ImageCoin").transform.position, 0.5f)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
    }
}
