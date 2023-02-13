using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemListClass
{
    public string listName;
    public List<GameObject> listObjekts;

    public List<GameObject> AddToThisList(GameObject objektet, List<GameObject> objektList, out List<GameObject> theObjektList)
    {
        objektList.Add(objektet);
        theObjektList = objektList;

        return theObjektList;
    }
    //public List<ItemListClass> listListClass;
}
