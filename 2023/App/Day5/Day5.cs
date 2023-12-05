using System.Runtime.CompilerServices;
using App.Common;
using BenchmarkGenerator;
using RunGenerator;

namespace App.Day5;

public static class Day5
{
    [GenerateRun("Day5/Day5.input")]
    [GenerateBenchmark("Day5/Day5.input")]
    public static long RunA(ReadOnlySpan<char> input)
    {
        var result = 0;
        Span<long> seeds = stackalloc long[50];
        Span<MapDescriptor> tmpBuffer = stackalloc MapDescriptor[250];
        var listBufferIndex = 0;
        Span<ListDescriptor> listBuffer = stackalloc ListDescriptor[20];

        var allLines = input.EnumerateLines();

        allLines.MoveNext();

        var line = allLines.Current;
        line = line.Slice(line.IndexOf(':') + 2); //Skip to data

        var seedCount = ParseSeedNumbers(line, seeds);
        seeds = seeds.Slice(0, seedCount);

        allLines.MoveNext();
        var bufferIndex = 0;
        while (true)
        {
            if (!allLines.MoveNext()) break;

            listBuffer[listBufferIndex].From = bufferIndex;

            while (true)
            {
                allLines.MoveNext();
                line = allLines.Current;
                if (line.IsEmpty) break;

                var toSpan = line.Slice(0, line.IndexOf(' '));
                line = line.Slice(toSpan.Length + 1);
                var fromSpan = line.Slice(0, line.IndexOf(' '));
                var rangeSpan = line.Slice(fromSpan.Length + 1);

                tmpBuffer[bufferIndex].From = ParseUtil.ParseLongFast(fromSpan);
                tmpBuffer[bufferIndex].To = ParseUtil.ParseLongFast(toSpan);
                tmpBuffer[bufferIndex].Range = ParseUtil.ParseLongFast(rangeSpan);

                bufferIndex += 1;
            }

            listBuffer[listBufferIndex].To = bufferIndex;
            listBufferIndex += 1;
        }

        listBuffer = listBuffer.Slice(0, listBufferIndex);


        var minLocation = long.MaxValue;
        for (var seedIndex = 0; seedIndex < seeds.Length; seedIndex++)
        {
            var seed = seeds[seedIndex];
            for (var listIndex = 0; listIndex < listBuffer.Length; listIndex++)
            {
                var list = listBuffer[listIndex];
                for (var i = list.From; i < list.To; i++)
                {
                    var map = tmpBuffer[i];
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

        var debug = "";

        return minLocation;
    }

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ParseSeedNumbers(ReadOnlySpan<char> line, Span<long> result)
    {
        var resultIndex = 0;
        while (!line.IsEmpty)
        {
            var tokenIndex = line.IndexOf(' ');
            if (tokenIndex == -1)
            {
                var lastNumber = line.Slice(0, line.Length);
                result[resultIndex] = ParseUtil.ParseLongFast(lastNumber);
                break;
            }

            var number = line.Slice(0, tokenIndex);
            result[resultIndex] = ParseUtil.ParseLongFast(number);
            line = line.Slice(number.Length + 1);
            resultIndex += 1;
        }

        return resultIndex + 1;
    }

    private struct Range
    {
        public long From;
        public long To;
    }

    [GenerateRun("Day5/Day5.input")]
    [GenerateBenchmark("Day5/Day5.input")]
    public static long RunB(ReadOnlySpan<char> input)
    {
        var result = 0;
        Span<long> seeds = stackalloc long[50];
        Span<MapDescriptor> mapBuffer = stackalloc MapDescriptor[250];

        var listBufferIndex = 0;
        Span<ListDescriptor> listBuffer = stackalloc ListDescriptor[20];

        var allLines = input.EnumerateLines();

        allLines.MoveNext();

        var line = allLines.Current;
        line = line.Slice(line.IndexOf(':') + 2); //Skip to data

        var seedCount = ParseSeedNumbers(line, seeds);
        seeds = seeds.Slice(0, seedCount);

        allLines.MoveNext();
        var bufferIndex = 0;
        while (true)
        {
            if (!allLines.MoveNext()) break;

            listBuffer[listBufferIndex].From = bufferIndex;

            while (true)
            {
                allLines.MoveNext();
                line = allLines.Current;
                if (line.IsEmpty) break;

                var toSpan = line.Slice(0, line.IndexOf(' '));
                line = line.Slice(toSpan.Length + 1);
                var fromSpan = line.Slice(0, line.IndexOf(' '));
                var rangeSpan = line.Slice(fromSpan.Length + 1);

                mapBuffer[bufferIndex].From = ParseUtil.ParseLongFast(fromSpan);
                mapBuffer[bufferIndex].To = ParseUtil.ParseLongFast(toSpan);
                mapBuffer[bufferIndex].Range = ParseUtil.ParseLongFast(rangeSpan);

                bufferIndex += 1;
            }

            listBuffer[listBufferIndex].To = bufferIndex;
            listBufferIndex += 1;
        }

        listBuffer = listBuffer.Slice(0, listBufferIndex);

        var rangeCount = seeds.Length / 2;
        Span<Range> rangeBuffer = stackalloc Range[rangeCount];
        for (var rangeIndex = 0; rangeIndex < rangeCount; rangeIndex++)
        {
            var seedIndex = rangeIndex * 2;

            rangeBuffer[rangeIndex].From = seeds[seedIndex];
            rangeBuffer[rangeIndex].To = seeds[seedIndex] + seeds[seedIndex + 1] - 1;
        }

        var minLocation = long.MaxValue;
        for (var rangeIndex = 0; rangeIndex < rangeBuffer.Length; rangeIndex++)
        {
            var min = FindMinLocationInRange(rangeBuffer[rangeIndex], mapBuffer, listBuffer);
            if (min < minLocation) minLocation = min;
        }


        var debug = "";

        return minLocation;
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
    public static long RunBBrute(ReadOnlySpan<char> input)
    {
        var result = 0;
        Span<long> seeds = stackalloc long[50];
        Span<MapDescriptor> mapBuffer = stackalloc MapDescriptor[250];

        var listBufferIndex = 0;
        Span<ListDescriptor> listBuffer = stackalloc ListDescriptor[20];

        var allLines = input.EnumerateLines();

        allLines.MoveNext();

        var line = allLines.Current;
        line = line.Slice(line.IndexOf(':') + 2); //Skip to data

        var seedCount = ParseSeedNumbers(line, seeds);
        seeds = seeds.Slice(0, seedCount);

        allLines.MoveNext();
        var bufferIndex = 0;
        while (true)
        {
            if (!allLines.MoveNext()) break;

            listBuffer[listBufferIndex].From = bufferIndex;

            while (true)
            {
                allLines.MoveNext();
                line = allLines.Current;
                if (line.IsEmpty) break;

                var toSpan = line.Slice(0, line.IndexOf(' '));
                line = line.Slice(toSpan.Length + 1);
                var fromSpan = line.Slice(0, line.IndexOf(' '));
                var rangeSpan = line.Slice(fromSpan.Length + 1);

                mapBuffer[bufferIndex].From = ParseUtil.ParseLongFast(fromSpan);
                mapBuffer[bufferIndex].To = ParseUtil.ParseLongFast(toSpan);
                mapBuffer[bufferIndex].Range = ParseUtil.ParseLongFast(rangeSpan);

                bufferIndex += 1;
            }

            listBuffer[listBufferIndex].To = bufferIndex;
            listBufferIndex += 1;
        }

        listBuffer = listBuffer.Slice(0, listBufferIndex);

        var rangeCount = seeds.Length / 2;
        Span<Range> rangeBuffer = stackalloc Range[rangeCount];
        for (var rangeIndex = 0; rangeIndex < rangeCount; rangeIndex++)
        {
            var seedIndex = rangeIndex * 2;

            rangeBuffer[rangeIndex].From = seeds[seedIndex];
            rangeBuffer[rangeIndex].To = seeds[seedIndex] + seeds[seedIndex + 1] - 1;
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