using UnityEngine;

public class CurrencyView : MonoBehaviour
{
    public void AddCurrency(CurrencyModel currencyModel, int amount)
    {
        currencyModel.currencyManager.AddCurrency(amount);
    }

    public void SpendCurrency(CurrencyModel currencyModel, int amount)
    {
        currencyModel.currencyManager.SpendCurrency(amount);
    }

    public void UpdateCurrencyText(CurrencyModel currencyModel)
    {
        currencyModel.currencyText.text = currencyModel.currencyManager.GetCurrentCurrency().ToString();
    }
}
