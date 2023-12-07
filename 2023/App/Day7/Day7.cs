using System.Runtime.CompilerServices;
using System.Text;
using App.Common;
using BenchmarkGenerator;
using RunGenerator;

namespace App.Day7;

public static class Day7
{
    private enum HandType
    {
        HighCard = 0,
        OnePair,
        TwoPair,
        ThreeOfKind,
        FullHouse,
        FourOfKind,
        FiveOfKind,
    }
    
    //Data-layout:
    //            56        48        40        32        24        16
    //b0_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000
    //   0000_0111|0000_1100|0000_1100|0000_1100|0000_1100|0000_1100|0000_0011_1110_1000
    //  |     type|     card|     card|     card|     card|     card|                bid
    
    private static int[] CardToByteMap = { 0, 1, 2, 3, 4, 5, 6, 7, -1, -1, -1, -1, -1, -1, -1, 12, -1, -1, -1, -1, -1, -1, -1, -1, 9, 11, -1, -1, -1, -1, -1, 10, -1, -1, 8};
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte CardToByte(char card)
    {
        var index = card - '2';
        return (byte)CardToByteMap[index];
    }
    
    private static int[] CardWithJokerToByteMap = { 1, 2, 3, 4, 5, 6, 7, 8, -1, -1, -1, -1, -1, -1, -1, 12, -1, -1, -1, -1, -1, -1, -1, -1, 0, 11, -1, -1, -1, -1, -1, 10, -1, -1, 9};
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte CardWithJokerToByte(char card)
    {
        var index = card - '2';
        return (byte)CardWithJokerToByteMap[index];
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static char ByteToCardWithJoker(byte card)
    {
        var availableCards = "J23456789TQKA";
        return availableCards[card];
    }
    
    [GenerateRun("Day7/Day7.input")]
    [GenerateBenchmark("Day7/Day7.input")]
    public static long RunA(ReadOnlySpan<char> input)
    {
        var width = input.IndexOf('\n');
        var length = input.Length / width;
        
        Span<long> hands = stackalloc long[length];
        var handCount = 0;

        var lineEnumerator = input.EnumerateLines();
        while (lineEnumerator.MoveNext())
        {
            var line = lineEnumerator.Current;

            if (line.IsEmpty) break;

            var tokenIndex = line.IndexOf(' ');
            var cardSpan = line.Slice(0, tokenIndex);
            var bidSpan = line.Slice(tokenIndex + 1, line.Length - tokenIndex - 1);

            //Pack entire hand into a long.
            long type = 0;
            
            long cards = 0;
            for (var index = 0; index < 5; index++)
            {
                var c = cardSpan[index];
                var cardAsByte = CardToByte(c);
                cards = (cards << 8) + cardAsByte;
            }

            long bid = ParseUtil.ParseIntFast(bidSpan);
            hands[handCount] = (type << 56) | (cards << 16) | bid;
            handCount += 1;
        }

        hands = hands.Slice(0, handCount);

        Span<byte> typeMap = stackalloc byte[13];
        for (var handIndex = 0; handIndex < hands.Length; handIndex++)
        {
            typeMap.Clear();
            var cards = (hands[handIndex] >> 16) & 0xFF_FFFF_FFFF;
            
            for (var cardIndex = 4; cardIndex >= 0; cardIndex--)
            {
                var c = (byte)(cards >> (8 * cardIndex));
                typeMap[c] += 1;
            }

            var type = (HandType)(hands[handIndex] >> 56 & 0xFFFF);
            for (var typeMapIndex = 0; typeMapIndex < typeMap.Length; typeMapIndex++)
            {
                var t = typeMap[typeMapIndex];
                if (t < 2) continue;
                if (t == 2)
                {
                    if (type == HandType.ThreeOfKind)
                    {
                        type = HandType.FullHouse;
                    }
                    else if (type == HandType.OnePair)
                    {
                        type = HandType.TwoPair;
                    }
                    else
                    {
                        type = HandType.OnePair;
                    }
                }
                else if (t == 3)
                {
                    if (type == HandType.OnePair)
                    {
                        type = HandType.FullHouse;
                    }
                    else
                    {
                        type = HandType.ThreeOfKind;
                    }
                }
                else if (t == 4)
                {
                    type = HandType.FourOfKind;
                }
                else if (t == 5)
                {
                    type = HandType.FiveOfKind;
                }
            }

            hands[handIndex] &= ~((long)0xFFFF << 56);
            hands[handIndex] |= (long)type << 56;
        }
        
        hands.Sort();
        
        long result = 0;
        for (var i = 0; i < hands.Length; i++)
        {
            result += (i + 1) * (hands[i] & 0xFFFF);
        }
        
        return result;
    }
    
    [GenerateRun("Day7/Day7.input")]
    [GenerateBenchmark("Day7/Day7.input")]
    public static long RunB(ReadOnlySpan<char> input)
    {
        var width = input.IndexOf('\n');
        var length = input.Length / width;
        
        Span<long> hands = stackalloc long[length];
        var handCount = 0;

        var lineEnumerator = input.EnumerateLines();
        while (lineEnumerator.MoveNext())
        {
            var line = lineEnumerator.Current;

            if (line.IsEmpty) break;

            var tokenIndex = line.IndexOf(' ');
            var cardSpan = line.Slice(0, tokenIndex);
            var bidSpan = line.Slice(tokenIndex + 1, line.Length - tokenIndex - 1);

            //Pack entire hand into a long.
            long type = 0;
            
            long cards = 0;
            for (var index = 0; index < 5; index++)
            {
                var c = cardSpan[index];
                var cardAsByte = CardWithJokerToByte(c);
                cards = (cards << 8) + cardAsByte;
            }

            long bid = ParseUtil.ParseIntFast(bidSpan);
            hands[handCount] = (type << 56) | (cards << 16) | bid;
            handCount += 1;
        }

        hands = hands.Slice(0, handCount);

        Span<byte> typeMap = stackalloc byte[13];
        var joker = CardWithJokerToByte('J');
        for (var handIndex = 0; handIndex < hands.Length; handIndex++)
        {
            var jokerCount = 0;
            typeMap.Clear();
            var cards = (hands[handIndex] >> 16) & 0xFF_FFFF_FFFF;
            
            for (var cardIndex = 4; cardIndex >= 0; cardIndex--)
            {
                var c = (byte)(cards >> (8 * cardIndex));
                if (c == joker)
                {
                    jokerCount += 1;
                }
                else
                {
                    typeMap[c] += 1;
                }
            }

            var type = (HandType)(hands[handIndex] >> 56 & 0xFFFF);
            
            for (var typeMapIndex = 0; typeMapIndex < typeMap.Length; typeMapIndex++)
            {
                var t = typeMap[typeMapIndex];
                if (t < 2) continue;
                if (t == 2)
                {
                    if (type == HandType.ThreeOfKind)
                    {
                        type = HandType.FullHouse;
                    }
                    else if (type == HandType.OnePair)
                    {
                        type = HandType.TwoPair;
                    }
                    else
                    {
                        type = HandType.OnePair;
                    }
                }
                else if (t == 3)
                {
                    if (type == HandType.OnePair)
                    {
                        type = HandType.FullHouse;
                    }
                    else
                    {
                        type = HandType.ThreeOfKind;
                    }
                }
                else if (t == 4)
                {
                    type = HandType.FourOfKind;
                }
                else if (t == 5)
                {
                    type = HandType.FiveOfKind;
                }
            }

            if (jokerCount == 1)
            {
                if (type == HandType.HighCard) type = HandType.OnePair;
                else if (type == HandType.OnePair) type = HandType.ThreeOfKind;
                else if (type == HandType.TwoPair) type = HandType.FullHouse;
                else if (type == HandType.ThreeOfKind) type = HandType.FourOfKind;
                else if (type == HandType.FourOfKind) type = HandType.FiveOfKind;
                else throw new Exception("JokerCount = 1, but no change");
            }
            else if (jokerCount == 2)
            {
                if (type == HandType.HighCard) type = HandType.ThreeOfKind;
                else if (type == HandType.OnePair) type = HandType.FourOfKind;
                else if (type == HandType.ThreeOfKind) type = HandType.FiveOfKind;
                else throw new Exception("JokerCount = 2, but no change");
            }
            else if (jokerCount == 3)
            {
                if (type == HandType.HighCard) type = HandType.FourOfKind;
                else if (type == HandType.OnePair) type = HandType.FiveOfKind;
                else throw new Exception("JokerCount = 3, but no change");
            }
            else if (jokerCount == 4)
            {
                if (type == HandType.HighCard) type = HandType.FiveOfKind;
                else throw new Exception("JokerCount = 5, but no change");
            }
            else if (jokerCount == 5)
            {
                type = HandType.FiveOfKind;
            }

            hands[handIndex] &= ~((long)0xFFFF << 56);
            hands[handIndex] |= (long)type << 56;
        }
        
        hands.Sort();
        
        long result = 0;
        for (var i = 0; i < hands.Length; i++)
        {
            result += (i + 1) * (hands[i] & 0xFFFF);
        }
        
        return result;
    }
    
    private static string HandsWithJokerToString(ReadOnlySpan<long> hands)
    {
        var sb = new StringBuilder();
        foreach (var hand in hands)
        {
            var type = (HandType)((hand >> 56) & 0xFFFF);
            var cards = (hand >> 16) & 0xFF_FFFF_FFFF;
            var bid = (int)(hand & 0xFFFF);
            for (var i = 4; i >= 0; i--)
            {
                var card = (byte)(cards >> (8 * i));
                sb.Append(ByteToCardWithJoker(card));
            }
            
            sb.Append(" ");
            sb.Append(bid);
            sb.Append("\n");
        }

        return sb.ToString();
    }

    public static long RunBuildMap()
    {

        var mapArray = new int['T' - '2' + 1];
        var availableCards = "J23456789TQKA";
        //need mapping from '2' to 'T'

        var sb = new StringBuilder();
        sb.Append('{');
        for (var index = 0; index < 'T' - '2' + 1; index++)
        {
            var c = '2' + index;
            var mapIndex = availableCards.IndexOf((char)c);
            sb.Append(' ');
            sb.Append(mapIndex);
            sb.Append(',');
        }
        sb.Append("};");
        Console.WriteLine(sb);
        return 0;
    }
}