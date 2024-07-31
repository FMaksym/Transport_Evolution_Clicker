using System.Collections.Generic;
using UnityEngine;

public class TransportHealthManager : MonoBehaviour
{
    [SerializeField] private List<TransportObjectData> _transportList;
    [SerializeField] private TransportObjectData _currentTransport;

    private int _currentHealth;
    private int _currentMaxHealth;
    private int _currentTransportIndex;
    private readonly string _currentHealthKey = "CurrentHealth";
    private readonly string _currentMaxHealthKey = "CurrentMaxHealth";
    private readonly string _currentTransportIndexKey = "CurrentTransportIndex";

    public delegate void HealthChangedHandler();
    public delegate void TransportHealthEndHandler();
    public static event HealthChangedHandler HealthChanged;
    public static event TransportHealthEndHandler TransportHealthEnd;

    public int CurrentHealth 
    {
        get { return _currentHealth; }
        private set
        {
            _currentHealth = Mathf.Clamp(value, 0, int.MaxValue);
            HealthChanged?.Invoke();

            if (_currentHealth <= 0)
            {
                ChangeTransportToNext();
            }
        }
    }

    private void Awake()
    {
        Initialize();
    }

    public void HealthReduction(int amount)
    {
        if(amount > 0)
        {
            CurrentHealth -= amount;
            SaveCurrentHealth();
        }
    }

    public int GetCurrentHealth()
    {
        return CurrentHealth;
    }

    public int GetCurrentMaxHealth()
    {
        return _currentMaxHealth;
    }

    public TransportObjectData GetCurrentTransportObject()
    {
        return _currentTransport;
    }

    private void Initialize() 
    {
        _currentTransportIndex = PlayerPrefs.GetInt(_currentTransportIndexKey, 0);
        _currentTransport = _transportList[_currentTransportIndex];
        _currentMaxHealth = PlayerPrefs.GetInt(_currentMaxHealthKey, _currentTransport.MaxHealth);
        CurrentHealth = PlayerPrefs.GetInt(_currentHealthKey, _currentMaxHealth);
    }

    private void ChangeTransportToNext()
    {
        _currentTransportIndex = (_currentTransportIndex + 1) % _transportList.Count;
        _currentTransport = _transportList[_currentTransportIndex];
        _currentMaxHealth = _currentTransport.MaxHealth;
        CurrentHealth = _currentMaxHealth;

        TransportHealthEnd?.Invoke();

        SaveCurrentTransport();
        SaveCurrentMaxHealth();
    }

    private void SaveCurrentHealth()
    {
        PlayerPrefs.SetInt(_currentHealthKey, CurrentHealth);
        PlayerPrefs.Save();
    }

    private void SaveCurrentMaxHealth()
    {
        PlayerPrefs.SetInt(_currentMaxHealthKey, _currentMaxHealth);
        PlayerPrefs.Save();
    }

    private void SaveCurrentTransport()
    {
        PlayerPrefs.SetInt(_currentTransportIndexKey, _currentTransportIndex);
        PlayerPrefs.Save();
    }
}
