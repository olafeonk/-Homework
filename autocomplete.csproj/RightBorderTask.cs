using System;
using System.Collections.Generic;

namespace Autocomplete
{
    public static class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            while (right - left > 1)
            {
                var m = left + (right - left) / 2;
                if (string.Compare(prefix, phrases[m], StringComparison.OrdinalIgnoreCase) >= 0
                    || phrases[m].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    left = m;
                }
                else
                {
                    right = m;
                }
            }
            return right;
        }
    }
}