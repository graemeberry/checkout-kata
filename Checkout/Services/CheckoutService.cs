using Checkout.Models;

namespace Checkout.Services;

public class CheckoutService
{
    private readonly List<StockItem> _stockItems = new List<StockItem>();

    public decimal GetTotalPrice()
    {
        return _stockItems.Sum(stockItem => stockItem.UnitPrice);
    }

    public void ScanItem(StockItem stockItem)
    {
        _stockItems.Add(stockItem);
    }
}