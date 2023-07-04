using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    public static GameScene game;
    public UiGame ui;
    public GameObject LogicGame, TreePrefabs, TreeParent, AnimalsPrefabs, AnimalsParent, meatOb, PanelUi;
    public GameObject[] EffectPrefabs;
    public int coin = 0;
    public int heart = 3;
    public bool isOver;
    public bool isCheckTreeParent;

    void Awake()
    {
        game = this;
        isOver = true;
    }

    void Start()
    {
        isCheckTreeParent = true;
        InvokeRepeating("ShowAnimals", 0f, 5.0f);
        // Invoke("ShowBin", 0.1f);
        // CancelInvoke("ShowAnimals");
    }

    public void checkCoin(int index)
    {
        switch (index)
        {
            case 0:
                coin += 1;
                break;
            case 1:
                coin += 10;
                break;
        }
        ui.UpdateTextCoin(coin);
    }

    public void checkOver()
    {
        if (isOver)
        {
            CancelInvoke("ShowAnimals");
            isCheckTreeParent = false;
            DOVirtual.DelayedCall(0.5f, () =>
            {
                LogicGame.SetActive(false);
                PanelUi.SetActive(false);
                ui.showPuOver(coin);
                isOver = false;
            });
        }
    }

    public void ShowEffect(int index, Vector3 pos)
    {
        GameObject ob = Instantiate(EffectPrefabs[index]);
        ob.transform.position = pos;
        ob.transform.SetParent(LogicGame.transform);
    }

    public void ShowAnimals()
    {
        meatOb.SetActive(true);
        meatOb.transform.position = new Vector2(UnityEngine.Random.Range(GameObject.Find("PosMeat1").transform.position.x, GameObject.Find("PosMeat2").transform.position.x), GameObject.Find("PosMeat3").transform.position.y);
        DOVirtual.DelayedCall(1.0f, () =>
        {
            GameObject ob = Instantiate(AnimalsPrefabs);
            ob.transform.position = meatOb.transform.position;
            ob.transform.SetParent(AnimalsParent.transform);
            Destroy(ob, 5.0f);
            meatOb.SetActive(false);
        });
    }

    public void checkHeart()
    {
        heart -= 1;
        ui.UpHeart(heart, false);
        if (heart == 0)
        {

        }
    }

    void Update()
    {
        if (isCheckTreeParent)
        {
            meatOb.transform.position = new Vector2(meatOb.transform.position.x, GameObject.Find("PosMeat3").transform.position.y);
            float distance = Vector3.Distance(GameObject.Find("Players").transform.position, TreeParent.transform.position);
            if (distance > 20)
            {
                TreeParent.transform.position = GameObject.Find("Players").transform.position;
                foreach (Transform childTransform in TreeParent.transform)
                {
                    Trees trees = childTransform.GetComponent<Trees>();
                    trees.UpPos();
                }
            }
        }
    }
}
