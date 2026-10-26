using System;
using System.Collections.Generic;
using System.Text;
using CfdiPlatform.Domain.ValueObjects;

namespace CfdiPlatform.Domain.UnitTests.ValueObjects;

public class RfcTests
{
    [Theory]
    [InlineData("GODE561231GR8")]
    [InlineData("ABC010101AB1")]
    public void Create_WithValidRfc_ShouldSucceed(string value)
    {
        var rfc = Rfc.Create(value);

        Assert.Equal(value, rfc.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("ABC123")]
    [InlineData("ABCDEFGHIJKLMN")]
    public void Create_WithInvalidRfc_ShouldThrow(string value)
    {
        Assert.Throws<ArgumentException>(() => Rfc.Create(value));
    }

    [Fact]
    public void Create_WithLowercaseRfc_ShouldNormalizeToUppercase()
    {
        var rfc = Rfc.Create("gode561231gr8");

        Assert.Equal("GODE561231GR8", rfc.Value);
    }

    [Fact]
    public void Create_WithSurroundingSpaces_ShouldTrim()
    {
        var rfc = Rfc.Create("  GODE561231GR8  ");

        Assert.Equal("GODE561231GR8", rfc.Value);
    }

    [Fact]
    public void TwoRfcs_WithSameValue_ShouldBeEqual()
    {
        var first = Rfc.Create("GODE561231GR8");
        var second = Rfc.Create("GODE561231GR8");

        Assert.Equal(first, second);
    }

    [Theory]
    [InlineData("ABC010101AB1")]
    [InlineData("GODE561231GR8")]
    public void Create_WithValidLength_ShouldReturnExpectedLength(
    string value)
    {
        var rfc = Rfc.Create(value);

        Assert.Equal(value.Length, rfc.Value.Length);
    }

    [Theory]
    [InlineData("ABC010101AB!")]
    [InlineData("ABC010101A 1")]
    [InlineData("ABC010101AB_")]
    [InlineData("ABC010101AB#")]
    public void Create_WithInvalidCharacters_ShouldThrow(string value)
    {
        Assert.Throws<ArgumentException>(() => Rfc.Create(value));
    }

    [Fact]
    public void Create_WithNull_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() => Rfc.Create(null));
    }

    [Fact]
    public void TwoRfcs_WithDifferentValues_ShouldNotBeEqual()
    {
        var first = Rfc.Create("ABC010101AB1");
        var second = Rfc.Create("GODE561231GR8");

        Assert.NotEqual(first, second);
    }

    [Fact]
    public void ToString_ShouldReturnNormalizedRfc()
    {
        var rfc = Rfc.Create("gode561231gr8");

        Assert.Equal("GODE561231GR8", rfc.ToString());
    }
}