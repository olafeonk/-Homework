using System;
using System.Collections.Generic;
using System.Linq;
namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        private static double[] GetSortLine(double[,] array)
        {
            var lineList = array.Cast<double>().ToList();
            lineList.Sort();
            lineList.Reverse();
            return lineList.ToArray();
        }

        private static double[,] DoWhiteAndBlackImage(double thresholdValue, double[,] original)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var blackAndWhiteImage = new double[width, height];
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    blackAndWhiteImage[i, j] = (original[i, j] >= thresholdValue) ? 1 : 0;
                }
            }
            return blackAndWhiteImage;
        }

        private static double GetThresholdValue(IReadOnlyList<double> lineArray, int countWhitePixels)
        {
            if (countWhitePixels == 0)
            {
                return lineArray[0] + 1;
            }
            for (; countWhitePixels < lineArray.Count; countWhitePixels++)
            {
                if (Math.Abs(lineArray[countWhitePixels] - lineArray[countWhitePixels - 1]) >= 1e-13)
                {
                    return lineArray[countWhitePixels - 1];
                }
            }
            return lineArray[countWhitePixels - 1] - 1;
        }
        
        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var lineArray = GetSortLine(original);
            return DoWhiteAndBlackImage(
                GetThresholdValue(lineArray, (int)(lineArray.Length * whitePixelsFraction)),
                original);
        }
    }
}