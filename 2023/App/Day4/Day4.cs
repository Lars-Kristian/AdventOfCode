using BenchmarkGenerator;
using RunGenerator;

namespace App.Day4;

public static class Day4
{
    [GenerateRun("Day4/Day4.input")]
    [GenerateBenchmark("Day4/Day4.input")]
    public static int RunA(ReadOnlySpan<char> input)
    {
        int GetWinningNumbersCount(ReadOnlySpan<char> line)
        {
            var ticketData = line.Slice(line.IndexOf(':') + 1);
            var separatorIndex = ticketData.IndexOf('|');
            var winningNumbers = ticketData.Slice(0, separatorIndex);
            return (winningNumbers.Length - 1) / 3;
        }
        
        int GetTicketNumbersCount(ReadOnlySpan<char> line)
        {
            var ticketData = line.Slice(line.IndexOf(':') + 1);
            var separatorIndex = ticketData.IndexOf('|');
            var ticketNumbers = ticketData.Slice(separatorIndex + 1);
            return ticketNumbers.Length / 3;
        }

        void ParseNumbers(ReadOnlySpan<char> data, Span<byte> buffer)
        {
            var bufferIndex = 0;
            for (var i = 0; i < data.Length; i++)
            {
                if(data[i] == ' ')continue;

                buffer[bufferIndex] = (byte)(buffer[bufferIndex] * 10 + data[i] - '0');

                if (i % 3 == 2) bufferIndex += 1;
            }
        }
        
        Span<int> resultTable = stackalloc int[] {0, 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024};
        var result = 0;
        var lineWidth = input.IndexOf('\n');
        var winningNumberCount = GetWinningNumbersCount(input.Slice(0, lineWidth));
        var ticketNumbersCount = GetTicketNumbersCount(input.Slice(0, lineWidth));

        Span<byte> winningNumberList = stackalloc byte[winningNumberCount];
        Span<byte> ticketNumberList = stackalloc byte[ticketNumbersCount];
        
        foreach (var immutableLine in input.EnumerateLines())
        {
            if (immutableLine.IsEmpty) break;
            
            var ticketData = immutableLine.Slice(immutableLine.IndexOf(':') + 1);
            var separatorIndex = ticketData.IndexOf('|');
            var winningNumbers = ticketData.Slice(0, separatorIndex);
            var ticketNumbers = ticketData.Slice(separatorIndex + 1);

            winningNumberList.Clear();
            ParseNumbers(winningNumbers, winningNumberList);
            
            ticketNumberList.Clear();
            ParseNumbers(ticketNumbers, ticketNumberList);

            var numbersFound = 0;
            for (var i = 0; i < winningNumberList.Length; i++)
            {
                if (ticketNumberList.Contains(winningNumberList[i])) numbersFound += 1;
            }

            result += resultTable[numbersFound];
        }
        
        return result;
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
        int GetWinningNumbersCount(ReadOnlySpan<char> line)
        {
            var ticketData = line.Slice(line.IndexOf(':') + 1);
            var separatorIndex = ticketData.IndexOf('|');
            var winningNumbers = ticketData.Slice(0, separatorIndex);
            return (winningNumbers.Length - 1) / 3;
        }
        
        int GetTicketNumbersCount(ReadOnlySpan<char> line)
        {
            var ticketData = line.Slice(line.IndexOf(':') + 1);
            var separatorIndex = ticketData.IndexOf('|');
            var ticketNumbers = ticketData.Slice(separatorIndex + 1);
            return ticketNumbers.Length / 3;
        }

        void ParseNumbers(ReadOnlySpan<char> data, Span<byte> buffer)
        {
            var bufferIndex = 0;
            for (var i = 0; i < data.Length; i++)
            {
                if(data[i] == ' ')continue;

                buffer[bufferIndex] = (byte)(buffer[bufferIndex] * 10 + data[i] - '0');

                if (i % 3 == 2) bufferIndex += 1;
            }
        }

        
        var lineWidth = input.IndexOf('\n') + 1;
        var lineCount = input.Length / lineWidth;
        Span<int> result = stackalloc int[lineCount];
        result.Fill(1);

        var winningNumberCount = GetWinningNumbersCount(input.Slice(0, lineWidth));
        var ticketNumbersCount = GetTicketNumbersCount(input.Slice(0, lineWidth));
        Span<byte> winningNumberList = stackalloc byte[winningNumberCount];
        Span<byte> ticketNumberList = stackalloc byte[ticketNumbersCount];
        
        var cardNumber = -1;
        foreach (var immutableLine in input.EnumerateLines())
        {
            
            if (immutableLine.IsEmpty) break;
            
            var ticketData = immutableLine.Slice(immutableLine.IndexOf(':') + 1);
            var separatorIndex = ticketData.IndexOf('|');
            var winningNumbers = ticketData.Slice(0, separatorIndex);
            var ticketNumbers = ticketData.Slice(separatorIndex + 1);

            winningNumberList.Clear();
            ParseNumbers(winningNumbers, winningNumberList);
            
            ticketNumberList.Clear();
            ParseNumbers(ticketNumbers, ticketNumberList);

            var numbersFound = 0;
            for (var i = 0; i < winningNumberList.Length; i++)
            {
                if (ticketNumberList.Contains(winningNumberList[i])) numbersFound += 1;
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