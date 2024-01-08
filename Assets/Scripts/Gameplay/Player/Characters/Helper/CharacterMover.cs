using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
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

    [Header("Hor Rotation On Move")]
    public float moveRotationAmount;
    public float moveRotationSpeed;


    [Header("Private Variables")]
    private CharacterBehaviour characterBehaviour;
    private float localMoveVariables;

    private void Awake()
    {
        characterBehaviour = GetComponent<CharacterBehaviour>();
    }

    private void Update()
    {
        ForwardMove();
    }

    public void ForwardMove()
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

    public void HorizontalMove(Vector3 i_direction)
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
    public void ChangeMoveSpeed(float i_amount)
    {
        forwardMoveSpeed += i_amount;

        if (i_amount < 0)
            characterBehaviour.powerUpParticle.Play();
        else
            characterBehaviour.powerDownParticle.Play();
    }

    /// <summary>
    /// Feedback when player hit an obstacles
    /// </summary>
    public void HitObstaclesFeedback()
    {
        characterBehaviour.characterShooter.StopShoot();

        transform.DOPause();
        transform.DOKill();

        localMoveVariables = forwardMoveSpeed;
        forwardMoveSpeed = 0;

        Vector3 jumpEndPos = transform.position - new Vector3(0, 0, hitObstacles_jumpLenght);

        transform.DOJump(jumpEndPos, hitObstacles_jumpHeight, 1, hitObstacles_jumpDuration)
            .SetDelay(hitObstacles_jumpDelay)                         
            .OnComplete(HitObstaclesFeedbackReset);
    }

    private void HitObstaclesFeedbackReset()
    {
        forwardMoveSpeed = localMoveVariables;

        characterBehaviour.characterShooter.StartShoot();
    }

    private void HorizontalRotate(Vector3 i_direction)
    {
        if (i_direction.x > 0)
        {
            characterBehaviour.model.transform.DOLocalRotate(new Vector3(0, 0, -EvaluateMoveRotationAmount(i_direction.x)), moveRotationSpeed);
        }

        else if (i_direction.x < 0)
        {
            characterBehaviour.model.transform.DOLocalRotate(new Vector3(0, 0, EvaluateMoveRotationAmount(i_direction.x)), moveRotationSpeed);
        }

        else
        {
            characterBehaviour.model.transform.DOLocalRotate(new Vector3(0, 0, 0), moveRotationSpeed);
        }
    }

    private float EvaluateMoveRotationAmount(float i_amount)
    {
        float o_amount = 0;

        float valueForMaxRot = 1.75f;

        o_amount = (Math.Abs(i_amount) * moveRotationAmount) / valueForMaxRot;

        if (o_amount > moveRotationAmount * 2)
            o_amount = moveRotationAmount * 2;

        return o_amount;
    }
}
