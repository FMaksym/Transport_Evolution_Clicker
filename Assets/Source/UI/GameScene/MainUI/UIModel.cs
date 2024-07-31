using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIModel : MonoBehaviour
{
    [Header("Background Scroll Parametres")]
    public float BackgroundScrollX;
    public float BackgroundScrollY;

    [Space(2), Header("Managers")]
    public EnergyManager EnergyManager;
    public ImprovementData UpgradeData;
    public TransportHealthManager TransportHealthManager;
    public InternetChecker InternetChecker;
    
    [Space(2), Header("UI Elements")]
    public RawImage BackgroundImage;
    public Image TransportImage;
    public Image NewTransportImage;
    public TMP_Text EnergyText;
    public TMP_Text CurrentHealthText;
    public TMP_Text TransportName;
    public TMP_Text NewTransportName;
    public TMP_Text RewardForVideoText;
    public Slider HealthSlider;

    [Space(2), Header("Game Message Panels")]
    public GameObject NoInternetMessagePanel;
    public GameObject NewLevelMessagePanel;
    
    [Space(2), Header("Panels")]
    public GameObject UpdatePanel;
    public GameObject RoadLogPanel;
    public GameObject ShopPanel;
    public GameObject InstructionPanel;
    public GameObject SettingsPanel;
    public GameObject DailyRewardPanel;
    public GameObject RewardedVideoPanel;
}
