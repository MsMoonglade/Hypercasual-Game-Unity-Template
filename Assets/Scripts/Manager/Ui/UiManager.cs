using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;

[RequireComponent(typeof(UiInstantiateObject))]
[RequireComponent(typeof(UiCurrency))]
[RequireComponent(typeof(UiRefresh))]
public class UiManager : Singleton<UiManager>
{
    [Header("Panel References")]
    public CanvasGroup mainMenuUi;
    public CanvasGroup gameUi;
    public CanvasGroup endGameUi;
    public CanvasGroup retryUi;
    
    [Header("Specific References")]
    public GameObject endGameConfetti;

    [Header("Local Component")]
    //Used To instantiate element in Ui
    [HideInInspector]
    public UiInstantiateObject instantiator;
    //Store game currencies
    [HideInInspector]
    public UiCurrency uiCurrency;
    //Store ui element that need to refresh
    [HideInInspector]
    public UiRefresh uiRefresh;

    public UiState State { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        instantiator.transform.GetComponent<UiInstantiateObject>();
        uiCurrency = transform.GetComponent<UiCurrency>();
    }

    public void ChangeState(UiState newState)
    {
        if (State == newState)
            return;

        State = newState;

        switch (newState)
        {
            case UiState.MainMenu:
                HandleMainMenu();
                break;

            case UiState.Game:
                HandleGame();
                break;

            case UiState.EndGame:
                HandleEndGame();
                break;

            case UiState.Retry:
                HandleRetry();
                break;
        }
    }

    private void HandleMainMenu()
    {
        mainMenuUi.alpha = 1;
        gameUi.alpha = 0;
        endGameUi.alpha = 0;
        retryUi.alpha = 0;
    }

    private void HandleGame()
    {
        gameUi.alpha = 1;
        endGameUi.alpha = 0;
        retryUi.alpha = 0;
        mainMenuUi.alpha = 0;
    }

    private void HandleEndGame()
    {
        retryUi.alpha = 0;
        mainMenuUi.alpha = 0;
        gameUi.alpha = 0;

        float alpha = 0;
        DOTween.To(() => alpha, x => alpha = x, 1, 0.5f)
            .OnUpdate(() => endGameUi.alpha = alpha)
            .OnComplete(() => OnEndGameUiComplete());
    }

    private void HandleRetry()
    {
        retryUi.alpha = 1;
        mainMenuUi.alpha = 0;
        gameUi.alpha = 0;
        endGameUi.alpha = 0;
    }

    private void OnEndGameUiComplete()
    {
        endGameUi.alpha = 1;
        endGameConfetti.gameObject.SetActive(true);
    }
}

[Serializable]
public enum UiState
{
    MainMenu = 0,
    Game = 1,
    EndGame = 2,
    Retry = 3,
}