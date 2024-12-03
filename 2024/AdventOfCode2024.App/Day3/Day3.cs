using System.Net;
using System.Text.RegularExpressions;
using AdventOfCode2024.App.Common;
using BenchmarkGenerator;
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
}

public partial class Day3Regex
{
    [GeneratedRegex("mul\\((\\d{1,3}),(\\d{1,3})\\)")]
    public static partial Regex MulRegex();
    
    [GeneratedRegex("\\d{1,3}")]
    public static partial Regex MulNumberRegex();
    
    [GeneratedRegex("(don't\\(\\)|do\\(\\))")]
    public static partial Regex DoDontRegex();
}