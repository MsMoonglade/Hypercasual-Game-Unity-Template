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
            PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        }
    }

    protected override void Awake()
    {
        base.Awake();
      
        LoadLevelSave();
    }

    public void GenerateLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene != SceneManager.sceneCount - 1)
            return;

        else        
            transform.GetComponent<LevelPopulator>().PopulateLevel(CurrentLevel);                
    }

    private void LoadLevelSave() 
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
            CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        else
        {
            CurrentLevel = 1;
            PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        }
    }
}