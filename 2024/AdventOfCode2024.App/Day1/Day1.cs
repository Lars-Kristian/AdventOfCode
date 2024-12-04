using System.Globalization;
using System.Numerics.Tensors;
using AdventOfCode2024.App.Common;
using BenchmarkGenerator;
using RunGenerator;

namespace AdventOfCode2024.App.Day1;

public class Day1
{
    [GenerateRun("Day1/Day1.input")]
    //[GenerateRun("Day1/Day1-test.input")]
    [GenerateBenchmark("Day1/Day1.input")]
    public static int RunA(ReadOnlySpan<char> input)
    {
        Span<int> leftData = stackalloc int[1000];
        Span<int> rightData = stackalloc int[1000];
        
        var lines = input.Split('\n');
        var indexCount = 0;
        foreach (var lineRange in lines)
        {
            var line = input[lineRange];
            if (line.IsEmpty) break;
            var tokens = line.Split("   ");

            tokens.MoveNext();
            leftData[indexCount] = int.Parse(line[tokens.Current]);
            
            tokens.MoveNext();
            rightData[indexCount] = int.Parse(line[tokens.Current]);
            
            indexCount += 1;
        }
        
        leftData = leftData.Slice(0, indexCount);
        rightData = rightData.Slice(0, indexCount);
        
        leftData.Sort();
        rightData.Sort();
        
        var result = 0;

        for (var i = 0; i < leftData.Length; i++)
        {
            result += Math.Abs(leftData[i] - rightData[i]);
        }

        return result;
    }
    
    [GenerateRun("Day1/Day1.input")]
    //[GenerateRun("Day1/Day1-test.input")]
    [GenerateBenchmark("Day1/Day1.input")]
    public static int RunA2(ReadOnlySpan<char> input)
    {
        Span<int> leftData = stackalloc int[1000];
        Span<int> rightData = stackalloc int[1000];
        
        Span<int> data = stackalloc int[1000];
        
        var lines = input.Split('\n');
        var indexCount = 0;
        foreach (var lineRange in lines)
        {
            var line = input[lineRange];
            if (line.IsEmpty) break;
            var tokens = line.Split("   ");

            /*
            tokens.MoveNext();
            int.TryParse(line[tokens.Current], NumberStyles.Integer, NumberFormatInfo.CurrentInfo,
                out leftData[indexCount]);
            tokens.MoveNext();
            int.TryParse(line[tokens.Current], NumberStyles.Integer, NumberFormatInfo.CurrentInfo,
                out rightData[indexCount]);
            */
            tokens.MoveNext();
            leftData[indexCount] = ParseUtil.ParseIntFast(line[tokens.Current]);
            
            tokens.MoveNext();
            rightData[indexCount] = ParseUtil.ParseIntFast(line[tokens.Current]);
            
            indexCount += 1;
        }
        
        leftData = leftData.Slice(0, indexCount);
        rightData = rightData.Slice(0, indexCount);
        data = data.Slice(0, indexCount);
        
        leftData.Sort();
        rightData.Sort();
        
        TensorPrimitives.Subtract(leftData, rightData, data);
        TensorPrimitives.Abs(data, data);
        return TensorPrimitives.Sum<int>(data);
    }
    
    [GenerateRun("Day1/Day1.input")]
    //[GenerateRun("Day1/Day1-test.input")]
    [GenerateBenchmark("Day1/Day1.input")]
    public static long RunB(ReadOnlySpan<char> input)
    {
        Span<int> leftData = stackalloc int[1000];
        Span<int> rightData = stackalloc int[1000];
        
        var lines = input.Split('\n');
        var indexCount = 0;
        foreach (var lineRange in lines)
        {
            var line = input[lineRange];
            if (line.IsEmpty) break;
            var tokens = line.Split("   ");

            tokens.MoveNext();
            leftData[indexCount] = ParseUtil.ParseIntFast(line[tokens.Current]); // int.Parse(line[tokens.Current]);
            
            tokens.MoveNext();
            rightData[indexCount] = ParseUtil.ParseIntFast(line[tokens.Current]); //int.Parse(line[tokens.Current]);
            
            indexCount += 1;
        }
        
        leftData = leftData.Slice(0, indexCount);
        rightData = rightData.Slice(0, indexCount);
        
        leftData.Sort();
        rightData.Sort();
        
        var result = 0L;
        
        var rightIndex = 0;
        for (var leftIndex = 0; leftIndex < leftData.Length; leftIndex++)
        {
            var leftNumber = leftData[leftIndex];
            var count = 0;
            while (rightIndex < rightData.Length && rightData[rightIndex] <= leftNumber)
            {
                if (leftNumber == rightData[rightIndex])
                {
                    count += 1;
                }
                rightIndex++;
            }

            result += leftNumber * count;
        }

        return result;
    }
}