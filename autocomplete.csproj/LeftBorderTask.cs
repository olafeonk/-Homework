    using System;
    using System.Collections.Generic;

    namespace Autocomplete
    {
        public static class LeftBorderTask
        {
            public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
            {
                if (right - left == 1)
                {
                    return left;
                }
                var m = left + (right - left) / 2;
                return string.Compare(prefix, phrases[m], StringComparison.OrdinalIgnoreCase) <= 0
                    ? GetLeftBorderIndex(phrases, prefix, left, m)
                    : GetLeftBorderIndex(phrases, prefix, m, right);
            }
        }
    }
