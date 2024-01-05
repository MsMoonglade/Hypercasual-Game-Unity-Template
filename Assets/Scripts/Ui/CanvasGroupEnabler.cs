using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupEnabler : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// check this object's Canvas Ground and Enable/Disabled Interactable based on Alpha
    /// </summary>
    public void ChangeInteractable()
    {
        if (canvasGroup.alpha != 1)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        else
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}