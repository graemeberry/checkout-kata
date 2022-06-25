namespace Checkout.Models;

public class StockItem
{
    public string Code { get; }
    public decimal UnitPrice { get; }

    public SpecialPrice? SpecialPrice { get; }

    public StockItem(string code, decimal unitPrice)
    {
        this.Code = code;
        this.UnitPrice = unitPrice;
        this.SpecialPrice = null;
    }

    public StockItem(string code, decimal unitPrice, SpecialPrice specialPrice)
    {
        this.Code = code;
        this.UnitPrice = unitPrice;
        this.SpecialPrice = specialPrice;
    }
}