namespace Recognizer
{
	public static class GrayscaleTask
	{ 
		public static double[,] ToGrayscale(Pixel[,] original)
		{
			var width = original.GetLength(0);
			var height = original.GetLength(1);
			var grayscale = new double[width, height];
			for (var i = 0; i < width; i++)
			{
				for (var j = 0; j < height; j++)
				{
					var pixel = original[i, j];
					grayscale[i, j] = (0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B) / 255;
				}
			}
			return grayscale;
		}
	}
}