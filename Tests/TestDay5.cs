using App.Day5;
using FluentAssertions;

namespace Tests;

public static class TestDay5
{
    public class RunAShould
    {
        [Fact]
        public void ReturnExpectedResult()
        {
            var input = "    [D]    \n[N] [C]    \n[Z] [M] [P]\n 1   2   3 \n\nmove 1 from 2 to 1\nmove 3 from 1 to 3\nmove 2 from 2 to 1\nmove 1 from 1 to 2\n";

            var result = Day5.RunA(input);
            result.Should().Be("CMZ");
        }
    }
    
    public class RunBShould
    {
        [Fact]
        public void ReturnExpectedResult()
        {
            var input = "    [D]    \n[N] [C]    \n[Z] [M] [P]\n 1   2   3 \n\nmove 1 from 2 to 1\nmove 3 from 1 to 3\nmove 2 from 2 to 1\nmove 1 from 1 to 2\n";

            var result = Day5.RunB(input);
            result.Should().Be("MCD");
        }
    }
}