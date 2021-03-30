using System;
using System.Collections.Generic;

namespace func_rocket
{
	public static class LevelsTask
	{
		private static readonly Physics StandardPhysics = new Physics();
		private static readonly Vector Target = new Vector(600, 200);
		private static readonly Rocket Rocket = new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);
		private static List<Level> Levels = new List<Level>();
		public static Level GenerateLevel(string name, Gravity gravity, Vector target=null)
		{
			return	new Level(name,
				Rocket,
				target ?? Target,
				gravity,
				StandardPhysics);
		}
		public static IEnumerable<Level> CreateLevels()
		{
			Levels.Add(GenerateLevel("Zero", (size, v) => Vector.Zero));
			yield return new Level("Zero", 
				Rocket,
				Target, 
				(size, v) => Vector.Zero, StandardPhysics);
			yield return new Level("Heavy",
				Rocket,
				Target,
				(size, location) => new Vector(0, 0.9), StandardPhysics);
			yield return new Level("Up",
				Rocket,
				new Vector(700, 500),
				(size, location) => new Vector(0, -(300/ (600 - location.Y + 300))), StandardPhysics);
			yield return new Level("WhiteHole", 
				Rocket,
				Target,
				(size, location) =>
				{
					var d = Math.Sqrt((600 - location.X) * (600 - location.X) +
					                  (200 - location.Y) * (200 - location.Y));
					
					return (new Vector(600, 200) - location).Normalize() * -140*d/(d * d + 1) ;
				}, StandardPhysics);
			yield return new Level("BlackHole", 
				Rocket,
				Target,
				(size, location) =>
				{
					var anomaly = ((new Vector(600, 200) + new Vector(200, 500)) / 2 - location);
					var d = anomaly.Length;
					return anomaly.Normalize() * 300 * d / (d * d + 1);
				}, StandardPhysics);
			yield return new Level("BlackAndWhite",
				Rocket,
				new Vector(600, 200),
				((size, location) =>
				{
					var anomaly = ((new Vector(600, 200) + new Vector(200, 500)) / 2 - location);
					var d = anomaly.Length;
					var gravity = anomaly.Normalize() * 300 * d / (d * d + 1);
					d = Math.Sqrt((600 - location.X) * (600 - location.X) +
					                  (200 - location.Y) * (200 - location.Y));

					return ((new Vector(600, 200) - location).Normalize() * -140 * d / (d * d + 1) + gravity) / 2;
				}), StandardPhysics);
		}
	}
}