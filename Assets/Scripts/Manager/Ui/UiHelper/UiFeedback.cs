using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFeedback : MonoBehaviour
{
    [Header("Local References")]
    public GameObject endGameConfetti;

    public void EndGameFeedback()
    {
        endGameConfetti.gameObject.SetActive(true);
    }  
}