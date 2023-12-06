using App.Day6;
using FluentAssertions;

namespace Test;

public static class Day6Tests
{
    public class RunAShould
    {
        [Fact]
        public void ReturnExpectedResult()
        {
            var input = @"Time:      7  15   30
Distance:  9  40  200
";
            var result = Day6.RunA(input);
            result.Should().Be(288);
        }
    }
    
    public class RunBShould
    {
        [Fact]
        public void ReturnExpectedResult()
        {
            var input = @"Time:      7  15   30
Distance:  9  40  200
";
            var result = Day6.RunB(input);
            result.Should().Be(71503);
        }
    }
}