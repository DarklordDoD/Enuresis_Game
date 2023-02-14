using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KledeSkab : MonoBehaviour
{
    [SerializeField]
    private Vector2 showPosition;
    [SerializeField]
    private GameObject noCosmetic;

    private AllItems itemManeger;
    [SerializeField]
    private List<ItemListClass> alleItemLists;
    private GameObject thePet;
    private GameObject playerPet;

    // Start is called before the first frame update
    void Start()
    {
        itemManeger = GameObject.FindGameObjectWithTag("GameController").GetComponent<AllItems>();
        Invoke("OpenSkab", 1);

        thePet = GameObject.FindGameObjectWithTag("ShowPet");

        playerPet = GameObject.FindGameObjectWithTag("Player");
        playerPet.GetComponent<muvePet>().enabled = false;
        playerPet.transform.position = showPosition;

        thePet.GetComponent<Animator>().SetBool("Run-element", false);
    }

    public void OpenSkab()
    {
        foreach (var item in itemManeger.boughtItems)
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
        List<GameObject> extraList = new List<GameObject>() {noCosmetic};

        if (hasList)
            extraList = extraListClass.listObjekts;

        List<GameObject> listOut;

        extraListClass.AddToThisList(item, extraList, out listOut);

        extraListClass.listObjekts = listOut;
    }

    public void RotateItem(string itemTyps, int itemNumber, bool goLeft = false)
    {
        GameObject theItem;

        foreach (ItemListClass itemL in alleItemLists)
        {
            if (itemTyps == itemL.listName)
            {
                if (goLeft)
                {
                    itemNumber -= 1;

                    if (itemNumber < 0)
                        itemNumber = itemL.listObjekts.Count - 1;
                    
                }
                else
                {
                    itemNumber += 1;

                    if (itemNumber > itemL.listObjekts.Count - 1)
                        itemNumber = 0;
                                  
                }             

                theItem = itemL.listObjekts[itemNumber];

                if(theItem != null)
                {
                    try
                    {
                        foreach (GameObject itemOS in GameObject.FindGameObjectsWithTag("ShopItem"))
                        {
                            if (itemOS.GetComponent<Item>().itemType == itemTyps)
                                Destroy(itemOS);
                        }
                    }
                    catch { }
                }

                try
                {
                    GameObject petLim = thePet.transform.GetChild(theItem.GetComponent<Item>().limNumber).gameObject;
                    Instantiate(theItem, petLim.GetComponent<Transform>());
                } 
                catch { }
            }
        }     

        foreach (var rotateButten in GameObject.FindGameObjectsWithTag("ItemSkift"))
        {
            rotateButten.GetComponent<ObjecktSkift>().SetItemNumber(itemNumber, itemTyps);
        }
    }

    public void GoOut()
    {
        playerPet.GetComponent<muvePet>().enabled = true;
    }
}
