using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HavePremadeLevel : MonoBehaviour
{
    public static HavePremadeLevel instance;

    private void Awake()
    {
        instance = this; 
    }
}