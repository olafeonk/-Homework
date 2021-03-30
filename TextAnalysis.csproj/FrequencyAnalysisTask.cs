using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    { 
        public static string GetKeyFromSentence(int typeGram, List<string> sentence, int startIndex)
        {
            if (typeGram == 2)
            {
                return sentence[startIndex - 1] + ":" + sentence[startIndex];
            }
            return sentence[startIndex - 2] + " " + sentence[startIndex - 1] + ":" + sentence[startIndex];
        }

        public static Dictionary<string, int> UpdateCountDictionary(
            Dictionary<string, int> countGram, 
            int typeGram,
            List<string> sentence)
        {
            for (var i = typeGram - 1; i < sentence.Count; i++)
            {
                var key = GetKeyFromSentence(typeGram, sentence, i);
                if (!countGram.ContainsKey(key))
                {
                    countGram[key] = 1;
                }
                else
                {
                    countGram[key]++;
                }
            }
            return countGram;
        }

        public static Dictionary<string, string> UpdateResult(Dictionary<string, int> countGram,
            Dictionary<string, string> result)
        {
            foreach (var elem in countGram)
            {
                var gram = elem.Key.Split(':');
                if (!result.ContainsKey(gram[0]))
                {
                    result[gram[0]] = gram[1];
                }
                else
                {
                    var key = gram[0] + ":" + result[gram[0]];
                    if (countGram[key] < elem.Value || 
                        (countGram[key] == elem.Value &&
                         string.CompareOrdinal(gram[1], result[gram[0]]) < 0))
                    {
                        result[gram[0]] = gram[1];
                    }
                }
            }
            return result;
        }

        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            var countBigram = new Dictionary<string, int>();
            var countTrigram = new Dictionary<string, int>();
            foreach (var sentence in text)
            {
                countBigram = UpdateCountDictionary(countBigram, 2, sentence);
                countTrigram = UpdateCountDictionary(countTrigram, 3, sentence);
            }
            result = UpdateResult(countBigram, result);
            result = UpdateResult(countTrigram, result);
            return result;
        }
    }
}