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

    public string TilJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFraJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }

    public interface GemtData
    {
        void FyldData(SaveClass nogetGemt);
        void HentData(SaveClass nogetGemt);
    }

}
