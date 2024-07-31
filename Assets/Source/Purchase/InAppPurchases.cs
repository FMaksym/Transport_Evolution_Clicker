using UnityEngine;
using UnityEngine.Purchasing;

public class InAppPurchases : MonoBehaviour
{
    [SerializeField] private CurrencyManager _currencyManager;

    public void OnPurchaseCompleted(Product product)
    {
        BuySqrews(product);
    }

    private void BuySqrews(Product product)
    {
        _currencyManager.AddCurrency((int)product.definition.payout.quantity);
    }
}
