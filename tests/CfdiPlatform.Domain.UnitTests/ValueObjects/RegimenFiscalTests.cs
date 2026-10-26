using System;
using System.Collections.Generic;
using System.Text;
using CfdiPlatform.Domain.ValueObjects;

namespace CfdiPlatform.Domain.UnitTests.ValueObjects;

public class RegimenFiscalTests
{
    [Theory]
    [InlineData("601")]
    [InlineData("603")]
    [InlineData("605")]
    [InlineData("626")]
    public void Create_WithValidFormat_ShouldSucceed(string value)
    {
        var regimen = RegimenFiscal.Create(value);
        Assert.Equal(value, regimen.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("60")]
    [InlineData("6010")]
    [InlineData("ABC")]
    [InlineData("6A1")]
    [InlineData("6 1")]
    public void Create_WithInvalidFormat_ShouldThrow(string value)
    {
        Assert.Throws<ArgumentException>(() => RegimenFiscal.Create(value));
    }

    [Fact]
    public void Create_WithNull_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() => RegimenFiscal.Create(null));
    }

    [Fact]
    public void Create_WithSurroundingSpaces_ShouldTrim()
    {
        var regimen = RegimenFiscal.Create("  601  ");
        Assert.Equal("601", regimen.Value);
    }

    [Fact]
    public void TwoRegimenes_WithSameValue_ShouldBeEqual()
    {
        var first = RegimenFiscal.Create("601");
        var second = RegimenFiscal.Create("601");
        Assert.Equal(first, second);
    }

    [Fact]
    public void TwoRegimenes_WithDifferentValues_ShouldNotBeEqual()
    {
        var first = RegimenFiscal.Create("601");
        var second = RegimenFiscal.Create("603");
        Assert.NotEqual(first, second);
    }

    [Fact]
    public void ToString_ShouldReturnValue()
    {
        var regimen = RegimenFiscal.Create("601");
        Assert.Equal("601", regimen.ToString());
    }
}