using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPopulator : MonoBehaviour
{
    [Header("Automatic LevelVariables")]
    public AnimationCurve levelLenght;
    public float gameplayElement_StartPosOffset;
    public float gameplayElement_DistanceOffset;
    public float endElement_DistanceOffset;

    [Header("Level Composition")]
    public float wall_PercentageSpawnRate;
    public float interactable_PercentageSpawnRate;
    public float mixed_PercentageSpawnRate;

    [Header("Local References")]
    public GameObject wallParent;
    public GameObject interactableParent;
    public GameObject mixedParent;
    public GameObject floorObject;

    [Header("Project References")]
    public GameObject[] possibleWall;
    public GameObject[] possibleInteractable;
    public GameObject[] possibleMixed;

    public void PopulateLevel(int i_currentLevel)
    {
        float levelLenght = EvaluateLevelLenght(i_currentLevel);
        //Define how many LevelSegment you need
        int levelPieces = (int)(levelLenght / gameplayElement_DistanceOffset);

        //Start Position for LevelSegment Element
        Vector3 elementPosition = Vector3.zero;    
        elementPosition += new Vector3(0, 0, gameplayElement_StartPosOffset);

        for (int i = 0; i < levelPieces; i++)
        {
            //GENERATE WALL
            float levelElementPercentage = Random.Range(0f, 1f);

            if (levelElementPercentage <= wall_PercentageSpawnRate)
            {
                GameObject element = PickRandomElement(possibleWall, wallParent);
                element.transform.position = elementPosition;
            }

            //GENERATE INTERACTABLE
            else if (levelElementPercentage - wall_PercentageSpawnRate <= interactable_PercentageSpawnRate)
            {
                GameObject element = PickRandomElement(possibleInteractable, interactableParent);
                element.transform.position = elementPosition;
            }

            //GENERATE MIXED
            else if (levelElementPercentage - wall_PercentageSpawnRate - interactable_PercentageSpawnRate <= mixed_PercentageSpawnRate)
            {
                GameObject element = PickRandomElement(possibleMixed, mixedParent);
                element.transform.position = elementPosition;
            }

            else
                continue;

            elementPosition += new Vector3(0, 0, gameplayElement_DistanceOffset);
        }

        //GENERATE END GAME
        elementPosition += new Vector3(0, 0, endElement_DistanceOffset);
        floorObject.transform.position = elementPosition;

        //Each Gameplay Element Recive this Event and Take the difficulty
        EventManager.TriggerEvent(Events.setDifficulty);
    }

    private GameObject PickRandomElement(GameObject[] possiblePool, GameObject parent)
    {
        int index = Random.Range(0, possiblePool.Length);

        GameObject o = Instantiate(possiblePool[index], Vector2.zero, Quaternion.identity, parent.transform);
        return o;
    }

    private float EvaluateLevelLenght(int i_level)
    {
        int fixedLevel = 0;

        if (i_level < 25)
        {
            fixedLevel = i_level;
        }

        else
            fixedLevel = 25;

        float o_lenght = levelLenght.Evaluate(fixedLevel);
        
        return o_lenght;
    }
}