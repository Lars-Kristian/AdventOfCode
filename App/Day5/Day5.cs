using App.Common;
using RunGenerator;

namespace App.Day5;

public static class Day5
{
    [GenerateRun("Day5/Day5.input")]
    public static string RunA(ReadOnlySpan<char> data)
    {
        var tokenIndex = data.IndexOf("\n\n");
        var drawing = data.Slice(0, tokenIndex);

        var boxCount = 0;
        for (var i = 1; i < drawing.Length; i += 1)
        {
            if (drawing[i] >= 'A' && drawing[i] <= 'Z') boxCount += 1;
        }
        
        var stackCount = 0;
        tokenIndex = drawing.IndexOf("\n");
        var line = drawing.Slice(0, tokenIndex + 1);
        stackCount = line.Length / 4;

        var height = drawing.Length / line.Length;

        Span<char> state = stackalloc char[stackCount * boxCount];
        Span<int> cursors = stackalloc int[stackCount];
        
        data = data.Slice(drawing.Length + 2);

        for (var h = height - 1; h >= 0; h--)
        {
            var drawingLine = drawing.Slice(h * line.Length, line.Length);
            
            for (var i = 0; i < stackCount; i++)
            {
                var box = drawingLine[1 + 4 * i];
                if (!(box < 'A' || box > 'Z'))
                {
                    PushCrate(state, cursors, i, box, boxCount);
                }
            }
            
            var debug = "";
        }
        
        ReadOnlySpan<char> part1 = stackalloc char[] {'m', 'o', 'v', 'e', ' '};
        ReadOnlySpan<char> part2 = stackalloc char[] {' ', 'f', 'r', 'o', 'm', ' ', };
        ReadOnlySpan<char> part3 = stackalloc char[] {' ', 't', 'o', ' '};
        ReadOnlySpan<char> part4 = stackalloc char[] {'\n'};
        while (!data.IsEmpty)
        {
            data = data.Slice(part1.Length);
            var countSpan = data.Slice(0, data.IndexOf(part2));
            data = data.Slice(countSpan.Length + part2.Length);
            var fromSpan = data.Slice(0, data.IndexOf(part3));
            data = data.Slice(fromSpan.Length + part3.Length);
            var toSpan = data.Slice(0, data.IndexOf(part4));
            data = data.Slice(toSpan.Length + part4.Length);

            var count = ParseUtil.ParseIntFast(countSpan);
            var from = ParseUtil.ParseIntFast(fromSpan) - 1;
            var to = ParseUtil.ParseIntFast(toSpan) - 1;

            if (cursors[from] - count < 0) count = cursors[from];

            for (var i = 0; i < count; i++)
            {
                var crate = PopCrate(state, cursors, from, boxCount);
                PushCrate(state, cursors, to, crate, boxCount);
            }
            
        }

        var result = string.Empty;
        
        for (var i = 0; i < cursors.Length; i++)
        {
            result += state[boxCount * i + cursors[i] - 1];
        }


        return result;
    }

    private static void PushCrate(Span<char> state, Span<int> cursors, int cursorIndex, char crate, int width)
    {
        var indexInState = width * cursorIndex + cursors[cursorIndex];
        state[indexInState] = crate;
        cursors[cursorIndex] += 1;
    }
    
    private static char PopCrate(Span<char> state, Span<int> cursors, int cursorIndex, int width)
    {
        var indexInState = width * cursorIndex + cursors[cursorIndex] - 1;
        cursors[cursorIndex] -= 1;
        return state[indexInState];
    }
}

/*
    [W]         [J]     [J]
    [V]     [F] [F] [S] [S]
    [S] [M] [R] [W] [M] [C]
    [M] [G] [W] [S] [F] [G]     [C]
[W] [P] [S] [M] [H] [N] [F]     [L]
[R] [H] [T] [D] [L] [D] [D] [B] [W]
[T] [C] [L] [H] [Q] [J] [B] [T] [N]
[G] [G] [C] [J] [P] [P] [Z] [R] [H]
 1   2   3   4   5   6   7   8   9
*/

//maybe count width and height -> faster
//3 11 17 24 32 41 50 59