using System.Buffers;
using BenchmarkGenerator;
using RunGenerator;

namespace App.Day3;

public static class Day3
{
    [GenerateRun("Day3/Day3.input")]
    [GenerateBenchmark("Day3/Day3.input")]
    public static int RunA(ReadOnlySpan<char> input)
    {
        var result = 0;
        
        var tokens = SearchValues.Create(".0123456789\n");
        var width = input.IndexOf('\n') + 1;
        var index = -1;
        
        Span<int> numbersAroundGear = stackalloc int[6];
        
        var remainingInput = input;
        while (!remainingInput.IsEmpty)
        {
            var tokenIndex = remainingInput.IndexOfAnyExcept(tokens);
            if (tokenIndex == -1) break;

            index += tokenIndex + 1;

            numbersAroundGear.Clear();
            var numbersFound = LookForGearNumber(numbersAroundGear, 0, input, index - width - 1, index - width + 1);
            numbersFound = LookForGearNumber(numbersAroundGear, numbersFound, input, index - 1, index + 1);
            numbersFound = LookForGearNumber(numbersAroundGear, numbersFound, input, index + width - 1,
                index + width + 1);
            
            if (numbersFound > 0)
            {
                for (var i = 0; i < numbersFound; i++)
                {
                    result += numbersAroundGear[i];
                }
            }

            remainingInput = remainingInput.Slice(tokenIndex + 1);
        }

        return result;
    }
    
    [GenerateRun("Day3/Day3.input")]
    [GenerateBenchmark("Day3/Day3.input")]
    public static int RunAInitialSolution(ReadOnlySpan<char> input)
    {
        var tokens = SearchValues.Create(".0123456789\n");

        var width = input.IndexOf('\n') + 1;

        var remainingInput = input;

        var result = 0;
        var index = -1;
        while (!remainingInput.IsEmpty)
        {
            var tokenIndex = remainingInput.IndexOfAnyExcept(tokens);
            if (tokenIndex == -1) break;

            index += tokenIndex + 1;

            result += AddNumber(input, index - width - 1, index - width + 1);
            result += AddNumber(input, index - 1, index + 1);
            result += AddNumber(input, index + width - 1, index + width + 1);

            remainingInput = remainingInput.Slice(tokenIndex + 1);
        }

        return result;
    }

    private static int AddNumber(ReadOnlySpan<char> input, int from, int to)
    {
        var result = 0;
        var index = from;

        while (input[index] >= '0' && input[index] <= '9' && index > 0)
            index -= 1; //Rewind cursor to capture numbers starting before <from>. 
        while (input[index] < '0' || input[index] > '9') index += 1; //Forward cursor to skip '.'-chars.


        var currentNumber = 0;
        while (index <= to)
        {
            if (input[index] < '0' || input[index] > '9')
            {
                result += currentNumber;
                currentNumber = 0;
                index += 1;
                continue;
            }

            currentNumber = currentNumber * 10 + input[index] - '0';
            index += 1;
        }

        if (currentNumber != 0)
        {
            while (input[index] >= '0' && input[index] <= '9' && index < input.Length - 1)
            {
                currentNumber = currentNumber * 10 + input[index] - '0';
                index += 1;
            }

            result += currentNumber;
        }

        return result;
    }


    [GenerateRun("Day3/Day3.input")]
    [GenerateBenchmark("Day3/Day3.input")]
    public static long RunB(ReadOnlySpan<char> input)
    {
        long result = 0;

        var width = input.IndexOf('\n') + 1;
        var index = -1;

        Span<int> numbersAroundGear = stackalloc int[6];

        var remainingInput = input;
        while (!remainingInput.IsEmpty)
        {
            var tokenIndex = remainingInput.IndexOf('*');
            if (tokenIndex == -1) break;

            index += tokenIndex + 1;

            numbersAroundGear.Clear();
            var numbersFound = LookForGearNumber(numbersAroundGear, 0, input, index - width - 1, index - width + 1);
            numbersFound = LookForGearNumber(numbersAroundGear, numbersFound, input, index - 1, index + 1);
            numbersFound = LookForGearNumber(numbersAroundGear, numbersFound, input, index + width - 1,
                index + width + 1);

            if (numbersFound > 1)
            {
                var product = 1;

                for (var i = 0; i < numbersFound; i++)
                {
                    product *= numbersAroundGear[i];
                }

                result += product;
            }

            remainingInput = remainingInput.Slice(tokenIndex + 1);
        }

        return result;
    }

    private static int LookForGearNumber(Span<int> numbersAroundGear, int numberIndex, ReadOnlySpan<char> input,
        int from, int to)
    {
        var index = from;

        while (input[index] >= '0' && input[index] <= '9' && index > 0)
            index -= 1; //Rewind cursor to capture numbers starting before <from>. 
        while (input[index] < '0' || input[index] > '9') index += 1; //Forward cursor to skip '.'-chars.

        var currentNumber = 0;
        while (index <= to)
        {
            if (input[index] < '0' || input[index] > '9')
            {
                if (currentNumber > 0)
                {
                    numbersAroundGear[numberIndex] = currentNumber;
                    numberIndex += 1;
                }

                currentNumber = 0;
                index += 1;
                continue;
            }

            currentNumber = currentNumber * 10 + input[index] - '0';
            index += 1;
        }

        if (currentNumber == 0) return numberIndex;

        while (input[index] >= '0' && input[index] <= '9' && index < input.Length - 1)
        {
            currentNumber = currentNumber * 10 + input[index] - '0';
            index += 1;
        }

        if (currentNumber > 0)
        {
            numbersAroundGear[numberIndex] = currentNumber;
            numberIndex += 1;
        }

        return numberIndex;
    }

    [GenerateRun("Day3/Day3.input")]
    [GenerateBenchmark("Day3/Day3.input")]
    public static long RunTest(ReadOnlySpan<char> input)
    {
        //If we replaced all non-interesting input with '.' maybe searching would be faster.
        
        Span<char> tmpInput = stackalloc char[input.Length];
        input.CopyTo(tmpInput);

        tmpInput.Replace('\n', '.');
        tmpInput.Replace('0', '.');
        tmpInput.Replace('1', '.');
        tmpInput.Replace('2', '.');
        tmpInput.Replace('3', '.');
        tmpInput.Replace('4', '.');
        tmpInput.Replace('5', '.');
        tmpInput.Replace('6', '.');
        tmpInput.Replace('7', '.');
        tmpInput.Replace('8', '.');
        tmpInput.Replace('9', '.');

        return tmpInput.Length;
    }
}