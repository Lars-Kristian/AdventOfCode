using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using AdventOfCode2024.App.Common;
using BenchmarkGenerator;
using RunGenerator;

namespace AdventOfCode2024.App.Day5;

public class Day5
{
    public enum LookupValues : byte
    {
        Undefined = 0,
        Left = 1,
        Right = 2
    }
    
    [GenerateRun("Day5/Day5.input")]
    //[GenerateRun("Day5/Day5-test.input")]
    [GenerateBenchmark("Day5/Day5.input")]
    public static int RunA(ReadOnlySpan<char> input)
    {
        var result = 0;
        
        Span<LookupValues> lookup = stackalloc LookupValues[100 * 100];
        
        var lineEnumerator = Day5Regex.LineRegex().EnumerateSplits(input);
        
        while (lineEnumerator.MoveNext())
        {
            var range = lineEnumerator.Current;
            var line = input[range];
            if (line.IsEmpty) break;
            
            var left = ParseUtil.ParseIntFast(line.Slice(0, 2));
            var right = ParseUtil.ParseIntFast(line.Slice(3, 2));

            lookup[left * 100 + right] = LookupValues.Left;
            lookup[right * 100 + left] = LookupValues.Right;
        }
        
        Span<int> numbersBuffer = stackalloc int[100];
        while (lineEnumerator.MoveNext())
        {
            var range = lineEnumerator.Current;
            var line = input[range];
            if (line.IsEmpty) break;

            var indexCount = 0;
            foreach (var numberRange in Day5Regex.CommaRegex().EnumerateSplits(line))
            {
                numbersBuffer[indexCount] = ParseUtil.ParseIntFast(line[numberRange]);
                indexCount += 1;
            }

            var numbers = numbersBuffer.Slice(0, indexCount);

            var success = true;
            for (var i = 0; i < numbers.Length; i++)
            {
                var n1 = numbers[i];
                for (var a = i + 1; a < numbers.Length; a++)
                {
                    var n2 = numbers[a];
                    if (lookup[n1 * 100 + n2] == LookupValues.Right)
                    {
                        success = false;
                        break;
                    }
                }
                
                if(!success) break;
            }

            if (!success) continue;
            
            int midIndex = numbers.Length / 2;
            result += numbers[midIndex];
        }
        
        return result;
    }
    
    [GenerateRun("Day5/Day5.input")]
    //[GenerateRun("Day5/Day5-test.input")]
    [GenerateBenchmark("Day5/Day5.input")]
    public static int RunA2(ReadOnlySpan<char> input)
    {
        var result = 0;
        
        Span<LookupValues> lookup = stackalloc LookupValues[100 * 100];
        
        var lineEnumerator = input.EnumerateLines();
        
        while (lineEnumerator.MoveNext())
        {
            var line = lineEnumerator.Current;
            if (line.IsEmpty) break;
            
            var left = ParseUtil.ParseIntFast(line.Slice(0, 2));
            var right = ParseUtil.ParseIntFast(line.Slice(3, 2));

            //lookup[left * 100 + right] = LookupValues.Left;
            lookup[right * 100 + left] = LookupValues.Right;
        }
        
        Span<int> numbersBuffer = stackalloc int[100];
        while (lineEnumerator.MoveNext())
        {
            var line = lineEnumerator.Current;
            if (line.IsEmpty) break;

            var indexCount = 0;
            foreach (var numberRange in Day5Regex.CommaRegex().EnumerateSplits(line))
            {
                numbersBuffer[indexCount] = ParseUtil.ParseIntFast(line[numberRange]);
                indexCount += 1;
            }

            var numbers = numbersBuffer.Slice(0, indexCount);

            var success = true;
            for (var i = 0; i < numbers.Length; i++)
            {
                var n1 = numbers[i];
                for (var a = i + 1; a < numbers.Length; a++)
                {
                    var n2 = numbers[a];
                    if (lookup[n1 * 100 + n2] == LookupValues.Right)
                    {
                        success = false;
                        break;
                    }
                }
                
                if(!success) break;
            }

            if (!success) continue;
            
            int midIndex = numbers.Length / 2;
            result += numbers[midIndex];
        }
        
        return result;
    }
    
    [GenerateRun("Day5/Day5.input")]
    //[GenerateRun("Day5/Day5-test.input")]
    [GenerateBenchmark("Day5/Day5.input")]
    public static int RunB(ReadOnlySpan<char> input)
    {
        var result = 0;
        
        Span<LookupValues> lookup = stackalloc LookupValues[100 * 100];
        
        var lineEnumerator = Day5Regex.LineRegex().EnumerateSplits(input);
        
        while (lineEnumerator.MoveNext())
        {
            var range = lineEnumerator.Current;
            var line = input[range];
            if (line.IsEmpty) break;
            
            var left = ParseUtil.ParseIntFast(line.Slice(0, 2));
            var right = ParseUtil.ParseIntFast(line.Slice(3, 2));

            lookup[left * 100 + right] = LookupValues.Left;
            lookup[right * 100 + left] = LookupValues.Right;
        }
        
        Span<int> numbersBuffer = stackalloc int[100];
        while (lineEnumerator.MoveNext())
        {
            var range = lineEnumerator.Current;
            var line = input[range];
            if (line.IsEmpty) break;

            var indexCount = 0;
            foreach (var numberRange in Day5Regex.CommaRegex().EnumerateSplits(line))
            {
                numbersBuffer[indexCount] = ParseUtil.ParseIntFast(line[numberRange]);
                indexCount += 1;
            }

            var numbers = numbersBuffer.Slice(0, indexCount);

            var success = true;
            for (var i = 0; i < numbers.Length; i++)
            {
                var n1 = numbers[i];
                for (var a = i + 1; a < numbers.Length; a++)
                {
                    var n2 = numbers[a];
                    if (lookup[n1 * 100 + n2] == LookupValues.Right)
                    {
                        success = false;
                        break;
                    }
                }
                
                if(!success) break;
            }

            if (success) continue;

            for (var i = 0; i < numbers.Length; i++)
            {
                for (var a = 0; a < numbers.Length; a++)
                {
                    if(i == a) continue;
                    if (lookup[numbers[i] * 100 + numbers[a]] == LookupValues.Left)
                    {
                        (numbers[i], numbers[a]) = (numbers[a], numbers[i]);
                    }
                }   
            }
            
            int midIndex = numbers.Length / 2;
            result += numbers[midIndex];
        }
        
        return result;
    }
    
    [GenerateRun("Day5/Day5.input")]
    //[GenerateRun("Day5/Day5-test.input")]
    [GenerateBenchmark("Day5/Day5.input")]
    public static int RunB2(ReadOnlySpan<char> input)
    {
        var result = 0;
        
        Span<LookupValues> lookup = stackalloc LookupValues[100 * 100];
        
        var lineEnumerator = Day5Regex.LineRegex().EnumerateSplits(input);
        
        while (lineEnumerator.MoveNext())
        {
            var range = lineEnumerator.Current;
            var line = input[range];
            if (line.IsEmpty) break;
            
            var left = ParseUtil.ParseIntFast(line.Slice(0, 2));
            var right = ParseUtil.ParseIntFast(line.Slice(3, 2));

            lookup[left * 100 + right] = LookupValues.Left;
            lookup[right * 100 + left] = LookupValues.Right;
        }
        
        Span<int> numbersBuffer = stackalloc int[100];
        while (lineEnumerator.MoveNext())
        {
            var range = lineEnumerator.Current;
            var line = input[range];
            if (line.IsEmpty) break;

            var indexCount = 0;
            foreach (var numberRange in Day5Regex.CommaRegex().EnumerateSplits(line))
            {
                numbersBuffer[indexCount] = ParseUtil.ParseIntFast(line[numberRange]);
                indexCount += 1;
            }

            var numbers = numbersBuffer.Slice(0, indexCount);

            if (CheckForSuccess(numbers, lookup)) continue;

            for (var i = 0; i < numbers.Length; i++)
            {
                for (var a = 0; a < numbers.Length; a++)
                {
                    if(i == a) continue;
                    if (lookup[numbers[i] * 100 + numbers[a]] == LookupValues.Left)
                    {
                        (numbers[i], numbers[a]) = (numbers[a], numbers[i]);
                    }
                }   
            }
            
            int midIndex = numbers.Length / 2;
            result += numbers[midIndex];
        }
        
        return result;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool CheckForSuccess(Span<int> numbers, Span<LookupValues> lookup)
    {
        for (var i = 0; i < numbers.Length; i++)
        {
            var n1 = numbers[i];
            for (var a = i + 1; a < numbers.Length; a++)
            {
                if (lookup[n1 * 100 +  numbers[a]] != LookupValues.Right) continue;
                
                return false;
            }
        }

        return true;
    }
}

public partial class Day5Regex
{
    [GeneratedRegex("\n")]
    public static partial Regex LineRegex();
    
    [GeneratedRegex(",")]
    public static partial Regex CommaRegex();
}
