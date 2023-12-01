using BenchmarkGenerator;
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
        var tokens = new List<string>()
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
        
        var values = new List<int>()
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
            
            for (var i = 0; i < tokens.Count; i++)
            {
                var index = line.IndexOf(tokens[i]);
                if(index == -1) continue;
                if (index < first_index)
                {
                    first_index = index;
                    first_value = values[i];
                }
            }
            
            for (var i = 0; i < tokens.Count; i++)
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
}