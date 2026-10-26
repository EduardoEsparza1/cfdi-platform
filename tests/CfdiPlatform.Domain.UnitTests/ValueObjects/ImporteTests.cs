using System;
using System.Collections.Generic;
using System.Text;
using CfdiPlatform.Domain.ValueObjects;
using Xunit;

namespace CfdiPlatform.Domain.UnitTests.ValueObjects;

public class ImporteTests
{
    [Theory]
    [InlineData("2", "150.00", "300.00")]
    [InlineData("3", "250.50", "751.50")]
    [InlineData("1.5", "100.00", "150.00")]
    [InlineData("2", "0", "0")]
    [InlineData("0.125", "80.00", "10.00")]
    public void Calculate_WithValidValues_ShouldReturnCorrectAmount(string quantity, string unitPrice, string expectedAmount)
    {
        // Arrange
        var cantidad = Cantidad.Create(decimal.Parse(quantity, System.Globalization.CultureInfo.InvariantCulture));
        var valorUnitario = ValorUnitario.Create(decimal.Parse(unitPrice, System.Globalization.CultureInfo.InvariantCulture));
        decimal expected = decimal.Parse(expectedAmount, System.Globalization.CultureInfo.InvariantCulture);

        // Act
        var result = Importe.Calculate(cantidad, valorUnitario);

        // Assert
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void Calculate_WithNullCantidad_ShouldThrowArgumentNullException()
    {
        // Arrange
        var valorUnitario = ValorUnitario.Create(100m);

        // Act
        Action action = () => Importe.Calculate(null!, valorUnitario);

        // Assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Fact]
    public void Calculate_WithNullValorUnitario_ShouldThrowArgumentNullException()
    {
        // Arrange
        var cantidad = Cantidad.Create(2m);

        // Act
        Action action = () => Importe.Calculate(cantidad, null!);

        // Assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Fact]
    public void Calculate_WithSameResult_ShouldBeEqual()
    {
        // Arrange
        var cantidad1 = Cantidad.Create(2m);
        var precio1 = ValorUnitario.Create(150m);

        var cantidad2 = Cantidad.Create(3m);
        var precio2 = ValorUnitario.Create(100m);

        // Act
        var importe1 = Importe.Calculate(cantidad1, precio1);
        var importe2 = Importe.Calculate(cantidad2, precio2);

        // Assert
        Assert.Equal(importe1, importe2);
    }

    [Fact]
    public void Calculate_WithDecimalPrecision_ShouldPreserveResult()
    {
        // Arrange
        var cantidad = Cantidad.Create(1.25m);
        var valorUnitario = ValorUnitario.Create(10.123456m);

        // Act
        var result = Importe.Calculate(cantidad, valorUnitario);

        // Assert
        Assert.Equal(12.65432000m, result.Value);
    }

    [Fact]
    public void Calculate_WithOverflow_ShouldThrowOverflowException()
    {
        // Arrange
        var cantidad = Cantidad.Create(decimal.MaxValue);
        var valorUnitario = ValorUnitario.Create(2m);

        // Act
        Action action = () => Importe.Calculate(cantidad, valorUnitario);

        // Assert
        Assert.Throws<OverflowException>(action);
    }
}