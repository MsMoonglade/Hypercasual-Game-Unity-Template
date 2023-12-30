using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameInventory : Inventory
{
    protected override void OnEnable()
    {
        base.OnEnable();

        LoadCellValue(Saves.gameInventoryCells);
    }

    public override void SaveCellsValue()
    {
        SaveCellValue(Saves.gameInventoryCells);
    }
}