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
    
    public static long ParseLongFast(ReadOnlySpan<char> span)
    {
        long result = 0;
        for (var i = 0; i < span.Length; i++)
            result = result * 10 + span[i] - '0';

        return result;
    }
}