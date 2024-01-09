using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour, ICharacter
{
    public CharactersMover Mover { get { return transform.GetComponent<CharactersMover>(); } }
    public CharacterShooter Shooter { get { return transform.GetComponent<CharacterShooter>(); } }

    protected virtual void OnTriggerEnter(Collider col)
    {
        //handle different type of collision for each individual character        
    }
}