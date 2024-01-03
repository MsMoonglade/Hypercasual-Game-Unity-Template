using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameReward : MonoBehaviour
{
    [Header("Variables")]
    public AnimationCurve endLevelRewardAmount;
    public EndGameRewardType rewardType;

    public void GiveRewards()
    {
        int rewardAmount = (int)endLevelRewardAmount.Evaluate(LevelManager.Instance.CurrentLevel);

        if(rewardType == EndGameRewardType.Gold)     
            CurrencyManager.Instance.AddGold(rewardAmount);
    }
}

public enum EndGameRewardType
{ 
    Gold
}
