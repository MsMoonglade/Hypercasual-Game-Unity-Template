using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : Singleton<Inventory>
{
    [Header("Grid Variables")]
    public int row;
    public int column;
    public float rowSpacing;
    public float columnSpacing;

    [Header("Grid Prefabs Object")]
    public GameObject cellObject;

    [Header("Project Reference")]
    public GameObject objectToPopulateCell;

    [Header("Local Variables")]
    private Vector3 currentCellPos = Vector3.zero;

    [HideInInspector]
    public InventoryCells[,] cells;

    private void OnEnable()
    {
        CenterGridLocalPositioning();   
        GenerateInventorySlots();

        LoadCellValue();
    }

    /// <summary>
    /// Check if you have empty Cell in Grid
    /// </summary>
    /// <returns></returns>
    public bool HaveFreeCell()
    {
        foreach (var cell in cells)
        {
            if (cell.Empty)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Check if you have empty Cell in Grid
    /// </summary>
    /// <param name="i_amount"><amount of cells needed/param>
    /// <returns></returns>
    public bool HaveFreeCell(int i_amount)
    {
        int freeCellCount = 0;

        foreach (var cell in cells)
        {
            if (cell.Empty)
                freeCellCount ++;
        }

        return (freeCellCount >= i_amount) ? true : false;
    }

    /// <summary>
    /// Get First empty Cell of the Grid
    /// </summary>
    /// <returns></returns>
    public InventoryCells ReturnFirstEmptyCell()
    {
        foreach (var cell in cells)
        {
            if (cell.Empty)
                return cell;
        }

        return null;
    }

    /// <summary>
    /// Recenter Grid Slots based on Gameobject Positon
    /// </summary>
    private void CenterGridLocalPositioning()
    {
        //Center X Position if Column is Odd
        if(column % 2 != 0)
        {
            currentCellPos -= new Vector3((cellObject.GetComponent<InventoryCells>().xSize + columnSpacing) * (column -1) /2  , 0 , 0);
        }

        //Center Y Position if Row is Odd
        if (row % 2 != 0)
        {
            currentCellPos += new Vector3(0, 0, (cellObject.GetComponent<InventoryCells>().ySize + rowSpacing) * (row - 1) / 2);
        }
    }

    /// <summary>
    /// Generate Inventory Grid
    /// </summary>
    private void GenerateInventorySlots()
    {
        cells = new InventoryCells[row, column];

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                GameObject slot = Instantiate(cellObject, currentCellPos, Quaternion.identity, transform);
                slot.transform.localPosition = currentCellPos;
                slot.transform.localRotation = new Quaternion(0, 0, 0, 0);

                cells[i,j] = slot.GetComponent<InventoryCells>();
                cells[i,j].CellIndex = int.Parse(i.ToString() + j.ToString());

                currentCellPos += new Vector3(0, 0, cells[i,j].xSize + columnSpacing);
            }

            currentCellPos += new Vector3(cells[i,0].ySize + rowSpacing, 0, (-rowSpacing * (cells[i, 0].ySize + rowSpacing)));
        }
    }

    private void LoadCellValue()
    {
        List<CellComposition> savedCells = SaveManager.Instance.LoadCells(Saves.inventoryCellObjects);

        if (savedCells.Count == 0)
            return;

        foreach(InventoryCells cell in cells)
        {
            foreach(CellComposition savedCell in savedCells)
            {
                if(cell.CellIndex == savedCell.GetCellIndex)
                {
                    cell.CellValue = savedCell.GetCellValue;
                    break;
                }
            }
        }                      
    }

    /// <summary>
    /// Save Cell Value in Cell Index
    /// </summary>
    private void SaveCellValue()
    {
        List<CellComposition> o_cellsList = new List<CellComposition>();

        foreach (InventoryCells cell in cells)
        {
            CellComposition newCell = new CellComposition(cell.CellIndex, cell.CellValue);
            o_cellsList.Add(newCell);
        }

        SaveManager.Instance.Save(o_cellsList , Saves.inventoryCellObjects);
    }
}