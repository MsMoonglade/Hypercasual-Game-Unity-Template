using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LevelPopulator))]
public class LevelManager : Singleton<LevelManager>
{
    //Level Stats
    private int currentLevel;
    public int CurrentLevel
    {
        get
        {
            return currentLevel;
        }

        set
        {
            currentLevel = value;
            SaveManager.Instance.Save(CurrentLevel, Saves.currentLevel);
        }
    }

    protected override void Awake()
    {
        base.Awake();
      
        Load();
    }

    public void GenerateLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene != SceneManager.sceneCount - 2)
            return;

        else        
            transform.GetComponent<LevelPopulator>().PopulateLevel(CurrentLevel);                
    }

    private void Load() 
    {
        CurrentLevel = SaveManager.Instance.Load<int>(Saves.currentLevel);

        if (CurrentLevel == 0)
            CurrentLevel = 1;
    }
}