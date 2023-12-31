using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    //use wath you need to update current item
    public GameObject modelParent;
    public Material modelMaterial;

    public TMP_Text valueText;

    private Vector3 animNewLocalPos;

    public int Value { get; private set; } = 0;

    public void UpdateValue(int i_valueToAdd)
    {
        Value += i_valueToAdd;

        valueText.text = Value.ToString();

        UpdateModel();
    }

    private void UpdateModel()
    {

    }

    /// <summary>
    /// Set New Position for Shoot Animation
    /// </summary>
    /// <param name="i_localPos"></param>
    public void AnimationSetDestination(Vector3 i_localPos)
    {
        animNewLocalPos = i_localPos;
    }

    /// <summary>
    /// Start Shoot Animation
    /// </summary>
    public void AnimateToNewPosition(float i_animTime)
    {
        transform.DOLocalMove(animNewLocalPos, i_animTime)
            .OnComplete(() => EnableModel());
    }

    /// <summary>
    /// Disable Model at shoot animations
    /// </summary>
    public void DisableModel()
    {
        if (modelParent.activeInHierarchy)
            modelParent.SetActive(false);
    }

    /// <summary>
    /// ReEnable Model after shoot animations
    /// </summary>
    private void EnableModel()
    {
        if(!modelParent.activeInHierarchy)
            modelParent.SetActive(true);
    }
}
