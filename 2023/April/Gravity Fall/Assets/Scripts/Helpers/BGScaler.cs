using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour
{
    public int index;
    // Use this for initialization
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 tempScale = transform.localScale;

        float height = sr.bounds.size.y;
        float width = sr.bounds.size.x;

        float worldHeight = Camera.main.orthographicSize * 2f;//10
        float worldWidth = worldHeight * Screen.width / Screen.height;//10 * 203/339

        if (index == 0)
        {
            tempScale.y = worldHeight / height;
            tempScale.x = worldWidth / width;
        }
        else if (index == 1)
        {
            // tempScale.y = worldHeight / height;
            tempScale.x = worldWidth / width;
        }

        transform.localScale = tempScale;

    }
}
