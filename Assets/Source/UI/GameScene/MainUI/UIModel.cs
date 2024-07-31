using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIModel : MonoBehaviour
{
    public float BackgroundScrollX;
    public float BackgroundScrollY;

    public EnergyManager EnergyManager;
    public ImprovementData UpgradeData;
    public TransportHealthManager TransportHealthManager;
    public InternetChecker InternetChecker;

    public RawImage BackgroundImage;
    public Image TransportImage;
    public Image NewTransportImage;
    public TMP_Text EnergyText;
    public TMP_Text CurrentHealthText;
    public TMP_Text TransportName;
    public TMP_Text NewTransportName;
    public TMP_Text RewardForVideoText;
    public Slider HealthSlider;

    public GameObject NoInternetMessagePanel;
    public GameObject NewLevelMessagePanel;

    public GameObject UpdatePanel;
    public GameObject RoadLogPanel;
    public GameObject ShopPanel;
    public GameObject InstructionPanel;
    public GameObject SettingsPanel;
    public GameObject DailyRewardPanel;
    public GameObject RewardedVideoPanel;
}
