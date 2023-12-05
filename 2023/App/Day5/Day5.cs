using System.Runtime.CompilerServices;
using System.Text;
using App.Common;
using BenchmarkGenerator;
using RunGenerator;

namespace App.Day5;

public static class Day5
{
    private struct ListDescriptor
    {
        public int From;
        public int To;
    }

    private struct MapDescriptor
    {
        public long From;
        public long To;
        public long Range;
    }

    private struct Range
    {
        public long From;
        public long To;
    }
    
    
    [GenerateRun("Day5/Day5.input")]
    [GenerateBenchmark("Day5/Day5.input")]
    public static long RunA(ReadOnlySpan<char> input)
    {
        Span<long> seedBuffer = stackalloc long[50];
        Span<MapDescriptor> mapBuffer = stackalloc MapDescriptor[250];
        Span<ListDescriptor> listBuffer = stackalloc ListDescriptor[50];

        var lines = input.EnumerateLines();
        lines.MoveNext();
        
        ParseSeedNumbers(lines, ref seedBuffer);
        lines.MoveNext();
        
        ParseMaps(lines, ref listBuffer, ref mapBuffer);
        
        var minResult = long.MaxValue;
        for (var seedIndex = 0; seedIndex < seedBuffer.Length; seedIndex++)
        {
            var seed = seedBuffer[seedIndex];
            for (var listIndex = 0; listIndex < listBuffer.Length; listIndex++)
            {
                var list = listBuffer[listIndex];
                for (var i = list.From; i < list.To; i++)
                {
                    var map = mapBuffer[i];
                    if (seed >= map.From && seed < map.From + map.Range)
                    {
                        var mapOffset = seed - map.From;
                        seed = map.To + mapOffset;
                        break;
                    }
                }
            }

            if (seed < minResult) minResult = seed;
        }

        return minResult;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ParseSeedNumbers(SpanLineEnumerator lines, ref Span<long> seeds)
    {
        var line = lines.Current;
        line = line.Slice(line.IndexOf(':') + 2); //Skip prefix
        
        var seedCount = 0;
        while (!line.IsEmpty)
        {
            var tokenIndex = line.IndexOf(' ');
            if (tokenIndex == -1)
            {
                var lastNumber = line.Slice(0, line.Length);
                seeds[seedCount] = ParseUtil.ParseLongFast(lastNumber);
                break;
            }

            var number = line.Slice(0, tokenIndex);
            seeds[seedCount] = ParseUtil.ParseLongFast(number);
            line = line.Slice(number.Length + 1);
            seedCount += 1;
        }

        seeds = seeds.Slice(0, seedCount + 1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ParseMaps(SpanLineEnumerator lines, ref Span<ListDescriptor> listBuffer, ref Span<MapDescriptor> mapBuffer)
    {
        var listCount = 0;
        var mapCount = 0;
        while (true)
        {
            if (!lines.MoveNext()) break;

            listBuffer[listCount].From = mapCount;

            while (true)
            {
                lines.MoveNext();
                var line = lines.Current;
                if (line.IsEmpty) break;

                var toSpan = line.Slice(0, line.IndexOf(' '));
                line = line.Slice(toSpan.Length + 1);
                var fromSpan = line.Slice(0, line.IndexOf(' '));
                var rangeSpan = line.Slice(fromSpan.Length + 1);

                mapBuffer[mapCount].From = ParseUtil.ParseLongFast(fromSpan);
                mapBuffer[mapCount].To = ParseUtil.ParseLongFast(toSpan);
                mapBuffer[mapCount].Range = ParseUtil.ParseLongFast(rangeSpan);

                mapCount += 1;
            }

            listBuffer[listCount].To = mapCount;
            listCount += 1;
        }

        mapBuffer = mapBuffer.Slice(0, mapCount);
        listBuffer = listBuffer.Slice(0, listCount);
    }
    
    [GenerateRun("Day5/Day5.input")]
    [GenerateBenchmark("Day5/Day5.input")]
    public static long RunB(ReadOnlySpan<char> input)
    {
        Span<long> seedBuffer = stackalloc long[50];
        Span<MapDescriptor> mapBuffer = stackalloc MapDescriptor[250];
        Span<ListDescriptor> listBuffer = stackalloc ListDescriptor[50];

        var lines = input.EnumerateLines();
        lines.MoveNext();
        
        ParseSeedNumbers(lines, ref seedBuffer);
        lines.MoveNext();
        
        ParseMaps(lines, ref listBuffer, ref mapBuffer);

        Span<Range> rangeBuffer = stackalloc Range[seedBuffer.Length / 2];
        
        for (var rangeIndex = 0; rangeIndex < rangeBuffer.Length; rangeIndex++)
        {
            var seedIndex = rangeIndex * 2;

            rangeBuffer[rangeIndex].From = seedBuffer[seedIndex];
            rangeBuffer[rangeIndex].To = seedBuffer[seedIndex] + seedBuffer[seedIndex + 1] - 1;
        }

        var minResult = long.MaxValue;
        for (var rangeIndex = 0; rangeIndex < rangeBuffer.Length; rangeIndex++)
        {
            var min = FindMinLocationInRange(rangeBuffer[rangeIndex], mapBuffer, listBuffer);
            if (min < minResult) minResult = min;
        }

        return minResult;
    }

    private static long FindMinLocationInRange(Range range, Span<MapDescriptor> mapBuffer,
        Span<ListDescriptor> listBuffer, int initialListIndex = 0, int initialMapIndex = 0)
    {
        var minLocation = long.MaxValue;

        var nextFrom = range.From;
        var nextTo = range.To;

        for (var listIndex = initialListIndex; listIndex < listBuffer.Length; listIndex++)
        {
            var list = listBuffer[listIndex];
            
            var mapIndex = list.From;
            if (initialMapIndex > mapIndex) mapIndex = initialMapIndex;
            
            for (; mapIndex < list.To; mapIndex++)
            {
                var map = mapBuffer[mapIndex];

                if ((nextFrom >= map.From && nextFrom < map.From + map.Range) &&
                    (nextTo >= map.From && nextTo < map.From + map.Range))
                {
                    
                    nextFrom = map.To + nextFrom - map.From;
                    nextTo = map.To + nextTo - map.From;
                    break;
                }
                
                if ((nextFrom >= map.From && nextFrom < map.From + map.Range) &&
                    (nextTo >= map.From + map.Range))
                {
                    var splitRangeLeft = new Range()
                    {
                        From = nextFrom,
                        To = map.From + map.Range - 1
                    };

                    var splitRangeRight = new Range()
                    {
                        From = map.From + map.Range,
                        To = nextTo
                    };

                    var minLeft =  FindMinLocationInRange(splitRangeLeft, mapBuffer, listBuffer, listIndex, mapIndex);
                    var minRight = FindMinLocationInRange(splitRangeRight, mapBuffer, listBuffer, listIndex, mapIndex);
                    
                    if (minLeft < minLocation) minLocation = minLeft;
                    if (minRight < minLocation) minLocation = minRight;
                    
                    return minLocation;
                }
                
                if (nextFrom < map.From && 
                    (nextTo >= map.From && nextTo < map.From + map.Range))
                {
                    var splitRangeLeft = new Range()
                    {
                        From = nextFrom,
                        To = map.From - 1
                    };

                    var splitRangeRight = new Range()
                    {
                        From = map.From,
                        To = nextTo
                    };

                    var minLeft =  FindMinLocationInRange(splitRangeLeft, mapBuffer, listBuffer, listIndex, mapIndex);
                    var minRight = FindMinLocationInRange(splitRangeRight, mapBuffer, listBuffer, listIndex, mapIndex);
                    
                    if (minLeft < minLocation) minLocation = minLeft;
                    if (minRight < minLocation) minLocation = minRight;
                    
                    return minLocation;
                }
                
                if (nextFrom < map.From && nextTo >= map.From + map.Range)
                {
                    var splitRangeLeft = new Range()
                    {
                        From = nextFrom,
                        To = map.From - 1
                    };
                    
                    var splitRangeMid = new Range()
                    {
                        From = map.From,
                        To = map.From + map.Range - 1
                    };

                    var splitRangeRight = new Range()
                    {
                        From = map.From + map.Range,
                        To = nextTo
                    };
                    
                    var minLeft =  FindMinLocationInRange(splitRangeLeft, mapBuffer, listBuffer, listIndex, mapIndex);
                    var minMid =   FindMinLocationInRange(splitRangeMid, mapBuffer, listBuffer, listIndex, mapIndex);
                    var minRight = FindMinLocationInRange(splitRangeRight, mapBuffer, listBuffer, listIndex, mapIndex);
                    
                    if (minLeft < minLocation) minLocation = minLeft;
                    if (minMid < minLocation) minLocation = minMid;
                    if (minRight < minLocation) minLocation = minRight;
                    
                    return minLocation;
                }
            }
        }

        if (nextFrom < minLocation) minLocation = nextFrom;
        if (nextTo < minLocation) minLocation = nextTo;

        return minLocation;
    }
    
    
    [GenerateRun("Day5/Day5.input")]
    [GenerateBenchmark("Day5/Day5.input")]
    public static long RunBBruteForce(ReadOnlySpan<char> input)
    {
        Span<long> seedBuffer = stackalloc long[50];
        Span<MapDescriptor> mapBuffer = stackalloc MapDescriptor[250];
        Span<ListDescriptor> listBuffer = stackalloc ListDescriptor[50];

        var lines = input.EnumerateLines();
        lines.MoveNext();
        
        ParseSeedNumbers(lines, ref seedBuffer);
        lines.MoveNext();
        
        ParseMaps(lines, ref listBuffer, ref mapBuffer);

        Span<Range> rangeBuffer = stackalloc Range[seedBuffer.Length / 2];
        
        for (var rangeIndex = 0; rangeIndex < rangeBuffer.Length; rangeIndex++)
        {
            var seedIndex = rangeIndex * 2;

            rangeBuffer[rangeIndex].From = seedBuffer[seedIndex];
            rangeBuffer[rangeIndex].To = seedBuffer[seedIndex] + seedBuffer[seedIndex + 1] - 1;
        }

        var minLocation = long.MaxValue;
        for (var rangeIndex = 0; rangeIndex < rangeBuffer.Length; rangeIndex++)
        {
            Console.WriteLine("Next range!");
            var range = rangeBuffer[rangeIndex];
    
            for (var s = range.From; s < range.To; s++)
            {
                long seed = s;
                
                for (var listIndex = 0; listIndex < listBuffer.Length; listIndex++)
                {
                    var list = listBuffer[listIndex];
                    for (var i = list.From; i < list.To; i++)
                    {
                        var map = mapBuffer[i];
                        if (seed >= map.From && seed < map.From + map.Range)
                        {
                            var mapOffset = seed - map.From;
                            seed = map.To + mapOffset;
                            break;
                        }
                    }
                }

                if (seed < minLocation)
                {
                    minLocation = seed;
                }
            }
        }

        return minLocation;
    }
}