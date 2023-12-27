using DG.Tweening;
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

    public void HorizontalMove(Vector3 direction)
    {           
        transform.Translate(direction * Time.deltaTime * horMoveSpeed);
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
}
