using System.Collections.Generic;

namespace yield
{
	public static class MovingMaxTask
	{
		public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
		{
			var items = new LinkedList<double>();
			var itemsX = new Queue<double>();
			foreach (var item in data)
			{
				itemsX.Enqueue(item.X);
				if (itemsX.Count > windowWidth && items.First.Value <= itemsX.Dequeue())
				{
					items.RemoveFirst();
					items.RemoveFirst();
				}

				while (items.Count != 0 && items.Last.Value < item.OriginalY)
				{
					items.RemoveLast();
					items.RemoveLast();
				}
				items.AddLast(item.X);
				items.AddLast(item.OriginalY);
				yield return item.WithMaxY(items.First.Next.Value);
			}
		}
	}
}