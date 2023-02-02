using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> muligeShopItem;

    private List<string> gotList;
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

    public void GemShop()
    {

    }
}
