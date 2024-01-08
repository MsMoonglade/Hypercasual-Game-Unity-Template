using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : Singleton<CharactersManager>
{
    public Characters CurrentActiveCharacter { get; private set; }

    public void SetCurrentActiveCharacter<T>(T i_newChar) where T : Characters
    {
        CurrentActiveCharacter = i_newChar;
    }
}