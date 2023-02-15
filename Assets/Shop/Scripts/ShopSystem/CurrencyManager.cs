using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{

    public static CurrencyManager currencyManager;
    [SerializeField] private int currency;

    public Text currencyText;

    // Start is called before the first frame update
    void Start()
    {
        currencyManager = this;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddCurrency(int amount)
    {
        currency += amount;
        UpdateUI();
    }

    public void LowerCurrency(int amount)
    {
        currency -= amount;
        UpdateUI();
    }

    public bool RequestCurrency(int amount)
    {
        if(amount <= currency)
        {
            return true;
        }
        return false;
    }

    void UpdateUI()
    {
        currencyText.text = "$ " + currency.ToString("N0");
    }

}
