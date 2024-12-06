using System.Text;
using BenchmarkGenerator;
using RunGenerator;

namespace AdventOfCode2024.App.Day6;

public class Day6
{
    [GenerateRun("Day6/Day6.input")]
    //[GenerateRun("Day6/Day6-test.input")]
    [GenerateBenchmark("Day6/Day6.input")]
    public static int RunA(ReadOnlySpan<char> input)
    {
        Span<bool> map = stackalloc bool[input.Length];
        
        var width = input.IndexOf('\n');
        var height = input.Length / width - 1;

        var position = input.IndexOf('^');

        var x = position % (width + 1);
        var y = position / (width + 1);

        width += 1;
        
        map[y * width + x] = true;

        while (true)
        {
            var done = true;
            for (var i = y; i >= 0; i--)
            {
                if (input[i * width + x] == '#')
                {
                    y = i + 1;
                    done = false;
                    break;
                }
                
                map[i * width + x] = true;
            }
            
            if(done) break;
            done = true;
            
            for (var i = x; i < width - 1; i++)
            {
                if (input[y * width + i] == '#')
                {
                    x = i - 1;
                    done = false;
                    break;
                }
                
                map[y * width + i] = true;
            }
            
            if(done) break;
            done = true;
            
            for (var i = y; i < height; i++)
            {
                if (input[i * width + x] == '#')
                {
                    y = i - 1;
                    done = false;
                    break;
                }
                
                map[i * width + x] = true;
            }
            
            if(done) break;
            done = true;
            
            for (var i = x; i >= 0; i--)
            {
                if (input[y * width + i] == '#')
                {
                    x = i + 1;
                    done = false;
                    break;
                }
                
                map[y * width + i] = true;
            }
            
            if(done) break;
            done = true;
        }

        return map.Count(true);
    }

    [GenerateRun("Day6/Day6.input")]
    //[GenerateRun("Day6/Day6-test.input")]
    [GenerateBenchmark("Day6/Day6.input")]
    public static int RunA2(ReadOnlySpan<char> input)
    {
        Span<bool> map = stackalloc bool[input.Length];
        
        var width = input.IndexOf('\n');
        var height = input.Length / width - 1;

        var position = input.IndexOf('^');

        var x = position % (width + 1);
        var y = position / (width + 1);

        width += 1;

        MarkGuardPath(input, map, width, height, x, y);

        return map.Count(true);
    }
    
    [Flags]
    public enum Direction : byte
    {
        Undefined = 0,
        Up = 1,
        Right = 2,
        Down = 4,
        Left = 8,
    }
    
    [GenerateRun("Day6/Day6.input")]
    //[GenerateRun("Day6/Day6-test.input")]
    [GenerateBenchmark("Day6/Day6.input")]
    public static int RunB(ReadOnlySpan<char> originalInput)
    {
        Span<char> input = stackalloc char[originalInput.Length];
        originalInput.CopyTo(input);
        
        Span<bool> map = stackalloc bool[input.Length];
        
        var width = input.IndexOf('\n');
        var height = input.Length / width - 1;

        var position = input.IndexOf('^');

        var x = position % (width + 1);
        var y = position / (width + 1);

        width += 1;

        MarkGuardPath(input, map, width, height, x, y);

        var result = 0;
        for (var i = 0; i < map.Length; i++)
        {
            if(!map[i] || i == position) continue;

            var tmp = input[i];
            input[i] = '#';

            if (!WillGuardFindEdge(input, width, height, x, y))
            {
                result += 1;
            }
            
            input[i] = tmp;
        }
        
        return result;
    }
    
    [GenerateRun("Day6/Day6.input")]
    //[GenerateRun("Day6/Day6-test.input")]
    [GenerateBenchmark("Day6/Day6.input")]
    public static int RunB2(ReadOnlySpan<char> originalInput)
    {
        Span<char> input = stackalloc char[originalInput.Length];
        originalInput.CopyTo(input);
        
        Span<bool> map = stackalloc bool[input.Length];
        
        var width = input.IndexOf('\n');
        var height = input.Length / width - 1;

        var position = input.IndexOf('^');

        var x = position % (width + 1);
        var y = position / (width + 1);

        width += 1;

        MarkGuardPath(input, map, width, height, x, y);

        var result = 0;
        for (var i = 0; i < map.Length; i++)
        {
            if(!map[i] || i == position) continue;

            var tmp = input[i];
            input[i] = '#';

            if (!WillGuardFindEdge2(input, width, height, x, y))
            {
                result += 1;
            }
            
            input[i] = tmp;
        }
        
        return result;
    }
    
    private static void MarkGuardPath(ReadOnlySpan<char> input, Span<bool> map, int width, int height, int x, int y)
    {
        map[y * width + x] = true;

        while (true)
        {
            var done = true;
            for (var i = y; i >= 0; i--)
            {
                if (input[i * width + x] == '#')
                {
                    y = i + 1;
                    done = false;
                    break;
                }
                
                map[i * width + x] = true;
            }
            
            if(done) break;
            done = true;
            
            for (var i = x; i < width - 1; i++)
            {
                if (input[y * width + i] == '#')
                {
                    x = i - 1;
                    done = false;
                    break;
                }
                
                map[y * width + i] = true;
            }
            
            if(done) break;
            done = true;
            
            for (var i = y; i < height; i++)
            {
                if (input[i * width + x] == '#')
                {
                    y = i - 1;
                    done = false;
                    break;
                }
                
                map[i * width + x] = true;
            }
            
            if(done) break;
            done = true;
            
            for (var i = x; i >= 0; i--)
            {
                if (input[y * width + i] == '#')
                {
                    x = i + 1;
                    done = false;
                    break;
                }
                
                map[y * width + i] = true;
            }
            
            if(done) break;
            done = true;
        }
    }
    
    public static bool WillGuardFindEdge(ReadOnlySpan<char> input, int width, int height, int x, int y)
    {
        Span<Direction> walkMap = stackalloc Direction[input.Length];
        
        while (true)
        {
            var done = true;
            for (var i = y; i >= 0; i--)
            {
                if (input[i * width + x] == '#')
                {
                    y = i + 1;
                    done = false;
                    break;
                }
                
                if ((walkMap[i * width + x] & Direction.Up) == Direction.Up)
                {
                    //Console.WriteLine(MapToString(walkMap, width, height));
                    return false;
                }
                
                walkMap[i * width + x] |= Direction.Up;
            }
            
            if(done) break;
            done = true;
            
            for (var i = x; i < width - 1; i++)
            {
                if (input[y * width + i] == '#')
                {
                    x = i - 1;
                    done = false;
                    break;
                }
                
                if ((walkMap[y * width + i] & Direction.Right) == Direction.Right)
                {
                    //Console.WriteLine(MapToString(walkMap, width, height));
                    return false;
                }
                
                walkMap[y * width + i] |= Direction.Right;
            }
            
            if(done) break;
            done = true;
            
            for (var i = y; i < height; i++)
            {
                if (input[i * width + x] == '#')
                {
                    y = i - 1;
                    done = false;
                    break;
                }
                
                if ((walkMap[i * width + x] & Direction.Down) == Direction.Down)
                {
                    //Console.WriteLine(MapToString(walkMap, width, height));
                    return false;
                }
                
                walkMap[i * width + x] |= Direction.Down;
            }
            
            if(done) break;
            done = true;
            
            for (var i = x; i >= 0; i--)
            {
                if (input[y * width + i] == '#')
                {
                    x = i + 1;
                    done = false;
                    break;
                }
                
                if ((walkMap[y * width + i] & Direction.Left) == Direction.Left)
                {
                    //Console.WriteLine(MapToString(walkMap, width, height));
                    return false;
                }
                
                walkMap[y * width + i] |= Direction.Left;
            }
            
            if(done) break;
            done = true;
        }

        return true;
    }
    
    public static bool WillGuardFindEdge2(ReadOnlySpan<char> input, int width, int height, int x, int y)
    {
        Span<Direction> walkMap = stackalloc Direction[input.Length];
        
        while (true)
        {
            var done = true;
            for (var i = y; i >= 0; i--)
            {
                if (input[i * width + x] == '#')
                {
                    if ((walkMap[(i + 1) * width + x + 1] & Direction.Right) == Direction.Right) return false;
                    y = i + 1;
                    done = false;
                    break;
                }
                
                walkMap[i * width + x] |= Direction.Up;
            }
            
            if(done) break;
            done = true;
            
            for (var i = x; i < width - 1; i++)
            {
                if (input[y * width + i] == '#')
                {
                    if ((walkMap[(y + 1) * width + (i - 1)] & Direction.Down) == Direction.Down) return false;
                    x = i - 1;
                    done = false;
                    break;
                }
                
                
                walkMap[y * width + i] |= Direction.Right;
            }
            
            if(done) break;
            done = true;
            
            for (var i = y; i < height; i++)
            {
                if (input[i * width + x] == '#')
                {
                    if ((walkMap[(i - 1) * width + x - 1] & Direction.Left) == Direction.Left) return false;
                    y = i - 1;
                    done = false;
                    break;
                }
                
                
                walkMap[i * width + x] |= Direction.Down;
            }
            
            if(done) break;
            done = true;
            
            for (var i = x; i >= 0; i--)
            {
                if (input[y * width + i] == '#')
                {
                    if ((walkMap[(y + 1) * width + (i + 1)] & Direction.Up) == Direction.Up) return false;
                    x = i + 1;
                    done = false;
                    break;
                }
                
                walkMap[y * width + i] |= Direction.Left;
            }
            
            if(done) break;
            done = true;
        }

        return true;
    }
    
    
    private static string MapToString(Span<Direction> map, int width, int height)
    {
        var sb = new StringBuilder();

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (map[y * width + x] == Direction.Undefined)
                {
                    sb.Append(".");
                }
                else
                {
                    if (map[y * width + x] == (Direction.Up | Direction.Right))
                    {
                        sb.Append("\u250c");
                    } else if (map[y * width + x] == (Direction.Right | Direction.Down))
                    {
                        sb.Append("\u2510");
                    } else if (map[y * width + x] == (Direction.Down | Direction.Left))
                    {
                        sb.Append("\u2518");
                    } else if (map[y * width + x] == (Direction.Left | Direction.Up))
                    {
                        sb.Append("\u2514");
                    }
                    
                    else if ((map[y * width + x] & Direction.Right) == Direction.Right ||
                               (map[y * width + x] & Direction.Left) == Direction.Left)
                    {
                        sb.Append("\u2500");
                    } else if ((map[y * width + x] & Direction.Down) == Direction.Down ||
                              (map[y * width + x] & Direction.Up) == Direction.Up)
                    {
                        sb.Append("\u2502");
                    }
                }
            }   
            sb.Append("\n");
        }
        
        return sb.ToString();
    }
}