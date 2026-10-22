using System;
using System.Collections.Generic;
using System.Text;
using CfdiPlatform.Domain.Entities;
using CfdiPlatform.Domain.ValueObjects;

namespace CfdiPlatform.Domain.UnitTests.Entities;

public class EmisorTests
{
    private static readonly Rfc RfcValido = Rfc.Create("ABC010101AB1");

    private static readonly CodigoPostal CpValido = CodigoPostal.Create("20000");

    private static readonly RegimenFiscal RegimenValido = RegimenFiscal.Create("601");

    [Fact]
    public void Create_WithValidData_ShouldCreateEmisor()
    {
        var emisor = Emisor.Create(RfcValido,"EMPRESA DE PRUEBA",RegimenValido, CpValido);

        Assert.NotEqual(Guid.Empty, emisor.Id);
        Assert.Equal(RfcValido, emisor.Rfc);
        Assert.Equal("EMPRESA DE PRUEBA", emisor.Nombre);
        Assert.Equal(RegimenValido, emisor.RegimenFiscal);
        Assert.Equal(CpValido, emisor.CodigoPostal);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidNombre_ShouldThrow(
        string nombre)
    {
        Assert.Throws<ArgumentException>(() =>
            Emisor.Create(
                RfcValido,
                nombre,
                RegimenValido,
                CpValido));
    }

    [Fact]
    public void Create_WithNullNombre_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() =>
            Emisor.Create(
                RfcValido,
                null!,
                RegimenValido,
                CpValido));
    }

    [Fact]
    public void Create_WithSurroundingSpaces_ShouldTrimNombre()
    {
        var emisor = Emisor.Create(
            RfcValido,
            "  EMPRESA DE PRUEBA  ",
            RegimenValido,
            CpValido);

        Assert.Equal("EMPRESA DE PRUEBA", emisor.Nombre);
    }

    [Fact]
    public void Create_WithNullRegimenFiscal_ShouldThrow()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Emisor.Create(
                RfcValido,
                "EMPRESA DE PRUEBA",
                null!,
                CpValido));
    }

    [Fact]
    public void Create_WithNullRfc_ShouldThrow()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Emisor.Create(
                null!,
                "EMPRESA DE PRUEBA",
                RegimenValido,
                CpValido));
    }

    [Fact]
    public void Create_WithNullCodigoPostal_ShouldThrow()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Emisor.Create(
                RfcValido,
                "EMPRESA DE PRUEBA",
                RegimenValido,
                null!));
    }

    [Fact]
    public void UpdateNombre_WithValidValue_ShouldUpdate()
    {
        var emisor = CrearEmisorValido();

        emisor.ActualizarNombre("NUEVA RAZON SOCIAL");

        Assert.Equal("NUEVA RAZON SOCIAL", emisor.Nombre);
    }

    [Fact]
    public void UpdateNombre_WithInvalidValue_ShouldPreserveOriginal()
    {
        var emisor = CrearEmisorValido();

        Assert.Throws<ArgumentException>(() =>
            emisor.ActualizarNombre(""));

        Assert.Equal("EMPRESA DE PRUEBA", emisor.Nombre);
    }

    private static Emisor CrearEmisorValido()
    {
        return Emisor.Create(
            RfcValido,
            "EMPRESA DE PRUEBA",
            RegimenValido,
            CpValido);
    }

    [Fact]
    public void UpdateRegimenFiscal_WithValidValue_ShouldUpdate()
    {
        var emisor = CrearEmisorValido();

        var nuevoRegimen = RegimenFiscal.Create("603");
        emisor.ActualizarRegimenFiscal(nuevoRegimen);

        Assert.Equal(nuevoRegimen, emisor.RegimenFiscal);
    }

    [Fact]
    public void UpdateCodigoPostal_WithValidValue_ShouldUpdate()
    {
        var emisor = CrearEmisorValido();
        var nuevoCodigoPostal = CodigoPostal.Create("01000");

        emisor.ActualizarCodigoPostal(nuevoCodigoPostal);

        Assert.Equal(nuevoCodigoPostal, emisor.CodigoPostal);
    }

    [Fact]
    public void UpdateCodigoPostal_WithNull_ShouldPreserveOriginal()
    {
        var emisor = CrearEmisorValido();
        var codigoPostalOriginal = emisor.CodigoPostal;

        Assert.Throws<ArgumentNullException>(() =>
            emisor.ActualizarCodigoPostal(null!));

        Assert.Equal(codigoPostalOriginal, emisor.CodigoPostal);
    }

    [Fact]
    public void Create_TwoEmisores_ShouldHaveDifferentIds()
    {
        var first = CrearEmisorValido();
        var second = CrearEmisorValido();

        Assert.NotEqual(first.Id, second.Id);
    }
}