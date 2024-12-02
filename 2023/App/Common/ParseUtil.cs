using System.Runtime.CompilerServices;

namespace App.Common;

public static class ParseUtil
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ParseIntFast(ReadOnlySpan<char> span)
    {
        var result = 0;
        for (var i = 0; i < span.Length; i++)
            result = result * 10 + span[i] - '0';

        return result;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ParseIntFast2(ReadOnlySpan<char> span)
    {
        var result = 0;

        if (span[0] == '-')
        {
            for (var i = 1; i < span.Length; i++)
                result = result * 10 + span[i] - '0';

            result *= -1;
        }
        else
        {
            for (var i = 0; i < span.Length; i++)
                result = result * 10 + span[i] - '0';
        }
        
        return result;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ParseLongFast(ReadOnlySpan<char> span)
    {
        var result = 0L;
        for (var i = 0; i < span.Length; i++)
            result = result * 10 + span[i] - '0';

        return result;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ParseLongIntFast2(ReadOnlySpan<char> span)
    {
        var result = 0L;

        if (span[0] == '-')
        {
            for (var i = 1; i < span.Length; i++)
                result = result * 10 + span[i] - '0';

            result *= -1;
        }
        else
        {
            for (var i = 0; i < span.Length; i++)
                result = result * 10 + span[i] - '0';
        }
        
        return result;
    }
}