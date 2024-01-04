using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceByValue : SingletonPersistent<ReferenceByValue>
{
    [Header("Value Variables")]
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
    public T ReturnValue<T>(T[] possibleValue, int i_index)
    {
        int localIndex = i_index > possibleValue.Length - 1 ? possibleValue.Length - 1 : i_index;

        localIndex = localIndex / valueSegment;

        return possibleValue[localIndex];
    }
}