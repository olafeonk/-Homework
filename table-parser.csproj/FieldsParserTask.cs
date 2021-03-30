using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        [TestCase("text", new[] { "text" })]
        [TestCase("hello world", new[] { "hello", "world" })]
        [TestCase("'a'", new[] { "a" })]
        [TestCase("''", new[] { "" })]
        [TestCase("''  ''", new[] { "", "" })]
        [TestCase("'hel fg'", new[] { "hel fg" })]
        [TestCase("a''", new[] { "a", "" })]
        [TestCase("'' a", new[] { "", "a" })]
        [TestCase("'f\'", new[] { "f" })]
        [TestCase("'a \"c\"'", new[] { "a \"c\"" })]
        [TestCase("\"a 'b'\"", new[] { "a 'b'" })]
        [TestCase("a 'b' c", new[] { "a", "b", "c" })]
        [TestCase("'\\\\'", new[] { "\\" })]
        [TestCase("\"\\\\\"", new[] { "\\" })]
        [TestCase("'\\\''", new[] { "\'" })]
        [TestCase("\"\\\"\"", new[] { "\"" })]
        [TestCase("a ", new[] { "a" })]
        [TestCase("'a b''a b", new[] { "a b", "a b" })]
        [TestCase("'a b ", new[] { "a b " })]
        [TestCase("", new string[] { })]
        [TestCase("\"\\\\\" b", new[] {"\\", "b"} )]
        [TestCase("\\slash\\_in_simple_field_is_just_slash\\", new[] {"\\slash\\_in_simple_field_is_just_slash\\"})]
        [TestCase(" d ", new[] {"d"})]
        public static void RunTests(string input, string[] expectedOutput)
        {
            TestFieldsParserTask(input, expectedOutput);
        }
        
        public static void TestFieldsParserTask(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (var i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }

        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase("'a b ", 0, "a b ", 5)]
        [TestCase("\"\\\\\" b", 0, "\\", 4)]
        [TestCase("b 'a'", 2, "a", 3)]
        [TestCase("'", 0, "", 1)]
        [TestCase("'a\\\' b'", 0, "a' b", 7)]
        [TestCase("\"a\\\" b\"", 0, "a\" b", 7)]
        [TestCase("'\\\\\\\\\\\' 1", 0, "\\\\\' 1", 9)]
        public void TestQuotedFieldTask(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    public class FieldsParserTask
    { 
        public static List<Token> ParseLine(string line)
        {
            var tokens = new List<Token>();
            for (var i = GetFirstNonWhitespaceIndex(line, 0); i < line.Length; i = GetFirstNonWhitespaceIndex(line, i))
            {
                var token = ReadField(line, i);
                i = token.GetIndexNextToToken();
                tokens.Add(token);
            }
            return tokens; 
        }

        public static int GetFirstNonWhitespaceIndex(string line, int startIndex)
        {
            var i = startIndex;
            while (i < line.Length && line[i] == ' ')
            {
                i++;
            }
            return i;
        }
		
        public static Token ReadSimpleField(string line, int startIndex)
        {
            var value = new StringBuilder();
            var i = startIndex;
            for (; i < line.Length && (line[i] != '"' && line[i] != '\'' && line[i] != ' '); i++)
            {
                value.Append(line[i]);
            }
            return new Token(value.ToString(), i - value.Length, value.Length);
        }
        
        public static Token ReadField(string line, int startIndex)
        {
            if (line[startIndex] == '"' || line[startIndex] == '\'')
            {
                return ReadQuotedField(line, startIndex);
            }
            return ReadSimpleField(line, startIndex);
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }
    }
}