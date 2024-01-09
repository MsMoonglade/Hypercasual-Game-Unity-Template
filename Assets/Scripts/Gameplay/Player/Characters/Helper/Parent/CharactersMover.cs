using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersMover : MonoBehaviour , ICharacterMover
{
    [Header("Move Variables")]
    public float forwardMoveSpeed;
    public float horMoveSpeed;
    public float moveXLimit;

    [Header("Hit Obstacles Variables")]
    public float hitObstacles_jumpLenght;
    public float hitObstacles_jumpHeight;
    public float hitObstacles_jumpDuration;
    public float hitObstacles_jumpDelay;

    [Header("Horizontal Rotation On Move")]
    public float moveRotationAmount;
    public float moveRotationSpeed;

    [Header("Private Variables")]
    protected Characters character;
    protected float originalMoveSpeed;

    protected virtual void Awake()
    {
        character = transform.GetComponent<Characters>();   
    }

    protected virtual void Update(){ }

    protected virtual void ForwardMove()
    {
        //MOVE
        if (GameManager.Instance.IsInGame)
        {
            //move forward if is in game
            transform.Translate(Vector3.forward * forwardMoveSpeed * Time.deltaTime);

            //Clamp Character X Position
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -moveXLimit, moveXLimit), transform.position.y, transform.position.z);
        }
    }

    public virtual void HorizontalMove(Vector3 i_direction)
    {           
        transform.Translate(i_direction * Time.deltaTime * horMoveSpeed);

        if (moveRotationAmount > 0)
        {
            HorizontalRotate(i_direction);
        }
    }

    /// <summary>
    /// Increase/Decrease MoveSpeed
    /// </summary>
    /// <param name="i_amount"><Amount to Increase or Decrease Fire Rate /param>
    public virtual void ChangeMoveSpeed(float i_amount)
    {
        forwardMoveSpeed += i_amount;
    }

    /// <summary>
    /// Feedback when player hit an obstacles
    /// </summary>
    public virtual void HitObstaclesFeedback()
    {
        transform.DOPause();
        transform.DOKill();

        originalMoveSpeed = forwardMoveSpeed;
        forwardMoveSpeed = 0;

        Vector3 jumpEndPos = transform.position - new Vector3(0, 0, hitObstacles_jumpLenght);

        transform.DOJump(jumpEndPos, hitObstacles_jumpHeight, 1, hitObstacles_jumpDuration)
            .SetDelay(hitObstacles_jumpDelay)                         
            .OnComplete(HitObstaclesFeedbackReset);
    }

    protected virtual void HitObstaclesFeedbackReset()
    {
        forwardMoveSpeed = originalMoveSpeed;
    }

    protected virtual void HorizontalRotate(Vector3 i_direction)
    {

    }

    protected virtual float EvaluateMoveRotationAmount(float i_amount)
    {
        float o_amount = 0;

        float valueForMaxRot = 1.75f;

        o_amount = (Math.Abs(i_amount) * moveRotationAmount) / valueForMaxRot;

        if (o_amount > moveRotationAmount * 2)
            o_amount = moveRotationAmount * 2;

        return o_amount;
    }
}
