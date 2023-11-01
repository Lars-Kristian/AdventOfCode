using System.Runtime.CompilerServices;

namespace ConsoleApp.Common;

public class ParseUtil
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ParseIntFast(ReadOnlySpan<char> span)
    {
        var result = 0;
        for (var i = 0; i < span.Length; i++)
            result = result * 10 + span[i] - '0';

        return result;
    }
}