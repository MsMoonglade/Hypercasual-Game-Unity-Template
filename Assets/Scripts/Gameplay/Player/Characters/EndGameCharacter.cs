using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharactersMover))]
[RequireComponent(typeof(CharacterShooter))]
[RequireComponent(typeof(CharacterUi))]
public class EndGameCharacter : CharacterBehaviour
{
    public void EnableEndGameCharacter()
    {
        instance = this;

        characterMover = transform.GetComponent<CharactersMover>();
        characterShooter = transform.GetComponent<CharacterShooter>();
        characterUi = transform.GetComponent<CharacterUi>();
    }
}