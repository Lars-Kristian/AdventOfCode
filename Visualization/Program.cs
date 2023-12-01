using System.Numerics;
using Raylib_cs;
using Visualization;

Console.WriteLine("Hello, World!");



Global.WindowWidth = 1280;
Global.WindowHeight = 720;
Global.FontSize = 24;

Raylib.SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_MSAA_4X_HINT);
Raylib.InitWindow(Global.WindowWidth, Global.WindowHeight, "Hello World");

var font = Raylib.LoadFontEx("font/bitstream_vera_mono/VeraMono.ttf", 24, null, 250);
Global.Font = font;
Global.PrimaryColor = Color.BLUE;
Global.SecondaryColor = Color.SKYBLUE;

var data1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
var data2 = new int[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
var data3 = new int[] { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };

var animationState = new AnimationState();

animationState.Add(i => Util.DrawAnimatedArray(i,font, data1, 0, 100, 1)).Frames(100);
animationState.Add(i => Util.DrawAnimatedArray(i,font, data2, 0, 150, 2)).Frames(100);
animationState.Add(i => Util.DrawAnimatedArray(i,font, data3, 0, 200, 3)).Frames(100);

var text = new Text(@"public static int RunA(ReadOnlySpan<char> data)
    {
        var result = 0;
        while (data.Length > 0)
        {
            var tokenIndex = data.IndexOf('-');
            var tokenText = data.Slice(0, tokenIndex);
            var aStart = int.Parse(tokenText);
            data = data.Slice(tokenIndex + 1);

            tokenIndex = data.IndexOf(',');
            tokenText = data.Slice(0, tokenIndex);
            var aStop = int.Parse(tokenText);
            data = data.Slice(tokenIndex + 1);

            tokenIndex = data.IndexOf('-');
            tokenText = data.Slice(0, tokenIndex);
            var bStart = int.Parse(tokenText);
            data = data.Slice(tokenIndex + 1);

            tokenIndex = data.IndexOf('\n');
            tokenText = data.Slice(0, tokenIndex);
            var bStop = int.Parse(tokenText);
            data = data.Slice(tokenIndex + 1);
            
            if ((bStart >= aStart && bStop <= aStop) || 
                    (aStart >= bStart && aStop <= bStop)) 
                result += 1;
        }
        
        return result;
    }");

text.Highlight("data.Length");

while (!Raylib.WindowShouldClose())
{
    
    //Console.WriteLine(Raylib.GetFrameTime());
    
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.LIGHTGRAY);

    //Raylib.DrawText("Hello, world!", 12, 12, 20, Color.BLACK);

    //var m = Raylib.MeasureTextEx(font, "Hello world!\nHello world!", 24, 0);
    //Raylib.DrawRectangle(0, 0, (int)m.X, (int)m.Y, Color.RED);
    //Raylib.DrawRectangleLines(0, 0, (int)m.X, (int)m.Y, Color.RED);

    //var m = Raylib.MeasureTextEx(font, source, 24, 0);
    text.Render();
    //animationState.MoveNext();
    
    
    Raylib.EndDrawing();
}

Raylib.CloseWindow();