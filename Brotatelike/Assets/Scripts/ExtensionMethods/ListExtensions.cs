using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            // 0 から n までのランダムなインデックスを選択
            int k = Random.Range(0, n + 1);

            // 要素の入れ替え
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}
