using System;
using Spectre.Console;

namespace SNAKEANDLADDER_MIDTERM.styling
{
    internal class Gradient
    {
        public Color startColor, endColor;

        public Gradient(Color col1, Color col2)
        {
            startColor = col1;
            endColor = col2;
        }

        private static Color[] GenerateGradient(Color start, Color end, int steps)
        {
            Color[] gradient = new Color[steps];

            for (int i = 0; i < steps; i++)
            {
                float ratio = (float)i / (steps - 1);
                byte r = (byte)(start.R + (end.R - start.R) * ratio);
                byte g = (byte)(start.G + (end.G - start.G) * ratio);
                byte b = (byte)(start.B + (end.B - start.B) * ratio);

                gradient[i] = new Color(r, g, b);
            }

            return gradient;
        }

        public string GetGradientText(string text)
        {
            Color[] gradient = GenerateGradient(startColor, endColor, text.Length);
            System.Text.StringBuilder result = new System.Text.StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                string hexColor = $"#{gradient[i].R:X2}{gradient[i].G:X2}{gradient[i].B:X2}"; // Convert to #RRGGBB
                result.Append($"[bold {hexColor}]{text[i]}[/]");
            }

            return result.ToString();
        }
    }
}
