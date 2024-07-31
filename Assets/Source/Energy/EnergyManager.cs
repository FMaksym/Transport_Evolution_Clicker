using System.Threading.Tasks;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    [SerializeField] private AbsenceTimeCalculator _absenceTimeCalculator;
    [SerializeField] private ImprovementData _improvementData;

    private int _currentEnergy;
    private int energyRecoveryInterval;
    private bool _isRestoringEnergy = true;

    private readonly string _currentEnergyKey = "CurrentEnergyAmount";
    
    public delegate void EnergyChangedHandler();
    public static event EnergyChangedHandler EnergyAmountChanged;

    public int CurrentEnergy
    {
        get { return _currentEnergy; }
        private set
        {
            _currentEnergy = Mathf.Clamp(value, 0, _improvementData.MaxEnergy);
            EnergyAmountChanged?.Invoke();
        }
    }

    private void OnEnable()
    {
        ImprovementData.RechargeSpeedChanged += CalcualteInterval;
    }

    private void Start()
    {
        LoadEnergy();
        CalcualteInterval();
        RestoreEnergyOnReEntry();
        StartEnergyRestoration();
    }

    public void AddEnergyAmount(int amount)
    {
        if (amount > 0)
            CurrentEnergy += amount;

        SaveEnergyAmount();
    }

    public void SpendEnergy(int amount)
    {
        CurrentEnergy -= amount;
    }

    public bool EnergyEnought(int tapEnergyCost)
    {
        return CurrentEnergy - tapEnergyCost >= 0;
    }

    private void LoadEnergy()
    {
        CurrentEnergy = PlayerPrefs.GetInt(_currentEnergyKey, _improvementData.MaxEnergy);
    }

    private void SaveEnergyAmount()
    {
        PlayerPrefs.SetInt(_currentEnergyKey, CurrentEnergy);
        PlayerPrefs.Save();
    }

    private async void StartEnergyRestoration()
    {
        while (_isRestoringEnergy)
        {
            if (CurrentEnergy < _improvementData.MaxEnergy)
            {
                AddEnergyAmount(_improvementData.ClickCost);
            }

            await Task.Delay(energyRecoveryInterval);
        }
    }

    private void RestoreEnergyOnReEntry()
    {
        int absenceTimeInSeconds = _absenceTimeCalculator.GetAbsenceTimeInSeconds();
        int energyToRestore = absenceTimeInSeconds * _improvementData.ClickCost;

        if (CurrentEnergy + energyToRestore > _improvementData.MaxEnergy)
        {
            CurrentEnergy = _improvementData.MaxEnergy;
        }
        else
        {
            CurrentEnergy += energyToRestore;
        }
    }

    private void CalcualteInterval()
    {
        energyRecoveryInterval = (int)(1000 * _improvementData.RechargeSpeed);
    }

    private void OnDestroy()
    {
        _isRestoringEnergy = false;
        ImprovementData.RechargeSpeedChanged -= CalcualteInterval;
    }
}
