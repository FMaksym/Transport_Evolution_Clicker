using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImproveManager : MonoBehaviour
{
    public ImprovementData improvementData;
    [SerializeField] private ClickerBot _clickerBot;

    private void Start()
    {
        _clickerBot.gameObject.SetActive(improvementData.IsTapBotPurchased);
    }

    public void ImproveClickReward(CurrencyManager currency)
    {
        if (improvementData.ClickRewardCurrentLevel < improvementData.ClickRewardMaxLevel)
        {
            int cost = improvementData.ClickRewardPrices[improvementData.ClickRewardCurrentLevel];
            if (currency.SpendCurrency(cost))
            {
                improvementData.ClickRewardCurrentLevel++;
                improvementData.ClickCost += improvementData.ClickRewardUpgradeStep;
                improvementData.SaveData();
            }
        }
    }

    public void ImproveMaxEnergy(CurrencyManager currency)
    {
        if (improvementData.MaxEnergyCurrentLevel < improvementData.MaxEnergyMaxLevel)
        {
            int cost = improvementData.MaxEnergyPrices[improvementData.MaxEnergyCurrentLevel];
            if (currency.SpendCurrency(cost))
            {
                improvementData.MaxEnergyCurrentLevel++;
                improvementData.MaxEnergy += improvementData.MaxEnergyUpgradeStep;
                improvementData.SaveData();
            }
        }
    }

    public void ImproveRechargeSpeed(CurrencyManager currency)
    {
        if (improvementData.RechargeSpeedCurrentLevel < improvementData.RechargeSpeedMaxLevel)
        {
            int cost = improvementData.RechargeSpeedPrices[improvementData.RechargeSpeedCurrentLevel];
            if (currency.SpendCurrency(cost))
            {
                improvementData.RechargeSpeedCurrentLevel++;
                improvementData.RechargeSpeed -= improvementData.RechargeSpeedUpgradeStep;
                improvementData.SaveData();
            }
        }
    }

    public void PurchaseTapBot(CurrencyManager currency)
    {
        if (!improvementData.IsTapBotPurchased)
        {
            int cost = improvementData.TapBotPrice;
            if (currency.SpendCurrency(cost))
            {
                improvementData.IsTapBotPurchased = true;
                improvementData.SaveData();
            }
        }
    }

    //private void LoadUpgradeLevels()
    //{
    //    _clickRewardLevel = PlayerPrefs.GetInt(_clickRewardLevelKey, 1); // Default level is 1
    //    _maxEnergyLevel = PlayerPrefs.GetInt(_maxEnergyLevelKey, 1); // Default level is 1
    //}

    //private void SaveClickRewardLevel()
    //{
    //    PlayerPrefs.SetInt(_clickRewardLevelKey, upgradeData._clickRewardCurrentLevel);
    //    PlayerPrefs.Save();
    //}

    //private void SaveMaxEnergyLevel()
    //{
    //    PlayerPrefs.SetInt(_maxEnergyLevelKey, upgradeData._maxEnergyCurrentLevel);
    //    PlayerPrefs.Save();
    //}

    //public int GetClickRewardLevel()
    //{
    //    return _clickRewardLevel;
    //}

    //public int GetMaxEnergyLevel()
    //{
    //    return _maxEnergyLevel;
    //}
}
