using System;
using System.Collections.Generic;
using System.Text;
using CfdiPlatform.Domain.ValueObjects;
using Xunit;

namespace CfdiPlatform.Domain.UnitTests.ValueObjects;

public class CantidadTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(10.5)]
    [InlineData(0.001)]
    [InlineData(100.123456)]
    public void Create_WithPositiveValue_ShouldCreateCantidad(
        double value)
    {
        // Arrange
        decimal cantidad = (decimal)value;

        // Act
        var result = Cantidad.Create(cantidad);

        // Assert
        Assert.Equal(cantidad, result.Value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10.5)]
    public void Create_WithZeroOrNegativeValue_ShouldThrowArgumentOutOfRangeException(double value)
    {
        // Arrange
        decimal cantidad = (decimal)value;

        // Act
        Action action = () => Cantidad.Create(cantidad);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void Create_WithSameValue_ShouldBeEqual()
    {
        // Arrange
        var cantidad1 = Cantidad.Create(10.5m);
        var cantidad2 = Cantidad.Create(10.5m);

        // Assert
        Assert.Equal(cantidad1, cantidad2);
    }

    [Fact]
    public void Create_WithHighPrecision_ShouldPreserveDecimalValue()
    {
        // Arrange
        decimal value = 123.123456m;

        // Act
        var cantidad = Cantidad.Create(value);

        // Assert
        Assert.Equal(value, cantidad.Value);
    }
}