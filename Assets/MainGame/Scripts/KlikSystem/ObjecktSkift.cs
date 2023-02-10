using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjecktSkift : MonoBehaviour
{
    [SerializeField]
    private bool right; //Hvilken retning skal den roter

    [SerializeField]
    private string itemType;
    [SerializeField]
    private int itemNumber;

    KledeSkab itemS;

    private void Start()
    {
        itemS = GameObject.FindGameObjectWithTag("GameController").GetComponent<KledeSkab>();
    }

    public void SkiftItemTil()
    {
        itemS.RotateItem(itemType, itemNumber, right);
    }

    public void SetItemNumber(int itemN, string itemT)
    {
        if (itemT == itemType)
            itemNumber = itemN;
    }
}
