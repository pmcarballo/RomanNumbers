namespace RomanNumbers.Extensions;

public static class Int32Extensions
{
    /// <summary>
    /// Counts the number of digits.
    /// </summary>
    /// <param name="number">The number whose digits will be counted.</param>
    /// <returns>The count of digits.</returns>
    public static int Length(this int number)
    {
        int count = 0;

        while (number > 0)
        {
            number = number / 10;
            count++;
        }

        return count;
    }
}