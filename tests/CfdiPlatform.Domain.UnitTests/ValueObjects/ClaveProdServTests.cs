using System;
using System.Collections.Generic;
using System.Text;
using CfdiPlatform.Domain.ValueObjects;
using Xunit;

namespace CfdiPlatform.Domain.UnitTests.ValueObjects;

public class ClaveProdServTests
{
    [Theory]
    [InlineData("43211503")]
    [InlineData("01010101")]
    [InlineData("81112100")]
    public void Create_WithValidCode_ShouldCreateClaveProdServ(string value)
    {
        // Act
        var result = ClaveProdServ.Create(value);

        // Assert
        Assert.Equal(value, result.Value);
    }

    [Theory]
    [InlineData("1234567")]
    [InlineData("123456789")]
    [InlineData("ABCDEFGH")]
    [InlineData("1234ABCD")]
    [InlineData("")]
    [InlineData("        ")]
    [InlineData("1234 678")]
    [InlineData("１２３４５６７８")]
    public void Create_WithInvalidCode_ShouldThrowArgumentException(string value)
    {
        // Act
        Action action = () => ClaveProdServ.Create(value);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Create_WithNullValue_ShouldThrowArgumentException()
    {
        // Act
        Action action = () => ClaveProdServ.Create(null!);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Create_WithSurroundingSpaces_ShouldTrimValue()
    {
        // Arrange
        string value = " 43211503 ";

        // Act
        var result = ClaveProdServ.Create(value);

        // Assert
        Assert.Equal("43211503", result.Value);
    }

    [Fact]
    public void Create_WithSameValue_ShouldBeEqual()
    {
        // Arrange
        var clave1 = ClaveProdServ.Create("43211503");
        var clave2 = ClaveProdServ.Create("43211503");

        // Assert
        Assert.Equal(clave1, clave2);
    }

    [Fact]
    public void Create_WithDifferentValues_ShouldNotBeEqual()
    {
        // Arrange
        var clave1 = ClaveProdServ.Create("43211503");
        var clave2 = ClaveProdServ.Create("81112100");

        // Assert
        Assert.NotEqual(clave1, clave2);
    }

    [Fact]
    public void Create_WithSurroundingWhitespace_ShouldNormalizeValue()
    {
        // Arrange
        string value = "\n\t43211503\t\n";

        // Act
        var result = ClaveProdServ.Create(value);

        // Assert
        Assert.Equal("43211503", result.Value);
    }
}
