using Xunit;
using RomanNumbers;
using System;

namespace RomanNumbers.Tests;

public class RomanNumberTests
{
    [Theory]
    [InlineData("I", 1)]
    [InlineData("MMMCMXCIX", 3999)]
    [InlineData("IX", 9)]
    [InlineData("XL", 40)]
    [InlineData("XC", 90)]
    [InlineData("CD", 400)]
    [InlineData("CM", 900)]
    public void Test_Ctor_WithNumber(string expect, int value)
    {
        // Arrange
        var rn = new RomanNumber(value);

        // Assert
        Assert.Equal(expect, rn.RomanRepresentation);
    }

    [Theory]
    [InlineData("I", 1)]
    [InlineData("MMMCMXCIX", 3999)]
    [InlineData("IX", 9)]
    [InlineData("XL", 40)]
    [InlineData("XC", 90)]
    [InlineData("CD", 400)]
    [InlineData("CM", 900)]
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
    public void ToNumber_WhenStringIsNotValid_ThenThrowsFormatException()
    {
        // Act & Assert
        Assert.Throws<FormatException>(() => RomanNumber.ToNumber("MCCMLXXXII"));
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

    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(1982, 40, 2022)]
    public void Test_Sum_Int(int value1, int value2, int expect)
    {
        // Arrange
        var rn1 = new RomanNumber(value1);
        var rn2 = new RomanNumber(value2);

        // Act
        var result = rn1 + rn2;

        // Assert
        Assert.Equal(expect, result.Number);
    }

    [Theory]
    [InlineData("I", "I", "II")]
    [InlineData("I", "XL", "XLI")]
    public void Test_Sum_String(string value1, string value2, string expect)
    {
        // Arrange
        var rn1 = new RomanNumber(value1);
        var rn2 = new RomanNumber(value2);

        // Act
        var result = rn1 + rn2;

        // Assert
        Assert.Equal(expect, result.RomanRepresentation);
    }

    [Theory]
    [InlineData(11, 1, 10)]
    [InlineData(2022, 40, 1982)]
    public void Test_Difference_Int(int value1, int value2, int expect)
    {
        // Arrange
        var rn1 = new RomanNumber(value1);
        var rn2 = new RomanNumber(value2);

        // Act
        var result = rn1 - rn2;

        // Assert
        Assert.Equal(expect, result.Number);
    }

    [Theory]
    [InlineData("IX", "I", "VIII")]
    [InlineData("L", "I", "XLIX")]
    [InlineData("C", "I", "XCIX")]
    [InlineData("M", "I", "CMXCIX")]
    public void Test_Difference_String(string value1, string value2, string expect)
    {
        // Arrange
        var rn1 = new RomanNumber(value1);
        var rn2 = new RomanNumber(value2);

        // Act
        var result = rn1 - rn2;

        // Assert
        Assert.Equal(expect, result.RomanRepresentation);
    }

    [Theory]
    [InlineData(12, 10, 120)]
    [InlineData(1999, 2, 3998)]
    public void Test_Product_Int(int value1, int value2, int expect)
    {
        // Arrange
        var rn1 = new RomanNumber(value1);
        var rn2 = new RomanNumber(value2);

        // Act
        var result = rn1 * rn2;

        // Assert
        Assert.Equal(expect, result.Number);
    }

    [Theory]
    [InlineData("XII", "XII", "CXLIV")]
    [InlineData("MCMXCIX", "II", "MMMCMXCVIII")]
    public void Test_Product_String(string value1, string value2, string expect)
    {
        // Arrange
        var rn1 = new RomanNumber(value1);
        var rn2 = new RomanNumber(value2);

        // Act
        var result = rn1 * rn2;

        // Assert
        Assert.Equal(expect, result.RomanRepresentation);
    }

    [Fact]
    public void Test_Product_ResultOutOfRange()
    {
        // Arrange
        var rn1 = new RomanNumber(2000);
        var rn2 = new RomanNumber(2);

        // Act && Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => rn1 * rn2);
    }

    [Theory]
    [InlineData(120, 10, 12)]
    [InlineData(63, 2, 31)]
    public void Test_Quotient(int value1, int value2, int expect)
    {
        // Arrange
        var rn1 = new RomanNumber(value1);
        var rn2 = new RomanNumber(value2);

        // Act
        var result = rn1 / rn2;

        // Assert
        Assert.Equal(expect, result.Number);
    }

    [Fact]
    public void Test_Quotient_ResultOutOfRange()
    {
        // Arrange
        var rn1 = new RomanNumber(1);
        var rn2 = new RomanNumber(2);

        // Act && Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => rn1 / rn2);
    }

    [Theory]
    [InlineData(4, 3, 1)]
    [InlineData(9, 2, 1)]
    public void Test_Module(int value1, int value2, int expect)
    {
        // Arrange
        var rn1 = new RomanNumber(value1);
        var rn2 = new RomanNumber(value2);

        // Act
        var result = rn1 % rn2;

        // Assert
        Assert.Equal(expect, result.Number);
    }

    [Fact]
    public void Test_Module_ResultOutOfRange()
    {
        // Arrange
        var rn1 = new RomanNumber(9);
        var rn2 = new RomanNumber(9);

        // Act && Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => rn1 % rn2);
    }
}