using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGeneric : MonoBehaviour
{
    private void Awake()
    {
        float value = 19.24f;
        TestGenericType(value);
    }

    public void TestGenericType<T>(T i_value)
    {
        if (typeof(T) == typeof(float))
        {
            Debug.Log(Convert.ToDecimal(i_value));
        }
    }
}
