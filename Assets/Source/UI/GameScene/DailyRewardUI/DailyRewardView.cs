using System.Threading.Tasks;
using UnityEngine;

public class DailyRewardView : MonoBehaviour
{
    public void InitializeRewards(DailyRewardModel rewardModel)
    {
        UpdateRewardStreak(rewardModel);

        UpdateClaimRewardButton(rewardModel, rewardModel.DailyRewardSystem.CanClaimReward());
    }

    public void UpdateRewardStreak(DailyRewardModel rewardModel)
    {
        for (int i = 0; i < rewardModel.DailyRewardSystem.currentStreak; i++)
        {
            rewardModel.RewardCheakmarkList[i].SetActive(true);
        }
    }

    public async void UpdateRewardTimer(DailyRewardModel rewardModel)
    {
        while (true)
        {
            await Task.Delay(1000);
            rewardModel.TimerText.text = rewardModel.DailyRewardSystem.TimeToNextReward();
        }
    }

    public void ResetRewards(DailyRewardModel rewardModel)
    {
        foreach (var rewardCheakmark in rewardModel.RewardCheakmarkList)
        {
            rewardCheakmark.SetActive(false);
        }
    }

    public void ClimeReward(DailyRewardModel rewardModel)
    {
        if (rewardModel.DailyRewardSystem.CanClaimReward())
        {
            int reward = rewardModel.DailyRewardSystem.rewardList[rewardModel.DailyRewardSystem.currentStreak];

            rewardModel.DailyRewardSystem.CollectReward();
            rewardModel.CurrencyManager.AddCurrency(reward);

            UpdateClaimRewardButton(rewardModel, rewardModel.DailyRewardSystem.CanClaimReward());
        }
    }

    public void ClosePanel(DailyRewardModel rewardModel)
    {
        rewardModel.DailyRewardPanel.SetActive(false);
    }

    private void UpdateClaimRewardButton(DailyRewardModel rewardModel, bool canClimeReward)
    {
        if (!canClimeReward)
        {
            rewardModel.UnactiveClimeRewardButton.SetActive(true);
            rewardModel.ClimeRewardButton.SetActive(false);
        }
        else
        {
            rewardModel.ClimeRewardButton.SetActive(true);
            rewardModel.UnactiveClimeRewardButton.SetActive(false);
        }
    }
}
