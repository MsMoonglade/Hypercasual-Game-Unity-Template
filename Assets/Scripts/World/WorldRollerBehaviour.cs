using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRollerBehaviour : Singleton<WorldRollerBehaviour>
{
    [Header("Variables")]
    public float objectMoveSpeed;

    [Header("Take Objects Variables")]
    public float takeObjectZPositionOffset;
    public float takeObjectAnimSpeed;

    [Header("Local References")]
    public GameObject endRollerDestination;

    /// <summary>
    /// Take Object from World to Roller than move It
    /// </summary>
    /// <param name="i_object"></param>
    public void TakeObject(GameObject i_object)
    {
        i_object.transform.SetParent(transform, true);

        MoveToRoller(i_object);
    }

    /// <summary>
    /// Make object jump to nearest Roller Position 
    /// </summary>
    /// <param name="i_object"></param>
    private void MoveToRoller(GameObject i_object)
    {
        Vector3 nearestRollerPos = new Vector3(transform.position.x, transform.position.y, i_object.transform.position.z + takeObjectZPositionOffset + Random.Range(1f, takeObjectZPositionOffset));

        Sequence takeSequence = DOTween.Sequence();

        takeSequence.Append(i_object.transform.DOLocalRotate(new Vector3(0, 0, 0), takeObjectAnimSpeed));
        takeSequence.Append(transform.DOLocalJump(nearestRollerPos, 2, 1, takeObjectAnimSpeed)
           .OnComplete(() => StartObjectMoving(i_object)));
    }

    /// <summary>
    /// Start Object Moving
    /// </summary>
    /// <param name="i_object"><Object to move/param>
    private void StartObjectMoving(GameObject i_object)
    {
        float zDistance = endRollerDestination.transform.position.z - i_object.transform.position.z;
        float reachEndNecessaryTime = zDistance / objectMoveSpeed;

        i_object.transform.DOMoveZ(endRollerDestination.transform.position.z, reachEndNecessaryTime)
            .SetEase(Ease.InSine)
            .OnComplete(() => i_object.gameObject.SetActive(false));
    }
}