using System.Runtime.CompilerServices;
using BenchmarkGenerator;
using RunGenerator;

namespace App.Day4;

public static class Day4
{
    [GenerateRun("Day4/Day4.input")]
    [GenerateBenchmark("Day4/Day4.input")]
    public static int RunA(ReadOnlySpan<char> input)
    {
        var result = 0;
        
        var lineWidth = input.IndexOf('\n');
        var startTokenIndex = input.IndexOf(':') + 1;
        var separatorTokenIndex = input.IndexOf('|');
        var winNumbersCount = (separatorTokenIndex - startTokenIndex - 1) / 3;
        var ticketNumbersCount = (lineWidth - separatorTokenIndex - 1) / 3;

        Span<int> resultTable = stackalloc int[] {0, 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024};
        Span<byte> winNumberList = stackalloc byte[winNumbersCount];
        Span<byte> ticketNumberList = stackalloc byte[ticketNumbersCount];
        
        foreach (var immutableLine in input.EnumerateLines())
        {
            if (immutableLine.IsEmpty) break;
            
            var winNumbers = immutableLine.Slice(startTokenIndex, separatorTokenIndex - startTokenIndex - 1);
            var ticketNumbers = immutableLine.Slice(separatorTokenIndex + 1);

            ParseNumbers(winNumbers, winNumberList);
            ParseNumbers(ticketNumbers, ticketNumberList);

            var numbersFound = 0;
            for (var i = 0; i < winNumberList.Length; i++)
            {
                if (ticketNumberList.Contains(winNumberList[i])) numbersFound += 1;
            }

            result += resultTable[numbersFound];
        }
        
        return result;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ParseNumbers(ReadOnlySpan<char> input, Span<byte> result)
    {
        for (var resultIndex = 0; resultIndex < result.Length; resultIndex++)
        {
            var number = 0;
            for (var a = 0; a < 3; a++)
            {
                if(input[resultIndex * 3 + a] == ' ') continue;
                number = number * 10 + input[resultIndex * 3 + a] - '0';
            }

            result[resultIndex] = (byte)number;
        }
    }
    
    [GenerateRun("Day4/Day4.input")]
    [GenerateBenchmark("Day4/Day4.input")]
    public static int RunAInitialSolution(ReadOnlySpan<char> input)
    {
        Span<int> resultTable = stackalloc int[] {0, 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024};
        var result = 0;
        
        foreach (var immutableLine in input.EnumerateLines())
        {
            if (immutableLine.IsEmpty) break;
            
            var ticketData = immutableLine.Slice(immutableLine.IndexOf(':') + 1);
            var separatorIndex = ticketData.IndexOf('|');
            var winningNumbers = ticketData.Slice(0, separatorIndex);
            var ticketNumbers = ticketData.Slice(separatorIndex + 1);

            var winningNumberCount = (winningNumbers.Length - 1) / 3;

            var numbersFound = 0;
            for (var i = 0; i < winningNumberCount; i++)
            {
                var numberToCheck = winningNumbers.Slice((i * 3), 3);
                var found = ticketNumbers.IndexOf(numberToCheck);
                if (found != -1) numbersFound += 1;
            }

            result += resultTable[numbersFound];
        }
        
        return result;
    }

    
    
    [GenerateRun("Day4/Day4.input")]
    [GenerateBenchmark("Day4/Day4.input")]
    public static long RunB(ReadOnlySpan<char> input)
    {   
        var lineWidth = input.IndexOf('\n') + 1;
        var lineCount = input.Length / lineWidth;
        var startTokenIndex = input.IndexOf(':') + 1;
        var separatorTokenIndex = input.IndexOf('|');
        var winNumbersCount = (separatorTokenIndex - startTokenIndex - 1) / 3;
        var ticketNumbersCount = (lineWidth - separatorTokenIndex - 1) / 3;

        Span<byte> winNumberList = stackalloc byte[winNumbersCount];
        Span<byte> ticketNumberList = stackalloc byte[ticketNumbersCount];
        
        Span<int> result = stackalloc int[lineCount];
        result.Fill(1);
        
        var cardNumber = -1;
        foreach (var immutableLine in input.EnumerateLines())
        {
            
            if (immutableLine.IsEmpty) break;
            
            var winNumbers = immutableLine.Slice(startTokenIndex, separatorTokenIndex - startTokenIndex - 1);
            var ticketNumbers = immutableLine.Slice(separatorTokenIndex + 1);

            ParseNumbers(winNumbers, winNumberList);
            ParseNumbers(ticketNumbers, ticketNumberList);

            var numbersFound = 0;
            for (var i = 0; i < winNumberList.Length; i++)
            {
                if (ticketNumberList.Contains(winNumberList[i])) numbersFound += 1;
            }

            cardNumber += 1;
            var numberOfCopies = result[cardNumber];
            for (var i = 1; i <= numbersFound; i++)
            {
                if(cardNumber + i >= result.Length) break;
                
                result[cardNumber + i] += numberOfCopies;
            }
        }

        long sum = 0;
        for (var i = 0; i < result.Length; i++)
        {
            sum += result[i];
        }
        
        return sum;
    }

    
    [GenerateRun("Day4/Day4.input")]
    [GenerateBenchmark("Day4/Day4.input")]
    public static long RunBInitialSolution(ReadOnlySpan<char> input)
    {
        var lineWidth = input.IndexOf('\n') + 1;
        var lineCount = input.Length / lineWidth;
        Span<int> result = stackalloc int[lineCount];
        result.Fill(1);

        var cardNumber = -1;
        foreach (var immutableLine in input.EnumerateLines())
        {
            if (immutableLine.IsEmpty) break;
            
            var ticketData = immutableLine.Slice(immutableLine.IndexOf(':') + 1);
            var separatorIndex = ticketData.IndexOf('|');
            var winNumbers = ticketData.Slice(0, separatorIndex);
            var ticketNumbers = ticketData.Slice(separatorIndex + 1);

            var winNumberCount = (winNumbers.Length - 1) / 3;

            var numbersFound = 0;
            for (var i = 0; i < winNumberCount; i++)
            {
                var numberToCheck = winNumbers.Slice((i * 3), 3);
                var found = ticketNumbers.IndexOf(numberToCheck);
                if (found != -1) numbersFound += 1;
            }

            cardNumber += 1;
            var numberOfCopies = result[cardNumber];
            for (var i = 1; i <= numbersFound; i++)
            {
                if(cardNumber + i >= result.Length) break;
                
                result[cardNumber + i] += numberOfCopies;
            }
        }

        long sum = 0;
        for (var i = 0; i < result.Length; i++)
        {
            sum += result[i];
        }
        
        return sum;
    }
}