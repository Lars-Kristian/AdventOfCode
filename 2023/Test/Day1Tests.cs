using App.Day1;
using FluentAssertions;

namespace Test;

public static class Day1Tests
{
    public class RunAShould
    {
        [Theory]
        [InlineData("1abc2\npqr3stu8vwx\na1b2c3d4e5f\ntreb7uchet\n", 142)]
        public void ReturnResult(string input, int expecedResult)
        {
            var result = Day1.RunA(input);
            result.Should().Be(expecedResult);
        }
    }
    
    public class RunBShould
    {
        [Theory]
        [InlineData("two1nine\neightwothree\nabcone2threexyz\nxtwone3four\n4nineeightseven2\nzoneight234\n7pqrstsixteen\n", 281)]
        public void ReturnResult(string input, int expecedResult)
        {
            var result = Day1.RunB(input);
            result.Should().Be(expecedResult);
        }
    }
}