using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameBehaviour : Singleton<EndGameBehaviour>
{
    [Header("Variables")]
    public AnimationCurve endLevelCoinReward;

    [Header("Local References")]
    public Canvas endGameCanvas;


    [Header("Private References")]
    private Coroutine startEndGameCoroutine;

    private void Awake()
    {
        startEndGameCoroutine = null;

        UpdateEndGameUi();
    }

    private void Start()
    {
        CameraManager.Instance.endGameCamera = transform.GetComponentInChildren<CinemachineVirtualCamera>();
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