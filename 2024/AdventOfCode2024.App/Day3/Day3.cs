using System.Buffers;
using System.Net;
using System.Text.RegularExpressions;
using AdventOfCode2024.App.Common;
using BenchmarkGenerator;
using Microsoft.Extensions.Primitives;
using RunGenerator;

namespace AdventOfCode2024.App.Day3;

public class Day3
{
    
    [GenerateRun("Day3/Day3.input")]
    //[GenerateRun("Day3/Day3-test.input")]
    [GenerateBenchmark("Day3/Day3.input")]
    public static int RunA(ReadOnlySpan<char> input)
    {
        var result = 0;
            
        foreach (var match in Day3Regex.MulRegex().EnumerateMatches(input))
        {
            var span = input.Slice(match.Index, match.Length);

            var numberMatch = Day3Regex.MulNumberRegex().EnumerateMatches(span);

            numberMatch.MoveNext();
            var aSpan = span.Slice(numberMatch.Current.Index, numberMatch.Current.Length);
            var a = ParseUtil.ParseIntFast(aSpan);
            
            numberMatch.MoveNext();
            var bSpan = span.Slice(numberMatch.Current.Index, numberMatch.Current.Length);
            var b = ParseUtil.ParseIntFast(bSpan);

            result += a * b;
        }
        
        return result;
    }
    
    [GenerateRun("Day3/Day3.input")]
    //[GenerateRun("Day3/Day3-test.input")]
    [GenerateBenchmark("Day3/Day3.input")]
    public static int RunB(ReadOnlySpan<char> input)
    {
        var result = 0;

        Range prevRange = default;
        
        foreach (var splitRange in Day3Regex.DoDontRegex().EnumerateSplits(input))
        {
            var split = input[splitRange];

            var testRange = new Range(prevRange.End, splitRange.Start);

            var testSpan = input[testRange];

            if (testSpan.IsEmpty || testSpan.IndexOf("do()") == 0)
            {
                foreach (var match in Day3Regex.MulRegex().EnumerateMatches(split))
                {
                    var span = split.Slice(match.Index, match.Length);

                    var numberMatch = Day3Regex.MulNumberRegex().EnumerateMatches(span);

                    numberMatch.MoveNext();
                    var aSpan = span.Slice(numberMatch.Current.Index, numberMatch.Current.Length);
                    var a = ParseUtil.ParseIntFast(aSpan);
                
                    numberMatch.MoveNext();
                    var bSpan = span.Slice(numberMatch.Current.Index, numberMatch.Current.Length);
                    var b = ParseUtil.ParseIntFast(bSpan);

                    result += a * b;
                }
            }

            prevRange = splitRange;
        }
        
        return result;
    }
    
    [GenerateRun("Day3/Day3.input")]
    //[GenerateRun("Day3/Day3-test.input")]
    [GenerateBenchmark("Day3/Day3.input")]
    public static int RunB2(ReadOnlySpan<char> input)
    {
        var result = 0;

        Range prevRange = default;
        
        foreach (var splitRange in Day3Regex.DoDontRegex().EnumerateSplits(input))
        {
            var doDontRange = new Range(prevRange.End, splitRange.Start);
            var doDontSpan = input[doDontRange];

            if (doDontSpan.IsEmpty || doDontSpan.Length == 4)
            {
                var split = input[splitRange];
                
                foreach (var match in Day3Regex.MulRegex().EnumerateMatches(split))
                {
                    var span = split.Slice(match.Index, match.Length);
                    var index = span.IndexOf(',');
                    
                    var spanA = span.Slice("mul(".Length, index - "mul(".Length);
                    var spanB = span.Slice(index + 1, span.Length - index - 2);
                    var a = ParseUtil.ParseIntFast(spanA);
                    var b = ParseUtil.ParseIntFast(spanB);

                    result += a * b;
                }
            }

            prevRange = splitRange;
        }
        
        return result;
    }
    
    [GenerateRun("Day3/Day3.input")]
    //[GenerateRun("Day3/Day3-test.input")]
    [GenerateBenchmark("Day3/Day3.input")]
    public static int RunB3(ReadOnlySpan<char> input)
    {
        var result = 0;

        var enabled = true;
        var text = new[] { "do()", "don't()", "mul(" };
        var searchValues = SearchValues.Create(text, StringComparison.Ordinal);

        while (!input.IsEmpty)
        {
            var index = input.IndexOfAny(searchValues);
            if (index == -1)
            {
                break;
            }
            
            input = input.Slice(index);
            if (input.StartsWith("do()"))
            {
                enabled = true;
                input = input.Slice("do()".Length);
            }
            else if(input.StartsWith("don't()"))
            {
                enabled = false;
                input = input.Slice("don't()".Length);
            }
            else if(enabled)
            {
                foreach (var match in Day3Regex.StartWithMulRegex().EnumerateMatches(input))
                {
                    var span = input.Slice(match.Index, match.Length);
                    var ofIndex = span.IndexOf(',');

                    var spanA = span.Slice("mul(".Length, ofIndex - "mul(".Length);
                    var spanB = span.Slice(ofIndex + 1, span.Length - ofIndex - 2);
                    var a = ParseUtil.ParseIntFast(spanA);
                    var b = ParseUtil.ParseIntFast(spanB);

                    result += a * b;
                }
                  
                input = input.Slice("mul(".Length);
            }
            else
            {
                input = input.Slice("mul(".Length);
            }
        }
        
        return result;
    }
    
    [GenerateRun("Day3/Day3.input")]
    //[GenerateRun("Day3/Day3-test.input")]
    [GenerateBenchmark("Day3/Day3.input")]
    public static int RunB4(ReadOnlySpan<char> input)
    {
        var result = 0;

        var enabled = true;
        while (!input.IsEmpty)
        {
            if (!enabled)
            {
                var index = input.IndexOf("do()");
                if (index == -1) break;
                enabled = true;
                input = input.Slice(index + "do()".Length - 1);
            }
            else
            {
                var index = input.IndexOfAny(Day3Regex.MySearchValues);
                if (index == -1) break;
                input = input.Slice(index);
                
                if(input.StartsWith("don't()"))
                {
                    enabled = false;
                    input = input.Slice("don't()".Length - 1);
                }
                else
                {
                    foreach (var match in Day3Regex.StartWithMulRegex().EnumerateMatches(input))
                    {
                        var span = input.Slice(match.Index, match.Length);
                        var separatorIndex = span.IndexOf(',');

                        var spanA = span.Slice("mul(".Length, separatorIndex - "mul(".Length);
                        var spanB = span.Slice(separatorIndex + 1, span.Length - separatorIndex - 2);
                        var a = ParseUtil.ParseIntFast(spanA);
                        var b = ParseUtil.ParseIntFast(spanB);

                        result += a * b;
                    }
                  
                    input = input.Slice("mul(".Length);
                }
            }
        }
        
        return result;
    }
}

public partial class Day3Regex
{
    [GeneratedRegex("mul\\((\\d{1,3}),(\\d{1,3})\\)")]
    public static partial Regex MulRegex();
    
    [GeneratedRegex("^mul\\((\\d{1,3}),(\\d{1,3})\\)")]
    public static partial Regex StartWithMulRegex();
    
    [GeneratedRegex("\\d{1,3}")]
    public static partial Regex MulNumberRegex();
    
    [GeneratedRegex("(don't\\(\\)|do\\(\\))")]
    public static partial Regex DoDontRegex();
    
    public static SearchValues<string>  MySearchValues = SearchValues.Create(new[] { "mul(", "don't()"}, StringComparison.Ordinal);
}