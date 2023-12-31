using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public Inventory[] inventories;

    private void OnEnable()
    {
        inventories = FindObjectsOfType<Inventory>();
    }

    public void SaveInvsValues()
    {
        foreach(Inventory inv in inventories)
        {
            inv.SaveCellsValue();
        }
    }
}