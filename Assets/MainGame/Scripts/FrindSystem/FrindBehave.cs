using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FrindBehave : MonoBehaviour
{
    [SerializeField]
    private GameObject noCosmetic;

    private AllItems itemManeger;
    private GameObject frindSkin;
    [SerializeField]
    private List<ItemListClass> alleItemLists;
    private GameObject petLim;
    private GameObject thePet;
    private string isFront;

    private void Start()
    {
        itemManeger = GameObject.FindGameObjectWithTag("GameController").GetComponent<AllItems>();
        thePet = GameObject.FindGameObjectWithTag("Player");

        frindSkin = transform.GetChild(transform.childCount - 1).gameObject;

        foreach (var item in itemManeger.items)
        {
            if (item.GetComponent<Item>())
            {
                FindItemList(item);
            }
        }

        RngCosmetic();
    }

    private void Update()
    {
        if (transform.position.y >= thePet.transform.position.y && isFront != "FrindBack")
        {
            setLayer("FrindBack");
        }
        else if (transform.position.y < thePet.transform.position.y && isFront != "FrindFrond")
        {
            setLayer("FrindFrond");
        }
    }

    private void setLayer(string front)
    {
        isFront = front;

        foreach (SpriteRenderer showLim in frindSkin.GetComponentsInChildren<SpriteRenderer>())
        {
            showLim.sortingLayerName = front;
        }
    }

    private void RngCosmetic()
    {
        foreach (ItemListClass itemList in alleItemLists)
        {
            int rngN = Random.Range(0, itemList.listObjekts.Count);

            GameObject theItem = itemList.listObjekts[rngN];
            Item itemN = theItem.GetComponent<Item>();

            try {
               petLim = frindSkin.transform.GetChild(itemN.limNumber).gameObject;
            }
            catch { petLim = this.gameObject; }

            Instantiate(theItem, petLim.GetComponent<Transform>());
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
        List<GameObject> extraList = new List<GameObject>() { noCosmetic };

        if (hasList)
            extraList = extraListClass.listObjekts;

        List<GameObject> listOut;

        extraListClass.AddToThisList(item, extraList, out listOut);

        extraListClass.listObjekts = listOut;
    }
}
