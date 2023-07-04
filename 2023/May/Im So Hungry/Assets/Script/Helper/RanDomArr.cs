using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class RanDomArr
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        System.Random rnd = new System.Random();
        while (n > 1)
        {
            int k = (rnd.Next(0, n) % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
            // Debug.Log(list[n]);
        }
    }
}
