using System;
using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            var bestPermutation = new int[checkpoints.Length];
            for (var i = 0; i < bestPermutation.Length; i++)
            {
                bestPermutation[i] = i;
            }
            MakePermutations(new int[checkpoints.Length], 1, bestPermutation, checkpoints);
            return bestPermutation;
        }

        private static void UpdateBestPermutation(int[] bestPermutation, int[] permutation, Point[] checkpoints)
        {
            if (!(checkpoints.GetPathLength(permutation) < checkpoints.GetPathLength(bestPermutation)))
            {
                return;
            }
            for (var i = 0; i < permutation.Length; i++)
            {
                bestPermutation[i] = permutation[i];
            }
        }
		
        private static void MakePermutations(
            int[] permutation,
            int position,
            int[] bestPermutation,
            Point[] checkpoints)
        {
            if (position == permutation.Length)
            {
                UpdateBestPermutation(bestPermutation, permutation, checkpoints);
                return;
            }
            if (checkpoints.GetPathLength(permutation.Take(position).ToArray()) 
                >= checkpoints.GetPathLength(bestPermutation))
            {
                return;
            }
            for (var i = 1; i < permutation.Length; i++)
            {
                if (Array.IndexOf(permutation, i, 0, position) != -1)
                {
                    continue;
                }
                permutation[position] = i;
                MakePermutations(permutation, position + 1,  bestPermutation, checkpoints);
            }
        }
    }
}