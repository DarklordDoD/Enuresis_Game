using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlikBuy : MonoBehaviour
{
    [SerializeField]
    private GameObject theItem;
    [SerializeField]
    private int pris;

    private AllItems itemList;

    private void Start()
    {
        itemList = GameObject.FindGameObjectWithTag("GameController").GetComponent<AllItems>();
    }

    public void BuyThisItem()
    {
        if (pris <= itemList.monny)
        {
            itemList.monny -= pris;

            itemList.BuyItem(theItem.name, true);
            itemList.SaveList();

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
