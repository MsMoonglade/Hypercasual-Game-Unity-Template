using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    /// <summary>
    /// Clear all PlayerPrefs
    /// </summary>
    public void ClearAllSave()
    {
        PlayerPrefs.DeleteAll();
    }

    /// <summary>
    /// Save an int at given name
    /// </summary>
    /// <param name="i_value"><Value To Save/param>
    /// <param name="i_saveName"><Save Name/param>
    public void Save(int i_value, string i_saveName)
    {
        PlayerPrefs.SetInt(i_saveName, i_value);
    }

    /// <summary>
    /// Save a string at given name
    /// </summary>
    /// <param name="i_value"><Value To Save/param>
    /// <param name="i_saveName"><Save Name/param>
    public void Save(string i_value, string i_saveName)
    {
        PlayerPrefs.SetString(i_saveName, i_value);
    }

    /// <summary>
    /// Save an float at given name
    /// </summary>
    /// <param name="i_value"><Value To Save/param>
    /// <param name="i_saveName"><Save Name/param>
    public void Save(float i_value, string i_saveName)
    {
        PlayerPrefs.SetFloat(i_saveName, i_value);
    }

    /// <summary>
    /// Save 2 Value In A given Name
    /// </summary>
    /// <param name="i_firstValue"><First Value To Save/param>
    /// <param name="i_secondValue"><Second Value To Save/param>
    /// <param name="i_saveName"><Save Name/param>
    public void Save(int i_firstValue , int i_secondValue , string i_saveName)
    {
        int[] valueToSave = new int[2];

        valueToSave[0] = i_firstValue;
        valueToSave[1] = i_secondValue;

        string stringToSave = ArrayToString(valueToSave);

        PlayerPrefs.DeleteKey(i_saveName);
        PlayerPrefs.SetString(i_saveName, stringToSave);
    }

    /// <summary>
    /// Save list of cells In A given Name
    /// </summary>
    /// <param name="i_cells"><Cells to save/param>
    /// <param name="i_saveName"><Save Name/param>
    public void Save(List<CellComposition> i_cells , string i_saveName)
    {
        string[] valuesInCells = new string[i_cells.Count];

        for (int i = 0; i < valuesInCells.Length; i++)
        {
            valuesInCells[i] = i_cells[i].GetCellIndex.ToString() + "/" + i_cells[i].GetCellValue.ToString();
        }

        PlayerPrefs.DeleteKey(i_saveName);
        PlayerPrefs.SetString(i_saveName, string.Join("#", valuesInCells));
    }

    /// <summary>
    /// Return int at Given Name
    /// </summary>
    /// <param name="i_savedName"><given saved name/param>
    /// <returns></returns>
    public int LoadInt(string i_savedName)
    {
        if (PlayerPrefs.HasKey(i_savedName))
            return PlayerPrefs.GetInt(i_savedName);
        
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// Return string at Given Name
    /// </summary>
    /// <param name="i_savedName"><given saved name/param>
    /// <returns></returns>
    public string LoadString(string i_savedName)
    {
        if (PlayerPrefs.HasKey(i_savedName))
            return PlayerPrefs.GetString(i_savedName);

        else
        {
            return "";
        }
    }

    /// <summary>
    /// Return Array of int at Given Name
    /// </summary>
    /// <param name="i_savedName"><given saved name/param>
    /// <returns></returns>
    public int[] Load(string i_savedName)
    {
        string savedString = PlayerPrefs.GetString(i_savedName);

        int[] o_result = StringToArray(savedString);       

        return o_result;
    }

    /// <summary>
    /// Return list of Cells at given name
    /// </summary>
    /// <param name="i_savedName"><given saved name/param>
    /// <returns></returns>
    public List<CellComposition> LoadCells(string i_savedName)
    {
        string savedString = PlayerPrefs.GetString(i_savedName);

        string[] splittedString = SplitString(savedString , '#');

        List<CellComposition> o_savedCells = new List<CellComposition>();

        foreach(string values in splittedString)
        {
            string[] cellsValue = SplitString(values, '/');

            if (cellsValue.Length != 2)
                Debug.LogWarning("Cells Value not valid");
            else
            {
                CellComposition cell = new CellComposition(int.Parse(cellsValue[0]) , int.Parse(cellsValue[1]));  
                o_savedCells.Add(cell); 
            }
        }

        return o_savedCells;
    }

    /// <summary>
    /// Convert Array into a String
    /// </summary>
    /// <param name="i_array"><Array to convert/param>
    private string ArrayToString(int[] i_array)
    {
        string o_output = "";
            
        foreach (int i in i_array)
        {
            o_output += $"{i.ToString()} ";
        }

        return o_output;
    }

    /// <summary>
    /// Convert String into a Int Array
    /// </summary>
    /// <param name="i_array"><StringToConvert/param>
    private int[] StringToArray(string i_string)
    {
        string[] splittedString = i_string.Split(' ');
        int[] o_output = new int[splittedString.Length];
        
        for (int i = 0; i < o_output.Length; i++)
        {         
            o_output[i] = int.Parse(splittedString[i]);
        }

        return o_output;
    }

    /// <summary>
    /// Separate string into Strings at Some Char Operator
    /// </summary>
    /// <param name="i_string"><StringToConvert/param>
    /// <param name="i_splitter"><char that split strings/param>
    private string[] SplitString(string i_string , char i_splitter)
    { 
        string[] o_splittedString = i_string.Split(new[] { i_splitter }, StringSplitOptions.None);

        return o_splittedString;
    }
}


/*
public void Save(int i_cellIndex, int i_valueToSave)
{
    int[] valueToSave = new int[moneyParent.transform.childCount];
    Vector3[] posToSave = new Vector3[moneyParent.transform.childCount];

    for (int i = 0; i < moneyParent.transform.childCount; i++)
    {
        valueToSave[i] = moneyParent.transform.GetChild(i).GetComponent<PlayerMoneyObject>().value;
        posToSave[i] = moneyParent.transform.GetChild(i).transform.localPosition;
    }

    PlayerPrefs.DeleteKey("SavedInvValue");
    PlayerPrefs.SetString("SavedInvValue", string.Join("###", valueToSave));

    string posToSaveString = SerializeVector3Array(posToSave);
    PlayerPrefs.DeleteKey("SavedInvPos");
    PlayerPrefs.SetString("SavedInvPos", posToSaveString);
}

public void Load()
{
    if (PlayerPrefs.HasKey("SavedInvValue") && PlayerPrefs.HasKey("SavedInvPos"))
    {
        //LoadValue
        string[] tempValue = PlayerPrefs.GetString("SavedInvValue").Split(new[] { "###" }, StringSplitOptions.None);

        if (tempValue[0] != "")
        {
            if (tempValue.Length >= 1)
                for (int i = 0; i < tempValue.Length; i++)
                {
                    gridElementValue.Add(int.Parse(tempValue[i]));
                }

            //LoadPos
            string posStringNotSplitted = PlayerPrefs.GetString("SavedInvPos");
            Vector3[] allPosSplitted = DeserializeVector3Array(posStringNotSplitted);
            if (allPosSplitted.Length >= 1)
            {
                for (int i = 0; i < allPosSplitted.Length; i++)
                {
                    gridElementPos.Add(allPosSplitted[i]);
                }
            }
        }
    }
}

 public static string SerializeVector3Array(Vector3[] aVectors)
    {
        StringBuilder sb = new StringBuilder();
        foreach (Vector3 v in aVectors)
        {
            sb.Append(v.x).Append(" ").Append(v.y).Append(" ").Append(v.z).Append("|");
        }
        if (sb.Length > 0) // remove last "|"
            sb.Remove(sb.Length - 1, 1);
        return sb.ToString();
    }
    public static Vector3[] DeserializeVector3Array(string aData)
    {
        string[] vectors = aData.Split('|');
        Vector3[] result = new Vector3[vectors.Length];
        for (int i = 0; i < vectors.Length; i++)
        {
            string[] values = vectors[i].Split(' ');
            if (values.Length != 3)
                throw new System.FormatException("component count mismatch. Expected 3 components but got " + values.Length);
            result[i] = new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
        }
        return result;
    }    
*/