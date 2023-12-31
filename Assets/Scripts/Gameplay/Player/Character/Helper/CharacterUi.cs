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
        characterCanvasPanel.alpha = 0;
    }

    private void RefreshCharacterUi()
    {
        characterValueText.text = "Character Value";
    }
}
