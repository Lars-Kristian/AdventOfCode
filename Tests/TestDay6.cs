using ConsoleApp.Missions;
using FluentAssertions;

namespace Tests;

public class TestDay6
{
    public class RunAShould
    {
        [Theory]
        [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
        [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
        [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
        [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
        public void ReturnExpectedResult(string input, int expectedResult)
        {
            var result = Day6.RunA(input);
            result.Should().Be(expectedResult);
        }
    }
    
    public class RunBShould
    {
        [Theory]
        [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
        [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
        [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 23)]
        [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
        [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
        public void ReturnExpectedResult(string input, int expectedResult)
        {
            var result = Day6.RunB(input);
            result.Should().Be(expectedResult);
        }
    }
}