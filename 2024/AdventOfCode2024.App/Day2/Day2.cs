using AdventOfCode2024.App.Common;
using BenchmarkGenerator;
using RunGenerator;

namespace AdventOfCode2024.App.Day2;

public static class Day2
{
    [GenerateRun("Day2/Day2.input")]
    //GenerateRun("Day2/Day2-test.input")]
    [GenerateBenchmark("Day2/Day2.input")]
    public static int RunA(ReadOnlySpan<char> input)
    {
        var result = 0;
        
        var lines = input.Split('\n');
        foreach (var lineRange in lines)
        {
            var line = input[lineRange];
            if (line.IsEmpty) break;
            var tokens = line.Split(' ');
            
            tokens.MoveNext();
            var range = tokens.Current;
            var token = line[range];
            var prevNumber = int.Parse(token);
            
            tokens.MoveNext();
            range = tokens.Current;
            token = line[range];
            var number = int.Parse(token);
            
            var difference = number - prevNumber;
            if(!DifferenceIsInsideValidRange(difference)) continue;
            var isRising = difference > 0;
            prevNumber = number;
            
            var isValid = true;
            foreach (var tokenRange in tokens)
            {
                token = line[tokenRange];
                number = int.Parse(token);
                difference = number - prevNumber;
                
                if (DifferenceIsInsideValidRange(difference) && isRising == difference > 0)
                {
                    prevNumber = number;
                    continue;
                }
                
                isValid = false;
                break;
            }

            if (isValid) result += 1;
        }
        
        return result;
    }

    private static bool DifferenceIsInsideValidRange(int number)
    {
        return number is not (0 or < -3 or > 3);
    }
    
    [GenerateRun("Day2/Day2.input")]
    //GenerateRun("Day2/Day2-test.input")]
    [GenerateBenchmark("Day2/Day2.input")]
    public static int RunA2(ReadOnlySpan<char> input)
    {
        var result = 0;
        
        var lines = input.Split('\n');
        
        Span<int> dataBuffer = stackalloc int[10];
        Span<int> otherBuffer = stackalloc int[10];
        
        
        foreach (var lineRange in lines)
        {
            dataBuffer.Clear();
            var line = input[lineRange];
            if (line.IsEmpty) break;
            var tokens = line.Split(' ');

            var indexCount = 0;
            foreach (var tokenRange in tokens)
            {
               var token = line[tokenRange];
               var number = int.Parse(token);
               dataBuffer[indexCount] = number;
               indexCount++;
            }
            
            var data = dataBuffer.Slice(0, indexCount);
            
            var skipIndex = -1;
            var index = -1;
            var isValid = true;
            var prevNumber = 0;
            var isRising = false;
            for (var i = 0; i < data.Length; i++)
            {
                if(i == skipIndex) continue;
                
                index += 1;
                var number = data[i];
                if (index == 0)
                {
                    prevNumber = number;
                    continue;
                }

                if (index == 1)
                {
                    var difference = number - prevNumber;
                    if (DifferenceIsInsideValidRange(difference))
                    {
                        isRising = difference > 0;
                        prevNumber = number;
                        continue;
                    }
                    else
                    {
                        isValid = false;
                        break;
                    }
                }
                else
                {
                    var difference = number - prevNumber;
                    if (DifferenceIsInsideValidRange(difference) && isRising == difference > 0)
                    {
                        prevNumber = number;
                        continue;
                    }
            
                    isValid = false;
                    break;
                }
            }

            if (isValid)
            {
                result += 1;
            }
        }
        
        return result;
    }
    
    [GenerateRun("Day2/Day2.input")]
    //GenerateRun("Day2/Day2-test.input")]
    [GenerateBenchmark("Day2/Day2.input")]
    public static int RunB(ReadOnlySpan<char> input)
    {
        var result = 0;
        
        var lines = input.Split('\n');
        Span<int> dataBuffer = stackalloc int[10];
        
        foreach (var lineRange in lines)
        {
            var line = input[lineRange];
            if (line.IsEmpty) break;
            var tokens = line.Split(' ');

            var indexCount = 0;
            foreach (var tokenRange in tokens)
            {
               var token = line[tokenRange];
               var number = ParseUtil.ParseIntFast(token); // int.Parse(token);
               dataBuffer[indexCount] = number;
               indexCount++;
            }
            
            var data = dataBuffer.Slice(0, indexCount);
            for (var skipIndex = -1; skipIndex < data.Length; skipIndex += 1)
            {
                var index = -1;
                var isValid = true;
                var prevNumber = 0;
                var isRising = false;
                for (var i = 0; i < data.Length; i++)
                {
                    if(i == skipIndex) continue;
                    
                    index += 1;
                    var number = data[i];
                    if (index == 0)
                    {
                        prevNumber = number;
                        continue;
                    }

                    if (index == 1)
                    {
                        var difference = number - prevNumber;
                        if (DifferenceIsInsideValidRange(difference))
                        {
                            isRising = difference > 0;
                            prevNumber = number;
                        }
                        else
                        {
                            isValid = false;
                            break;
                        }
                    }
                    else
                    {
                        var difference = number - prevNumber;
                        if (DifferenceIsInsideValidRange(difference) && isRising == difference > 0)
                        {
                            prevNumber = number;
                            continue;
                        }
                
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                {
                    result += 1;
                    break;
                }
                
            }
        }
        
        return result;
    }
}