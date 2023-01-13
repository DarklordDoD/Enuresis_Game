using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveClass
{
    public DateTime dato;
    public float tisData;
    public float vandData;
    public float gladData;
    public int points;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFormJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
}
