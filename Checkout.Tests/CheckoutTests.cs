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
}