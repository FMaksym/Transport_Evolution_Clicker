using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewModel : MonoBehaviour
{
    public UIModel uiModel;
    public UIView uiView;

    private void OnEnable()
    {
        EnergyManager.EnergyAmountChanged += UpdateEnergyAmountText;
        ImprovementData.MaxEnergyAmountChanged += UpdateEnergyAmountText;
        TransportHealthManager.HealthChanged += UpdateHealthInfo;
        TransportHealthManager.TransportHealthEnd += LoadTransportInfo;
        TransportHealthManager.TransportHealthEnd += ShowNewLevelMessage;
        InternetChecker.InternetConnectionLose += ShowNoInternetMessage;
    }

    private void Awake()
    {
        uiView.Initialize(uiModel);

        Debug.Log("Initialize LoadView");
    }

    private void Start()
    {
        UpdateEnergyAmountText();
        LoadTransportInfo();
    }

    private void FixedUpdate()
    {
        uiView.ScrollBackground(uiModel);
    }

    private void ShowNoInternetMessage()
    {
        uiView.ShowGameMessage(uiModel.NoInternetMessagePanel);
    }

    private void ShowNewLevelMessage()
    {
        uiView.UpdateNewLevelMessageInfo(uiModel);
        uiView.ShowGameMessage(uiModel.NewLevelMessagePanel);
    }

    private void UpdateEnergyAmountText() => uiView.UpadateEnergyText(uiModel);

    private void LoadTransportInfo() => uiView.LoadTransportInfo(uiModel);
    
    private void UpdateHealthInfo() => uiView.UpadateHealth(uiModel);

    public void OpenUpdatePanel() => uiView.OpenPanel(uiModel.UpdatePanel);

    public void OpenRoadLogPanel() => uiView.OpenPanel(uiModel.RoadLogPanel);

    public void OpenShopPanel() => uiView.OpenPanel(uiModel.ShopPanel);

    public void OpenInstructionPanel() => uiView.OpenPanel(uiModel.InstructionPanel);
    
    public void OpenSettingsPanel() => uiView.OpenPanel(uiModel.SettingsPanel);
    
    public void OpenDailyRewardPanel() => uiView.OpenPanel(uiModel.DailyRewardPanel);

    public void OpenRewardedVideoPanel()
    {
        uiView.UpdateRewardForVideoInfo(uiModel);
        uiView.OpenPanel(uiModel.RewardedVideoPanel);
    }

    public void CloseUpdatePanel() => uiView.ClosePanel(uiModel.UpdatePanel);

    public void CloseRoadLogPanel() => uiView.ClosePanel(uiModel.RoadLogPanel);

    public void CloseShopPanel() => uiView.ClosePanel(uiModel.ShopPanel);

    public void CloseInstructionPanel() => uiView.ClosePanel(uiModel.InstructionPanel);

    public void CloseSettingsPanel() => uiView.ClosePanel(uiModel.SettingsPanel);

    public void CloseDailyRewardPanel() => uiView.ClosePanel(uiModel.DailyRewardPanel);

    public void CloseNewLevelMessage() => uiView.HideMessage(uiModel.NewLevelMessagePanel);
    
    public void CloseRewardedVideoPanel() => uiView.ClosePanel(uiModel.RewardedVideoPanel);

    public void OnClickRestartGame() => uiView.RestartGame(uiModel);

    private void OnDisable()
    {
        EnergyManager.EnergyAmountChanged -= UpdateEnergyAmountText;
        ImprovementData.MaxEnergyAmountChanged -= UpdateEnergyAmountText;
        TransportHealthManager.HealthChanged -= UpdateHealthInfo;
        TransportHealthManager.TransportHealthEnd -= LoadTransportInfo;
        TransportHealthManager.TransportHealthEnd -= ShowNewLevelMessage;
        InternetChecker.InternetConnectionLose -= ShowNoInternetMessage;
    }
}