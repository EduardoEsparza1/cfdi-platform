using System;
using System.Collections.Generic;
using System.Text;
using CfdiPlatform.Domain.ValueObjects;
using Xunit;

namespace CfdiPlatform.Domain.UnitTests.ValueObjects;

public class ObjetoImpTests
{
    [Theory]
    [InlineData("01")]
    [InlineData("02")]
    [InlineData("03")]
    [InlineData("04")]
    [InlineData("05")]
    public void Create_WithValidCode_ShouldCreateObjetoImp(string value)
    {
        // Act
        var result = ObjetoImp.Create(value);

        // Assert
        Assert.Equal(value, result.Value);
    }

    [Theory]
    [InlineData("00")]
    [InlineData("06")]
    [InlineData("99")]
    [InlineData("1")]
    [InlineData("ABC")]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidCode_ShouldThrowArgumentException(string value)
    {
        // Act
        Action action = () => ObjetoImp.Create(value);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Create_WithNullValue_ShouldThrowArgumentException()
    {
        // Act
        Action action = () => ObjetoImp.Create(null!);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Create_WithSurroundingSpaces_ShouldNormalizeValue()
    {
        // Arrange
        string value = " 02 ";

        // Act
        var result = ObjetoImp.Create(value);

        // Assert
        Assert.Equal("02", result.Value);
    }

    [Fact]
    public void Create_WithSameValue_ShouldBeEqual()
    {
        // Arrange
        var objeto1 = ObjetoImp.Create("02");
        var objeto2 = ObjetoImp.Create("02");

        // Assert
        Assert.Equal(objeto1, objeto2);
    }

    [Fact]
    public void Create_WithDifferentValues_ShouldNotBeEqual()
    {
        // Arrange
        var objeto1 = ObjetoImp.Create("01");
        var objeto2 = ObjetoImp.Create("02");

        // Assert
        Assert.NotEqual(objeto1, objeto2);
    }
}
