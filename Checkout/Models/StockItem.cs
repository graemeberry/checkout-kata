namespace Checkout.Models;

public class StockItem
{
    public string Code { get; }
    public decimal UnitPrice { get; }

    public StockItem(string code, decimal unitPrice)
    {
        this.Code = code;
        this.UnitPrice = unitPrice;
    }
}