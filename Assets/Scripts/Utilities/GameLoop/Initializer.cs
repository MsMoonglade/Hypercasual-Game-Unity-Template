using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
    void Awake()
    {
        CheckNewGameVersion();

        DOTween.Init();

        SceneManager.LoadScene(1);
    }

    //is new Store version is downloaded clear PlayerPrefs
    void CheckNewGameVersion()
    {
        if (PlayerPrefs.HasKey("Build_1.0"))
            return;

        else
        {
            PlayerPrefs.DeleteAll();

            PlayerPrefs.SetInt("Build_1.0", 1);
        }
    }    
}