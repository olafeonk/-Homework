using System;
using NUnit.Framework;

namespace Manipulation
{
    public static class TriangleTask
    {
        public static double GetABAngle(double a, double b, double c)
        {
            if (a > 0 && b > 0 && c >= 0 && a + b >= c && b + c >= a && a + c >= b)
            {
                return Math.Acos((a * a + b * b - c * c) / (2 * a * b));
            }    
            return double.NaN;
        }
    }

    [TestFixture]
    public class TriangleTask_Tests
    {
        [TestCase(3, 4, 5, Math.PI / 2)]
        [TestCase(1, 1, 100, double.NaN)]
        [TestCase(3, 3, 6, Math.PI)]
        [TestCase(6, 3, 3, 0)]
        [TestCase(-1, 2, 3, double.NaN)]
        [TestCase(1, -3, 3, double.NaN)]
        [TestCase(0.3, 0.4, 0.5, Math.PI / 2)]
        [TestCase(0,0,0, double.NaN)]
        [TestCase(1, 1, -1, double.NaN)]
        [TestCase(10, 10, 10, Math.PI / 3)]
        [TestCase(1, 1, 0, 0)]
        public void TestGetABAngle(double a, double b, double c, double expectedAngle)
        {
            var angle = TriangleTask.GetABAngle(a, b, c);
            Assert.AreEqual(expectedAngle, angle, 1e-5, "Wrong cos");
        }
    }
}