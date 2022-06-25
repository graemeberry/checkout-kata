using Checkout.Models;

namespace Checkout.Services.Interfaces;
public interface ICheckoutService
{
    public void ScanItem(StockItem stockItem);

    public decimal GetTotalPrice();

}