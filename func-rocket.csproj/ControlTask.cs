using System;

namespace func_rocket
{
	public static class ControlTask
	{
		public static Turn ControlRocket(Rocket rocket, Vector target)
		{
			var targetAngle = new Vector(target.X - rocket.Location.X, target.Y - rocket.Location.Y).Angle;
			var directionAngle = targetAngle - rocket.Direction;
			var velocityAngle = targetAngle - rocket.Velocity.Angle;
			var resultAngle =
				Math.Abs(directionAngle) < 0.5 || Math.Abs(velocityAngle) < 0.5
				? (directionAngle + velocityAngle) / 2
				: directionAngle;
			return resultAngle < 0 ? Turn.Left : resultAngle > 0 ? Turn.Right : Turn.None;
		}
	}
}