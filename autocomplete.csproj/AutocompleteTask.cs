using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        /// <returns>
        /// Возвращает первую фразу словаря, начинающуюся с prefix.
        /// </returns>
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            
            return null;
        }

        /// <returns>
        /// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count) 
        /// элементов словаря, начинающихся с prefix.
        /// </returns>
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            var startIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            var topByPrefix = new string[Math.Min(count, GetCountByPrefix(phrases, prefix))];
            for (var index = startIndex; index < topByPrefix.Length + startIndex; ++index)
            {
                topByPrefix[index - startIndex] = phrases[index];
            }
            return topByPrefix;
        }

        /// <returns>
        /// Возвращает количество фраз, начинающихся с заданного префикса
        /// </returns>
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            return RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count) - 1
                - LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
        }
    }


    [TestFixture]
    public class AutocompleteTests
    {
        [TestCase(null, "A", 1, null)]
        [TestCase(new [] {"a", "ab", "abc", "bc"}, "a", 1, new[] {"a"})]
        [TestCase(new [] {"a", "ab", "abc", "bc"}, "a", 3, new[] {"a", "ab", "abc"})]
        [TestCase(new [] {"a", "ab", "abc", "bc"}, "a", 10, new[] {"a", "ab", "abc"})]
        [TestCase(new [] {"a", "ab", "abc", "bc"}, "bcd", 10, null)]
        public void GetTopByPrefix_Test(
            IReadOnlyList<string> phrases,
            string prefix,
            int count,
            string[] expectedTopWords)
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            Assert.AreEqual(expectedTopWords, actualTopWords, "Wrong");    
            
        }

        [TestCase(0, null, "")]
        [TestCase(3,  new [] {"a", "ab", "abc", "bc"}, "a")]
        [TestCase(0, new[] {"a", "b", "c"}, "z")]
        [TestCase(2, new[] {"a", "ab", "abc", "ac"}, "ab")]
        [TestCase(1, new[] {"a"}, "a")]
        public void GetCountByPrefix_Test(int expectedCount,IReadOnlyList<string> phrases, string prefix)
        {
            var actualCount = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            Assert.AreEqual(expectedCount, actualCount, "Wrong");
        }
    }
}
