using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class ParsingTask
	{
		/// <param name="lines">все строки файла, которые нужно распарсить. Первая строка заголовочная.</param>
		/// <returns>Словарь: ключ — идентификатор слайда, значение — информация о слайде</returns>
		/// <remarks>Метод должен пропускать некорректные строки, игнорируя их</remarks>
		public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
		{
			return lines
				.Skip(1)
				.Select(line => line.Split(';'))
				.Where(
					slide => 
						slide.Length == 3 &&
						int.TryParse(slide[0], out _) &&
						Enum.TryParse(slide[1], true, out SlideType _))
				.Select(slide =>
				{
					Enum.TryParse(slide[1], true, out SlideType slideType);
					return new SlideRecord(
						int.Parse(slide[0]),
						slideType,
						slide[2]);
				})
				.ToDictionary(slideRecord => slideRecord.SlideId, slideRecord => slideRecord);
		}

		/// <param name="lines">все строки файла, которые нужно распарсить. Первая строка — заголовочная.</param>
		/// <param name="slides">Словарь информации о слайдах по идентификатору слайда. 
		/// Такой словарь можно получить методом ParseSlideRecords</param>
		/// <returns>Список информации о посещениях</returns>
		/// <exception cref="FormatException">Если среди строк есть некорректные</exception>
		public static IEnumerable<VisitRecord> ParseVisitRecords(
			IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
		{
			return lines
				.Skip(1)
				.Select(line =>
				{
					var visit = line.Split(';');
					try
					{
						return new VisitRecord(
							int.Parse(visit[0]), 
							int.Parse(visit[1]),
							DateTime.Parse($"{visit[2]} {visit[3]}"),
							slides[int.Parse(visit[1])].SlideType);
					}
					catch
					{
						throw new FormatException($"Wrong line [{line}]");
					}
				});
		}
	}
}