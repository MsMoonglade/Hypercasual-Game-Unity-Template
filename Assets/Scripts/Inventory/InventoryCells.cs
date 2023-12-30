using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCells : MonoBehaviour
{
    [Header("Variables")]
    public float xSize, ySize;

    [HideInInspector]
    public GameObject contain;

    public int CellIndex {  get; set; }

    public int CellValue
    {
        get
        {
            if (contain == null)
            {
                return 0;
            }
            else
            {
                //here return contain value if have one
                return 1;
            }
        }

        set
        {
            InitializeObjectInCell(value);
        }
    }

    public bool Empty
    {
        get
        {
            if (contain == null)
                return true;

            else
                return false;
        }
    }

    /// <summary>
    /// Setup Object into this Cell
    /// </summary>
    public void InitializeObjectInCell(int i_value)
    {
        if (i_value == 0)
            return;

        if(contain == null)
        {
            //when set new GameObject destroy the old one
            foreach(Transform t in transform)
            {
                Destroy(t.gameObject);
            }

            GameObject newObj = Instantiate(Inventory.Instance.objectToPopulateCellPref, transform.position, Quaternion.identity, transform);
            contain = newObj;
        }

        //ThisMethod should be in the class of object in cell
        contain.SendMessage("UpdateValue", i_value);
    }

    /// <summary>
    /// Put an object into cell
    /// </summary>
    /// <param name="i_obj"><object to insert/param>
    public void PutInCell(GameObject i_obj)
    {
        if (contain == null)
        {
            i_obj.transform.SetParent(this.transform, true);
            i_obj.transform.position = transform.position;
            i_obj.transform.rotation = transform.rotation;
            i_obj.transform.localScale = new Vector3(1, 1, 1);
            contain = i_obj;
        }

        else
        {
            //ThisMethod should be in the class of object in cell
            // int o_valueToAdd = i_obj.value ;
            
            int o_valueToAdd = 0;
            contain.SendMessage("UpdateValue", o_valueToAdd);
        }

        InventoryManager.Instance.SaveInvsValues();
    }


    /// <summary>
    /// Remove Object From Cell
    /// </summary>
    /// <returns></returns>
    public GameObject RemoveFromCell()
    {
        GameObject o_return = contain;

        contain = null;

        return o_return;
    }
}


public struct CellComposition
{
    private int index;
    private int value;

    public int GetCellIndex
    {
        get { return index; }
    }

    public int GetCellValue
    {
        get { return value; }
    }

    public CellComposition(int i_index, int i_value)
    {
        index = i_index;
        value = i_value;
    }
}