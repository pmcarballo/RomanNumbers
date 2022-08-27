using Xunit;
using RomanNumbers;
using System;

namespace RomanNumbers.Tests;

public class RomanNumberTests
{
    [Theory]
    [InlineData(1, "I")]
    [InlineData(3999, "MMMCMXCIX")]
    public void Test_Ctor_WithNumber(int value, string expect)
    {
        // Arrange
        var rn = new RomanNumber(value);

        // Assert
        Assert.Equal(expect, rn.RomanRepresentation);
    }

    [Theory]
    [InlineData("I", 1)]
    [InlineData("MMMCMXCIX", 3999)]
    //[InlineData("MMMM")]
    public void Test_Ctor_WithString(string value, int expect)
    {
        // Arrange
        var rn = new RomanNumber(value);

        // Assert
        Assert.Equal(expect, rn.Number);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void Test_Ctor_WithLowerNumber(int value)
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new RomanNumber(value));
    }

    [Theory]
    [InlineData(4000)]
    [InlineData(8000)]
    [InlineData(int.MaxValue)]
    public void Test_Ctor_WithHigherNumber(int value)
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new RomanNumber(value));
    }

    [Fact]
    public void Test_Ctor_WithValidString()
    {
        // Arrange
        var rn = new RomanNumber("IX");

        // Assert
        Assert.Equal(9, rn.Number);
    }

    [Fact]
    public void ToRomanNumber_WhenNumberIsValid_ThenConvertToRoman()
    {
        // Act
        string rn = RomanNumber.ToRomanNumber(1982);

        // Assert
        Assert.Equal("MCMLXXXII", rn);
    }

    [Fact]
    public void ToNumber_WhenStringIsValid_ThenConvertToNumber()
    {
        // Act
        int n = RomanNumber.ToNumber("MCMLXXXII");

        // Assert
        Assert.Equal(1982, n);
    }

    [Fact]
    public void IsValid_WhenStringIsValid_ThenReturnsTrue()
    {
        // Act
        bool result = RomanNumber.IsValid("MCMLXXXII");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsValid_WhenStringIsNotValid_ThenReturnsFalse()
    {
        // Act
        bool result = RomanNumber.IsValid("MCMLXXXIIII");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void RomanRepresentation_WhenNumberIsValid_ThenReturnsRoman()
    {
        // Act
        var rn = new RomanNumber(1982);

        // Assert
        Assert.Equal("MCMLXXXII", rn.RomanRepresentation);
    }
}