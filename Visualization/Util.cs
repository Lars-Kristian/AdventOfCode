using System.Numerics;
using Raylib_cs;

namespace Visualization;

public class Util
{
    public static void DrawAnimatedArray(int frame, Font font, int[] data, int x, int y, int mark)
    {
        var max = data.Max();
        var maxText = max.ToString();
        var maxSize = Raylib.MeasureTextEx(font, max.ToString(), 24, 0);

        var padding = 5;

        var test = (frame / 100) + 1 ;

        var currentX = x;
        for (var i = 0; i < test; i++)
        {
            var text = LeftPad(data[i].ToString(), maxText.Length);
            if (i == mark)
            {
                Raylib.DrawRectangle(currentX, y, (int)maxSize.X + padding * 2, (int)maxSize.Y + padding * 2,
                    Color.SKYBLUE);
            }

            Raylib.DrawRectangleLines(currentX, y, (int)maxSize.X + padding * 2, (int)maxSize.Y + padding * 2,
                Color.BLUE);
            Raylib.DrawTextEx(font, text, new Vector2(currentX + padding, y + padding), 24, 0, Color.BLUE);
            currentX += (int)maxSize.X + padding * 2;
        }
    }


    public static void DrawArray(Font font, int[] data, int x, int y, int mark)
    {
        var max = data.Max();
        var maxText = max.ToString();
        var maxSize = Raylib.MeasureTextEx(font, max.ToString(), 24, 0);

        var padding = 5;

        var currentX = x;
        for (var i = 0; i < data.Length; i++)
        {
            var text = LeftPad(data[i].ToString(), maxText.Length);
            if (i == mark)
            {
                Raylib.DrawRectangle(currentX, y, (int)maxSize.X + padding * 2, (int)maxSize.Y + padding * 2,
                    Color.SKYBLUE);
            }

            Raylib.DrawRectangleLines(currentX, y, (int)maxSize.X + padding * 2, (int)maxSize.Y + padding * 2,
                Color.BLUE);
            Raylib.DrawTextEx(font, text, new Vector2(currentX + padding, y + padding), 24, 0, Color.BLUE);
            currentX += (int)maxSize.X + padding * 2;
        }
    }

    private static string LeftPad(string text, int size)
    {
        while (text.Length < size)
        {
            text = " " + text;
        }

        return text;
    }


    public static void DrawArrayOld(Font font, int[] data, int x, int y)
    {
        var max = data.Max();
        var maxText = max.ToString();
        var maxSize = Raylib.MeasureTextEx(font, max.ToString(), 24, 0);

        var padding = 5;

        var currentX = x;
        for (var i = 0; i < data.Length; i++)
        {
            var text = LeftPad(data[i].ToString(), maxText.Length);
            Raylib.DrawRectangleLines(currentX, y, (int)maxSize.X + padding * 2, (int)maxSize.Y + padding * 2,
                Color.BLUE);
            Raylib.DrawTextEx(font, text, new Vector2(currentX + padding, y + padding), 24, 0, Color.BLUE);
            currentX += (int)maxSize.X + padding * 2;
        }
    }
}