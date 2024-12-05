using System.Buffers;
using System.Text.RegularExpressions;
using BenchmarkGenerator;
using RunGenerator;

namespace AdventOfCode2024.App.Day4;

public class Day4
{
    
    [GenerateRun("Day4/Day4.input")]
    //[GenerateRun("Day4/Day4-test.input")]
    [GenerateBenchmark("Day4/Day4.input")]
    public static int RunA(ReadOnlySpan<char> input)
    {
        var result = 0;

        var xCount = 0;

        var width = input.IndexOf('\n') + 1;
        var height = input.Length / width;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if(input[y * width + x] != 'X') continue;
                xCount += 1;
                
                if (x < width - 3 &&
                    input[y * width + x + 1] == 'M' &&
                    input[y * width + x + 2] == 'A' &&
                    input[y * width + x + 3] == 'S')
                {
                    result += 1;
                }

                if (x >= 3 &&
                    input[y * width + x - 1] == 'M' &&
                    input[y * width + x - 2] == 'A' &&
                    input[y * width + x - 3] == 'S')
                {
                    result += 1;
                }

                if (y >= 3 &&
                    input[(y - 1) * width + x] == 'M' &&
                    input[(y - 2) * width + x] == 'A' &&
                    input[(y - 3) * width + x] == 'S')
                {
                    result += 1;
                }

                if (y < height - 3 &&
                    input[(y + 1) * width + x] == 'M' &&
                    input[(y + 2) * width + x] == 'A' &&
                    input[(y + 3) * width + x] == 'S')
                {
                    result += 1;
                }

                if (x >= 3 && y >= 3 &&
                    input[(y - 1) * width + x - 1] == 'M' &&
                    input[(y - 2) * width + x - 2] == 'A' &&
                    input[(y - 3) * width + x - 3] == 'S')
                {
                    result += 1;
                }

                if (x < width - 3 && y < height - 3 &&
                    input[(y + 1) * width + x + 1] == 'M' &&
                    input[(y + 2) * width + x + 2] == 'A' &&
                    input[(y + 3) * width + x + 3] == 'S')
                {
                    result += 1;
                }
                
                if (x < width - 3 && y >= 3 &&
                    input[(y - 1) * width + x + 1] == 'M' &&
                    input[(y - 2) * width + x + 2] == 'A' &&
                    input[(y - 3) * width + x + 3] == 'S')
                {
                    result += 1;
                }
                
                if (x >= 3 && y < height - 3 &&
                    input[(y + 1) * width + x - 1] == 'M' &&
                    input[(y + 2) * width + x - 2] == 'A' &&
                    input[(y + 3) * width + x - 3] == 'S')
                {
                    result += 1;
                }
            }
        }

        return result;
    }
    
    
    //InComplete
    [GenerateRun("Day4/Day4.input")]
    //[GenerateRun("Day4/Day4-test.input")]
    [GenerateBenchmark("Day4/Day4.input")]
    public static int RunA2(ReadOnlySpan<char> input)
    {
        var result = 0;

        var width = input.IndexOf('\n') + 1;
        var height = input.Length / width;
        
        Span<char> data = stackalloc char[input.Length];
        
        

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                data[x * height + y] = input[y * width + x];
            }
        }
        
        foreach (var _ in Day4Regex.XmasRegex().EnumerateMatches(data))
        {
            result += 1;
        }

        return result;
    }
    
    [GenerateRun("Day4/Day4.input")]
    //[GenerateRun("Day4/Day4-test.input")]
    [GenerateBenchmark("Day4/Day4.input")]
    public static int RunB(ReadOnlySpan<char> input)
    {
        var result = 0;

        var width = input.IndexOf('\n') + 1;
        var height = input.Length / width;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if(input[y * width + x] != 'A') continue;
                if(!(x > 0 && y > 0 && x < width - 1 && y < height - 1)) continue;


                if ((input[(y - 1) * width + x - 1] == 'M' &&
                    input[(y + 1) * width + x + 1] == 'S') ||
                    (input[(y - 1) * width + x - 1] == 'S' &&
                     input[(y + 1) * width + x + 1] == 'M'))
                {
                    if ((input[(y + 1) * width + x - 1] == 'M' &&
                         input[(y - 1) * width + x + 1] == 'S') ||
                        (input[(y + 1) * width + x - 1] == 'S' &&
                         input[(y - 1) * width + x + 1] == 'M'))
                    {
                        result += 1;
                    }   
                }
            }
        }

        return result;
    }
    
    [GenerateRun("Day4/Day4.input")]
    //[GenerateRun("Day4/Day4-test.input")]
    [GenerateBenchmark("Day4/Day4.input")]
    public static int RunB2(ReadOnlySpan<char> input)
    {
        var result = 0;

        var width = input.IndexOf('\n') + 1;
        var height = input.Length / width;

        for (var y = 1; y < height - 1; y++)
        {
            for (var x = 1; x < width - 1; x++)
            {
                if(input[y * width + x] != 'A') continue;

                var topLeft = (y - 1) * width + x - 1;
                var topRight = (y - 1) * width + x + 1;
                var bottomLeft = (y + 1) * width + x - 1;
                var bottomRight = (y + 1) * width + x + 1;

                if ((input[topLeft] != 'M' ||
                     input[bottomRight] != 'S') &&
                    (input[topLeft] != 'S' ||
                     input[bottomRight] != 'M')) continue;
                
                if ((input[topRight] != 'M' ||
                     input[bottomLeft] != 'S') &&
                    (input[topRight] != 'S' ||
                     input[bottomLeft] != 'M')) continue;
                
                result += 1;
            }
        }

        return result;
    }
    
    
    [GenerateRun("Day4/Day4.input")]
    //[GenerateRun("Day4/Day4-test.input")]
    [GenerateBenchmark("Day4/Day4.input")]
    public static int RunB3(ReadOnlySpan<char> input)
    {
        var m = input.IndexOf('\n');
        var rowLength = m + 1;
        var res = 0;
        for (var i = rowLength; i < input.Length - rowLength; i++)
        {
            if (input[i] != 'A') continue;

            var topLeft = i - rowLength - 1;
            var topRight = i - rowLength + 1;
            var bottomLeft = i + rowLength - 1;
            var bottomRight = i + rowLength + 1;
            
            if ((input[topLeft] != 'M' ||
                 input[bottomRight] != 'S') &&
                (input[topLeft] != 'S' ||
                 input[bottomRight] != 'M') ||
                (input[topRight] != 'M' ||
                 input[bottomLeft] != 'S') &&
                (input[topRight] != 'S' ||
                 input[bottomLeft] != 'M'))
            {
                continue;
            }

            res += 1;
        }

        return res;
    }
    
    [GenerateRun("Day4/Day4.input")]
    //[GenerateRun("Day4/Day4-test.input")]
    [GenerateBenchmark("Day4/Day4.input")]
    public static int RunB4(ReadOnlySpan<char> input)
    {
        var m = input.IndexOf('\n');
        var rowLength = m + 1;

        var currentInput = input.Slice(rowLength + 1, input.Length - (rowLength + 1) * 2);
        var currentIndex = rowLength;
        
        var res = 0;

        while (!currentInput.IsEmpty)
        {
            var index = currentInput.IndexOf('A');
            if (index == -1) break;

            currentInput = currentInput.Slice(index + 1);
            currentIndex += index + 1;
            
            var topLeft = currentIndex - rowLength - 1;
            var topRight = currentIndex - rowLength + 1;
            var bottomLeft = currentIndex + rowLength - 1;
            var bottomRight = currentIndex + rowLength + 1;

            if ((input[topLeft] != 'M' ||
                 input[bottomRight] != 'S') &&
                (input[topLeft] != 'S' ||
                 input[bottomRight] != 'M'))
            {
                continue;
            }

            if ((input[topRight] != 'M' ||
                 input[bottomLeft] != 'S') &&
                (input[topRight] != 'S' ||
                 input[bottomLeft] != 'M'))
            {
                continue;
            }

            res += 1;
        }

        return res;
    }
}

public partial class Day4Regex
{
    [GeneratedRegex("XMAS|SAMX")]
    public static partial Regex XmasRegex();
    
    public static SearchValues<string>  MySearchValues = SearchValues.Create(new[] { "XMAS", "SAMX"}, StringComparison.Ordinal);
}