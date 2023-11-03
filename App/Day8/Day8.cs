using RunGenerator;

namespace App.Day8;

public static class Day8
{
    [GenerateRun("Day8/Day8.input")]
    public static int RunA(ReadOnlySpan<char> data)
    {
        var width = data.IndexOf('\n');
        var height = data.Length / (width + 1);

        Span<byte> state = stackalloc byte[width * height];
        
        for (var y = 0; y < height; y++)
        {
            var maxHeight = -1;
            var heightIndex = 0;
            for (var x = 0; x < width; x++)
            {
                var heightFromState = data[y * (width + 1) + x];
                if (heightFromState <= maxHeight) continue;
                state[y * width + x] = 1;
                maxHeight = heightFromState;
                heightIndex = x;
            }  
            
            maxHeight = -1;
            for (var x = width - 1; x > heightIndex; x--)
            {
                var heightFromState = data[y * (width + 1) + x];
                if (heightFromState <= maxHeight) continue;
                state[y * width + x] = 1;
                maxHeight = heightFromState;
            }
        }
        
        for (var x = 0; x < width; x++)
        {
            var maxHeight = -1;
            var heightIndex = 0;
            for (var y = 0; y < height; y++)
            {
                var heightFromState = data[y * (width + 1) + x];
                if (heightFromState <= maxHeight) continue;
                state[y * width + x] = 1;
                maxHeight = heightFromState;
            }  
            
            maxHeight = -1;
            for (var y = height - 1; y > heightIndex; y--)
            {
                var heightFromState = data[y * (width + 1) + x];
                if (heightFromState <= maxHeight) continue;
                state[y * width + x] = 1;
                maxHeight = heightFromState;
            } 
        }

        var result = 0;
        for (var i = 0; i < state.Length; i++)
        {
            result += state[i];
        }
        
        return result;
    }
    
}