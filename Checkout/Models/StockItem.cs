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
        return this.SpecialPrice == null ? GetStandardPrice(quantity) : GetSpecialPrice(quantity);
    }

    private decimal GetStandardPrice(int quantity)
    {
        return this.UnitPrice * quantity;
    }

    private decimal GetSpecialPrice(int quantity)
    {
        if (this.SpecialPrice == null || this.SpecialPrice.Quantity == 0) { return GetStandardPrice(quantity); }

        decimal price = 0;
        int quantityRemaining = quantity;
        while (quantityRemaining >= this.SpecialPrice.Quantity)
        {
            price += this.SpecialPrice.Price;
            quantityRemaining -= this.SpecialPrice.Quantity;
        }

        return price + GetStandardPrice(quantityRemaining);
    }
}