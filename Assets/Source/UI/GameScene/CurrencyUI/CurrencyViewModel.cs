using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyViewModel : MonoBehaviour
{
    [SerializeField] private CurrencyModel _currencyModel;
    [SerializeField] private CurrencyView _currencyView;

    private void OnEnable()
    {
        CurrencyManager.CurrencyChanged += UpdateCurrencyText;
    }

    private void Awake()
    {
        UpdateCurrencyText();
    }

    private void UpdateCurrencyText()
    {
        _currencyView.UpdateCurrencyText(_currencyModel);
    }

    private void OnDisable()
    {
        CurrencyManager.CurrencyChanged -= UpdateCurrencyText;
    }
}
