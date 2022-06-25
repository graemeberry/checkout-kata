using Checkout.Models;

namespace Checkout.Services;

public class CheckoutService
{
    private readonly List<StockItem> _stockItems = new List<StockItem>();

    public decimal GetTotalPrice()
    {
        return GetDistinctStockItems().Sum(stockItem => stockItem.GetPrice(GetTotalQuantityScannedForStockCode(stockItem.Code)));
    }

    private IEnumerable<StockItem> GetDistinctStockItems()
    {
        return _stockItems.Distinct().ToList();
    }

    private int GetTotalQuantityScannedForStockCode(string code)
    {
        return _stockItems.Count(stockItem => stockItem.Code == code);
    }

    public void ScanItem(StockItem stockItem)
    {
        _stockItems.Add(stockItem);
    }
}