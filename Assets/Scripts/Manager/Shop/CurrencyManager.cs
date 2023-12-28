using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    public int CurrencyGold {  get; private set; }

    protected override void Awake()
    {
        base.Awake();

        LoadCurrencies();
    }

    public void AddGold(int i_amount)
    {
        CurrencyGold += i_amount;
        SaveCurrencies();
    }

    public void SpendGold(int i_amount)
    {
        CurrencyGold -= i_amount;
        SaveCurrencies() ;
    }

    public bool HaveGoldAmount(int i_amount)
    {
        bool o_have;
        o_have = (CurrencyGold >= i_amount) ? true : false;

        return o_have;       
    }

    private void SaveCurrencies()
    {
        PlayerPrefs.SetInt(Currency.currency_Gold, CurrencyGold);
    }

    private void LoadCurrencies()
    {
        if (PlayerPrefs.HasKey(Currency.currency_Gold))
            CurrencyGold = PlayerPrefs.GetInt(Currency.currency_Gold);
        else
        {
            CurrencyGold = 0;
            PlayerPrefs.SetInt(Currency.currency_Gold, CurrencyGold);
        }
    }
}