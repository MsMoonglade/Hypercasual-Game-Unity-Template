using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
    void Awake()
    {
        Init();
    }

    void Init()
    {
        CheckNewGameVersion();

        DOTween.Init();

        SceneManager.LoadScene(1);
    }

    //if new Store version is downloaded clear PlayerPrefs
    void CheckNewGameVersion()
    {
        if (PlayerPrefs.HasKey(Saves.gameVersion))
            return;

        else
        {
            SaveManager.Instance.ClearAllSave();
            SaveManager.Instance.Save(1, Saves.gameVersion);
        }
    }    
}