using System.Numerics;
using BenchmarkGenerator;
using RunGenerator;

namespace App.Day6;

public static class Day6
{
    [GenerateBenchmark("Day6/Day6.input")]
    [GenerateRun("Day6/Day6.input")]
    public static int RunA(ReadOnlySpan<char> data)
    {
        uint state = 0;

        for (var i = 0; i < 4; i++)
        {
            var next = data[i] % 32;
            state ^= BitOperations.RotateLeft(1, next);
        }

        for (var i = 4; i < data.Length; i++)
        {
            var prev = data[i - 4] % 32;
            state ^= BitOperations.RotateLeft(1, prev);

            var next = data[i] % 32;
            state ^= BitOperations.RotateLeft(1, next);
            ;

            if (BitOperations.PopCount(state) == 4) return i + 1;
        }


        return -1;
    }

    [GenerateBenchmark("Day6/Day6.input")]
    [GenerateRun("Day6/Day6.input")]
    public static int RunB(ReadOnlySpan<char> data)
    {
        uint state = 0;

        for (var i = 0; i < 14; i++)
        {
            var next = data[i] % 32;
            state ^= BitOperations.RotateLeft(1, next);
        }

        for (var i = 14; i < data.Length; i++)
        {
            var prev = data[i - 14] % 32;
            state ^= BitOperations.RotateLeft(1, prev);

            var next = data[i] % 32;
            state ^= BitOperations.RotateLeft(1, next);

            if (BitOperations.PopCount(state) == 14) return i + 1;
        }


        return -1;
    }

    public static int RunOther(string s)
    {
        int n = 14;
        int[] d = new int[26];
        int i, c = 0;
        for (i = 0; i < s.Length && c < n; ++i)
        {
            if (d[s[i] - 'a']++ == 0)
                ++c;
            if (i >= n && --d[s[i - n] - 'a'] == 0)
                --c;
        }

        return i;
    }
}