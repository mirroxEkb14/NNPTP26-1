using System;
using System.Drawing;

namespace NNPTPZ1.Rendering
{
    public class ColorMapper
    {
        private readonly Color[] _palette = new Color[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        private static int ClampByte(int v) => Math.Min(Math.Max(0, v), 255);

        public Color MapColor(int rootIndex, int iterations)
        {
            var baseColor = _palette[(rootIndex % _palette.Length + _palette.Length) % _palette.Length];
            int r = ClampByte(baseColor.R - iterations * 2);
            int g = ClampByte(baseColor.G - iterations * 2);
            int b = ClampByte(baseColor.B - iterations * 2);
            return Color.FromArgb(r, g, b);
        }

        public struct Rgb { public int R; public int G; public int B; }

        public Rgb MapRgb(int rootIndex, int iterations)
        {
            var c = MapColor(rootIndex, iterations);
            return new Rgb { R = c.R, G = c.G, B = c.B };
        }
    }
}
