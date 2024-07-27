using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    [SerializeField] private int _defaultMaxEnergyAmount = 2000;
    [SerializeField] private AbsenceTimeCalculator _absenceTimeCalculator;
    [SerializeField] private ImprovementData _improvementData;

    private int _currentEnergy;
    private int _maxEnergy;
    private int energyRecoveryInterval;
    private bool _isRestoringEnergy = true;

    private readonly string _currentEnergyKey = "CurrentEnergyAmount";
    private readonly string _maxEnergyKey = "MaxEnergyAmount";
    
    public delegate void EnergyChangedHandler();
    public static event EnergyChangedHandler EnergyAmountChanged;

    public int MaxEnergy
    {
        get { return _maxEnergy; }
        private set
        {
            _maxEnergy = value;
        }
    }

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

    public void AddMaxEnergyAmount(int amount)
    {
        if (amount > 0)
            MaxEnergy += amount;

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

    public int GetMaxEnergyAmount()
    {
        return MaxEnergy;
    }

    private void LoadEnergy()
    {
        //MaxEnergy = PlayerPrefs.GetInt(_maxEnergyKey, _upgradeData.DefaultMaxEnergy);
        //MaxEnergy = PlayerPrefs.GetInt(_maxEnergyKey, _defaultMaxEnergyAmount);
        //MaxEnergy = PlayerPrefs.GetInt(_maxEnergyKey, _defaultMaxEnergyAmount);
        CurrentEnergy = PlayerPrefs.GetInt(_currentEnergyKey, _improvementData.MaxEnergy);
    }

    private void SaveEnergyAmount()
    {
        //PlayerPrefs.SetInt(_maxEnergyKey, MaxEnergy);
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
                //AddEnergyAmount(_energyRestoreRate);
            }

            //timeForWait = (int)(1000 * _upgradeData.RechargeSpeed);//Вынести расчет времени в отдельный метод и вызывать при старте
            //Debug.Log($"1000 * {_upgradeData.RechargeSpeed} = {timeForWait}");//и при вызове события улучшения скорости восстановления
            await Task.Delay(energyRecoveryInterval);
        }
    }

    private void RestoreEnergyOnReEntry()
    {
        int absenceTimeInSeconds = _absenceTimeCalculator.GetAbsenceTimeInSeconds();
        int energyToRestore = absenceTimeInSeconds * _improvementData.ClickCost;
        //int energyToRestore = absenceTimeInSeconds * _energyRestoreRate;

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
