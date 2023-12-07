using System.Runtime.CompilerServices;
using System.Text;
using App.Common;
using BenchmarkGenerator;
using RunGenerator;

namespace App.Day7;

public static class Day7
{
    private struct Hand
    {
        public int Bid;
        public HandType Type;
        public long Cards;
    }

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

    [GenerateRun("Day7/Day7.input")]
    [GenerateBenchmark("Day7/Day7.input")]
    public static long RunA(ReadOnlySpan<char> input)
    {
        var width = input.IndexOf('\n');
        var length = input.Length / width;

        Span<Hand> hands = stackalloc Hand[length];
        var handCount = 0;

        var lineEnumerator = input.EnumerateLines();
        while (lineEnumerator.MoveNext())
        {
            var line = lineEnumerator.Current;

            if (line.IsEmpty) break;

            var tokenIndex = line.IndexOf(' ');
            var cardSpan = line.Slice(0, tokenIndex);
            var bidSpan = line.Slice(tokenIndex + 1, line.Length - tokenIndex - 1);

            //Pack cards into long because C# does not support fixed size arrays in structs. :(
            long cards = 0;
            foreach (var c in cardSpan)
            {
                var cardAsByte = CardToByteOptimized(c);
                cards = (cards << 8) + cardAsByte;
            }

            hands[handCount].Bid = ParseUtil.ParseIntFast(bidSpan);
            hands[handCount].Cards = cards;
            handCount += 1;
        }

        hands = hands.Slice(0, handCount);

        Span<byte> typeMap = stackalloc byte[13];
        for (var handIndex = 0; handIndex < hands.Length; handIndex++)
        {
            typeMap.Clear();
            for (var cardIndex = 4; cardIndex >= 0; cardIndex--)
            {
                var c = (byte)(hands[handIndex].Cards >> (8 * cardIndex));
                typeMap[c] += 1;
            }

            var type = HandType.HighCard;
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

            hands[handIndex].Type = type;
        }


        hands.Sort<Hand>((a, b) =>
        {
            var diff = a.Type - b.Type;
            if (diff != 0) return diff;

            for (var i = 4; i >= 0; i--)
            {
                var aCard = (byte)(a.Cards >> (8 * i));
                var bCard = (byte)(b.Cards >> (8 * i));
                var cardDiff = aCard - bCard;
                if (cardDiff != 0) return cardDiff;
            }

            return 0;
        });

        long result = 0;
        for (var i = 0; i < hands.Length; i++)
        {
            result += (i + 1) * hands[i].Bid;
        }
        
        return result;
    }


    //Optimize: can use a map;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte CardToByte(char card)
    {
        var availableCards = "23456789TJQKA";
        var index = availableCards.IndexOf(card);
        if (index == -1) throw new Exception("Card not found");
        return (byte)index;
    }

    private static int[] CardToByteMap = { 0, 1, 2, 3, 4, 5, 6, 7, -1, -1, -1, -1, -1, -1, -1, 12, -1, -1, -1, -1, -1, -1, -1, -1, 9, 11, -1, -1, -1, -1, -1, 10, -1, -1, 8};
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte CardToByteOptimized(char card)
    {
        var index = card - '2';
        return (byte)CardToByteMap[index];
    }

    //Optimize: can use a map;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte CardWithJokerToByte(char card)
    {
        var availableCards = "J23456789TQKA";
        var index = availableCards.IndexOf(card);
        if (index == -1) throw new Exception("Card not found");
        return (byte)index;
    }
    
    private static int[] CardWithJokerToByteMap = { 1, 2, 3, 4, 5, 6, 7, 8, -1, -1, -1, -1, -1, -1, -1, 12, -1, -1, -1, -1, -1, -1, -1, -1, 0, 11, -1, -1, -1, -1, -1, 10, -1, -1, 9};
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte CardWithJokerToByteOptimized(char card)
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

    //Optimize: can use a map;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte CardToByteDebug(char card)
    {
        return (byte)card;
    }
    
    [GenerateRun("Day7/Day7.input")]
    [GenerateBenchmark("Day7/Day7.input")]
    public static long RunB(ReadOnlySpan<char> input)
    {
        var width = input.IndexOf('\n');
        var length = input.Length / width;

        Span<Hand> hands = stackalloc Hand[length];
        var handCount = 0;

        var lineEnumerator = input.EnumerateLines();
        while (lineEnumerator.MoveNext())
        {
            var line = lineEnumerator.Current;

            if (line.IsEmpty) break;

            var tokenIndex = line.IndexOf(' ');
            var cardSpan = line.Slice(0, tokenIndex);
            var bidSpan = line.Slice(tokenIndex + 1, line.Length - tokenIndex - 1);

            //Pack cards into long because C# does not support fixed size arrays in structs. :(
            long cards = 0;
            foreach (var c in cardSpan)
            {
                var cardAsByte = CardWithJokerToByteOptimized(c);
                cards = (cards << 8) + cardAsByte;
            }

            hands[handCount].Bid = ParseUtil.ParseIntFast(bidSpan);
            hands[handCount].Cards = cards;
            handCount += 1;
        }

        hands = hands.Slice(0, handCount);

        Span<byte> typeMap = stackalloc byte[13];
        var joker = CardWithJokerToByteOptimized('J');
        for (var handIndex = 0; handIndex < hands.Length; handIndex++)
        {
            var jokerCount = 0;
            typeMap.Clear();
            for (var cardIndex = 4; cardIndex >= 0; cardIndex--)
            {
                var c = (byte)(hands[handIndex].Cards >> (8 * cardIndex));
                if (c == joker)
                {
                    jokerCount += 1;
                }
                else
                {
                    typeMap[c] += 1;
                }
            }

            var type = HandType.HighCard;
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

            hands[handIndex].Type = type;
        }


        hands.Sort((a, b) =>
        {
            var diff = a.Type - b.Type;
            if (diff != 0) return diff;

            for (var i = 4; i >= 0; i--)
            {
                int aCard = (byte)(a.Cards >> (8 * i));
                int bCard = (byte)(b.Cards >> (8 * i));
                var cardDiff = aCard - bCard;
                if (cardDiff != 0) return cardDiff;
            }

            return 0;
        });

        long result = 0;
        for (var i = 0; i < hands.Length; i++)
        {
            result += (i + 1) * hands[i].Bid;
        }
        
        return result;
    }
    
    private static string HandsToString(ReadOnlySpan<Hand> hands)
    {
        var sb = new StringBuilder();
        foreach (var hand in hands)
        {
            for (var i = 4; i >= 0; i--)
            {
                var card = (byte)(hand.Cards >> (8 * i));
                sb.Append(ByteToCardWithJoker(card));
            }
            
            sb.Append(" ");
            sb.Append(hand.Bid);
            sb.Append("\n");
        }

        return sb.ToString();
    }

    [GenerateRun("Day7/Day7.input")]
    public static long RunBuildMap(ReadOnlySpan<char> input)
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