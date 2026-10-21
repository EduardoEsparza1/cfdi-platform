using System;
using System.Collections.Generic;
using System.Text;
using CfdiPlatform.Domain.ValueObjects;

namespace CfdiPlatform.Domain.UnitTests.ValueObjects
{
    public class SubtotalTests
    {
        [Fact]
        public void Constructor_WithValidValue_ShouldSetValue()
        {
            // Arrange
            decimal expected = 1500.50m;

            // Act
            var subtotal = new Subtotal(expected);

            // Assert
            Assert.Equal(expected, subtotal.Value);
        }

        [Fact]
        public void Constructor_WithNegativeValue_ShouldThrowException()
        {
            // Arrange
            decimal invalidValue = -100m;

            // Act
            Action action = () => new Subtotal(invalidValue);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(action);
        }

        [Fact]
        public void Constructor_WithZero_ShouldBeValid()
        {
            // Arrange
            decimal value = 0m;

            // Act
            var subtotal = new Subtotal(value);

            // Assert
            Assert.Equal(0m, subtotal.Value);
        }
    }
}
