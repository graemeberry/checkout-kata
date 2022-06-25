namespace Checkout.Models;

public class SpecialPrice
{
    public int Quantity { get; }
    public decimal Price { get; }

    public SpecialPrice(int quantity, decimal price)
    {
        this.Quantity = quantity;
        this.Price = price;
    }
}