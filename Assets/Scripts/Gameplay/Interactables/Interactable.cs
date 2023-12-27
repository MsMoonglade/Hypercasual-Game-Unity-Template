using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Interactable : MonoBehaviour
{
    public bool disableBulletOnHit;

    [Header("Base LocalReference")]
    public TMP_Text dynamicText;

    [Header("Private Local Variables")]
    protected bool isCompleted;

    protected virtual void Awake()
    {
        UpdateUi();

        isCompleted = false;
    }

    protected virtual void OnEnable()
    {
        EventManager.StartListening(Events.setDifficulty, OnSetDifficulty);
    }

    protected virtual void OnDisable()
    {
        EventManager.StopListening(Events.setDifficulty, OnSetDifficulty);
    }

    /// <summary>
    /// The condition for Tis Object Is simply Pass It's Z Pos
    /// </summary>
    public virtual void Take()
    {
      
    }

    /// <summary>
    /// Recive Bullet Hit With a Value
    /// </summary>
    /// <param name="i_value"></param>
    public virtual void TakeHit(int i_value)
    {

    }

    /// <summary>
    /// Recive Bullet Hit With a Game Object
    /// </summary>
    /// <param name="i_object"></param>
    public virtual void TakeHit(GameObject i_object)
    {

    }

    //Check if Player Z Position is greather than this Position Z
    protected virtual bool CheckPlayerPosition()
    {
        if (CharacterBehaviour.Instance.transform.position.z >= transform.position.z)
        {
            return true;
        }

        else
            return false;   
    }

    /// <summary>
    /// Complete This Interactable in TakeHit
    /// </summary>
    /// <param name="i_completed">if completed or nor completed</param>
    protected virtual void Complete(bool i_completed)
    {
        if (!isCompleted)
        {
            isCompleted = true;

            if (i_completed)
                HandleComplete();

            else
                HandleDisable();
        }
    }

    /// <summary>
    /// Behaviour for fully complete the interactable
    /// </summary>
    protected virtual void HandleComplete()
    {
    }

    /// <summary>
    /// Behaviour for disable the interactable
    /// </summary>
    protected virtual void HandleDisable()
    {
    }

    protected virtual void MoveToWorldRoller(GameObject i_objToMove)
    {
        WorldRollerBehaviour.Instance.TakeObject(i_objToMove);
    }

    /// <summary>
    /// Update any Ui Attached to this Interacrable
    /// </summary>
    protected virtual void UpdateUi()
    {
        TweenTextScale();
    }

    private void TweenTextScale()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(dynamicText.transform.DOScale(1.3f, 0.16f));
        mySequence.Append(dynamicText.transform.DOScale(1, 0.16f));
    }

    /// <summary>
    /// When Difficulty Event is Called Set the Interacrable Difficulty
    /// </summary>
    /// <param name="sender"></param>
    protected virtual void OnSetDifficulty(object sender)
    {
    }
}