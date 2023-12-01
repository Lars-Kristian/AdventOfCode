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

        //var test = Math.Clamp((frame / 2) + 1, 0, data.Length);
        
        var currentX = x;
        for (var i = 0; i < data.Length; i++)
        {
            var l = Easings.InverseLerp(i * 4, (i + 1) * 4, frame);
            var newY = (int)Easings.Lerp(y - 50, y, Easings.EaseCubicIn(l, 0, 1, 1));
            
            var a = (int)Easings.Lerp(0f, 255f, l);
            var tintBlue = new Color(0, 121, 241, a);
            var tintSkyBlue = new  Color(102, 191, 255, a);
            
            //var asd = Color.BLUE
            
            var text = LeftPad(data[i].ToString(), maxText.Length);
            if (i == mark)
            {
                Raylib.DrawRectangle(currentX, newY, (int)maxSize.X + padding * 2, (int)maxSize.Y + padding * 2,
                    tintSkyBlue);
            }

            Raylib.DrawRectangleLines(currentX, newY, (int)maxSize.X + padding * 2, (int)maxSize.Y + padding * 2,
                tintBlue);
            
            Raylib.DrawTextEx(font, text, new Vector2(currentX + padding, newY + padding), 24, 0, tintBlue);
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