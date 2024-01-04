using OpenCover.Framework.Model;
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
    /// Save Generic value at Given Name
    /// </summary>
    /// <param name="i_savedName"><given save name/param>
    /// <returns></returns>
    public void Save<T> (T i_save , string i_savedName )
    {
        if (typeof(T) == typeof(int))
        {
            Debug.Log("Save a Int");
            PlayerPrefs.SetInt(i_savedName, Convert.ToInt32(i_save));
        }

        if (typeof(T) == typeof(float))
        {
            Debug.Log("Save a float");
            PlayerPrefs.SetFloat(i_savedName, (float)Convert.ToDecimal(i_save));
        }

        if (typeof(T) == typeof(string))
        {
            Debug.Log("Save a string");
            PlayerPrefs.SetString(i_savedName, Convert.ToString(i_save));
        }       
    }

    /// <summary>
    /// Save Generic[] at Given Name
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="i_save"><given array to name/param>
    /// <param name="i_savedName"><given save name/param>
    public void Save<T , U>(T[] i_value , U[] i_index , string i_savedName)
    {
        if (typeof(T) == typeof(int) && i_index.Length == 0)
        {
            Debug.Log("Save a Int[]");

            int[] valueToSave = new int[i_value.Length];

            for (int i = 0; i < i_value.Length; i++)
            {
                valueToSave[i] = Convert.ToInt32(i_value[i]);
            }

            string stringToSave = ArrayToString(valueToSave);

            PlayerPrefs.DeleteKey(i_savedName);
            PlayerPrefs.SetString(i_savedName, stringToSave);
        }

        if (typeof(T) == typeof(int) && i_index.Length >= 1)
        {
            Debug.Log("Save a List<CellComposition>");

            string[] cellsData = new string[i_value.Length];
            for (int i = 0; i < cellsData.Length; i++)
            {
                cellsData[i] = i_index[i].ToString() + "/" + i_value[i].ToString();
            }

            PlayerPrefs.DeleteKey(i_savedName);
            PlayerPrefs.SetString(i_savedName, string.Join("#", cellsData));
        }
    }

    /// <summary>
    /// Return Generic value at Given Name
    /// </summary>
    /// <param name="i_savedName"><given saved name/param>
    /// <returns></returns>
    public T Load<T>(string i_savedName)
    {
        if (PlayerPrefs.HasKey(i_savedName))
        {
            if (typeof(T) == typeof(int))
                return (T)(object)PlayerPrefs.GetInt(i_savedName);

            if (typeof(T) == typeof(float))
                return (T)(object)PlayerPrefs.GetFloat(i_savedName);

            if (typeof(T) == typeof(string))
                return (T)(object)PlayerPrefs.GetInt(i_savedName);

            if (typeof(T) == typeof(int[]))
            {
                string savedString = PlayerPrefs.GetString(i_savedName);
                int[] o_result = StringToArray(savedString);
                return (T)(object)o_result;
            }

            if (typeof(T) == typeof(List<CellComposition>))
            {
                string savedString = PlayerPrefs.GetString(i_savedName);

                string[] splittedString = SplitString(savedString, '#');

                List<CellComposition> o_savedCells = new List<CellComposition>();

                foreach (string values in splittedString)
                {
                    string[] cellsValue = SplitString(values, '/');

                    if (cellsValue.Length != 2)
                        Debug.LogWarning("Cells Value not valid");
                    else
                    {
                        CellComposition cell = new CellComposition(int.Parse(cellsValue[0]), int.Parse(cellsValue[1]));
                        o_savedCells.Add(cell);
                    }
                }

                return (T)(object)o_savedCells;
            }
        }

        Debug.LogWarning("You don't have this saved name");
        return (T)(object)default;
    }

    /// <summary>
    /// Convert Array into a String
    /// </summary>
    /// <param name="i_array"><Array to convert/param>
    private static string ArrayToString(int[] i_array)
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
    private static int[] StringToArray(string i_string)
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
    private static string[] SplitString(string i_string , char i_splitter)
    { 
        string[] o_splittedString = i_string.Split(new[] { i_splitter }, StringSplitOptions.None);

        return o_splittedString;
    }
}