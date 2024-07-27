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

    public RawImage BackgroundImage;
    public Image TransportImage;
    public TMP_Text EnergyText;
    public TMP_Text CurrentHealthText;
    public TMP_Text TransportName;
    public Slider HealthSlider;

    public GameObject UpdatePanel;
    public GameObject RoadLogPanel;
    public GameObject ShopPanel;
    public GameObject InstructionPanel;
    public GameObject SettingsPanel;
    public GameObject DailyRewardPanel;
}
