using System.Collections.Generic;

namespace Passwords
{
    public static class CaseAlternatorTask
    {
        public static List<string> AlternateCharCases(string lowercaseWord)
        {
            var result = new List<string>();
            AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
            return result;
        }

        private static void AlternateCharCases(IList<char> word, int startIndex, ICollection<string> result)
        {
            if (startIndex == word.Count)
            {
                result.Add(string.Join("", word)); 
                return;
            }
            word[startIndex] = char.ToLower(word[startIndex]);
            AlternateCharCases(word, startIndex + 1, result);
            if (char.IsUpper(char.ToUpper(word[startIndex])))
            {
                word[startIndex] = char.ToUpper(word[startIndex]);
                AlternateCharCases(word, startIndex + 1, result);
            }
        }
    }
}