using System;
using System.Collections.Generic;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        private static readonly int[] Dx = {1, -1, 0, 0, 1, -1, };
        private static readonly int[] Dy = {-1, 0, 1, -1, 0, 1, -1, 0, 1};
        private static double GetMedian(double[] array)
        {
            var length = array.Length;
            Array.Sort(array);
            if (length % 2 == 0)
                return (array[length / 2] + array[length / 2 - 1]) / 2;
            return array[length / 2];
        }

        private static double[] GetBoundPixels(double[,] original, int x, int y)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var boundPixels = new List<double>();
            for (var i = 0; i < 9; i++)
            {
                var newX = Dx[i] + x;
                var newY = Dy[i] + y;
                if (newX < width && newX >= 0 && newY < height && newY >= 0)
                {
                    boundPixels.Add(original[newX, newY]);
                }	
            }
            return boundPixels.ToArray();
        }
		
        public static double[,] MedianFilter(double[,] original)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var medianFilter = new double[width, height];
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    medianFilter[i, j] = GetMedian(GetBoundPixels(original, i, j));
                }
            }
            return medianFilter;
        }
    }
}