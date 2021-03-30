using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace RefactorMe
{
    class Painter
    {
        static float x, y;
        static Graphics graphics;

        public static void Init(Graphics newPicture)
        {
            graphics = newPicture;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.Clear(Color.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            x = x0;
            y = y0;
        }

        public static void DrawTrajectory(Pen writePen, double len, double angle)
        {
            var x1 = GetX(len, angle) + x;
            var y1 = GetY(len, angle) + y;
            graphics.DrawLine(writePen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void ChangeCoordinates(double len, double angle)
        {
            x = GetX(len, angle) + x;
            y = GetY(len, angle) + y;
        }

        public static float GetX(double len, double angle)
        {
            return (float)(len * Math.Cos(angle));
        }

        public static float GetY(double len, double angle)
        {
            return (float)(len * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        static float longSideSize;
        static float shortSideSize;
        public static void Draw(int width, int height, double angleRotation, Graphics picture)
        {
            Painter.Init(picture);
            var size = Math.Min(width, height);
            longSideSize = 0.375f * size;
            shortSideSize = 0.04f * size;
            var diagonalLength = Math.Sqrt(2) * (longSideSize + shortSideSize) / 2;
            var x0 = Painter.GetX(diagonalLength, Math.PI / 4 + Math.PI) + width / 2f;
            var y0 = Painter.GetY(diagonalLength, Math.PI / 4 + Math.PI) + height / 2f;
            Painter.SetPosition(x0, y0);
            for (int i = 0; i < 4; i++)
            {
                DrawSide(angleRotation);
                angleRotation -= Math.PI / 2;
            }
        }

        private static void DrawSide(double angleRotation)
        {
            Painter.DrawTrajectory(Pens.Yellow, longSideSize, angleRotation);
            Painter.DrawTrajectory(Pens.Yellow, shortSideSize * Math.Sqrt(2), angleRotation + Math.PI / 4);
            Painter.DrawTrajectory(Pens.Yellow, longSideSize, angleRotation + Math.PI);
            Painter.DrawTrajectory(Pens.Yellow, longSideSize - shortSideSize, angleRotation + Math.PI / 2);
            Painter.ChangeCoordinates(shortSideSize, angleRotation + Math.PI);
            Painter.ChangeCoordinates(shortSideSize * Math.Sqrt(2), angleRotation + 3 * Math.PI / 4);
        }
    }
}