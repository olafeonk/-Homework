using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static bool IsSentenceUpdate(
            int typeGram,
            Dictionary<string, string> nextWords,
            List<string> sentence)
        {
            var key = sentence[sentence.Count - 1];
            switch (typeGram)
            {
                case 3 when sentence.Count > 1:
                    key = sentence[sentence.Count - 2] + " " + key;
                    break;
                case 3:
                    return false;
            }

            if (!nextWords.ContainsKey(key)) return false;
            sentence.Add(nextWords[key]);
            return true;
        }

        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            var sentence = phraseBeginning.Split(' ').ToList();
            for (int i = 0; i < wordsCount; i++)
            {
                if (IsSentenceUpdate(3, nextWords, sentence) || IsSentenceUpdate(2, nextWords, sentence))
                {
                    continue;
                }
                return string.Join(" ", sentence);
            }
            return string.Join(" ", sentence);
        }
    }
}