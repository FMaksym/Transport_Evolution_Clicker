using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ClickerBot : MonoBehaviour
{
    [SerializeField] private CurrencyManager _currencyManager;
    [SerializeField] private TransportHealthManager _healthManager;
    [SerializeField] private AbsenceTimeCalculator _timeCalculator;
    [SerializeField] private ImprovementData _improvementData;

    private int _inactiveMaxTime = 28800;
    [SerializeField] private int _clickCost = 1;
    [SerializeField] private int _tapSpeed = 1;

    private void Start()
    {
        InactiveClick();
        ActiveClick();
    }

    private async void ActiveClick()
    {
        while (true)
        {
            BotClick(_improvementData.ClickCost);
            await Task.Delay(1000 * _tapSpeed);
        }
    }

    private void InactiveClick()
    {
        //int inactiveTime = _timeCalculator.GetAbsenceTimeInSeconds();
        int totalEarned = _timeCalculator.GetAbsenceTimeInSeconds() <= _inactiveMaxTime
            ? _timeCalculator.GetAbsenceTimeInSeconds() * _improvementData.ClickCost
            : _inactiveMaxTime * _improvementData.ClickCost;

        if (_timeCalculator.GetAbsenceTimeInSeconds() <= _inactiveMaxTime)
        {
            BotClick(_timeCalculator.GetAbsenceTimeInSeconds() * _improvementData.ClickCost);
        }
        else
        {
            BotClick(_inactiveMaxTime * _improvementData.ClickCost);
        }
    }

    private void BotClick(int clickCoast)
    {
        _currencyManager.AddCurrency(clickCoast);
        ProcessClickDamage(clickCoast);
        _healthManager.HealthReduction(clickCoast);
    }

    private void ProcessClickDamage(int totalDamage)
    {
        while (totalDamage > 0)
        {
            int currentHealth = _healthManager.GetCurrentHealth();
            int damageToDeal = Mathf.Min(totalDamage, currentHealth);
            _healthManager.HealthReduction(damageToDeal);
            totalDamage -= damageToDeal;
        }
    }
}
