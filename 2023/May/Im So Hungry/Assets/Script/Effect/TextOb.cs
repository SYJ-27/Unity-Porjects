using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextOb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DOVirtual.DelayedCall(0.5f, () =>
       {
           Destroy(gameObject);
       });
        // transform.DOLocalMoveY(transform.position.y - 0.2f, 0.5f)
        //     .OnComplete(() =>
        //     {
        //         Destroy(gameObject);
        //     });
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * 2.5f * Time.deltaTime);
    }
}
