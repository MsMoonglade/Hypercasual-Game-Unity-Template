using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState State { get; private set; }

    public bool IsInGame
    {
        get
        {
            if (State == GameState.InGame || State == GameState.InEndGame)
                return true;
            else
                return false;
        }
    }

    public bool IsInMainMenu
    {
        get
        {
            if (State == GameState.InGame)
                return true;
            else
                return false;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        ChangeState(GameState.Starting);
    }

    public void ChangeState(GameState newState)
    {
        if (State == newState)
            return;

        State = newState;

        switch (newState)
        {
            case GameState.Starting:
                HandleStarting();
                break;

            case GameState.SpawningLevel:
                HandleSpawningLevel();
                break;

            case GameState.InMainMenu:
                HandleMainMenu();
                break;

            case GameState.InGame:
                HandleInGame();
                break;

            case GameState.InEndGame:
                HandleInEndGame();
                break;

            case GameState.InWin:
                HandleInWin();
                break;

            case GameState.InLose:
                HandleInLose();
                break;
        }
    }

    private void HandleStarting()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;

        DOTween.SetTweensCapacity(500, 250);

        ChangeState(GameState.SpawningLevel);
    }

    private void HandleSpawningLevel()
    {
        LevelManager.Instance.GenerateLevel();

        ChangeState(GameState.InMainMenu);
    }

    private void HandleMainMenu()
    {
        Debug.Log("InMain Menu");
    }

    private void HandleInGame()
    {
        UiManager.Instance.ChangeState(UiState.Game);
        EventManager.TriggerEvent(Events.playGame);

        Debug.Log("Game Started");
    }

    private void HandleInEndGame()
    {
        CameraManager.Instance.SetEndGameCameraPriority();

        Debug.Log("Start End Game Segment");
    }

    private void HandleInWin()
    {
        UiManager.Instance.ChangeState(UiState.EndGame);

        EndGameBehaviour.Instance.HandleEndGameVictory();
        LevelManager.Instance.CurrentLevel++;        
    }

    private void HandleInLose()
    {
        UiManager.Instance.ChangeState(UiState.Retry);
    }

    /// <summary>
    /// Called to start current Level
    /// </summary>
    public void StartGame()
    {
        ChangeState(GameState.InGame);
    }

    /// <summary>
    /// Start End Game Segment of Level
    /// </summary>
    public void StartEndGame()
    {
        ChangeState(GameState.InEndGame);
    }

    /// <summary>
    /// Called to end current Level , with a win or lose condition
    /// </summary>
    /// <param name="i_passed"></param>
    public void EndGame(bool i_passed)
    {
        if (i_passed)
        {
            ChangeState(GameState.InWin);
        }

        else
        {
            ChangeState(GameState.InLose);
        }
    }
}

[Serializable]
public enum GameState
{
    Starting = 0,
    SpawningLevel = 1,
    InMainMenu = 2,
    InGame = 3,
    InEndGame = 4,
    InWin = 5,
    InLose = 6,
}