using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Inventory
{
    [Header ("used for player shoot if have objects in Inv")]
    private InventoryItem[] items;
    private int shootIndex = 0;
    private float animTime;

    protected override void OnEnable()
    {
        base.OnEnable();

        LoadCellValue(Saves.playerInventoryCells);

        GetItemsFromInventory();
    }

    public override void SaveCellsValue()
    {
        SaveCellValue(Saves.playerInventoryCells);
    }

    /// <summary>
    /// Get Current Items in Inventory Cells SLots
    /// </summary>
    private void GetItemsFromInventory()
    {
        animTime = CharacterBehaviour.instance.characterShooter.fireRate;

        items = new InventoryItem[row];

        for(int i = 0; i < items.Length; i++)
        {
            items[i] = cells[0, i].contain;
        }
    }

    /// <summary>
    /// Return Current First element in playerInvValue
    /// </summary>
    /// <returns></returns>
    public int ReturnFirstElementValue()
    {
        int o_valueToReturn = 0;

        if (items[shootIndex] != null)        
            o_valueToReturn = items[shootIndex].Value;       

        IncreaseShootIndex();
        AnimateInvObjects();

        return o_valueToReturn;
    }

    /// <summary>
    /// Animate inventory Item in slots when shoot
    /// </summary>
    private void AnimateInvObjects()
    {
        //set new positions
        for(int i = 0; i < items.Length; i++)
        {
            int nextIndex = i + 1;

            if (i == items.Length - 1)
               nextIndex = 0;

            items[i].AnimationSetDestination(items[nextIndex].transform.localPosition);

            //disable current shoot object
            if(i == shootIndex)
                items[i].DisableModel();
        }

        //actual animate it
        foreach(InventoryItem item in items)
        {
            item.AnimateToNewPosition(animTime);
        }
    }

    private void IncreaseShootIndex()
    {
        shootIndex++;
        if (shootIndex >= items.Length)
            shootIndex = 0;
    }
}