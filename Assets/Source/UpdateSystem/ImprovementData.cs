using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImprovementData : MonoBehaviour
{
    [Header ("Default Parameteres Values")]
    [SerializeField] private int _defaultClickReward = 1;
    [SerializeField] private int _defaultMaxEnergy = 2000;
    [SerializeField] private float _defaultRechargeSpeed = 3.0f;
    [Header("Maximum Parameter Values")]
    [SerializeField] private int _clickRewardMaxLevel = 20;
    [SerializeField] private int _maxEnergyMaxLevel = 15;
    [SerializeField] private int _rechargeSpeedMaxLevel = 10;
    [Header("Parameter Prices")]
    [SerializeField] private List<int> _clickRewardPrices;
    [SerializeField] private List<int> _maxEnergyPrices;
    [SerializeField] private List<int> _rechargeSpeedPrices;
    [SerializeField] private int _tapBotPrice = 250000;
    [SerializeField] private int _secondsForVideoRewardMultiplier = 1800;

    private int _clickRewardUpgradeStep = 1;
    private int _maxEnergyUpgradeStep = 500;
    private float _rechargeSpeedUpgradeStep = 0.25f;
    private bool _tapBotPurchased = false;
    private int _clickCost;
    private int _maxEnergy;
    private float _rechargeSpeed;
    private int _clickRewardCurrentLevel;
    private int _maxEnergyCurrentLevel;
    private int _rechargeSpeedCurrentLevel;

    private readonly string _clickRewardKey = "ClickReward";
    private readonly string _maxEnergyKey = "MaxEnergy";
    private readonly string _rechargeSpeedKey = "RechargeSpeed";
    private readonly string _tapBotPurchasedKey = "TapBotPurchased";
    private readonly string _clickRewardCurrentLevelKey = "ClickRewardCurrentLevel";
    private readonly string _maxEnergyCurrentLevelKey = "MaxEnergyCurrentLevel";
    private readonly string _rechargeSpeedCurrentLevelKey = "RechargeSpeedCurrentLevel";

    public int ClickCost { get => _clickCost; set { _clickCost = value; ClickCostChanged?.Invoke(); } }
    public int MaxEnergy { get => _maxEnergy; set { _maxEnergy = value; MaxEnergyAmountChanged?.Invoke(); } }
    public float RechargeSpeed { get => _rechargeSpeed; set { _rechargeSpeed = value; RechargeSpeedChanged?.Invoke(); } }
    public bool IsTapBotPurchased { get => _tapBotPurchased; set { _tapBotPurchased = value; ClickBotPurchased?.Invoke(); } }
    public int DefaultClickReward { get => _defaultClickReward; set => _defaultClickReward = value; }
    public int DefaultMaxEnergy { get => _defaultMaxEnergy; set => _defaultMaxEnergy = value; }
    public float DefaultRechargeSpeed { get => _defaultRechargeSpeed; set => _defaultRechargeSpeed = value; }
    public int ClickRewardCurrentLevel { get => _clickRewardCurrentLevel; set => _clickRewardCurrentLevel = value; }
    public int MaxEnergyCurrentLevel { get => _maxEnergyCurrentLevel; set => _maxEnergyCurrentLevel = value; }
    public int RechargeSpeedCurrentLevel { get => _rechargeSpeedCurrentLevel; set => _rechargeSpeedCurrentLevel = value; }
    public int ClickRewardMaxLevel { get => _clickRewardMaxLevel; set => _clickRewardMaxLevel = value; }
    public int MaxEnergyMaxLevel { get => _maxEnergyMaxLevel; set => _maxEnergyMaxLevel = value; }
    public int RechargeSpeedMaxLevel { get => _rechargeSpeedMaxLevel; set => _rechargeSpeedMaxLevel = value; }
    public int ClickRewardUpgradeStep { get => _clickRewardUpgradeStep; set => _clickRewardUpgradeStep = value; }
    public int MaxEnergyUpgradeStep { get => _maxEnergyUpgradeStep; set => _maxEnergyUpgradeStep = value; }
    public float RechargeSpeedUpgradeStep { get => _rechargeSpeedUpgradeStep; set => _rechargeSpeedUpgradeStep = value; }
    public List<int> ClickRewardPrices { get => _clickRewardPrices; set => _clickRewardPrices = value; }
    public List<int> MaxEnergyPrices { get => _maxEnergyPrices; set => _maxEnergyPrices = value; }
    public List<int> RechargeSpeedPrices { get => _rechargeSpeedPrices; set => _rechargeSpeedPrices = value; }
    public int TapBotPrice { get => _tapBotPrice; set => _tapBotPrice = value; }
    public int VideoRewardMultiplier { get => _secondsForVideoRewardMultiplier; private set { } }

    public delegate void ClickCostChangedHandler();
    public delegate void MaxEnergyChangedHandler();
    public delegate void RechargeSpeedChangedHandler();
    public delegate void ClickBotPurchaseHandler();
    public static event ClickCostChangedHandler ClickCostChanged;
    public static event MaxEnergyChangedHandler MaxEnergyAmountChanged;
    public static event RechargeSpeedChangedHandler RechargeSpeedChanged;
    public static event ClickBotPurchaseHandler ClickBotPurchased;

    private void Awake()
    {
        LoadData();
    }

    public void LoadData()
    {
        ClickCost = PlayerPrefs.GetInt(_clickRewardKey, DefaultClickReward);
        MaxEnergy = PlayerPrefs.GetInt(_maxEnergyKey, DefaultMaxEnergy);
        RechargeSpeed = PlayerPrefs.GetFloat(_rechargeSpeedKey, DefaultRechargeSpeed);
        IsTapBotPurchased = PlayerPrefs.GetInt(_tapBotPurchasedKey, 0) == 1;
        ClickRewardCurrentLevel = PlayerPrefs.GetInt(_clickRewardCurrentLevelKey, 1); // Default level is 1
        MaxEnergyCurrentLevel = PlayerPrefs.GetInt(_maxEnergyCurrentLevelKey, 1); // Default level is 1
        RechargeSpeedCurrentLevel = PlayerPrefs.GetInt(_rechargeSpeedCurrentLevelKey, 1); // Default level is 1
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt(_clickRewardKey, ClickCost);
        PlayerPrefs.SetInt(_maxEnergyKey, MaxEnergy);
        PlayerPrefs.SetFloat(_rechargeSpeedKey, RechargeSpeed);
        PlayerPrefs.SetInt(_tapBotPurchasedKey, IsTapBotPurchased ? 1 : 0);
        PlayerPrefs.SetInt(_clickRewardCurrentLevelKey, ClickRewardCurrentLevel);
        PlayerPrefs.SetInt(_maxEnergyCurrentLevelKey, MaxEnergyCurrentLevel);
        PlayerPrefs.SetFloat(_rechargeSpeedCurrentLevelKey, RechargeSpeedCurrentLevel);
        PlayerPrefs.Save();
    }
}
