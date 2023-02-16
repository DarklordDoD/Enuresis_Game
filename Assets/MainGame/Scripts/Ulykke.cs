using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Ulykke : MonoBehaviour
{
    private List<string> gotList;

    private void Start()
    {
        try
        {
            SaveClass.LoadFromFile("Ulykke", out gotList);

            GetComponent<SpriteRenderer>().enabled = bool.Parse(gotList[0]);
            GetComponent<Collider2D>().enabled = bool.Parse(gotList[0]);
        }
        catch 
        { 
            gotList = new List<string>() {""};
        }
    }

    public void HarTisset(bool showStain)
    {
        GetComponent<SpriteRenderer>().enabled = showStain;
        GetComponent<Collider2D>().enabled = showStain;

        gotList[0] = showStain.ToString();
        SaveClass.WriteToFile("Ulykke", gotList, false);
    }
}
