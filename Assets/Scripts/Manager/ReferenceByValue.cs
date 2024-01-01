using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceByValue : SingletonPersistent<ReferenceByValue>
{
    [Header("Value Variables")]
    public int maxValue;
    public int valueSegment;

    [Space]
    [Header("Project References")]
    public Material[] possibleMats;
    public Color[] possibleColor;    

    /// <summary>
    /// Return Material At given Value
    /// </summary>
    /// <param name="i_value"><given value/param>
    /// <returns></returns>
    public Material ReturnMaterial(int i_value)
    {
        Material o_mat = null;

        int localIndex = i_value > maxValue ? maxValue : i_value;

        localIndex = localIndex / valueSegment;

        o_mat = possibleMats[localIndex];

        return o_mat;
    }

    /// <summary>
    /// Return Color At given Value
    /// </summary>
    /// <param name="i_value"><given value/param>
    /// <returns></returns>
    public Color ReturnColor(int i_value)
    {
        Color o_color = Color.black;

        int localIndex = i_value > maxValue ? maxValue : i_value;

        localIndex = localIndex / valueSegment;

        o_color = possibleColor[localIndex];

        return o_color;
    }
}