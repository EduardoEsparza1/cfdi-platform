using System;
using System.Collections.Generic;
using System.Text;
using CfdiPlatform.Domain.ValueObjects;

namespace CfdiPlatform.Domain.UnitTests.ValueObjects;

public class UsoCfdiTests
{
    [Theory]
    [InlineData("G01")]
    [InlineData("G03")]
    [InlineData("I01")]
    [InlineData("S01")]
    [InlineData("CP01")]
    public void Create_WithValidFormat_ShouldSucceed(string value)
    {
        var usoCfdi = UsoCfdi.Create(value);
        Assert.Equal(value, usoCfdi.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("G0")]
    [InlineData("G001")]
    [InlineData("123")]
    [InlineData("ABC")]
    [InlineData("G 1")]
    [InlineData("G@1")]
    public void Create_WithInvalidFormat_ShouldThrow(string value)
    {
        Assert.Throws<ArgumentException>(() => UsoCfdi.Create(value));
    }

    [Fact]
    public void Create_WithNull_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() => UsoCfdi.Create(null));
    }

    [Fact]
    public void Create_WithSurroundingSpaces_ShouldTrim()
    {
        var usoCfdi = UsoCfdi.Create("  G03  ");
        Assert.Equal("G03", usoCfdi.Value);
    }

    [Fact]
    public void Create_WithLowercase_ShouldNormalizeToUppercase()
    {
        var usoCfdi = UsoCfdi.Create("g03");
        Assert.Equal("G03", usoCfdi.Value);
    }

    [Fact]
    public void TwoUsoCfdi_WithSameValue_ShouldBeEqual()
    {
        var first = UsoCfdi.Create("G03");
        var second = UsoCfdi.Create("G03");
        Assert.Equal(first, second);
    }

    [Fact]
    public void TwoUsoCfdi_WithDifferentValues_ShouldNotBeEqual()
    {
        var first = UsoCfdi.Create("G01");
        var second = UsoCfdi.Create("G03");
        Assert.NotEqual(first, second);
    }

    [Fact]
    public void ToString_ShouldReturnValue()
    {
        var usoCfdi = UsoCfdi.Create("G03");
        Assert.Equal("G03", usoCfdi.ToString());
    }
}