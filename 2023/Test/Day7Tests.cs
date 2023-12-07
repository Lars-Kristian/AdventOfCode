using App.Day7;
using FluentAssertions;

namespace Test;

public static class Day7Tests
{
    public class RunAShould
    {
        [Fact]
        public void ReturnExpectedResult()
        {
            var input = @"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483
";
            var result = Day7.RunA(input);
            result.Should().Be(6440);
        }
    }
    
    public class RunBShould
    {
        [Fact]
        public void ReturnExpectedResult()
        {
            var input = @"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483
";
            var result = Day7.RunB(input);
            result.Should().Be(5905);
        }
        
        [Fact]
        public void ReturnExpectedResult2()
        {
            var input = @"2345A 1
Q2KJJ 13
Q2Q2Q 19
T3T3J 17
T3Q33 11
2345J 3
J345A 2
32T3K 5
T55J5 29
KK677 7
KTJJT 34
QQQJA 31
JJJJJ 37
JAAAA 43
AAAAJ 59
AAAAA 61
2AAAA 23
2JJJJ 53
JJJJ2 41
";
            var result = Day7.RunB(input);
            result.Should().Be(6839);
        }
        
        /*
         2345A 1
J345A 2
2345J 3
32T3K 5
KK677 7
T3Q33 11
Q2KJJ 13
T3T3J 17
Q2Q2Q 19
2AAAA 23
T55J5 29
QQQJA 31
KTJJT 34
JJJJJ 37
JJJJ2 41
JAAAA 43
2JJJJ 53
AAAAJ 59
AAAAA 61
         */
    }
}