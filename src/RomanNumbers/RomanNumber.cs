using System.Text;
using System.Text.RegularExpressions;
using RomanNumbers.Extensions;

namespace RomanNumbers;

public struct RomanNumber : IComparable, IComparable<RomanNumber>, IConvertible, IEquatable<RomanNumber>
{
    public const string One = "I";
    public const string Five = "V";
    public const string Ten = "X";
    public const string Fifty = "L";
    public const string OneHundred = "C";
    public const string FiveHundred = "D";
    public const string OneThousand = "M";
    public const string UnicodeOne = "Ⅰ";
    public const string UnicodeFive = "Ⅴ";
    public const string UnicodeTen = "Ⅹ";
    public const string UnicodeFifty = "Ⅼ";
    public const string UnicodeOneHundred = "Ⅽ";
    public const string UnicodeFiveHundred = "Ⅾ";
    public const string UnicodeOneThousand = "Ⅿ";
    public const string UnicodeOneThousandCD = "ↀ";
    public const string UnicodeFiveThousand = "ↁ";
    public const string UnicodeTenThousand = "ↂ";
    public const string UnicodeFiftyThousand = "ↇ";
    public const string UnicodeOneHundredThousand = "ↈ";

    #region Private constants
    private const int _minValue = 1;
    private const int _maxValue = 3999;
    #endregion
    private static string[] _powerOfTenSymbols = { One, Ten, OneHundred, OneThousand };
    private int _number;

    /// <summary>
    /// Represents the smallest possible value of a Roman Number. This field is readonly.
    /// </summary>
    public static readonly RomanNumber MinValue = new RomanNumber(_minValue);

    /// <summary>
    /// Represents the largest possible value of a Roman Number. This field is readonly.
    /// </summary>
    public static readonly RomanNumber MaxValue = new RomanNumber(_maxValue);

    /// <summary>
    /// Gets the actual Int32 behind the Roman Number.
    /// </summary>
    public int Number => _number;

    /// <summary>
    /// Gets the symbols for the current number.
    /// </summary>
    public string RomanRepresentation => ToRomanNumber(_number);

    /// <summary>
    /// Creates an instance of a Roman Number for the specified integer.
    /// </summary>
    /// <param name="number">The number to be represented as Roman.</param>
    public RomanNumber(int number)
    {
        Validate(number);

        _number = number;
    }

    private static void Validate(int number)
    {
        if (number
            < _minValue) throw new ArgumentOutOfRangeException($"Numbers lower than {_minValue} cannot be represented by Roman Numerals.");

        if (number
            > _maxValue) throw new ArgumentOutOfRangeException($"Numbers greater than {_maxValue} cannot be represented by Roman Numerals.");
    }

    /// <summary>
    /// Creates an instance of a Roman Number for the specified symbols.
    /// </summary>
    /// <param name="number">The roman symbols to be represented as Roman.</param>
    public RomanNumber(string number)
        : this(ToNumber(number))
    { }

    /// <summary>
    /// Converts an Int32 to Roman number.
    /// </summary>
    /// <param name="number">The integral number to convert to roman number.</param>
    /// <returns>The roman representation for a number.</returns>
    public static string ToRomanNumber(int number)
    {
        Validate(number);

        var sb = new StringBuilder();
        int currentRomanDigit = 0;

        for (int i = number.Length() - 1; i >= 0; i--)
        {
            int digit = number % 10;
            var rd = String.Join(String.Empty, Enumerable.Repeat(_powerOfTenSymbols[currentRomanDigit], digit));
            sb.Insert(0, rd);
            currentRomanDigit++;
            number = number / 10;
        }

        string compact = Compact(sb.ToString());

        return compact;
    }

    /// <summary>
    /// Converts a string representation of a Roman number to an integral number.
    /// </summary>
    /// <param name="romanNumber">Roman symbols that represent an integral number.</param>
    /// <returns>An integral number that corresponds to the Roman</returns>
    /// <exception cref="FormatException">The input is not a valid Roman numeral.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The input results in an integral number that is out of the range of possible Roman representations.</exception>
    public static int ToNumber(string romanNumber)
    {
        if (!IsValid(romanNumber)) throw new FormatException("Input is not a valid Roman Numeral.");

        int result = 0;
        int power = 1;
        string expand = Expand(romanNumber);

        for (int i = 0; i < _powerOfTenSymbols.Length; i++)
        {
            string symbol = _powerOfTenSymbols[i];
            result += expand.DigitCount(symbol) * power;
            power *= 10;
        }

        Validate(result);

        return result;
    }

    private static string Compact(string v)
    {
        v = v.Replace("CCCCCCCCC", "CM");
        v = v.Replace("CCCCC", "D");
        v = v.Replace("CCCC", "CD");
        v = v.Replace("XXXXXXXXX", "XC");
        v = v.Replace("XXXXX", "L");
        v = v.Replace("XXXX", "XL");
        v = v.Replace("IIIIIIIII", "IX");
        v = v.Replace("IIIII", "V");
        v = v.Replace("IIII", "IV");
        return v;
    }

    private static string Expand(string v)
    {
        v = v.Replace("CM", "CCCCCCCCC");
        v = v.Replace("CD", "CCCC");
        v = v.Replace("D", "CCCCC");
        v = v.Replace("XC", "XXXXXXXXX");
        v = v.Replace("XL", "XXXX");
        v = v.Replace("L", "XXXXX");
        v = v.Replace("IX", "IIIIIIIII");
        v = v.Replace("IV", "IIII");
        v = v.Replace("V", "IIIII");
        return v;
    }

    /// <summary>
    /// Validates the input is represented as a standard Roman representation.
    /// </summary>
    /// <param name="romanNumber">The Roman representation to validate.</param>
    /// <returns>True if the input is valid; otherwise False.</returns>
    public static bool IsValid(string romanNumber)
    {
        return Regex.IsMatch(romanNumber, "^(?=[MDCLXVI])M*(C[MD]|D?C{0,3})(X[CL]|L?X{0,3})(I[XV]|V?I{0,3})$");
    }

    #region IComparable members
    public int CompareTo(object? obj)
    {
        return Number.CompareTo(obj);
    }
    #endregion

    #region IComparable<RomanNumber> members
    public int CompareTo(RomanNumber other)
    {
        return this.CompareTo(other);
    }
    #endregion

    #region IConvertible members
    public TypeCode GetTypeCode() => TypeCode.Object;

    public bool ToBoolean(IFormatProvider? provider) => ((IConvertible)Number).ToBoolean(provider);

    public byte ToByte(IFormatProvider? provider) => ((IConvertible)Number).ToByte(provider);

    public char ToChar(IFormatProvider? provider) => ((IConvertible)Number).ToChar(provider);

    public DateTime ToDateTime(IFormatProvider? provider) => ((IConvertible)Number).ToDateTime(provider);

    public decimal ToDecimal(IFormatProvider? provider) => ((IConvertible)Number).ToDecimal(provider);

    public double ToDouble(IFormatProvider? provider) => ((IConvertible)Number).ToDouble(provider);

    public short ToInt16(IFormatProvider? provider) => ((IConvertible)Number).ToInt16(provider);

    public int ToInt32(IFormatProvider? provider) => ((IConvertible)Number).ToInt32(provider);

    public long ToInt64(IFormatProvider? provider) => ((IConvertible)Number).ToInt64(provider);

    public sbyte ToSByte(IFormatProvider? provider) => ((IConvertible)Number).ToSByte(provider);

    public float ToSingle(IFormatProvider? provider) => ((IConvertible)Number).ToSingle(provider);

    public string ToString(IFormatProvider? provider) => Number.ToString(provider);

    public object ToType(Type conversionType, IFormatProvider? provider) => ((IConvertible)Number).ToType(conversionType, provider);

    public ushort ToUInt16(IFormatProvider? provider) => ((IConvertible)Number).ToUInt16(provider);

    public uint ToUInt32(IFormatProvider? provider) => ((IConvertible)Number).ToUInt32(provider);

    public ulong ToUInt64(IFormatProvider? provider) => ((IConvertible)Number).ToUInt64(provider);
    #endregion

    #region IEquatable<RomanNumber> members
    public bool Equals(RomanNumber other) => Number.Equals(other.Number);
    #endregion

    // override object.Equals
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return this.Equals((RomanNumber)obj);
    }

    // override object.GetHashCode
    public override int GetHashCode() => Number.GetHashCode();

    // override object.ToString
    public override string ToString() => RomanRepresentation;

    #region Public operators

    public static RomanNumber operator +(RomanNumber value1, RomanNumber value2)
    {
        return new RomanNumber(value1.Number + value2.Number);
    }

    public static RomanNumber operator -(RomanNumber value1, RomanNumber value2)
    {
        return new RomanNumber(value1.Number - value2.Number);
    }

    public static RomanNumber operator *(RomanNumber value1, RomanNumber value2)
    {
        return new RomanNumber(value1.Number * value2.Number);
    }

    public static RomanNumber operator /(RomanNumber value1, RomanNumber value2)
    {
        return new RomanNumber(value1.Number / value2.Number);
    }

    public static RomanNumber operator %(RomanNumber value1, RomanNumber value2)
    {
        return new RomanNumber(value1.Number % value2.Number);
    }

    #endregion
}
