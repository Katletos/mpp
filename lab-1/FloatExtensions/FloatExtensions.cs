using System.Runtime.CompilerServices;

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
    public unsafe static char[] ToIeee754Representation(this float number)
    {
        var chars = new char[32];
        var bytes = stackalloc byte[sizeof(float)];
        Unsafe.As<byte, float>(ref bytes[0]) = number;
        
        for (var n = 0; n < 4; ++n)
        {
            for (var i = 0; i < 8; ++i)
            {
                chars[8 * n + i] = (bytes[n] >> i & 1) == 1 ? '1' : '0';
            }
        }
        
        return chars;
    }
    
    public static string ToIeee754RepresentationLinq(this float number)
    {
        return BitConverter
            .GetBytes(number)
            .Select(x => Convert.ToString(x, 2))
            .Select(x => x.PadLeft(8, '0'))
            .ToString()!;
    }
}