using System;
using System.Windows.Media;

namespace Quizlr.Lib.Utility
{
    public static class ColorExtensions
    {
        public static Color Lerp(this Color from, Color to, float amount)
        {
            float fr = from.R, fg = from.G, fb = from.B;
            float tr = to.R, tg = to.G, tb = to.B;
            byte r = (byte) fr.Lerp(tr, amount),
                g = (byte) fg.Lerp(tg, amount),
                b = (byte) fb.Lerp(tb, amount);
            return Color.FromRgb(r, g, b);
        }

        public static Color Lighten(this Color color, float amount)
        {
            return color.Lerp(Colors.White, amount);
        }

        public static Color Darken(this Color color, float amount)
        {
            return color.Lerp(Colors.Black, amount);
        }

        public static int GetBrightness(this Color color)
        {
            return (int) Math.Sqrt(
                color.R*color.R*.299 +
                color.G*color.G*.578 +
                color.B*color.B*.114);
        }

        public static Color GetForegroundColor(this Color color)
        {
            return color.GetBrightness() > 130 ? Colors.Black : Colors.White;
        }
    }
}