using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndGameBestScoreFlag : MonoBehaviour
{
    [Header("Local References")]
    public ParticleSystem bestScoreParticle;

    [Header("Private Variables")]
    private float currentBestScore;

    private void Awake()
    {
        currentBestScore = SaveManager.Instance.LoadInt(Saves.endGameFlagScore);          
        transform.localPosition = new Vector3(0, 0, currentBestScore);    
    }

    private void Update()
    {
        if (GameManager.Instance.IsInGame)
        {
            CheckPlayerPosInEndGame();
        }
    }

    /// <summary>
    /// Get Character Behaviour Position In End Game 
    /// </summary>
    private void CheckPlayerPosInEndGame()
    {
        if(CharacterBehaviour.Instance.transform.position.z > transform.position.z)
        {
            transform.DOMoveZ(CharacterBehaviour.Instance.transform.position.z, 0.1f);
        }

        SaveNewPos();
    }


    /// <summary>
    /// Update New FlagPosition
    /// </summary>
    private void SaveNewPos()
    {
        if(transform.localPosition.z > currentBestScore)
        {
            currentBestScore = transform.localPosition.z;

            SaveManager.Instance.Save(currentBestScore, Saves.endGameFlagScore);
        }       
    }
}