using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticsShop : MonoBehaviour
{
    public static CosmeticsShop cosmeticsShop;

    public List<Cosmetics> cosmeticsList = new List<Cosmetics>();

    private List<GameObject> itemHolderList = new List<GameObject>();

    public List<GameObject> buyButtonList = new List<GameObject>();

    public GameObject itemHolderPrefab;
    public Transform grid;

    // Start is called before the first frame update
    void Start()
    {
        cosmeticsShop = this;
        FillList();
    }

    void FillList()
    {
        for (int i = 0; i < cosmeticsList.Count; i++)
        {
           GameObject holder = Instantiate(itemHolderPrefab,grid,false);
            ItemHolder holderScript = holder.GetComponent<ItemHolder>();

            holderScript.itemName.text = cosmeticsList[i].cosmeticsName;
            holderScript.itemPrice.text = "$ " + cosmeticsList[i].cosmeticsPrice.ToString("N0");
            holderScript.itemID = cosmeticsList[i].cosmeticsID;

            // Buy button 
            holderScript.buyButton.GetComponent<BuyButton>().cosmeticsID = cosmeticsList[i].cosmeticsID;

            //Handle lists
            itemHolderList.Add(holder);
            buyButtonList.Add(holderScript.buyButton);

            if (cosmeticsList[i].bought)
            {
                holderScript.itemImage.sprite = Resources.Load<Sprite>("Sprites/" + cosmeticsList[i].SpriteName +"_sold");
             
            }
            else
            {
                holderScript.itemImage.sprite = Resources.Load<Sprite>("Sprites/" + cosmeticsList[i].SpriteName);

            }
        }
    }

    public void UpdateSprite(int cosmeticsID)
    {
        for (int i = 0; i< itemHolderList.Count; i++)
        {
          ItemHolder holderScript = itemHolderList[i].GetComponent<ItemHolder>();
            if(holderScript.itemID == cosmeticsID)
            {
                for(int j = 0; j < cosmeticsList.Count; j++)
                {
                    if(cosmeticsList[j].cosmeticsID == cosmeticsID)
                    {
                        if (cosmeticsList[j].bought)
                        {
                            holderScript.itemImage.sprite = Resources.Load<Sprite>("Sprites/" + cosmeticsList[i].SpriteName + "_Sold");
                            holderScript.itemPrice.text = "Solgt!";

                        }
                        else
                        {
                            holderScript.itemImage.sprite = Resources.Load<Sprite>("Sprites/" + cosmeticsList[i].SpriteName);

                        }
                        
                    }
                }
            }
        }
    }
}
