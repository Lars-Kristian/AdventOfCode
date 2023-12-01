using System.Numerics;
using Raylib_cs;

namespace Visualization;

public class Text
{
    private readonly string _data;
    private bool _showHighlight;
    private Rectangle _highlightRectangle;
    private float _charWidth;
    private float _charHeight;

    public Text(string data)
    {
        _data = data;
        
        var textMeasurement = Raylib.MeasureTextEx(Global.Font, "a\nb", Global.FontSize, 0);
        _charWidth = textMeasurement.X;
        _charHeight = textMeasurement.Y / 2;
    }

    public void Highlight(string text)
    {
        //text = text.Replace("\t", "    ");
        
        var data = _data.AsSpan();
        var y = -1;
        var x = 0;
        while (!data.IsEmpty)
        {
            y += 1;
            var tokenIndex = data.IndexOf('\n');
            var line = data.Slice(0, tokenIndex);
            tokenIndex = line.IndexOf(text);
            if (tokenIndex != -1)
            {
                x = tokenIndex;
                break;
            }

            data = data.Slice(line.Length + 1);
        }

        //var charSize = Raylib.MeasureTextEx(Global.Font, _data[0].ToString(), Global.FontSize, 0);

        _highlightRectangle.x = _charWidth * x;
        _highlightRectangle.y = _charHeight * (y + 0.5f);
        _highlightRectangle.width = _charWidth * text.Length;
        _highlightRectangle.height = _charHeight;

        _showHighlight = true;
    }

    public void ClearHighlight()
    {
        _showHighlight = false;
    }

    public void Render()
    {
        if (_showHighlight)
        {
            Raylib.DrawRectangle((int)_highlightRectangle.x, 
                (int)_highlightRectangle.y, 
                (int)_highlightRectangle.width, 
                (int)_highlightRectangle.height,
                Global.SecondaryColor);
        }
        
        Raylib.DrawTextEx(Global.Font, _data, new Vector2(0, 0), Global.FontSize, 0, Global.PrimaryColor);
    }
}