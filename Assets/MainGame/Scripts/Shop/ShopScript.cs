using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    [SerializeField]
    private List<string> gotList;
    [SerializeField]
    private bool randomiseShop;

    private void Awake()
    {
        try
        {
            SaveClass.LoadFromFile("MainGame", out gotList);

            if (gotList[0].Remove(10) == DateTime.Today.ToString("dd-MM-yyyy"))
                SaveClass.LoadFromFile("ShopInhold", out gotList);
            else
                gotList = null;

            if (gotList == null)
                randomiseShop = true;
        }
        catch { }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
