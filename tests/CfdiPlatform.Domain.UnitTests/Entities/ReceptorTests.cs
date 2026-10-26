using System;
using System.Collections.Generic;
using System.Text;
using CfdiPlatform.Domain.Entities;
using CfdiPlatform.Domain.ValueObjects;

namespace CfdiPlatform.Domain.UnitTests.Entities;

public class ReceptorTests
{
    private static readonly Rfc RfcValido = Rfc.Create("ABC010101AB1");

    private static readonly CodigoPostal CpValido = CodigoPostal.Create("20000");

    private static readonly RegimenFiscal RegimenValido = RegimenFiscal.Create("601");

    [Fact]
    public void Create_WithValidData_ShouldCreateReceptor()
    {
        var receptor = CrearReceptorValido();

        Assert.NotEqual(Guid.Empty, receptor.Id);
        Assert.Equal(RfcValido, receptor.Rfc);
        Assert.Equal("EMPRESA DE PRUEBA", receptor.Nombre);
        Assert.Equal(CpValido, receptor.DomicilioFiscal);
        Assert.Equal(RegimenValido, receptor.RegimenFiscal);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidNombre_ShouldThrow(string nombre)
    {
        Assert.Throws<ArgumentException>(() =>
            Receptor.Create(
                RfcValido,
                nombre,
                CpValido,
                RegimenValido));
    }

    [Fact]
    public void Create_WithNullNombre_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() =>
            Receptor.Create(
                RfcValido,
                null!,
                CpValido,
                RegimenValido));
    }

    [Fact]
    public void Create_WithSurroundingSpaces_ShouldTrimNombre()
    {
        var receptor = Receptor.Create(
            RfcValido,
            "  EMPRESA DE PRUEBA  ",
            CpValido,
            RegimenValido);

        Assert.Equal("EMPRESA DE PRUEBA", receptor.Nombre);
    }

    [Fact]
    public void Create_WithNullRfc_ShouldThrow()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Receptor.Create(
                null!,
                "EMPRESA DE PRUEBA",
                CpValido,
                RegimenValido));
    }

    [Fact]
    public void Create_WithNullDomicilioFiscal_ShouldThrow()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Receptor.Create(
                RfcValido,
                "EMPRESA DE PRUEBA",
                null!,
                RegimenValido));
    }

    [Fact]
    public void Create_WithNullRegimenFiscal_ShouldThrow()
    {
        Assert.Throws<ArgumentNullException>(() =>
        Receptor.Create(
            RfcValido,
            "EMPRESA DE PRUEBA",
            CpValido,
            null!));
    }

    [Fact]
    public void UpdateNombre_WithValidValue_ShouldUpdate()
    {
        var receptor = CrearReceptorValido();

        receptor.ActualizarNombre("NUEVA RAZON SOCIAL");

        Assert.Equal("NUEVA RAZON SOCIAL", receptor.Nombre);
    }

    [Fact]
    public void UpdateNombre_WithInvalidValue_ShouldPreserveOriginal()
    {
        var receptor = CrearReceptorValido();

        Assert.Throws<ArgumentException>(() =>
            receptor.ActualizarNombre(""));

        Assert.Equal("EMPRESA DE PRUEBA", receptor.Nombre);
    }

    [Fact]
    public void Create_TwoReceptores_ShouldHaveDifferentIds()
    {
        var primero = CrearReceptorValido();
        var segundo = CrearReceptorValido();

        Assert.NotEqual(primero.Id, segundo.Id);
    }

    private static Receptor CrearReceptorValido()
    {
        return Receptor.Create(
            RfcValido,
            "EMPRESA DE PRUEBA",
            CpValido,
            RegimenValido);
    }
}