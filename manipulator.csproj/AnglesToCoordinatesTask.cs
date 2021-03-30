using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        /// <summary>
        /// По значению углов суставов возвращает массив координат суставов
        /// в порядке new []{elbow, wrist, palmEnd}
        /// </summary>
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var elbowPos = GetPointJoint(new PointF(0, 0), Manipulator.UpperArm, shoulder);
            var wristPos = GetPointJoint(elbowPos, Manipulator.Forearm, Math.PI + shoulder + elbow);
            var palmEndPos= GetPointJoint(wristPos, Manipulator.Palm, shoulder + elbow + wrist);
            return new[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }

        private static PointF GetPointJoint(PointF startCoordinate, float lengthJoint, double angle)
        {
            return new PointF(
                startCoordinate.X + lengthJoint * (float) Math.Cos(angle),
                startCoordinate.Y + (float) Math.Sin(angle) * lengthJoint);
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(
            Math.PI / 2,
            Math.PI / 2,
            Math.PI,
            Manipulator.Forearm,
            Manipulator.UpperArm,
            0,
            Manipulator.UpperArm,
            Manipulator.Forearm + Manipulator.Palm,
            Manipulator.UpperArm)]
        [TestCase(
            0,
            0,
            0,
            Manipulator.UpperArm - Manipulator.Forearm,
            0,
            Manipulator.UpperArm,
            0,
            Manipulator.UpperArm - Manipulator.Palm,
            0 )]
        [TestCase(
            Math.PI,
            Math.PI,
            Math.PI,
            -Manipulator.UpperArm - Manipulator.Forearm,
            0,
            -Manipulator.UpperArm,
            0,
            - Manipulator.Forearm - Manipulator.UpperArm - Manipulator.Palm,
            0)]
        [TestCase(
            - Math.PI / 2,
            - Math.PI / 2,
            - Math.PI / 2,
            Manipulator.Forearm,
            -Manipulator.UpperArm,
            0,
            -Manipulator.UpperArm,
            Manipulator.Forearm,
            Manipulator.Palm - Manipulator.UpperArm)]
        [TestCase(
            Math.PI / 4,
            Math.PI / 4, 
            -Math.PI / 4,
            106.06601715087891d,
            -13.933982849121094d,
            106.06601715087891d,
            106.06601715087891d,
            148.492431640625d,
            28.492424011230469d)]
        [TestCase(
            Math.PI / 6,
            Math.PI / 2, 
            -Math.PI / 6,
            189.90380859375d,
            -28.923049926757813d,
            129.90380859375d,
            75.0d,
            189.90380859375d,
            31.076950073242188d)]
        public void TestGetJointPositions(
            double shoulder,
            double elbow,
            double wrist,
            double elbowX,
            double elbowY,
            double wristX,
            double wristY,
            double palmEndX,
            double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(wristX, joints[0].X, 1e-5, "wristX");
            Assert.AreEqual(wristY, joints[0].Y, 1e-5, "wristY");
            Assert.AreEqual(elbowX, joints[1].X, 1e-5, "elbowX");
            Assert.AreEqual(elbowY, joints[1].Y, 1e-5, "elbowY");
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
        }
    }
}