using Checkout.Models;
using Checkout.Services;
using NUnit.Framework;

namespace Checkout.Tests;
public class CheckoutTests
{
    private CheckoutService _checkoutService = new CheckoutService();

    [SetUp]
    public void Setup()
    {
        _checkoutService = new CheckoutService();
    }

    [Test]
    public void Should_Return_Zero_When_No_Items_Scanned()
    {
        // Arrange
        decimal expected = 0;

        // Act
        decimal actual = _checkoutService.GetTotalPrice();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestCase("A", 50)]
    [TestCase("B", 30)]
    [TestCase("C", 20)]
    [TestCase("D", 15)]
    public void Should_Return_Unit_Price_When_Item_Scanned_Once(string code, decimal unitPrice)
    {
        // Arrange
        StockItem stockItem = new StockItem(code, unitPrice);
        decimal expected = stockItem.UnitPrice;

        // Act
        _checkoutService.ScanItem(stockItem);
        decimal actual = _checkoutService.GetTotalPrice();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestCase("A", 50, 1)]
    [TestCase("B", 30, 2)]
    [TestCase("C", 20, 3)]
    [TestCase("D", 15, 4)]
    public void Should_Return_Unit_Price_Multiplied_By_Quantity_When_Scanned_Items_Scanned(string code, decimal unitPrice, int quantity)
    {
        // Arrange
        StockItem stockItem = new StockItem(code, unitPrice);
        decimal expected = unitPrice * quantity;

        // Act
        for (int i = 0; i < quantity; i++)
        {
            _checkoutService.ScanItem(stockItem);
        }
        decimal actual = _checkoutService.GetTotalPrice();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void Should_Return_Special_Price_When_Special_Price_Is_Applied_To_A_Scanned_Item()
    {
        // Arrange
        SpecialPrice specialPrice = new SpecialPrice(3, 130);
        StockItem stockItem = new StockItem("A", 50, specialPrice);
        decimal expected = 130;

        // Act
        _checkoutService.ScanItem(stockItem);
        _checkoutService.ScanItem(stockItem);
        _checkoutService.ScanItem(stockItem);
        decimal actual = _checkoutService.GetTotalPrice();

        // Assert
        Assert.AreEqual(expected, actual);
    }
}