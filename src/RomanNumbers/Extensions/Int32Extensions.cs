namespace RomanNumbers.Extensions;

public static class Int32Extensions
{
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