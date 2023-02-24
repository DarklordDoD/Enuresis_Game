using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuyButton : MonoBehaviour
{
    public int cosmeticsID;
    public Text buttonText;

    private AllItems itemList;
    private Snacks SnavkStorige;

    private void Start()
    {
        GameObject gameManeger = GameObject.FindGameObjectWithTag("GameController");
        itemList = gameManeger.GetComponent<AllItems>();
        SnavkStorige = gameManeger.GetComponent<Snacks>();
    }

    public void BuyCosmetic()//On click
    {
        if (cosmeticsID == 0)
        {
            Debug.Log("No cosmetic ID is set");
            return;
        }
       
        for(int i = 0; i < CosmeticsShop.cosmeticsShop.cosmeticsList.Count; i++)
        {

            if (CosmeticsShop.cosmeticsShop.cosmeticsList[i].cosmeticsID == cosmeticsID && !CosmeticsShop.cosmeticsShop.cosmeticsList[i].bought && CurrencyManager.currencyManager.RequestCurrency(CosmeticsShop.cosmeticsShop.cosmeticsList[i].cosmeticsPrice))
            {
                //Buy the weapon
                if (CosmeticsShop.cosmeticsShop.cosmeticsList[i].unlimited == false)
                {
                    CosmeticsShop.cosmeticsShop.cosmeticsList[i].bought = true;

                    itemList.BuyItem((i + 1).ToString(), true);
                }
                else
                {
                    SnavkStorige.changeSnaks(CosmeticsShop.cosmeticsShop.cosmeticsList[i].SpriteName, CosmeticsShop.cosmeticsShop.cosmeticsList[i].gladVerdi,  true);
                }

                CurrencyManager.currencyManager.LowerCurrency(CosmeticsShop.cosmeticsShop.cosmeticsList[i].cosmeticsPrice);
                //Set cosmetic ID in your system
            }
            else if (CosmeticsShop.cosmeticsShop.cosmeticsList[i].cosmeticsID == cosmeticsID && !CosmeticsShop.cosmeticsShop.cosmeticsList[i].bought && !CurrencyManager.currencyManager.RequestCurrency(CosmeticsShop.cosmeticsShop.cosmeticsList[i].cosmeticsPrice))
            {
                Debug.Log("Not enough money!");
            }
            else
            if (CosmeticsShop.cosmeticsShop.cosmeticsList[i].unlimited == false)
                if (CosmeticsShop.cosmeticsShop.cosmeticsList[i].cosmeticsID == cosmeticsID && CosmeticsShop.cosmeticsShop.cosmeticsList[i].bought)
                {
                    Debug.Log("Item has been sold already");
                    UpdateBuyButton();
                }
        }

        CosmeticsShop.cosmeticsShop.UpdateSprite(cosmeticsID); //This is tempoary, we need to grey out this system later, since we do not want to update on clicks, but instead need to change the system to update on buy.
    }

    public void UpdateBuyButton()
    {
        buttonText.text = "Aktiv";
        //Change cosmetic on your character / add the cosmetic
        for(int i = 0; i < CosmeticsShop.cosmeticsShop.buyButtonList.Count;i++)
        {
            BuyButton BuyButtonScript = CosmeticsShop.cosmeticsShop.buyButtonList[i].GetComponent<BuyButton>();
            for(int j = 0; j < CosmeticsShop.cosmeticsShop.cosmeticsList.Count; j++)
            {
                //Has the item been bought, does the cosmetic id match?
                if (CosmeticsShop.cosmeticsShop.cosmeticsList[j].cosmeticsID == BuyButtonScript.cosmeticsID && CosmeticsShop.cosmeticsShop.cosmeticsList[j].bought && CosmeticsShop.cosmeticsShop.cosmeticsList[j].cosmeticsID != cosmeticsID)
                {
                    BuyButtonScript.buttonText.text = "Brug";
                }
            }       
        }
       //This is implemented instead of the earlier on click for the save load system CosmeticsShop.cosmeticsShop.UpdateSprite(cosmeticsID);
    }
}
