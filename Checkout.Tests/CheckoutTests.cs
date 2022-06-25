using Checkout.Models;
using Checkout.Services;
using NUnit.Framework;

namespace Checkout.Tests;
public class CheckoutTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Should_Return_Zero_When_No_Items_Scanned()
    {
        // Arrange
        CheckoutService checkoutService = new CheckoutService();

        // Act
        decimal totalPrice = checkoutService.GetTotalPrice();

        // Assert
        Assert.AreEqual(0, totalPrice);
    }

    [Test]
    public void Should_Return_Unit_Price_When_Item_Scanned_Once()
    {
        // Arrange
        CheckoutService checkoutService = new CheckoutService();
        StockItem stockItem = new StockItem("A", 50);

        // Act
        checkoutService.ScanItem(stockItem);
        decimal totalPrice = checkoutService.GetTotalPrice();

        // Assert
        Assert.AreEqual(stockItem.UnitPrice, totalPrice);
    }
}