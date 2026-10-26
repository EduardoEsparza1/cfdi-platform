using System;
using System.Collections.Generic;
using System.Text;
using CfdiPlatform.Domain.Entities;
using CfdiPlatform.Domain.ValueObjects;
using Xunit;

namespace CfdiPlatform.Domain.UnitTests.Entities;

public class ConceptoTests
{
    [Fact]
    public void Create_WithValidData_ShouldCreateConcepto()
    {
        // Arrange
        var claveProdServ = ClaveProdServ.Create("43211503");
        var cantidad = Cantidad.Create(2m);
        var claveUnidad = ClaveUnidad.Create("H87");
        var valorUnitario = ValorUnitario.Create(12500m);
        var objetoImp = ObjetoImp.Create("02");

        // Act
        var concepto = Concepto.Create(claveProdServ, cantidad, claveUnidad, "Computadora portátil", valorUnitario, objetoImp);

        // Assert
        Assert.NotEqual(Guid.Empty, concepto.Id);
        Assert.Equal(claveProdServ, concepto.ClaveProdServ);
        Assert.Equal(cantidad, concepto.Cantidad);
        Assert.Equal(claveUnidad, concepto.ClaveUnidad);
        Assert.Equal("Computadora portátil", concepto.Descripcion);
        Assert.Equal(valorUnitario, concepto.ValorUnitario);
        Assert.Equal(objetoImp, concepto.ObjetoImp);
        Assert.Null(concepto.Descuento);
    }

    [Fact]
    public void Create_WithValidData_ShouldCalculateImporte()
    {
        // Arrange
        var claveProdServ = ClaveProdServ.Create("43211503");
        var cantidad = Cantidad.Create(2m);
        var claveUnidad = ClaveUnidad.Create("H87");
        var valorUnitario = ValorUnitario.Create(12500m);
        var objetoImp = ObjetoImp.Create("02");

        // Act
        var concepto = Concepto.Create(claveProdServ, cantidad, claveUnidad, "Computadora portátil", valorUnitario, objetoImp);

        // Assert
        Assert.Equal(25000m, concepto.Importe.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithEmptyDescription_ShouldThrowArgumentException(string descripcion)
    {
        // Arrange
        var claveProdServ = ClaveProdServ.Create("43211503");
        var cantidad = Cantidad.Create(2m);
        var claveUnidad = ClaveUnidad.Create("H87");
        var valorUnitario = ValorUnitario.Create(12500m);
        var objetoImp = ObjetoImp.Create("02");

        // Act
        Action action = () => Concepto.Create(claveProdServ, cantidad, claveUnidad, descripcion, valorUnitario, objetoImp);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Create_WithValidDiscount_ShouldAssignDiscount()
    {
        // Arrange
        var claveProdServ = ClaveProdServ.Create("43211503");
        var cantidad = Cantidad.Create(2m);
        var claveUnidad = ClaveUnidad.Create("H87");
        var valorUnitario = ValorUnitario.Create(12500m);
        var objetoImp = ObjetoImp.Create("02");

        // Act
        var concepto = Concepto.Create(claveProdServ, cantidad, claveUnidad, "Computadora portátil", valorUnitario, objetoImp, descuento: 1000m);

        // Assert
        Assert.NotNull(concepto.Descuento);
        Assert.Equal(1000m, concepto.Descuento.Value);
        Assert.Equal(25000m, concepto.Importe.Value);
    }

    [Fact]
    public void Create_WithZeroDiscount_ShouldAssignZeroDiscount()
    {
        // Arrange
        var claveProdServ = ClaveProdServ.Create("43211503");
        var cantidad = Cantidad.Create(2m);
        var claveUnidad = ClaveUnidad.Create("H87");
        var valorUnitario = ValorUnitario.Create(12500m);
        var objetoImp = ObjetoImp.Create("02");

        // Act
        var concepto = Concepto.Create(claveProdServ, cantidad, claveUnidad, "Computadora portátil", valorUnitario, objetoImp, descuento: 0m);

        // Assert
        Assert.NotNull(concepto.Descuento);
        Assert.Equal(0m, concepto.Descuento.Value);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-1000)]
    public void Create_WithNegativeDiscount_ShouldThrowArgumentException(decimal descuento)
    {
        // Arrange
        var claveProdServ = ClaveProdServ.Create("43211503");
        var cantidad = Cantidad.Create(2m);
        var claveUnidad = ClaveUnidad.Create("H87");
        var valorUnitario = ValorUnitario.Create(12500m);
        var objetoImp = ObjetoImp.Create("02");

        // Act
        Action action = () => Concepto.Create(claveProdServ, cantidad, claveUnidad, "Computadora portátil", valorUnitario, objetoImp, descuento);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Theory]
    [InlineData(25000.01)]
    [InlineData(30000)]
    public void Create_WithDiscountGreaterThanImporte_ShouldThrowArgumentException(decimal descuento)
    {
        // Arrange
        var claveProdServ = ClaveProdServ.Create("43211503");
        var cantidad = Cantidad.Create(2m);
        var claveUnidad = ClaveUnidad.Create("H87");
        var valorUnitario = ValorUnitario.Create(12500m);
        var objetoImp = ObjetoImp.Create("02");

        // Act
        Action action = () => Concepto.Create(claveProdServ, cantidad, claveUnidad, "Computadora portátil", valorUnitario, objetoImp, descuento);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void Create_WithDiscountEqualToImporte_ShouldAssignDiscount()
    {
        // Arrange
        var claveProdServ = ClaveProdServ.Create("43211503");
        var cantidad = Cantidad.Create(2m);
        var claveUnidad = ClaveUnidad.Create("H87");
        var valorUnitario = ValorUnitario.Create(12500m);
        var objetoImp = ObjetoImp.Create("02");

        // Act
        var concepto = Concepto.Create(claveProdServ, cantidad, claveUnidad, "Computadora portátil", valorUnitario, objetoImp, descuento: 25000m);

        // Assert
        Assert.NotNull(concepto.Descuento);
        Assert.Equal(25000m, concepto.Descuento.Value);
        Assert.Equal(25000m, concepto.Importe.Value);
    }

    [Theory]
    [InlineData("claveProdServ")]
    [InlineData("cantidad")]
    [InlineData("claveUnidad")]
    [InlineData("valorUnitario")]
    [InlineData("objetoImp")]
    public void Create_WithNullValueObject_ShouldThrowArgumentNullException(string parameterName)
    {
        // Arrange
        ClaveProdServ? claveProdServ = ClaveProdServ.Create("43211503");
        Cantidad? cantidad = Cantidad.Create(2m);
        ClaveUnidad? claveUnidad = ClaveUnidad.Create("H87");
        ValorUnitario? valorUnitario = ValorUnitario.Create(12500m);
        ObjetoImp? objetoImp = ObjetoImp.Create("02");

        switch (parameterName)
        {
            case "claveProdServ":
                claveProdServ = null;
                break;

            case "cantidad":
                cantidad = null;
                break;

            case "claveUnidad":
                claveUnidad = null;
                break;

            case "valorUnitario":
                valorUnitario = null;
                break;

            case "objetoImp":
                objetoImp = null;
                break;
        }

        // Act
        Action action = () => Concepto.Create(claveProdServ!, cantidad!, claveUnidad!, "Computadora portátil", valorUnitario!, objetoImp!);

        // Assert
        var exception = Assert.Throws<ArgumentNullException>(action);
        Assert.Equal(parameterName, exception.ParamName);
    }

    [Fact]
    public void Create_WithSameData_ShouldGenerateDifferentIds()
    {
        // Arrange
        var claveProdServ = ClaveProdServ.Create("43211503");
        var cantidad = Cantidad.Create(2m);
        var claveUnidad = ClaveUnidad.Create("H87");
        var valorUnitario = ValorUnitario.Create(12500m);
        var objetoImp = ObjetoImp.Create("02");

        // Act
        var concepto1 = Concepto.Create(claveProdServ, cantidad, claveUnidad, "Computadora portátil", valorUnitario, objetoImp);
        var concepto2 = Concepto.Create(claveProdServ, cantidad, claveUnidad, "Computadora portátil", valorUnitario, objetoImp);

        // Assert
        Assert.NotEqual(concepto1.Id, concepto2.Id);
    }

    [Theory]
    [InlineData(" Computadora portátil", "Computadora portátil")]
    [InlineData("Computadora portátil ", "Computadora portátil")]
    [InlineData("  Computadora portátil  ", "Computadora portátil")]
    public void Create_WithSurroundingSpaces_ShouldNormalizeDescription(string descripcion, string expected)
    {
        // Arrange
        var claveProdServ = ClaveProdServ.Create("43211503");
        var cantidad = Cantidad.Create(2m);
        var claveUnidad = ClaveUnidad.Create("H87");
        var valorUnitario = ValorUnitario.Create(12500m);
        var objetoImp = ObjetoImp.Create("02");

        // Act
        var concepto = Concepto.Create(claveProdServ, cantidad, claveUnidad, descripcion, valorUnitario, objetoImp);

        // Assert
        Assert.Equal(expected, concepto.Descripcion);
    }
}
