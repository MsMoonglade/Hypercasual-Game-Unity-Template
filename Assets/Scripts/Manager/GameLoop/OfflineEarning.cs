using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OfflineEarning : SingletonPersistent<OfflineEarning>
{ 
    public int GoldPerHour {  get; private set; }

    private int offlineEarningAmount;

    protected override void Awake()
    {
        base.Awake();

        offlineEarningAmount = GetOfflineEarningAmount();
    }

    public void ClaimOfflineEarning()
    {
        CurrencyManager.Instance.AddGold(offlineEarningAmount);
    }

    private int GetOfflineEarningAmount()
    {
        if (!PlayerPrefs.HasKey(Saves.lastLogin))
        {
            return 0;
        }

        return GoldPerHour * GetPassedHour();     
    }

    private int GetPassedHour()
    {
        string savedTime = SaveManager.Instance.Load<string>(Saves.lastLogin);
                
        DateTime oldLogin = System.DateTime.Parse(savedTime);
        DateTime currentLogin = System.DateTime.Now;

        TimeSpan difference = currentLogin.Subtract(oldLogin);

        return (int)difference.TotalHours;        
    }

    protected override void OnApplicationQuit()
    {
        base.OnApplicationQuit();

        SaveManager.Instance.Save(System.DateTime.Now.ToString(), Saves.lastLogin);
    }
}