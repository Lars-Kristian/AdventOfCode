using App.Common;
using RunsGenerator;

namespace App.Day4;

public static class Day4
{
    [GenerateRunWithData("Day4/Day4.input")]
    public static int RunA(ReadOnlySpan<char> data)
    {
        var result = 0;
        while (data.Length > 0)
        {
            var tokenIndex = data.IndexOf('-');
            var tokenText = data.Slice(0, tokenIndex);
            var aStart = int.Parse(tokenText);
            data = data.Slice(tokenIndex + 1);

            tokenIndex = data.IndexOf(',');
            tokenText = data.Slice(0, tokenIndex);
            var aStop = int.Parse(tokenText);
            data = data.Slice(tokenIndex + 1);

            tokenIndex = data.IndexOf('-');
            tokenText = data.Slice(0, tokenIndex);
            var bStart = int.Parse(tokenText);
            data = data.Slice(tokenIndex + 1);

            tokenIndex = data.IndexOf('\n');
            tokenText = data.Slice(0, tokenIndex);
            var bStop = int.Parse(tokenText);
            data = data.Slice(tokenIndex + 1);
            
            if ((bStart >= aStart && bStop <= aStop) || (aStart >= bStart && aStop <= bStop)) 
                result += 1;
        }
        
        return result;
    }
    
    [GenerateBenchmark]
    [GenerateRunWithData("Day4/Day4.input")]
    public static int RunB(ReadOnlySpan<char> data)
    {
        var result = 0;
        while (data.Length > 0)
        {
            var tokenIndex = data.IndexOf('-');
            var tokenText = data.Slice(0, tokenIndex);
            var aStart = ParseUtil.ParseIntFast(tokenText);
            data = data.Slice(tokenIndex + 1);

            tokenIndex = data.IndexOf(',');
            tokenText = data.Slice(0, tokenIndex);
            var aStop = ParseUtil.ParseIntFast(tokenText);
            data = data.Slice(tokenIndex + 1);

            tokenIndex = data.IndexOf('-');
            tokenText = data.Slice(0, tokenIndex);
            var bStart = ParseUtil.ParseIntFast(tokenText);
            data = data.Slice(tokenIndex + 1);

            tokenIndex = data.IndexOf('\n');
            tokenText = data.Slice(0, tokenIndex);
            var bStop = ParseUtil.ParseIntFast(tokenText);
            data = data.Slice(tokenIndex + 1);
            
            if (!(aStart > bStop || aStop < bStart || bStart > aStop || bStop < aStart)) 
                result += 1;
        }
        
        return result;
    }
    
    public static int RunBTest(ReadOnlySpan<char> data)
    {
        Span<int> dataAsInt = stackalloc int[data.Length];
        for (var i = 0; i < data.Length; i++)
        {
            dataAsInt[i] = data[i] - 48;
        }
        
        return dataAsInt[0];
    }
}