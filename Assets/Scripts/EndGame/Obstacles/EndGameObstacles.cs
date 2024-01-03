using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameObstacles : MonoBehaviour
{
    /// <summary>
    /// Set End Game Obstacles Set Hp
    /// </summary>
    /// <param name="i_value"><new Hp Amount/param>
    public void SetObstaclesHp(int i_value)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.GetComponent<EndGameObstacle>().SetHp(i_value);
        }
    }
}
