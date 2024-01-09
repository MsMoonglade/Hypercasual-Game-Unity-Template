using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterUi : MonoBehaviour
{
    [Header("Local References")]
    public CanvasGroup characterCanvasPanel;
    public TMP_Text characterValueText;

    private void Awake()
    {
        if(characterCanvasPanel != null)        
            characterCanvasPanel.alpha = 0;
    }

    private void RefreshCharacterUi()
    {
        if(characterValueText != null)        
            characterValueText.text = "Character Value";
    }
}