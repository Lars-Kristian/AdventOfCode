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
    
    
    [GenerateRun("Day8/Day8.input")]
    public static long RunB(ReadOnlySpan<char> data)
    {
        var width = data.IndexOf('\n');
        var height = data.Length / (width + 1);

        Span<long> state = stackalloc long[width * height];
        state.Fill(1);
        
        for (var y = 1; y < height - 1; y++)
        {
            for (var x = 1; x < width - 1; x++)
            {
                var treeHeight = data[y * (width + 1) + x];

                var done = false;

                for (var i = x + 1; i < width; i++)
                {
                    if (data[y * (width + 1) + i] < treeHeight) continue;
                    
                    state[y * width + x] *= i - x;
                    done = true;
                    break;
                }

                if (!done) state[y * width + x] *= width - 1 - x;

                done = false;
                for (var i = x - 1; i >= 0; i--)
                {
                    if (data[y * (width + 1) + i] < treeHeight) continue;
                    
                    state[y * width + x] *= x - i;
                    done = true;
                    break;
                }
                
                if (!done) state[y * width + x] *= x;
                done = false;
                
                for (var i = y + 1; i < height; i++)
                {
                    if (data[i * (width + 1) + x] < treeHeight) continue;
                    
                    state[y * width + x] *= i - y;
                    done = true;
                    break;
                }
                
                if (!done) state[y * width + x] *= height - 1 - y;
                done = false;
                
                for (var i = y - 1; i >= 0; i--)
                {
                    if (data[i * (width + 1) + x] < treeHeight) continue;
                    
                    state[y * width + x] *= y - i;
                    done = true;
                    break;
                }
                
                if (!done) state[y * width + x] *= y;
            }  
        }

        long result = 0;
        for (var i = 0; i < state.Length; i++)
        {
            result = state[i] > result ? state[i] : result;
        }
        
        return result;
    }

        [GenerateRun("Day8/Day8.input")]
    public static long RunB2(ReadOnlySpan<char> data)
    {
        var width = data.IndexOf('\n');
        var height = data.Length / (width + 1);

        Span<long> state = stackalloc long[width * height];
        state.Fill(1);
        
        for (var y = 1; y < height - 1; y++)
        {
            for (var x = 1; x < width - 1; x++)
            {
                var treeHeight = data[y * (width + 1) + x];

                var view = 0;
                for (var i = x + 1; i < width; i++)
                {
                    view += 1;
                    if (data[y * (width + 1) + i] < treeHeight) continue;
                    break;
                }

                state[y * width + x] *= view;

                
                view = 0;
                for (var i = x - 1; i >= 0; i--)
                {
                    view += 1;
                    if (data[y * (width + 1) + i] < treeHeight) continue;
                    break;
                }
                
                state[y * width + x] *= view;
                
                
                view = 0;
                for (var i = y + 1; i < height; i++)
                {
                    view += 1;
                    if (data[i * (width + 1) + x] < treeHeight) continue;
                    break;
                }
                
                state[y * width + x] *= view;
                
                view = 0;
                for (var i = y - 1; i >= 0; i--)
                {
                    view += 1;
                    if (data[i * (width + 1) + x] < treeHeight) continue;
                    break;
                }
                
                state[y * width + x] *= view;
            }  
        }

        long result = 0;
        for (var i = 0; i < state.Length; i++)
        {
            result = state[i] > result ? state[i] : result;
        }
        
        return result;
    }

}