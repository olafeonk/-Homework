using System;
using System.Collections.Generic;

namespace DistanceTask
{
    public static class DistanceTask
    {
        private const double Eps = 1e-9;

        private static double GetLength(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        private static bool IsObtuseAngle(IReadOnlyList<double> vectorA, IReadOnlyList<double> vectorB)
            => vectorA[0] * vectorB[0] + vectorA[1] * vectorB[1] < -Eps;

        private static double[] GetVector(double x1, double y1, double x2, double y2)
        {
            var vector = new[] { x2 - x1, y2 - y1 };
            return vector;
        }
        
        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            if (IsObtuseAngle(GetVector(ax, ay, bx, by), GetVector(ax, ay, x, y)) || Math.Abs(ax - bx) < Eps && Math.Abs(ay - by) < Eps)
            {
                return GetLength(ax, ay, x, y);
            }
            if (IsObtuseAngle(GetVector(bx, by, ax, ay), GetVector(bx, by, x, y)))
            {
                return GetLength(bx, by, x, y);
            }
            return Math.Abs((by - ay) * x + y * (ax - bx) + bx * ay - ax * by) / GetLength(ax, ay, bx, by);
        }
    }
}