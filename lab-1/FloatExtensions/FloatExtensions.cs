using System.Runtime.CompilerServices;
using System.Text;

namespace lab_1;

/// <summary>
/// This class performs float extensions.
/// </summary>
public static class FloatExtensions
{
    /// <summary>
    /// This class performs IEEE-754 float representation.
    /// </summary>
    /// <param name="number">The value to represent.</param>￥
    /// <returns>Return char[].</returns>
    public static char[] ToIeee754Representation(this float number)
    {
        var chars = new char[32];
        
        var val = Unsafe.BitCast<float, uint>(number);
        for (var n = 0; n < 32; ++n)
        {
            chars[31 - n] = (val >> n & 1) == 1 
                ? '1'
                : '0';
        }
        
        return chars;
    }
    
    public static string ToIeee754RepresentationLinq(this float number)
    {
        var arr = BitConverter
            .GetBytes(number)
            .Select(x => Convert.ToString(x, 2))
            .Select(x => x.PadLeft(8, '0'))
            .ToArray();

        var builder = new StringBuilder();
        builder.Append(arr[3]);
        builder.Append(arr[2]);
        builder.Append(arr[1]);
        builder.Append(arr[0]);

        return builder.ToString();
    }
}