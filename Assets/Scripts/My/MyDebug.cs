using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MyDebug
{

    public static void List<T>(List<T> list)
    {
        string log = "";

        foreach (var content in list.Select((val, idx) => new { val, idx })) // val,idxという二つのプロパティを持ったオブジェクトのリストに変換？
        {
            if (content.idx == list.Count - 1)
                log += content.val.ToString();
            else
                log += content.val.ToString() + ", ";
        }

        Debug.Log(log);
    }

    public static void Dictionary<T,U>(Dictionary<T,U> keyValuePairs)
    {
        string log = "";

        foreach (var content in keyValuePairs.Select((val, idx) => new { val, idx })) // val,idxという二つのプロパティを持ったオブジェクトのリストに変換？
        {
            if (content.idx == keyValuePairs.Count - 1)
                log += content.val.ToString();
            else
                log += content.val.ToString() + ", ";
        }

        Debug.Log(log);
    }
}
