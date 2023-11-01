using ConsoleApp.Day4;
using FluentAssertions;

namespace Tests;

public static class TestDay4
{
    public class RunAShould
    {
        [Theory]
        [InlineData("2-4,6-8\n2-3,4-5\n5-7,7-9\n2-8,3-7\n6-6,4-6\n2-6,4-8\n", 2)]
        public void ReturnExpectedResult(string input, int expectedResult)
        {
            var result = Day4.RunA(input);
            result.Should().Be(expectedResult);
        }
    }
    
    public class RunBShould
    {
        [Theory]
        [InlineData("2-4,6-8\n2-3,4-5\n5-7,7-9\n2-8,3-7\n6-6,4-6\n2-6,4-8\n", 4)]
        public void ReturnExpectedResult(string input, int expectedResult)
        {
            var result = Day4.RunB(input);
            result.Should().Be(expectedResult);
        }
    }
}