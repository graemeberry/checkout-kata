using Checkout.Models;

namespace Checkout.Services;

public class CheckoutService
{
    private readonly List<StockItem> _stockItems = new List<StockItem>();

    public decimal GetTotalPrice()
    {
        return (from stockItem in _stockItems.Distinct() let quantity = _stockItems.Count(s => s.Code == stockItem.Code) select stockItem.GetPrice(quantity)).Sum();
    }

    public void ScanItem(StockItem stockItem)
    {
        _stockItems.Add(stockItem);
    }
}