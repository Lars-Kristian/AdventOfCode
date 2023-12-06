using System.Runtime.CompilerServices;
using App.Common;
using BenchmarkGenerator;
using RunGenerator;

namespace App.Day6;

public static class Day6
{

    [GenerateRun("Day6/Day6.input")]
    [GenerateBenchmark("Day6/Day6.input")]
    public static long RunA(ReadOnlySpan<char> input)
    {
        Span<long> times = stackalloc long[20];
        Span<long> distances = stackalloc long[20];
        
        var lines = input.EnumerateLines();
        lines.MoveNext();
        ParseNumbers(lines.Current, ref times);
        lines.MoveNext();
        ParseNumbers(lines.Current, ref distances);

        long result = 1;
        for (var i = 0; i < times.Length; i++)
        {
            result *= Solve(times[i], distances[i]);
        }
        
        return result;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ParseNumbers(ReadOnlySpan<char> line, ref Span<long> result)
    {
        line = line.Slice(line.IndexOf(':') + 1); //Skip prefix
        line = line.Slice(line.IndexOfAnyExcept(' '));
        
        var count = 0;
        while (!line.IsEmpty)
        {
            var tokenIndex = line.IndexOf(' ');
            if (tokenIndex == -1)
            {
                var lastNumber = line.Slice(0, line.Length);
                result[count] = ParseUtil.ParseLongFast(lastNumber);
                count += 1;
                break;
            }

            var number = line.Slice(0, tokenIndex);
            result[count] = ParseUtil.ParseLongFast(number);
            line = line.Slice(number.Length + 1);
            line = line.Slice(line.IndexOfAnyExcept(' '));
            count += 1;
        }

        result = result.Slice(0, count);
    }
    
    [GenerateRun("Day6/Day6.input")]
    [GenerateBenchmark("Day6/Day6.input")]
    public static long RunAInitialSolution(ReadOnlySpan<char> input)
    {
        Span<long> times = stackalloc long[20];
        Span<long> distances = stackalloc long[20];
        long result = 1;
        
        var lines = input.EnumerateLines();
        lines.MoveNext();
        ParseNumbers(lines.Current, ref times);
        lines.MoveNext();
        ParseNumbers(lines.Current, ref distances);

        for (var i = 0; i < times.Length; i++)
        {
            var count = 0;
            var time = times[i];
            for (var speed = 1; speed < time; speed++)
            {
                var timeRemaining = time - speed;
                var distance = speed * timeRemaining;
                if (distance > distances[i])
                {
                    count += 1;
                }
            }

            result *= count;
        }
        
        return result;
    }
    
    
    [GenerateRun("Day6/Day6.input")]
    [GenerateBenchmark("Day6/Day6.input")]
    public static long RunB(ReadOnlySpan<char> input)
    {
        var lines = input.EnumerateLines();
        lines.MoveNext();
        var timeRecord = RemoveSpacAndParseNumber(lines.Current);
        lines.MoveNext();
        var distanceRecord = RemoveSpacAndParseNumber(lines.Current);
            
        return Solve(timeRecord, distanceRecord);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long RemoveSpacAndParseNumber(ReadOnlySpan<char> line)
    {
        long result = 0;
        for (var i = 0; i < line.Length; i++)
        {
            if(!char.IsDigit(line[i])) continue;
            result = result * 10 + line[i] - '0';
        }

        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long Solve(long time, long distance)
    {
        // T = total time of race
        // D = previous distance record of race
        
        //Button = Speed
        //Time = T - Button = T - Speed
        //Distance = Time * Speed = (T - Speed)*Speed = T*Speed - Speed*Speed
        //0 = T*Speed - Speed*Speed - Distance
        //Quadratic equation
        //(-T + sqrt(T*T-4*(-1)*(-Distance))) / (2 * (-1))
        
        var x1 = (-time + Math.Sqrt(time * time - 4 * (-1) * (-distance))) / (2 * -1);
        var x2 = time - x1;

        var buttonFirst = (long)(x1 + 1); //Ceiling
        var buttonLast = (long)x2; //Flooring
        
        return buttonLast - buttonFirst + 1;
    }
    
    [GenerateRun("Day6/Day6.input")]
    [GenerateBenchmark("Day6/Day6.input")]
    public static long RunBInitialSolution(ReadOnlySpan<char> input)
    {
        var result = 1;
        
        var lines = input.EnumerateLines();
        lines.MoveNext();
        var timeRecord = RemoveSpacAndParseNumber(lines.Current);
        lines.MoveNext();
        var distanceRecord = RemoveSpacAndParseNumber(lines.Current);

        long buttonStart = 0;
        for (long speed = 0; speed < timeRecord; speed++)
        {
            var timeRemaining = timeRecord - speed;
            var distance = speed * timeRemaining;
            if (distance > distanceRecord)
            {
                buttonStart = speed;
                break;
            }
        }

        long buttonEnd = 0;
        for (long speed = timeRecord; speed > 0; speed--)
        {
            var timeRemaining = timeRecord - speed;
            var distance = speed * timeRemaining;
            if (distance > distanceRecord)
            {
                buttonEnd = speed;
                break;
            }
        }
            
        return buttonEnd - buttonStart + 1;
    }
}