using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiMethod : MonoBehaviour
{
    //Called At End of level in win screen , this load scene "_Loader" , that load correct scene based on level
    public static void GoToNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    //Need To Do Events that reload all Gameplay element Or save current level formation 
    public void RestartThisLevel()
    {
        SceneManager.LoadScene(1);
    }

    //Claim Money Earned While Offline from game
    public void ClaimOfflineEarning()
    {
        OfflineEarning.Instance.ClaimOfflineEarning();
    }

    //         ---GM Method (Called For Debug In GM Ui Panel)---
    public void GM_ClearAllSave()
    {
        SaveManager.Instance.ClearAllSave();
    }
    public void GM_1kGold()
    {
        CurrencyManager.Instance.AddGold(1000);
    }



    //                 ---TO DO----


    public void ExitFromInventory()
    {
    }

    public void EnterInInventory()
    {
    }

    /*
    public void BuyMoney()
    {
        if (GameManager.instance.HaveGold(ShopCostHelper.instance.currentMoneyShopCost))
        {
            if (InventoryBehaviour.Instance.HaveFreeSlot())
            {
                GameManager.instance.DecreaseGold(ShopCostHelper.instance.currentMoneyShopCost);

                InventoryBehaviour.Instance.GenerateMoneyInInv(ShopCostHelper.instance.newMoneyValue);
                ShopCostHelper.instance.MoneyIsBuyed();
            }
        }
    }
    */
}