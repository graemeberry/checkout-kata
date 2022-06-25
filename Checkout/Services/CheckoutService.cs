using Checkout.Models;

namespace Checkout.Services;

public class CheckoutService
{
    private readonly List<StockItem> _stockItems = new List<StockItem>();

    public decimal GetTotalPrice()
    {
        decimal totalPrice = 0;

        foreach (var stockItem in _stockItems.Distinct())
        {
            int quantity = _stockItems.Count(s => s.Code == stockItem.Code);
            totalPrice += stockItem.GetPrice(quantity);
        }

        return totalPrice;
    }

    public void ScanItem(StockItem stockItem)
    {
        _stockItems.Add(stockItem);
    }
}