using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCharacterMover : CharactersMover
{
    private CharacterBehaviour characterBehaviour;

    protected override void Awake()
    {
        characterBehaviour = GetComponent<CharacterBehaviour>();
    }

    protected override void Update()
    {
        if (CharactersManager.Instance.CurrentActiveCharacter == characterBehaviour)
            ForwardMove();
    }

    public override void HorizontalMove(Vector3 i_direction)
    {
        base.HorizontalMove(i_direction);
    }

    public override void ChangeMoveSpeed(float i_amount)
    {
        base.ChangeMoveSpeed(i_amount);

        if (i_amount < 0)
            characterBehaviour.powerUpParticle.Play();
        else
            characterBehaviour.powerDownParticle.Play();
    }

    public override void HitObstaclesFeedback()
    {
        characterBehaviour.characterShooter.StopShoot();

        base.HitObstaclesFeedback();
    }

    protected override void HitObstaclesFeedbackReset()
    {
        base.HitObstaclesFeedbackReset();
        
        characterBehaviour.characterShooter.StartShoot();
    }

    protected override void HorizontalRotate(Vector3 i_direction)
    {
        base.HorizontalRotate(i_direction);

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
}