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
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (!currentSceneName.Contains("Randomizer"))
        {
            Debug.Log("This is a Premade Level Scene");
            return;
        }

        else
        {
            Debug.Log("Generating New Level");
            transform.GetComponent<LevelPopulator>().PopulateLevel(CurrentLevel);
        }
    }

    private void Load() 
    {
        CurrentLevel = SaveManager.Instance.Load<int>(Saves.currentLevel);

        if (CurrentLevel == 0)
            CurrentLevel = 1;
    }
}