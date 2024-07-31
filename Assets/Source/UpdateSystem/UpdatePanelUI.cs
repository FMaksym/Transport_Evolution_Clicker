using TMPro;
using UnityEngine;

public class UpdatePanelUI : MonoBehaviour
{
    [Space(2), Header("Managers")]
    [SerializeField] private ImprovementData _improvementData;
    [SerializeField] private ImproveManager _updateManager;
    [SerializeField] private CurrencyManager _currency;

    [Space(2), Header("Prise Texts")]
    [SerializeField] private TMP_Text ClickRewardPriseText;
    [SerializeField] private TMP_Text MaxEnergyPriseText;
    [SerializeField] private TMP_Text RechargeSpeedPriseText;
    [SerializeField] private TMP_Text TapBotPriseText;

    [Space(2), Header("Current Update Level Texts")]
    [SerializeField] private TMP_Text ClickRewardLevelText;
    [SerializeField] private TMP_Text MaxEnergyLevelText;
    [SerializeField] private TMP_Text RechargeSpeedLevelText;

    [Space(2), Header("Buy Status Texts")]
    [SerializeField] private TMP_Text ClickRewardLevelStatusText;
    [SerializeField] private TMP_Text MaxEnergyLevelStatusText;
    [SerializeField] private TMP_Text RechargeSpeedLevelStatusText;
    [SerializeField] private TMP_Text TapBotPurchaseStatusText;

    [Space(2), Header("Update Buttons")]
    [SerializeField] private GameObject ClickRewardUpdButton;
    [SerializeField] private GameObject MaxEnergyUpdButton;
    [SerializeField] private GameObject RechargeSpeedUpdButton;
    [SerializeField] private GameObject TapBotBuyButton;

    private void OnEnable()
    {
        ImprovementData.ClickCostChanged += UpdateClickRewardInfo;
        ImprovementData.MaxEnergyAmountChanged += UpdateMaxEnergyInfo;
        ImprovementData.RechargeSpeedChanged += UpdateRechargeSpeedInfo;
        ImprovementData.ClickBotPurchased += UpdateTapBotInfo;
    }

    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        UpdateClickRewardInfo();
        UpdateMaxEnergyInfo();
        UpdateRechargeSpeedInfo();
        UpdateTapBotInfo();
    }

    public void ImproveClickCost()
    {
        _updateManager.ImproveClickReward(_currency);
    }

    public void ImproveMaxEnergy()
    {
        _updateManager.ImproveMaxEnergy(_currency);
    }

    public void ImproveRechargeSpeed()
    {
        _updateManager.ImproveRechargeSpeed(_currency);
    }

    public void BuyTapBot()
    {
        _updateManager.PurchaseTapBot(_currency);
    }

    private void UpdateClickRewardInfo()
    {
        ChangeUpdateLevelText(ClickRewardLevelText, _improvementData.ClickRewardCurrentLevel, _improvementData.ClickRewardMaxLevel);

        if (_improvementData.ClickRewardCurrentLevel < _improvementData.ClickRewardMaxLevel)
        {
            ChangePriseText(ClickRewardPriseText, _improvementData.ClickRewardPrices[_improvementData.ClickRewardCurrentLevel]);
        }
        else
        {
            DisableImprovementOption(ClickRewardUpdButton, ClickRewardLevelStatusText);
        }
    }

    private void UpdateMaxEnergyInfo()
    {
        ChangeUpdateLevelText(MaxEnergyLevelText, _improvementData.MaxEnergyCurrentLevel, _improvementData.MaxEnergyMaxLevel);

        if (_improvementData.MaxEnergyCurrentLevel < _improvementData.MaxEnergyMaxLevel)
        {
            ChangePriseText(MaxEnergyPriseText, _improvementData.MaxEnergyPrices[_improvementData.MaxEnergyCurrentLevel]);
        }
        else
        {
            DisableImprovementOption(MaxEnergyUpdButton, MaxEnergyLevelStatusText);
        }
    }

    private void UpdateRechargeSpeedInfo()
    {
        ChangeUpdateLevelText(RechargeSpeedLevelText, _improvementData.RechargeSpeedCurrentLevel, _improvementData.RechargeSpeedMaxLevel);

        if (_improvementData.RechargeSpeedCurrentLevel < _improvementData.RechargeSpeedMaxLevel)
        {
            ChangePriseText(RechargeSpeedPriseText, _improvementData.RechargeSpeedPrices[_improvementData.RechargeSpeedCurrentLevel]);
        }
        else
        {
            DisableImprovementOption(RechargeSpeedUpdButton, RechargeSpeedLevelStatusText);
        }
    }

    private void UpdateTapBotInfo()
    {
        if (!_improvementData.IsTapBotPurchased)
        {
            ChangePriseText(TapBotPriseText, _improvementData.TapBotPrice);
        }
        else
        {
            DisableImprovementOption(TapBotBuyButton, TapBotPurchaseStatusText);
        }
    }

    private void ChangeUpdateLevelText(TMP_Text updateLevelText, int currentLevel, int maxLevel)
    {
        updateLevelText.text = $"{currentLevel}/{maxLevel}";
    }

    private void ChangePriseText(TMP_Text priceText, int newPrice)
    {
        priceText.text = newPrice.ToString();
    }

    private void DisableImprovementOption(GameObject button, TMP_Text levelStatusText)
    {
        button.SetActive(false);
        levelStatusText.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        ImprovementData.ClickCostChanged -= UpdateClickRewardInfo;
        ImprovementData.MaxEnergyAmountChanged -= UpdateMaxEnergyInfo;
        ImprovementData.RechargeSpeedChanged -= UpdateRechargeSpeedInfo;
    }
}
