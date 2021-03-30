using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static void AddSentence(List<List<string>> sentencesList, List<string> sentence, string word)
        {
            if (word.Length > 0)
            {
                sentence.Add(word.ToLower());
            }
            if (sentence.Count > 0)
            {
                sentencesList.Add(sentence);
            }
        }

        public static bool IsEndSentences(char character)
        {
            const string endSentenceCharacters = ".!?;:()";
            return endSentenceCharacters.Any(characterEnd => character == characterEnd);
        }
        
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var sentence = new List<string>();
            var word = new StringBuilder();
            foreach (var character in text)
            {
                if (char.IsLetter(character) || character == '\'')
                {
                    word.Append(character);
                }
                else if (IsEndSentences(character))
                {
                    AddSentence(sentencesList, sentence, word.ToString());
                    word = new StringBuilder();
                    sentence = new List<string>();
                }
                else if (word.Length > 0)
                {
                    sentence.Add(word.ToString().ToLower());
                    word = new StringBuilder();
                }
            }
            AddSentence(sentencesList, sentence, word.ToString());
            return sentencesList;
        }
    }
}