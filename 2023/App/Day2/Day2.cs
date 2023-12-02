using App.Common;
using BenchmarkGenerator;
using RunGenerator;

namespace App.Day2;

public static class Day2
{
    [GenerateRun("Day2/Day2.input")]
    [GenerateBenchmark("Day2/Day2.input")]
    public static int RunA(ReadOnlySpan<char> input)
    {
        var result = 0;
        var lineCount = 0;

        foreach (var immutableLine in input.EnumerateLines())
        {
            if (immutableLine.IsEmpty) continue;

            var index = immutableLine.IndexOf(":");

            var line = immutableLine.Slice(index + 2); //Forward to where the data is.

            var lineIsValid = true;

            while (!line.IsEmpty)
            {
                var tokenIndex = line.IndexOf(" ");
                if (tokenIndex == -1) break;
                if (tokenIndex == 2)
                {
                    var value = ParseUtil.ParseIntFast(line.Slice(0, tokenIndex));
                    var color = line[3];
                    if ((color == 'r' && value > 12) ||
                        (color == 'g' && value > 13) ||
                        (color == 'b' && value > 14))
                    {
                        lineIsValid = false;
                        break;
                    }
                }

                line = line.Slice(tokenIndex + 1);
            }

            lineCount += 1;
            if (lineIsValid)
            {
                result += lineCount;
            }
        }

        return result;
    }

    [GenerateRun("Day2/Day2.input")]
    [GenerateBenchmark("Day2/Day2.input")]
    public static int RunB(ReadOnlySpan<char> input)
    {
        var result = 0;

        foreach (var immutableLine in input.EnumerateLines())
        {
            if (immutableLine.IsEmpty) continue;

            var index = immutableLine.IndexOf(":");

            var line = immutableLine.Slice(index + 2); //Forward to where the data is.

            var red = 0;
            var green = 0;
            var blue = 0;

            while (!line.IsEmpty)
            {
                var tokenIndex = line.IndexOf(" ");
                if (tokenIndex == -1) break;
                if (tokenIndex <= 2)
                {
                    var value = ParseUtil.ParseIntFast(line.Slice(0, tokenIndex));
                    var color = line[tokenIndex + 1];

                    if (color == 'r')
                    {
                        if (value > red) red = value;
                    }
                    else if (color == 'g')
                    {
                        if (value > green) green = value;
                    }
                    else if (color == 'b')
                    {
                        if (value > blue) blue = value;
                    }
                }

                line = line.Slice(tokenIndex + 1);
            }

            result += red * green * blue;
        }

        return result;
    }
}