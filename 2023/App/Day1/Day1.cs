using BenchmarkGenerator;
using Microsoft.Extensions.Primitives;
using RunGenerator;

namespace App.Day1;

public static class Day1
{

    [GenerateBenchmark("Day1/Day1.input")]
    [GenerateRun("Day1/Day1.input")]
    public static int RunA(ReadOnlySpan<char> input)
    {
        var result = 0;

        var firstIndex = -1;
        var lastIndex = -1;

        while (!input.IsEmpty)
        {
            var tokenIndex = input.IndexOf('\n');
            var line = input.Slice(0, tokenIndex);

            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] >= '0' && line[i] <= '9')
                {
                    firstIndex = i;
                    break;
                }
            }
            
            for (var i = line.Length - 1; i >= 0; i--)
            {
                if (line[i] >= '0' && line[i] <= '9')
                {
                    lastIndex = i;
                    break;
                }
            }

            var a = line[firstIndex] - '0';
            var b = line[lastIndex] - '0';

            result += a * 10 + b;

            firstIndex = -1;
            lastIndex = -1;
            
            input = input.Slice(line.Length + 1);
        }

        return result;
    }

    
    [GenerateBenchmark("Day1/Day1.input")]
    [GenerateRun("Day1/Day1.input")]
    public static int RunB(ReadOnlySpan<char> input)
    {
        string[] tokens = 
        {
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"
        };
        
        Span<int> values = stackalloc int[]
        {
            1,2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9
        };

        var result = 0;

        while (!input.IsEmpty)
        {
            var tokenIndex = input.IndexOf('\n');
            var line = input.Slice(0, tokenIndex);

            var first_index = int.MaxValue;
            var first_value = 0;
            var last_index = int.MinValue;
            var last_value = 0;
            
            for (var i = 0; i < tokens.Length; i++)
            {
                var index = line.IndexOf(tokens[i]);
                if(index == -1) continue;
                if (index < first_index)
                {
                    first_index = index;
                    first_value = values[i];
                }
            }
            
            for (var i = 0; i < tokens.Length; i++)
            {
                var index = line.LastIndexOf(tokens[i]);
                if(index == -1) continue;
                if (index > last_index)
                {
                    last_index = index;
                    last_value = values[i];
                }
            }

            result += first_value * 10 + last_value;

            input = input.Slice(line.Length + 1);
        }

        return result;
    }
    
    [GenerateBenchmark("Day1/Day1.input")]
    [GenerateRun("Day1/Day1.input")]
    public static int RunB2(ReadOnlySpan<char> input)
    {
        var tokenString = "1|2|3|4|5|6|7|8|9|one|two|three|four|five|six|seven|eight|nine";
        Span<char> span = stackalloc char[tokenString.Length];
        tokenString.AsSpan().CopyTo(span);

        var tokenCount = span.Count('|') + 1;

        Span<int> sliceIndex = stackalloc int[tokenCount];
        Span<int> sliceLength = stackalloc int[tokenCount];

        var prevWasDelimiter = true;
        var prevIndex = 0;
        var count = 0;
        for (var i = 0; i < span.Length; i++)
        {
            if (prevWasDelimiter)
            {
                sliceIndex[count] = i;
                prevIndex = i;
                prevWasDelimiter = false;
            }

            if (span[i] == '|')
            {
                sliceLength[count] = i - prevIndex;
                prevWasDelimiter = true;
                count += 1;
            }
        }

        sliceLength[sliceLength.Length - 1] = span.Length - prevIndex;

        var test = span.Slice(sliceIndex[0], sliceLength[0]);
        
        Span<int> values = stackalloc int[]
        {
            1,2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9
        };

        var result = 0;

        while (!input.IsEmpty)
        {
            var tokenIndex = input.IndexOf('\n');
            var line = input.Slice(0, tokenIndex);

            var first_index = int.MaxValue;
            var first_value = 0;
            var last_index = int.MinValue;
            var last_value = 0;
            
            for (var i = 0; i < tokenCount; i++)
            {
                var index = line.IndexOf(span.Slice(sliceIndex[i], sliceLength[i]));
                if(index == -1) continue;
                if (index < first_index)
                {
                    first_index = index;
                    first_value = values[i];
                    if (index == 0) break;
                }
            }
            
            for (var i = 0; i < tokenCount; i++)
            {
                var index = line.LastIndexOf(span.Slice(sliceIndex[i], sliceLength[i]));
                if(index == -1) continue;
                if (index > last_index)
                {
                    last_index = index;
                    last_value = values[i];
                    if (index == line.Length - 1) break;
                }
            }

            result += first_value * 10 + last_value;

            input = input.Slice(line.Length + 1);
        }

        return result;
    }
    
    
    [GenerateBenchmark("Day1/Day1.input")]
    [GenerateRun("Day1/Day1.input")]
    public static int RunB3(ReadOnlySpan<char> input)
    {
        var tokenString = "one|two|three|four|five|six|seven|eight|nine";
        Span<char> span = stackalloc char[tokenString.Length];
        tokenString.AsSpan().CopyTo(span);

        var tokenCount = span.Count('|') + 1;

        Span<int> sliceIndex = stackalloc int[tokenCount];
        Span<int> sliceLength = stackalloc int[tokenCount];

        var prevWasDelimiter = true;
        var prevIndex = 0;
        var count = 0;
        for (var i = 0; i < span.Length; i++)
        {
            if (prevWasDelimiter)
            {
                sliceIndex[count] = i;
                prevIndex = i;
                prevWasDelimiter = false;
            }

            if (span[i] == '|')
            {
                sliceLength[count] = i - prevIndex;
                prevWasDelimiter = true;
                count += 1;
            }
        }

        sliceLength[sliceLength.Length - 1] = span.Length - prevIndex;

        var test = span.Slice(sliceIndex[0], sliceLength[0]);
        
        Span<int> values = stackalloc int[]
        {
            1,2,3,4,5,6,7,8,9
        };

        var result = 0;
            
        while (!input.IsEmpty)
        {
            var tokenIndex = input.IndexOf('\n');
            var line = input.Slice(0, tokenIndex);

            var firstIndex = -1;
            var lastIndex = -1;
            
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] >= '0' && line[i] <= '9')
                {
                    firstIndex = i;
                    break;
                }
            }
        
            for (var i = line.Length - 1; i >= 0; i--)
            {
                if (line[i] >= '0' && line[i] <= '9')
                {
                    lastIndex = i;
                    break;
                }
            }
            
            var firstValue = line[firstIndex] - '0';
            var lastValue = line[lastIndex] - '0';

            if (firstIndex > 2)
            {
                for (var i = 0; i < tokenCount; i++)
                {
                    var index = line.IndexOf(span.Slice(sliceIndex[i], sliceLength[i]));
                    if(index == -1) continue;
                    if (index < firstIndex)
                    {
                        firstIndex = index;
                        firstValue = values[i];
                        if (index == 0) break;
                    }
                }
            }

            if (lastIndex < line.Length - 3)
            {
                for (var i = 0; i < tokenCount; i++)
                {
                    var index = line.LastIndexOf(span.Slice(sliceIndex[i], sliceLength[i]));
                    if(index == -1) continue;
                    if (index > lastIndex)
                    {
                        lastIndex = index;
                        lastValue = values[i];
                        if (index == line.Length - 1) break;
                    }
                }
            }

            result += firstValue * 10 + lastValue;

            input = input.Slice(line.Length + 1);
        }

        return result;
    }
    
}