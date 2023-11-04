using App.Day8;
using FluentAssertions;

namespace Tests;

public static class TestDay8
{
    public class RunAShould
    {
        [Fact]
        public void ReturnExpectedResult()
        {
            var input = "30373\n25512\n65332\n33549\n35390\n";
            var result = Day8.RunA(input);
            result.Should().Be(21);
        }
    }
    
    public class RunBShould
    {
        [Fact]
        public void ReturnExpectedResult()
        {
            var input = "30373\n25512\n65332\n33549\n35390\n";
            var result = Day8.RunB(input);
            result.Should().Be(8);
        }
    }
}