using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class ExplosionVFX : MonoBehaviour
{
    private ParticleSystem rendere;
    public Sprite spriteOb;
    //public Texture2D tex;
    void Start()
    {
        //tex = sprite.texture;
        rendere = GetComponent<ParticleSystem>();
        var shape = rendere.shape;
        shape.texture = spriteOb.texture;
        //shape.texture = tex;
        var shapeMain = rendere.main;
        DOVirtual.DelayedCall(shapeMain.duration + 0.5f, () =>
        {
            Destroy(gameObject);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
