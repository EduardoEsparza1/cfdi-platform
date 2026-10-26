using System;
using System.Collections.Generic;
using System.Text;
using CfdiPlatform.Domain.ValueObjects;
using Xunit;

namespace CfdiPlatform.Domain.UnitTests.ValueObjects;

public class ValorUnitarioTests
{
    [Theory]
    [InlineData("0")]
    [InlineData("0.01")]
    [InlineData("100")]
    [InlineData("123.123456")]
    public void Create_WithValidValue_ShouldCreateValorUnitario(string value)
    {
        // Arrange
        decimal precio = decimal.Parse(value, System.Globalization.CultureInfo.InvariantCulture);

        // Act
        var result = ValorUnitario.Create(precio);

        // Assert
        Assert.Equal(precio, result.Value);
    }

    [Theory]
    [InlineData("-1")]
    [InlineData("-100.50")]
    public void Create_WithNegativeValue_ShouldThrowArgumentOutOfRangeException(string value)
    {
        // Arrange
        decimal precio = decimal.Parse(value, System.Globalization.CultureInfo.InvariantCulture);

        // Act
        Action action = () => ValorUnitario.Create(precio);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void Create_WithSameValue_ShouldBeEqual()
    {
        // Arrange
        var precio1 = ValorUnitario.Create(100.50m);
        var precio2 = ValorUnitario.Create(100.50m);

        // Assert
        Assert.Equal(precio1, precio2);
    }

    [Fact]
    public void Create_WithHighPrecision_ShouldPreserveDecimalValue()
    {
        // Arrange
        decimal precio = 123.123456m;

        // Act
        var result = ValorUnitario.Create(precio);

        // Assert
        Assert.Equal(precio, result.Value);
    }
}
