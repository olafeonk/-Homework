using System.Collections.Generic;

namespace yield
{
	public static class MovingAverageTask
	{
		public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
		{
			var result = 0.0;
			var queue = new Queue<double>();
			foreach (var item in data)
			{
				result += item.OriginalY;
				if (windowWidth <= queue.Count)
				{
					result -= queue.Dequeue();
				}
				queue.Enqueue(item.OriginalY);
				yield return item.WithAvgSmoothedY(result / queue.Count);
			}
		}
	}
}