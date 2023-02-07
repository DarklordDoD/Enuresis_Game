using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AllItems : MonoBehaviour
{
    public List<GameObject> items;
    public List<GameObject> boughtItems;

    public int monny;

    private List<string> loadItems;
    [SerializeField]
    private List<string> saveItems;

    private void Start()
    {
        try
        {
            LoadList();
        }
        catch { }
    }

    public void BuyItem(string item)
    {
        try
        {
            boughtItems.Add(items.Where(obj => obj.name == item).SingleOrDefault());
        }
        catch { }
    }

    private void LoadList()
    {
        SaveClass.LoadFromFile("Items", out loadItems);

        foreach (string item in loadItems)
        {
            BuyItem(item);           
        }

        try
        {
            monny = int.Parse(loadItems[0]);
        }
        catch { }
    }

    public void SaveList()
    {
        saveItems.Add(monny.ToString());

        foreach (GameObject item in boughtItems)
        {
            try
            {
                saveItems.Add(item.name);
            }
            catch { }
        }

        SaveClass.WriteToFile("Items", saveItems, false);
    }
}
