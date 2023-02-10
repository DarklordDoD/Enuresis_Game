using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class KledeSkab : MonoBehaviour
{
    private AllItems itemManeger;
    [SerializeField]
    private List<GameObject> itemsISkab;

    [SerializeField]
    private List<ItemListClass> alleItemLists;

    // Start is called before the first frame update
    void Start()
    {
        itemManeger = GameObject.FindGameObjectWithTag("GameController").GetComponent<AllItems>();
        Invoke("OpenSkab", 1);
    }

    public void OpenSkab()
    {
        itemsISkab = itemManeger.boughtItems;

        foreach (var item in itemsISkab)
        {
            try
            {
                if (item.GetComponent<Item>())
                {
                    FindItemList(item);
                }
            }
            catch {  }
        }
    }

    private void AddListToItemLists(GameObject item)
    {
        ItemListClass extraListClass = new ItemListClass();
        extraListClass.listName = item.GetComponent<Item>().itemType;
        alleItemLists.Add(extraListClass);

        AddItemToList(item, extraListClass, false);
    }

    private void FindItemList(GameObject item)
    {
        bool foundList = false;

        foreach (var itemT in alleItemLists)
        {
            if (itemT.listName == item.GetComponent<Item>().itemType)
            {
                foundList = true;
                AddItemToList(item, itemT, true);
            }
        }
        
        if (!foundList)
            AddListToItemLists(item);
    }

    private void AddItemToList(GameObject item, ItemListClass extraListClass, bool hasList)
    {
        List<GameObject> extraList = new List<GameObject>();

        if (hasList)
            extraList = extraListClass.listObjekts;

        List<GameObject> listOut;

        extraListClass.AddToThisList(item, extraList, out listOut);

        extraListClass.listObjekts = listOut;
    }

    public void RotateItem(string itemTyps, int itemNumber, bool goLeft = false)
    {
        try
        {
            if (goLeft)
                itemNumber -= 1;
            else
                itemNumber += 1;
        }
        catch
        {
            Debug.Log(itemTyps + " not faund");
        }

        foreach (var rotateButten in GameObject.FindGameObjectsWithTag("ItemSkift"))
        {
            rotateButten.GetComponent<ObjecktSkift>().SetItemNumber(itemNumber, itemTyps);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
