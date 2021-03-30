using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class ManipulatorTask
    {
        public static double[] MoveManipulatorTo(double x, double y, double alpha)
        {
            var wristPositionsX = x - Manipulator.Palm * Math.Cos(alpha);
            var wristPositionsY = y + Manipulator.Palm * Math.Sin(alpha);
            var elbow = TriangleTask.GetABAngle(
                Manipulator.Forearm,
                Manipulator.UpperArm,
                Math.Sqrt(wristPositionsX * wristPositionsX + wristPositionsY * wristPositionsY)
                );
            var shoulder = Math.Atan2(wristPositionsY, wristPositionsX) + TriangleTask.GetABAngle(
                    Manipulator.UpperArm,
                    Math.Sqrt(wristPositionsX * wristPositionsX + wristPositionsY * wristPositionsY),
                    Manipulator.Forearm);
            if (!double.IsNaN(elbow) && !double.IsNaN(shoulder))
                return new[] {shoulder, elbow, -alpha - shoulder - elbow};
            return new[] { double.NaN, double.NaN, double.NaN };
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        [Test]
        public void TestMoveManipulatorTo()
        {
            var random = new Random();
            const int countTests = 100;
            const double maxNumber = 500;
            const double minNumber = -500;
            for (var i = 0; i < countTests; i++)
            {
                var x = random.NextDouble() * (maxNumber - minNumber) + minNumber;
                var y = random.NextDouble() * (maxNumber - minNumber) + minNumber;
                CheckGeneratedTest(x, y, Math.PI * random.NextDouble());
            }
            
        }

        public void CheckGeneratedTest(double x, double y, double angle)
        {
            var anglesJoints = ManipulatorTask.MoveManipulatorTo(
                x,
                y,
                angle);
            var palmEndPositions = AnglesToCoordinatesTask.GetJointPositions(
                anglesJoints[0],
                anglesJoints[1],
                anglesJoints[2])[2];
            if (!double.IsNaN(anglesJoints[0]))
            {
                Assert.AreEqual(x, palmEndPositions.X, 1e-4, "Wrong X");
                Assert.AreEqual(y, palmEndPositions.Y, 1e-4, "Wrong Y");
            }
            else
            {
                Assert.IsNaN(anglesJoints[0]);
            }
        }
    }
}