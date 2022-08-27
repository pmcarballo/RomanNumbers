using System.Text;
using System.Text.RegularExpressions;
using RomanNumbers.Extensions;

namespace RomanNumbers;

public struct RomanNumber : IComparable, IComparable<RomanNumber>, IConvertible, IEquatable<RomanNumber>
{
    #region Private constants
    private const int _minValue = 1;
    private const int _maxValue = 3999;
    #endregion
    private static string[] _romanSymbols = { "I", "X", "C", "M" };
    private int _number;

    
    public static readonly RomanNumber MinValue = new RomanNumber(_minValue);

    public static readonly RomanNumber MaxValue = new RomanNumber(_maxValue);
    public int Number => _number;

    public string RomanRepresentation => ToRomanNumber(_number);

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

    public RomanNumber(string number)
        : this(ToNumber(number))
    { }

    public static string ToRomanNumber(int number)
    {
        Validate(number);

        var sb = new StringBuilder();
        int currentRomanDigit = 0;

        for (int i = number.Length() - 1; i >= 0; i--)
        {
            int digit = number % 10;
            var rd = String.Join(String.Empty, Enumerable.Repeat(_romanSymbols[currentRomanDigit], digit));
            sb.Insert(0, rd);
            currentRomanDigit++;
            number = number / 10;
        }

        string compact = Compact(sb.ToString());

        return compact;
    }

    public static int ToNumber(string romanNumber)
    {
        if (!IsValid(romanNumber)) throw new FormatException("Input is not a valid Roman Numeral.");

        int result = 0;
        int power = 1;
        string expand = Expand(romanNumber);

        for (int i = 0; i < _romanSymbols.Length; i++)
        {
            string symbol = _romanSymbols[i];
            result += expand.DigitCount(symbol) * power;
            power *= 10;
        }

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
        v = v.Replace("D", "CCCCC");
        v = v.Replace("CD", "CCCC");
        v = v.Replace("XC", "XXXXXXXXX");
        v = v.Replace("L", "XXXXX");
        v = v.Replace("XL", "XXXX");
        v = v.Replace("IX", "IIIIIIIII");
        v = v.Replace("V", "IIIII");
        v = v.Replace("IV", "IIII");

        return v;
    }

    public static bool IsValid(string romanNumber)
    {
        return Regex.IsMatch(romanNumber, "^(?=[MDCLXVI])M*(C[MD]|D?C{0,3})(X[CL]|L?X{0,3})(I[XV]|V?I{0,3})$");
    }

    public int CompareTo(object? obj)
    {
        return Number.CompareTo(obj);
    }

    public int CompareTo(RomanNumber other)
    {
        return this.CompareTo(other);
    }

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

    public bool Equals(RomanNumber other) => Number.Equals(other.Number);

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
}
