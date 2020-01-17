using NUnit.Framework;

namespace ConsoleApp1.Leet
{
    [TestFixture]
    internal class Leet_058
    {
        [TestCase(null, ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase(" ", ExpectedResult = 0)]
        [TestCase("  ", ExpectedResult = 0)]
        [TestCase("a", ExpectedResult = 1)]
        [TestCase("house", ExpectedResult = 5)]
        [TestCase("house ", ExpectedResult = 5)]
        [TestCase("house  ", ExpectedResult = 5)]
        [TestCase(" house", ExpectedResult = 5)]
        [TestCase("a house", ExpectedResult = 5)]
        [TestCase(" a house", ExpectedResult = 5)]
        [TestCase("house a", ExpectedResult = 1)]
        public int LengthOfLastWord(string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;

            int result = 0;

            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] != ' ')
                    result++;
                else
                {
                    if (result > 0)
                        return result;
                }
            }

            return result;
        }
    }
}
