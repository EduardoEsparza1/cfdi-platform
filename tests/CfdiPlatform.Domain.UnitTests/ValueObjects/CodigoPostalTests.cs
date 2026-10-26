using System;
using System.Collections.Generic;
using System.Text;
using CfdiPlatform.Domain.ValueObjects;

namespace CfdiPlatform.Domain.UnitTests.ValueObjects;

public class CodigoPostalTests
{
    [Theory]
    [InlineData("20000")]
    [InlineData("01000")]
    [InlineData("99999")]
    public void Create_WithFiveDigits_ShouldSucceed(string value)
    {
        var codigoPostal = CodigoPostal.Create(value);
        Assert.Equal(value, codigoPostal.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("1234")]
    [InlineData("123456")]
    [InlineData("ABCDE")]
    [InlineData("20A00")]
    [InlineData("20 00")]
    public void Create_WithInvalidFormat_ShouldThrow(string value)
    {
        Assert.Throws<ArgumentException>(
            () => CodigoPostal.Create(value));
    }

    [Fact]
    public void Create_WithNull_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(
            () => CodigoPostal.Create(null));
    }

    [Fact]
    public void Create_WithSurroundingSpaces_ShouldTrim()
    {
        var codigoPostal = CodigoPostal.Create("  20000  ");
        Assert.Equal("20000", codigoPostal.Value);
    }

    [Fact]
    public void TwoCodigoPostales_WithSameValue_ShouldBeEqual()
    {
        var first = CodigoPostal.Create("20000");
        var second = CodigoPostal.Create("20000");

        Assert.Equal(first, second);
    }

    [Fact]
    public void TwoCodigoPostales_WithDifferentValues_ShouldNotBeEqual()
    {
        var first = CodigoPostal.Create("20000");
        var second = CodigoPostal.Create("01000");

        Assert.NotEqual(first, second);
    }
}