using App.Day3;
using FluentAssertions;

namespace Test;

public static class Day3Tests
{
    public class RunAShould
    {
        [Fact]
        public void ReturnExpectedResult()
        {
            var input = "467..114..\n...*......\n..35..633.\n......#...\n617*......\n.....+.58.\n..592.....\n......755.\n...$.*....\n.664.598..\n";
            var result = Day3.RunA(input);
            result.Should().Be(4361);
        }
    }
    
    public class RunBShould
    {
        [Fact]
        public void ReturnExpectedResult()
        {
            var input = "467..114..\n...*......\n..35..633.\n......#...\n617*......\n.....+.58.\n..592.....\n......755.\n...$.*....\n.664.598..\n";
            var result = Day3.RunB(input);
            result.Should().Be(467835);
        }
    }
}