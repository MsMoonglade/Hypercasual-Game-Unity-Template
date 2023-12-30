using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public Inventory[] inventories;

    public void SaveInvsValues()
    {
        foreach(Inventory inv in inventories)
        {
            inv.SaveCellsValue();
        }
    }
}
