using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    int currencyGold;
    public int CurrencyGold 
    { 
        get 
        {
           return currencyGold;
        }
        private set
        {
            currencyGold = value;
            UiManager.Instance.uiCurrency.RefreshCurrency();
            SaveCurrencies();
        }
    }

    protected override void Awake()
    {
        base.Awake();

        LoadCurrencies();
    }

    public void AddGold(int i_amount)
    {
        UiManager.Instance.instantiator.InstantiateElementInUi(i_amount, 0);

        CurrencyGold += i_amount;
    }

    public void SpendGold(int i_amount)
    {
        CurrencyGold -= i_amount;
    }

    public bool HaveGoldAmount(int i_amount)
    {
        bool o_have;
        o_have = (CurrencyGold >= i_amount) ? true : false;

        return o_have;       
    }

    private void SaveCurrencies()
    {
        SaveManager.Instance.Save(CurrencyGold, Currency.currency_Gold);
    }

    private void LoadCurrencies()
    {
        CurrencyGold = SaveManager.Instance.Load<int>(Currency.currency_Gold);
    }
}