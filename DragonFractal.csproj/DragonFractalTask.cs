using System;
using System.Xml.Schema;

namespace Fractals
{
    internal static class DragonFractalTask
    {
        public static void CreateСoordinateFirst(ref double x, ref double y)
        {
            var newX = (x - y) / 2;
            var newY = (x + y) / 2;
            ChangeСoordinate(newX, newY, ref x, ref y);
        }

        public static void CreateСoordinateSecond(ref double x, ref double y)
        {
            var newX = 1 - (x + y) / 2;
            var newY = (x - y) / 2;
            ChangeСoordinate(newX, newY, ref x, ref y);
        }

        public static void ChangeСoordinate(double newX, double newY, ref double x, ref double y)
        {
            x = newX;
            y = newY;
        }

        public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
        {
            var random = new Random(seed);
            var x = 1.0;
            var y = 0.0;
            for (var i = 0; i <= iterationsCount; i++)
            {
                pixels.SetPixel(x, y);
                var nextNumber = random.Next(2);
                if (nextNumber == 1)
                {
                    CreateСoordinateFirst(ref x, ref y);
                }
                else
                {
                    CreateСoordinateSecond(ref x, ref y);
                }
            }
        }
    }
}