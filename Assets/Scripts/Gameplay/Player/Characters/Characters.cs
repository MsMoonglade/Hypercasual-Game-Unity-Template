using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour, ICharacter
{
    public CharacterMover Mover { get { return transform.GetComponent<CharacterMover>(); } }
    public CharacterShooter Shooter { get { return transform.GetComponent<CharacterShooter>(); } }

    protected virtual void OnTriggerEnter(Collider col)
    {
        //handle different type of collision for each individual character        
    }
}

public interface ICharacter
{
    CharacterMover Mover { get;}
    CharacterShooter Shooter { get;}

    void OnTriggerEnter(){}
}