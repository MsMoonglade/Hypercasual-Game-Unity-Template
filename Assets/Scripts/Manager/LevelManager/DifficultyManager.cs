using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : Singleton<DifficultyManager>
{
    [Header("Interactable Variables")]
    public AnimationCurve interactableIndex;
    public float interactableRandomizer;   

    [Header("Wall Variables")]
    public AnimationCurve wallIndex;
    public float wallRandomizer;

    public float InteractableIndex(int i_level)
    {
        return ReturnValue(interactableIndex, interactableRandomizer , i_level);
    }

    public int WallDifficulty(int i_level)
    {
        return ReturnValue(wallIndex, wallRandomizer, i_level);
    }

    public int ReturnValue(AnimationCurve i_curve , float i_Randomizer , int i_level)
    {
        int normalizedLevel = 0;

        if (i_level < 25)
        {
            normalizedLevel = i_level;
        }

        else
            normalizedLevel = 25;

        float o_value = i_curve.Evaluate(normalizedLevel);
        
        o_value += Random.Range(- i_Randomizer, i_Randomizer);
        o_value = Mathf.Abs(o_value);   

        return (int)o_value;
    }
}