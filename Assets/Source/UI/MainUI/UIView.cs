using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour
{
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void UpadateEnergyText(UIModel uiModel)
    {
        uiModel.EnergyText.text = $"{uiModel.EnergyManager.CurrentEnergy} / {uiModel.UpgradeData.MaxEnergy}";
    }

    public void UpadateHealth(UIModel uiModel)
    {
        uiModel.HealthSlider.value = uiModel.TransportHealthManager.GetCurrentHealth();
        uiModel.CurrentHealthText.text = $"{uiModel.TransportHealthManager.CurrentHealth} / {uiModel.TransportHealthManager.GetCurrentMaxHealth()}";
    }

    public void LoadTransportInfo(UIModel uiModel)
    {
        uiModel.TransportName.text = uiModel.TransportHealthManager.GetCurrentTransportObject().Name;
        uiModel.TransportImage.sprite = uiModel.TransportHealthManager.GetCurrentTransportObject().Image;
        uiModel.HealthSlider.maxValue = uiModel.TransportHealthManager.GetCurrentMaxHealth();

        UpadateHealth(uiModel);
    }

    public void ScrollBackground(UIModel uiModel)
    {
        if (uiModel.BackgroundImage.uvRect.x < 1 && uiModel.BackgroundImage.uvRect.y < 1)
        {
            uiModel.BackgroundImage.uvRect = new Rect(uiModel.BackgroundImage.uvRect.position + 
            new Vector2(uiModel.BackgroundScrollX, uiModel.BackgroundScrollY) * Time.fixedDeltaTime, 
            uiModel.BackgroundImage.uvRect.size);
        }
        else
        {
            uiModel.BackgroundImage.uvRect = new Rect( Vector2.zero, uiModel.BackgroundImage.uvRect.size);
        }
    }
}
