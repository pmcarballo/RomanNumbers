using System.Text.RegularExpressions;

namespace RomanNumbers.Extensions;

public static class StringExtensions
{
    public static int DigitCount(this string romanNumber, string romanDigit)
    {
        return Regex.Matches(romanNumber, romanDigit).Count;
    }
}