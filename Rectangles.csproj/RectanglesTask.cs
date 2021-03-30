using System;

namespace Rectangles
{
    public static class RectanglesTask
    {
        public static bool IsSetSmaller(int[] smallSet, int[] bigSet)
        {
            for (int i = 0; i < Math.Min(smallSet.Length, bigSet.Length); i++)
            {
                if (smallSet[i] > bigSet[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            var upperBounds = new[] { r2.Left, r1.Left, r1.Top, r2.Top };
            var lowerBounds = new[] { r1.Right, r2.Right, r2.Bottom, r1.Bottom };
            return IsSetSmaller(upperBounds, lowerBounds);
        }

        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            var widthIntersection = GetIntersectionDistance(r1.Right, r2.Right, r1.Left, r2.Left);
            var heightIntersection = GetIntersectionDistance(r1.Bottom, r2.Bottom, r1.Top, r2.Top);
            if (widthIntersection < 0 || heightIntersection < 0)
            {
                return 0;
            }
            return widthIntersection * heightIntersection;
        }

        public static int GetIntersectionDistance(int upperBound1, int upperBound2, int lowerBound1, int lowerBound2)
        {
            return Math.Min(upperBound1, upperBound2) - Math.Max(lowerBound1, lowerBound2);
        }

        public static bool IsRectangleInscribed(Rectangle r1, Rectangle r2)
        {
            var internalBounds = new[] { r2.Left, r1.Right, r2.Top, r1.Bottom };
            var outerBounds = new[] { r1.Left, r2.Right, r1.Top, r2.Bottom };
            return IsSetSmaller(internalBounds, outerBounds);
        }

        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        {
            if (IsRectangleInscribed(r1, r2))
            {
                return 0;
            }
            if (IsRectangleInscribed(r2, r1))
            {
                return 1;
            }
            return -1;
        }
    }
}