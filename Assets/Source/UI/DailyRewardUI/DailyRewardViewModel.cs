using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardViewModel : MonoBehaviour
{
    public DailyRewardModel rewardModel;
    public DailyRewardView rewardView;

    private void OnEnable()
    {
        DailyRewardSystem.StreakReseted += ResetRewardStreak;
        DailyRewardSystem.RewardClimed += UpdateRewards;
    }

    private void Start()
    {
        Initialize();
        rewardView.UpdateRewardTimer(rewardModel);
    }

    private void Initialize()
    {
        rewardView.InitializeRewards(rewardModel);
    }

    private void ResetRewardStreak()
    {
        rewardView.ResetRewards(rewardModel);
    }

    private void UpdateRewards()
    {
        rewardView.UpdateRewardStreak(rewardModel);
    }

    public void OnClickClaimReward()
    {
        rewardView.ClimeReward(rewardModel);
    }

    private void OnDisable()
    {
        DailyRewardSystem.StreakReseted -= ResetRewardStreak;
        DailyRewardSystem.RewardClimed -= UpdateRewards;
    }
}
