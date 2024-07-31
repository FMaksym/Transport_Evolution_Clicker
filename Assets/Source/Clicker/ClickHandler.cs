using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private ImprovementData _improvementData;
    [SerializeField] private TapEffectPool _tapEffectPool;
    [SerializeField] private CurrencyManager _currencyManager;
    [SerializeField] private EnergyManager _energyManager;
    [SerializeField] private TransportHealthManager _transportHealthManager;

    private Vector2 _mousePosition;

    public void OnTransportClick()
    {
        if (_energyManager.EnergyEnought(_improvementData.ClickCost))
        {
            _mousePosition = Input.mousePosition;
            ShowTapEffect(_mousePosition);
            ChangeParametres();
        }
    }

    private void ShowTapEffect(Vector2 tapPosition)
    {
        _tapEffectPool.GetVisualEffect(tapPosition);
    }

    private void ChangeParametres()
    {
        _currencyManager.AddCurrency(_improvementData.ClickCost);
        _energyManager.SpendEnergy(_improvementData.ClickCost);
        _transportHealthManager.HealthReduction(_improvementData.ClickCost);
    }
}
