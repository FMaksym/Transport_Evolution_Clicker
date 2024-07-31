using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    private int _currentCurrency;
    private readonly string _currencyKey = "Currency";

    public delegate void CurrencyChangedHandler();
    public static event CurrencyChangedHandler CurrencyChanged;

    public int CurrentCurrency
    {
        get { return _currentCurrency; }
        private set { 
            _currentCurrency = Mathf.Clamp(value, 0, int.MaxValue);
            CurrencyChanged?.Invoke();
        }
    }

    private void Awake()
    {
        LoadCurrency();
    }

    public void AddCurrency(int amount)
    {
        if (amount > 0)
        {
            CurrentCurrency += amount;
            SaveCurrency();
        }
    }

    public bool SpendCurrency(int amount)
    {
        if (amount > 0)
        if (CurrentCurrency >= amount)
        {
            CurrentCurrency -= amount;
            SaveCurrency();
            return true;
        }
        return false;
    }

    public int GetCurrentCurrency()
    {
        return CurrentCurrency;
    }

    private void LoadCurrency()
    {
        CurrentCurrency = PlayerPrefs.GetInt(_currencyKey, 0);
    }

    private void SaveCurrency()
    {
        PlayerPrefs.SetInt(_currencyKey, CurrentCurrency);
        PlayerPrefs.Save();
    }
}
