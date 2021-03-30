using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase("'a b ", 0, "a b ", 5)]
        [TestCase("\"\\\\\" b", 0, "\\", 4)]
        [TestCase("b 'a'", 2, "a", 3)]
        [TestCase("'", 0, "", 1)]
        [TestCase("'a\\\' b'", 0, "a' b", 7)]
        [TestCase("\"a\\\" b\"", 0, "a\" b", 7)]
        [TestCase("'\\\\\\\\\\\' 1", 0, "\\\\\' 1", 9)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var value = new StringBuilder();
            for (var i = startIndex + 1; i < line.Length; i++)
            {
                if (line[i] == line[startIndex])
                {
                    return new Token(value.ToString(), startIndex, i + 1 - startIndex);
                }
                if (line[i] == '\\')
                {
                    i++;
                }
                value.Append(line[i]);
            }
            return new Token(value.ToString(), startIndex, line.Length - startIndex);
        }
    }
}