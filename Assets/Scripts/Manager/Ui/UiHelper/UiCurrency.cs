using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//use this for all game currencies
public class UiCurrency : MonoBehaviour
{
    [Header("Currency References")]
    public TMP_Text currency_GoldText;

    public void RefreshCurrency()
    {
        currency_GoldText.text = CurrencyManager.Instance.currency_Gold.ToString();
    }
}