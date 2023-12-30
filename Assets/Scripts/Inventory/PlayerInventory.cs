using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Inventory
{
    protected override void OnEnable()
    {
        base.OnEnable();

        LoadCellValue(Saves.playerInventoryCells);
    }

    public override void SaveCellsValue()
    {
        SaveCellValue(Saves.playerInventoryCells);
    }
}