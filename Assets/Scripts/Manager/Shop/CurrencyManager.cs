using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    [HideInInspector]
    public int currency_Gold;

    protected override void Awake()
    {
        base.Awake();

        LoadCurrencies();
    }

    public void AddGold(int i_amount)
    {
        currency_Gold += i_amount;
        SaveCurrencies();
    }

    public void SpendGold(int i_amount)
    {
        currency_Gold -= i_amount;
        SaveCurrencies() ;
    }

    public bool HaveGoldAmount(int i_amount)
    {
        bool o_have;
        o_have = (currency_Gold >= i_amount) ? true : false;

        return o_have;       
    }

    private void SaveCurrencies()
    {
        PlayerPrefs.SetInt(Currency.currency_Gold, currency_Gold);
    }

    private void LoadCurrencies()
    {
        if (PlayerPrefs.HasKey(Currency.currency_Gold))
            currency_Gold = PlayerPrefs.GetInt(Currency.currency_Gold);
        else
        {
            currency_Gold = 0;
            PlayerPrefs.SetInt(Currency.currency_Gold, currency_Gold);
        }
    }
}