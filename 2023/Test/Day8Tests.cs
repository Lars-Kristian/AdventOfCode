using App.Day8;
using FluentAssertions;

namespace Test;

public static class Day8Tests
{
    public class RunAShould
    {
        [Fact]
        public void ReturnExpectedResult()
        {
            var input = @"LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)
";
            var result = Day8.RunA(input);
            result.Should().Be(6);
        }
    }
    
    public class RunBShould
    {
        [Fact]
        public void ReturnExpectedResult()
        {
            var input = @"LR

GGA = (GGB, XXX)
GGB = (XXX, GGZ)
GGZ = (GGB, XXX)
HHA = (HHB, XXX)
HHB = (HHC, HHC)
HHC = (HHZ, HHZ)
HHZ = (HHB, HHB)
XXX = (XXX, XXX)
";
            var result = Day8.RunB(input);
            result.Should().Be(6);
        }
    }
}