using System.Numerics;
using Raylib_cs;
using Visualization;

Console.WriteLine("Hello, World!");


Raylib.SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_MSAA_4X_HINT);
Raylib.InitWindow(1280, 720, "Hello World");

var font = Raylib.LoadFontEx("font/bitstream_vera_mono/VeraMono.ttf", 24, null, 250);

var data1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
var data2 = new int[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
var data3 = new int[] { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };

var animationState = new AnimationState();

animationState.Add(i => Util.DrawAnimatedArray(i,font, data1, 0, 100, 1));
animationState.Add(i => Util.DrawAnimatedArray(i,font, data2, 0, 150, 2));
animationState.Add(i => Util.DrawAnimatedArray(i,font, data3, 0, 200, 3));

while (!Raylib.WindowShouldClose())
{
    
    //Console.WriteLine(Raylib.GetFrameTime());
    
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.LIGHTGRAY);

    //Raylib.DrawText("Hello, world!", 12, 12, 20, Color.BLACK);

    var m = Raylib.MeasureTextEx(font, "Hello world!\nHello world!", 24, 0);
    //Raylib.DrawRectangle(0, 0, (int)m.X, (int)m.Y, Color.RED);
    Raylib.DrawRectangleLines(0, 0, (int)m.X, (int)m.Y, Color.RED);

    Raylib.DrawTextEx(font, "Hello world! \nHello world!", new Vector2(0, 0), 16, 0, Color.BLUE);

    animationState.MoveNext();
    
    
    Raylib.EndDrawing();
}

Raylib.CloseWindow();