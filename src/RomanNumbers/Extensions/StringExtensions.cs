using System.Text.RegularExpressions;

namespace RomanNumbers.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Counts the number of times a Roman symbol appears in a Roman Number.
    /// </summary>
    /// <param name="romanNumber">The Roman representation to analyze.</param>
    /// <param name="romanDigit">The Roman symbol to be searched.</param>
    /// <returns>The number of times.</returns>
    public static int DigitCount(this string romanNumber, string romanDigit)
    {
        return Regex.Matches(romanNumber, romanDigit).Count;
    }
}