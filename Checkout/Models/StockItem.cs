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

    public decimal GetPrice(int quantity)
    {
        decimal price = 0;

        if (this.SpecialPrice == null) { return this.UnitPrice * quantity; }

        int quantityRemaining = quantity;
        while (quantityRemaining >= this.SpecialPrice.Quantity)
        {
            price += this.SpecialPrice.Price;
            quantityRemaining -= this.SpecialPrice.Quantity;
        }

        return price += quantityRemaining * this.UnitPrice;
    }
}