using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;

public class TextDamageProReferences : MonoBehaviour
{
    public static TextDamageProReferences instance;

    [Header("Local References")]
    public DamageNumber damageProReference_01;

    private void Awake()
    {
        instance = this;    
    }
}
