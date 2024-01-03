using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePopulator : MonoBehaviour
{
    [Header("Variables")]
    public AnimationCurve obstaclesHp;
    public int obstaclesQuantity;
    public float obstaclesStartDistance;
    public float obstaclesDistance;

    [Header("Project References")]
    public GameObject endGameObstaclesPref;

    [Header("Private Variables")]
    private Vector3 localobstaclesPos;

    private void OnEnable()
    {
        GenerateObstacles();
        SetObstaclesHp();
    }

    /// <summary>
    /// Generate End Game Obstacles
    /// </summary>
    private void GenerateObstacles()
    {
        localobstaclesPos = Vector3.zero;
        localobstaclesPos += new Vector3(0, 0, obstaclesStartDistance);

        for(int i = 0; i < obstaclesQuantity; i++)
        {
            GameObject currentObstacles = Instantiate(endGameObstaclesPref , localobstaclesPos , Quaternion.identity , transform);
            currentObstacles.transform.localPosition = localobstaclesPos;

            localobstaclesPos += new Vector3(0, 0, obstaclesDistance);
        }
    }

    /// <summary>
    /// Set End Game Obstacles Hp , based on obstaclesHp animation curve
    /// </summary>
    private void SetObstaclesHp()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            int o_thisObstacleHp = (int)obstaclesHp.Evaluate(i);

            transform.GetChild(i).GetComponent<EndGameObstacles>().SetObstaclesHp(o_thisObstacleHp);
        }
    }
}