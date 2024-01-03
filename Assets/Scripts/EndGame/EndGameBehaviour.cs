using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EndGameReward))]
public class EndGameBehaviour : Singleton<EndGameBehaviour>
{
    [Header("Local References")]
    public Canvas endGameCanvas;

    [Header("Private References")]
    private Coroutine startEndGameCoroutine;

    private CinemachineVirtualCamera endGameCamera;
    private EndGameCharacter endGameCharacter;

    private void Awake()
    {
        startEndGameCoroutine = null;

        UpdateEndGameUi();
    }

    private void Start()
    {
        endGameCamera = transform.GetComponentInChildren<CinemachineVirtualCamera>();      
        endGameCharacter = transform.GetComponentInChildren<EndGameCharacter>();

        Transform objToFollow;
        objToFollow = (endGameCharacter == null) ? CharacterBehaviour.instance.transform  : endGameCharacter.transform;

        CameraManager.Instance.endGameCamera = endGameCamera;
    }

    public void HandleEndGameVictory()
    {
        transform.GetComponent<EndGameReward>().GiveRewards();
    }

    /// <summary>
    /// Take Bullet's Hit
    /// </summary>
    /// <param name="i_bulletValue"><bullet value/param>
    public void TakeBulletHit(int i_bulletValue)
    {
        UpdateEndGameUi();
    }

    /// <summary>
    /// Start End Game Behaviour
    /// </summary>
    public void StartEndGame()
    {
        if (startEndGameCoroutine == null)
        {
            startEndGameCoroutine = StartCoroutine(StartEndGameCoroutine());
        }
    }

    private IEnumerator StartEndGameCoroutine()
    {
        GameManager.Instance.StartEndGame();

        endGameCanvas.transform.DOScale(Vector3.zero, 0.5f);

        yield return null;
    }

    /// <summary>
    /// Update EndGame Ui
    /// </summary>
    private void UpdateEndGameUi()
    {
    }
}