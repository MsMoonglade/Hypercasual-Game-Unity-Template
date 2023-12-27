using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : Singleton<GameMaster>
{
    public void ClearSave()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }

    public void AddGold(int i_amount)
    {
        CurrencyManager.Instance.AddGold(i_amount);
    }
}