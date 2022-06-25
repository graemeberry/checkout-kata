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

    [TestCase("A", 50, 4, 3, 130, 180)]
    [TestCase("A", 50, 7, 3, 130, 310)]
    [TestCase("B", 30, 4, 2, 45, 90)]
    [TestCase("B", 30, 7, 2, 45, 165)]
    public void Should_Return_Special_Price_When_Special_Price_Is_Applied_To_A_Scanned_Item(string code, decimal unitPrice, int quantity, int specialPriceQuantity, decimal specialPricePrice, decimal expected)
    {
        // Arrange
        SpecialPrice specialPrice = new SpecialPrice(specialPriceQuantity, specialPricePrice);
        StockItem stockItem = new StockItem(code, unitPrice, specialPrice);

        // Act
        for (int i = 0; i < quantity; i++)
        {
            _checkoutService.ScanItem(stockItem);
        }
        decimal actual = _checkoutService.GetTotalPrice();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestCase("A", 50, 2, 100)]
    [TestCase("B", 30, 1, 30)]
    [TestCase("C", 20, 4, 80)]
    [TestCase("D", 15, 7, 105)]
    public void Should_Return_Standard_Price_When_Special_Price_Is_Added_With_No_Quantity(string code, decimal unitPrice, int quantity, decimal expected)
    {
        // Arrange
        SpecialPrice specialPrice = new SpecialPrice(0, 0);
        StockItem stockItem = new StockItem(code, unitPrice, specialPrice);

        // Act
        for (var i = 0; i < quantity; i++)
        {
            _checkoutService.ScanItem(stockItem);
        }
        decimal actual = _checkoutService.GetTotalPrice();

        // Assert
        Assert.AreEqual(expected, actual);
    }
}