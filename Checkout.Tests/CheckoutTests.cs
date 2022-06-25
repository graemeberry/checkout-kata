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

    [TestCase("A", 50)]
    [TestCase("B", 30)]
    [TestCase("C", 20)]
    [TestCase("D", 15)]
    public void Should_Return_Unit_Price_When_Item_Scanned_Once(string code, decimal unitPrice)
    {
        // Arrange
        CheckoutService checkoutService = new CheckoutService();
        StockItem stockItem = new StockItem(code, unitPrice);

        // Act
        checkoutService.ScanItem(stockItem);
        decimal totalPrice = checkoutService.GetTotalPrice();

        // Assert
        Assert.AreEqual(stockItem.UnitPrice, totalPrice);
    }
}