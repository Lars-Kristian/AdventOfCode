using System.Numerics;
using System.Text;
using App.Common;
using BenchmarkGenerator;
using RunGenerator;

namespace App.Day8;

public static class Day8
{
    [GenerateRun("Day8/Day8.input")]
    [GenerateBenchmark("Day8/Day8.input")]
    public static long RunA(ReadOnlySpan<char> input)
    {
        var itemCount = input.Length / 17;
        Span<byte> instructions = stackalloc byte[input.IndexOf('\n')];

        var startNodes = 0;
        var isEndNode = 0;
        Span<int> nameArray = stackalloc int[itemCount];
        Span<int> leftIndexArray = stackalloc int[itemCount];
        Span<int> rightIndexArray = stackalloc int[itemCount];

        var lineEnumerator = input.EnumerateLines();

        lineEnumerator.MoveNext();
        ParseInstructions(lineEnumerator.Current, ref instructions);
        lineEnumerator.MoveNext();

        var count = 0;
        while (lineEnumerator.MoveNext())
        {
            var line = lineEnumerator.Current;
            if (line.IsEmpty) break;

            var nameSpan = line.Slice(0, 3);
            var leftSpan = line.Slice(7, 3);
            var rightSpan = line.Slice(12, 3);

            var name = 0;
            var left = 0;
            var right = 0;
            for (var i = 0; i < 3; i++)
            {
                name = (name << 5) + (nameSpan[i] - 'A');
                left = (left << 5) + (leftSpan[i] - 'A');
                right = (right << 5) + (rightSpan[i] - 'A');
            }

            if (nameSpan[0] == 'A' && nameSpan[1] == 'A' && nameSpan[2] == 'A')
            {
                startNodes = count;
            }
            else if (nameSpan[0] == 'Z' && nameSpan[1] == 'Z' && nameSpan[2] == 'Z')
            {
                isEndNode = count;
            }

            nameArray[count] = name;
            leftIndexArray[count] = left;
            rightIndexArray[count] = right;

            count += 1;
        }

        nameArray = nameArray.Slice(0, count);
        leftIndexArray = leftIndexArray.Slice(0, count);
        rightIndexArray = rightIndexArray.Slice(0, count);

        for (var index = 0; index < leftIndexArray.Length; index++)
        {
            var name = leftIndexArray[index];
            var nameIndex = nameArray.IndexOf(name);
            leftIndexArray[index] = nameIndex;
        }

        for (var index = 0; index < rightIndexArray.Length; index++)
        {
            var name = rightIndexArray[index];
            var nameIndex = nameArray.IndexOf(name);
            rightIndexArray[index] = nameIndex;
        }
        
        var nodeIndex = startNodes;
        var cycles = 0;
        var instructionsIndex = -1;
        while (true)
        {
            if (isEndNode == nodeIndex) break;

            instructionsIndex += 1;
            if (instructionsIndex >= instructions.Length)
            {
                instructionsIndex = 0;
            }

            if (instructions[instructionsIndex] == 0)
            {
                nodeIndex = leftIndexArray[nodeIndex];
            }
            else
            {
                nodeIndex = rightIndexArray[nodeIndex];
            }

            cycles += 1;
        }

        return cycles;
    }

    [GenerateRun("Day8/Day8.input")]
    [GenerateBenchmark("Day8/Day8.input")]
    public static long RunB(ReadOnlySpan<char> input)
    {
        var itemCount = input.Length / 17;
        Span<byte> instructions = stackalloc byte[input.IndexOf('\n')];

        var startNodes = new List<int>();
        Span<bool> isEndNode = stackalloc bool[itemCount];
        Span<int> nameArray = stackalloc int[itemCount];
        Span<int> leftIndexArray = stackalloc int[itemCount];
        Span<int> rightIndexArray = stackalloc int[itemCount];

        var lineEnumerator = input.EnumerateLines();

        lineEnumerator.MoveNext();
        ParseInstructions(lineEnumerator.Current, ref instructions);
        lineEnumerator.MoveNext();

        var count = 0;
        while (lineEnumerator.MoveNext())
        {
            var line = lineEnumerator.Current;
            if (line.IsEmpty) break;

            var nameSpan = line.Slice(0, 3);
            var leftSpan = line.Slice(7, 3);
            var rightSpan = line.Slice(12, 3);

            var name = 0;
            var left = 0;
            var right = 0;
            for (var i = 0; i < 3; i++)
            {
                name = (name << 5) + (nameSpan[i] - 'A');
                left = (left << 5) + (leftSpan[i] - 'A');
                right = (right << 5) + (rightSpan[i] - 'A');
            }

            if (nameSpan[2] == 'A')
            {
                startNodes.Add(count);
            }

            nameArray[count] = name;
            leftIndexArray[count] = left;
            rightIndexArray[count] = right;
            isEndNode[count] = nameSpan[2] == 'Z';

            count += 1;
        }

        nameArray = nameArray.Slice(0, count);
        leftIndexArray = leftIndexArray.Slice(0, count);
        rightIndexArray = rightIndexArray.Slice(0, count);

        for (var index = 0; index < leftIndexArray.Length; index++)
        {
            var name = leftIndexArray[index];
            var nameIndex = nameArray.IndexOf(name);
            leftIndexArray[index] = nameIndex;
        }

        for (var index = 0; index < rightIndexArray.Length; index++)
        {
            var name = rightIndexArray[index];
            var nameIndex = nameArray.IndexOf(name);
            rightIndexArray[index] = nameIndex;
        }

        Span<long> nodeCycles = new long[startNodes.Count];

        for (var i = 0; i < startNodes.Count; i++)
        {
            var nodeIndex = startNodes[i];
            var cycles = 0;
            var instructionsIndex = -1;
            while (true)
            {
                if (isEndNode[nodeIndex]) break;

                instructionsIndex += 1;
                if (instructionsIndex >= instructions.Length) instructionsIndex = 0;

                if (instructions[instructionsIndex] == 0)
                {
                    nodeIndex = leftIndexArray[nodeIndex];
                }
                else
                {
                    nodeIndex = rightIndexArray[nodeIndex];
                }

                cycles += 1;
            }

            nodeCycles[i] = cycles;
        }

        return LcmUtil.Lcm(nodeCycles);
    }

    private static void ParseInstructions(ReadOnlySpan<char> line, ref Span<byte> instructions)
    {
        var index = 0;
        for (; index < line.Length; index++)
        {
            instructions[index] = line[index] - 'L' == 0 ? (byte)0 : (byte)1;
        }

        instructions = instructions.Slice(0, index);
    }

    private static long ParseNode(ReadOnlySpan<char> line)
    {
        var nameSpan = line.Slice(0, 3);
        var leftSpan = line.Slice(7, 3);
        var rightSpan = line.Slice(12, 3);

        long name = 0;
        long left = 0;
        long right = 0;
        for (var i = 0; i < 3; i++)
        {
            name = (name << 5) + (nameSpan[i] - 'A');
            left = (left << 5) + (leftSpan[i] - 'A');
            right = (right << 5) + (rightSpan[i] - 'A');
        }

        var data = name << 30 | left << 15 | right;
        return data;
    }

    private static long UnpackLeft(long node)
    {
        return (node >> 15 & 0x7FFF) << 30;
    }

    private static long UnpackRight(long node)
    {
        return (node & 0x7FFF) << 30;
    }

    private static long CreateMask(char letter)
    {
        var node = (long)(letter - 'A');
        return node << 30;
    }

    private static void FindNodesWhereLastLetterIsA(Span<long> map, ref Span<long> currentNodes)
    {
        var letter = CreateMask('A');
        var nodeCount = 0;

        const long nameLastLetterMask = (long)0x1F << 30;

        foreach (var node in map)
        {
            if ((node & nameLastLetterMask) == letter)
            {
                currentNodes[nodeCount] = node;
                nodeCount += 1;
            }
        }

        currentNodes = currentNodes.Slice(0, nodeCount);
    }

    private static bool IsLastLetterZ(Span<long> currentNodes)
    {
        var mask = CreateMask('Z');

        const long nameLastLetterMask = (long)0x1F << 30;

        foreach (var node in currentNodes)
        {
            if ((node & nameLastLetterMask) != mask) return false;
        }

        return true;
    }

    private static void StepNextNodeLeft(Span<long> map, ref Span<long> currentNodes)
    {
        for (var index = 0; index < currentNodes.Length; index++)
        {
            var next = UnpackLeft(currentNodes[index]);
            var nextIndex = -1 * map.BinarySearch(next) - 1;
            currentNodes[index] = map[nextIndex];
        }
    }

    private static void StepNextNodeRight(Span<long> map, ref Span<long> currentNodes)
    {
        for (var index = 0; index < currentNodes.Length; index++)
        {
            var next = UnpackRight(currentNodes[index]);
            var nextIndex = -1 * map.BinarySearch(next) - 1;
            currentNodes[index] = map[nextIndex];
        }
    }

    private static long StepNextNodeLeftSingle(Span<long> map, long node)
    {
        var next = UnpackLeft(node);
        var nextIndex = -1 * map.BinarySearch(next) - 1;
        return map[nextIndex];
    }

    private static long StepNextNodeRightSingle(Span<long> map, long node)
    {
        var next = UnpackRight(node);
        var nextIndex = -1 * map.BinarySearch(next) - 1;
        return map[nextIndex];
    }

    private static string MapToString(ReadOnlySpan<long> map)
    {
        var sb = new StringBuilder();

        foreach (var node in map)
        {
            var name = node >> 30 & 0x7FFF;
            var left = node >> 15 & 0x7FFF;
            var right = node & 0x7FFF;

            for (var i = 2; i >= 0; i--)
            {
                var c = (char)(name >> (5 * i) & 0x1F);
                c += 'A';
                sb.Append(c);
            }

            sb.Append(" ");

            for (var i = 2; i >= 0; i--)
            {
                var c = (char)(left >> (5 * i) & 0x1F);
                c += 'A';
                sb.Append(c);
            }

            sb.Append(", ");

            for (var i = 2; i >= 0; i--)
            {
                var c = (char)(right >> (5 * i) & 0x1F);
                c += 'A';
                sb.Append(c);
            }

            sb.Append("\n");
        }

        return sb.ToString();
    }

    private static string NodeToString(long node)
    {
        var sb = new StringBuilder();

        var name = node >> 30 & 0x7FFF;
        var left = node >> 15 & 0x7FFF;
        var right = node & 0x7FFF;

        for (var i = 2; i >= 0; i--)
        {
            var c = (char)(name >> (5 * i) & 0x1F);
            c += 'A';
            sb.Append(c);
        }

        sb.Append(" ");

        for (var i = 2; i >= 0; i--)
        {
            var c = (char)(left >> (5 * i) & 0x1F);
            c += 'A';
            sb.Append(c);
        }

        sb.Append(", ");

        for (var i = 2; i >= 0; i--)
        {
            var c = (char)(right >> (5 * i) & 0x1F);
            c += 'A';
            sb.Append(c);
        }

        return sb.ToString();
    }

    [GenerateRun("Day8/Day8.input")]
    [GenerateBenchmark("Day8/Day8.input")]
    public static long RunAInitialSolution(ReadOnlySpan<char> input)
    {
        Span<byte> instructions = stackalloc byte[input.IndexOf('\n')];
        Span<long> map = stackalloc long[input.Length / 17];

        var lineEnumerator = input.EnumerateLines();

        lineEnumerator.MoveNext();
        ParseInstructions(lineEnumerator.Current, ref instructions);
        lineEnumerator.MoveNext();

        var size = 'Z' - 'A' + 1;

        var count = 0;
        while (lineEnumerator.MoveNext())
        {
            var line = lineEnumerator.Current;
            if (line.IsEmpty) break;

            map[count] = ParseNode(line);

            count += 1;
        }

        map = map.Slice(0, count);
        map.Sort();
        var currentNode = map[0];
        var targetNode = map[^1];

        var result = 0;
        var index = -1;
        while (true)
        {
            if (currentNode == targetNode) break;

            index += 1;
            index = index % instructions.Length;

            var next = 0L;
            if (instructions[index] == 0)
            {
                next = UnpackLeft(currentNode);
            }
            else
            {
                next = UnpackRight(currentNode);
            }

            var nextIndex = -1 * map.BinarySearch(next) - 1;
            currentNode = map[nextIndex];

            result += 1;
        }


        return result;
    }

    [GenerateRun("Day8/Day8.input")]
    [GenerateBenchmark("Day8/Day8.input")]
    public static BigInteger RunBInitialSolution(ReadOnlySpan<char> input)
    {
        Span<byte> instructions = stackalloc byte[input.IndexOf('\n')];
        Span<long> map = stackalloc long[input.Length / 17];

        var lineEnumerator = input.EnumerateLines();

        lineEnumerator.MoveNext();
        ParseInstructions(lineEnumerator.Current, ref instructions);
        lineEnumerator.MoveNext();

        var count = 0;
        while (lineEnumerator.MoveNext())
        {
            var line = lineEnumerator.Current;
            if (line.IsEmpty) break;

            map[count] = ParseNode(line);

            count += 1;
        }

        map = map.Slice(0, count);
        map.Sort();

        Span<long> nodes = stackalloc long[map.Length / 2];
        FindNodesWhereLastLetterIsA(map, ref nodes);
        Span<long> nodeCycles = stackalloc long[nodes.Length];


        var nameLastLetterIsZ = CreateMask('Z');
        const long nameLastLetterMask = (long)0x1F << 30;

        for (var i = 0; i < nodes.Length; i++)
        {
            var node = nodes[i];
            var cycles = 0;
            var instructionsIndex = -1;
            while (true)
            {
                if ((node & nameLastLetterMask) == nameLastLetterIsZ) break;

                instructionsIndex += 1;
                instructionsIndex = instructionsIndex % instructions.Length;

                if (instructions[instructionsIndex] == 0)
                {
                    node = StepNextNodeLeftSingle(map, node);
                }
                else
                {
                    node = StepNextNodeRightSingle(map, node);
                }

                cycles += 1;
            }

            nodeCycles[i] = cycles;
        }

        return LcmUtil.Lcm(nodeCycles);
    }
}