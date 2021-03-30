using System.Collections.Generic;

namespace yield
{
	public static class ExpSmoothingTask
	{
		public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
		{
			var isFirstStep = true;
			var previous = 1.0;
			foreach (var item in data)
			{
				if (isFirstStep)
				{
					isFirstStep = false;
					previous = item.OriginalY;
				}
				else
				{
					previous = alpha * item.OriginalY + (1 - alpha) * previous;
				}
				yield return item.WithExpSmoothedY(previous);;
			}
		}
	}
}