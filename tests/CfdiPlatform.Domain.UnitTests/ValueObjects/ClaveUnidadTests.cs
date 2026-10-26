using System;
using System.Collections.Generic;
using System.Text;
using CfdiPlatform.Domain.ValueObjects;
using Xunit;

namespace CfdiPlatform.Domain.UnitTests.ValueObjects;

public class ClaveUnidadTests
{
    [Theory]
    [InlineData("H87")]
    [InlineData("E48")]
    [InlineData("KGM")]
    [InlineData("LTR")]
    [InlineData("MTR")]
    [InlineData("A")]
    [InlineData("AB")]
    public void Create_WithValidCode_ShouldCreateClaveUnidad(string value)
    {
        // Act
        var result = ClaveUnidad.Create(value);

        // Assert
        Assert.Equal(value, result.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("H 87")]
    [InlineData("H-87")]
    [InlineData("ABCD")]
    [InlineData("1234")]
    [InlineData("H@7")]
    [InlineData("Ｈ８７")]
    public void Create_WithInvalidCode_ShouldThrowArgumentException(string value)
    {
        // Act
        Action action = () => ClaveUnidad.Create(value);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Create_WithNullValue_ShouldThrowArgumentException()
    {
        // Act
        Action action = () => ClaveUnidad.Create(null!);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Theory]
    [InlineData("h87", "H87")]
    [InlineData("e48", "E48")]
    [InlineData("kgm", "KGM")]
    [InlineData(" H87 ", "H87")]
    [InlineData("  kgm  ", "KGM")]
    public void Create_WithLowercaseOrSurroundingSpaces_ShouldNormalizeValue(string value, string expected)
    {
        // Act
        var result = ClaveUnidad.Create(value);

        // Assert
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void Create_WithSameValue_ShouldBeEqual()
    {
        // Arrange
        var clave1 = ClaveUnidad.Create("H87");
        var clave2 = ClaveUnidad.Create("h87");

        // Assert
        Assert.Equal(clave1, clave2);
    }

    [Fact]
    public void Create_WithDifferentValues_ShouldNotBeEqual()
    {
        // Arrange
        var clave1 = ClaveUnidad.Create("H87");
        var clave2 = ClaveUnidad.Create("E48");

        // Assert
        Assert.NotEqual(clave1, clave2);
    }
}
