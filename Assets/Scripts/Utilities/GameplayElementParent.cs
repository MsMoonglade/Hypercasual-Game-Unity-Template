using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayElementParent : MonoBehaviour
{
    public static GameplayElementParent instance;

    private void Awake()
    {
        instance = this; 
    }
}