using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public static class ExtensionsTask
	{
		/// <summary>
		/// Медиана списка из нечетного количества элементов — это серединный элемент списка после сортировки.
		/// Медиана списка из четного количества элементов — это среднее арифметическое 
        /// двух серединных элементов списка после сортировки.
		/// </summary>
		/// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
		public static double Median(this IEnumerable<double> items)
		{
			var newItems = items.ToList().OrderBy(item => item).ToList();
			var count = newItems.Count();
			if (count <= 0)
			{
				throw new InvalidOperationException();
			}
			return (newItems[(count - 1) / 2] + newItems[count / 2]) / 2;
		}

		/// <returns>
		/// Возвращает последовательность, состоящую из пар соседних элементов.
		/// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
		/// </returns>
		public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
		{
			var enumerator = items.GetEnumerator();
			enumerator.MoveNext();
			var oldItem = enumerator.Current;
			while (enumerator.MoveNext())
			{
				yield return Tuple.Create(oldItem, enumerator.Current);
				oldItem = enumerator.Current;
			}
		}
	}
}