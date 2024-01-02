using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PassiveEarning : SingletonPersistent<PassiveEarning>
{ 
    public int GoldPerHour {  get; private set; }   


    protected override void Awake()
    {
        base.Awake();
    }

    public int GetPassiveEarning()
    {
        if (!PlayerPrefs.HasKey(Saves.lastLogin))
        {
            return 0;
        }

        return GoldPerHour * GetPassedHour();     
    }

    private int GetPassedHour()
    {
        string savedTime = SaveManager.Instance.LoadString(Saves.lastLogin);
                
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