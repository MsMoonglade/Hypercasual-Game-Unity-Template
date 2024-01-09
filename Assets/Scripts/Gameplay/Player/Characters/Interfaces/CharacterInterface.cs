using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    CharactersMover Mover { get; }
    CharacterShooter Shooter { get; }

    void OnTriggerEnter() { }
}

public interface ICharacterMover
{
    void ForewardMove() { }
    void HorizontalMove() { }
    void HitObstaclesFeedback() { }
    void HorizontalRotate() { }
}

public interface ICharacterShooter
{
    void StartShoot() { }
    void StopShoot() { }
    void Shoot() { }
    IEnumerator ShootCoroutine() { return null; }    
}