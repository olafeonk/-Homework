using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        private static double MultiplyMatrixByNeighborhood(
            double[,] squareFilterMatrix,
            int centralPixelX,
            int centralPixelY,
            double[,] original)
        {
            var sumX = 0.0;
            var sumY = 0.0;
            var sideSquareMatrix = squareFilterMatrix.GetLength(0);
            for (var i = 0; i < sideSquareMatrix; i++)
            {
                for (var j = 0; j < sideSquareMatrix; j++)
                {
                    sumX += squareFilterMatrix[i, j]
                            * original[centralPixelX - sideSquareMatrix / 2 + i,
                                centralPixelY - sideSquareMatrix / 2 + j];
                    sumY += squareFilterMatrix[j, i]
                            * original[centralPixelX - sideSquareMatrix / 2 + i,
                                centralPixelY - sideSquareMatrix / 2 + j];
                }
            }
            return Math.Sqrt(sumX * sumX + sumY * sumY);
        }
        
        public static double[,] SobelFilter(double[,] original, double[,] squareFilterMatrix)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var result = new double[width, height];
            var sideSquareMatrix = squareFilterMatrix.GetLength(0);
            for (var x = sideSquareMatrix / 2; x < width - sideSquareMatrix / 2; x++)
            {
                for (var y = sideSquareMatrix / 2; y < height - sideSquareMatrix / 2; y++)
                {
                    result[x, y] = MultiplyMatrixByNeighborhood(squareFilterMatrix, x, y, original);
                }
            }
            return result;
        }
    }
}