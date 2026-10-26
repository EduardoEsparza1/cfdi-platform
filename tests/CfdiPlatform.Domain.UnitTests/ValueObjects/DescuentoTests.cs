using System;
using System.Collections.Generic;
using System.Text;
using CfdiPlatform.Domain.ValueObjects;
using Xunit;

namespace CfdiPlatform.Domain.UnitTests.ValueObjects;

public class DescuentoTests
{
    [Theory]
    [InlineData("0", "100")]
    [InlineData("50", "100")]
    [InlineData("100", "100")]
    [InlineData("12.123456", "100")]
    public void Create_WithValidDiscount_ShouldCreateDescuento(string discount, string amount)
    {
        // Arrange
        decimal descuento = decimal.Parse(discount, System.Globalization.CultureInfo.InvariantCulture);
        decimal importeValue = decimal.Parse(amount, System.Globalization.CultureInfo.InvariantCulture);
        var importe = Importe.Calculate(Cantidad.Create(1m), ValorUnitario.Create(importeValue));

        // Act
        var result = Descuento.Create(descuento, importe);

        // Assert
        Assert.Equal(descuento, result.Value);
    }

    [Theory]
    [InlineData("-1", "100")]
    [InlineData("-0.01", "100")]
    public void Create_WithNegativeDiscount_ShouldThrowArgumentOutOfRangeException(string discount, string amount)
    {
        // Arrange
        decimal descuento = decimal.Parse(discount, System.Globalization.CultureInfo.InvariantCulture);
        decimal importeValue = decimal.Parse(amount, System.Globalization.CultureInfo.InvariantCulture);
        var importe = Importe.Calculate(Cantidad.Create(1m), ValorUnitario.Create(importeValue));

        // Act
        Action action = () => Descuento.Create(descuento, importe);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void Create_WithDiscountGreaterThanAmount_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var importe = Importe.Calculate(Cantidad.Create(1m), ValorUnitario.Create(100m));

        // Act
        Action action = () => Descuento.Create(101m, importe);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void Create_WithNullImporte_ShouldThrowArgumentNullException()
    {
        // Act
        Action action = () => Descuento.Create(10m, null!);

        // Assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Fact]
    public void Create_WithSameValue_ShouldBeEqual()
    {
        // Arrange
        var importe = Importe.Calculate(Cantidad.Create(1m), ValorUnitario.Create(100m));

        // Act
        var descuento1 = Descuento.Create(20m, importe);
        var descuento2 = Descuento.Create(20m, importe);

        // Assert
        Assert.Equal(descuento1, descuento2);
    }
}
