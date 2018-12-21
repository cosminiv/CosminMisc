using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Misc
{
    class StringSearch
    {
        public void Test() {
            TestCase[] testCases = new TestCase[] {
                new TestCase(
                    "Blah",
                    new[] { "adrian123" },
                    new string[0]),

                new TestCase(
                    "@Blah hehehe @adria @",
                    new[] { "adrian123", "adrian" },
                    new string[0]),

                new TestCase(
                    "@Blah hehehe @adrian @",
                    new[] { "adrian123", "adrian" },
                    new[] { "adrian" }),

                new TestCase(
                    "@Blah hehehe @adrian1 @",
                    new[] { "adrian123", "adrian" },
                    new[] { "adrian" }),

                new TestCase(
                    "@Blah hehehe @adrian12",
                    new[] { "adrian123", "adrian" },
                    new[] { "adrian" }),

                new TestCase(
                    "@Blah hehehe @adrian123 blah blah",
                    new[] { "adrian123", "adrian" },
                    new[] { "adrian123" }),

                new TestCase(
                    "Blah @cosminxx @abb blah @adrian1, @adriax, how you doin @adrian1 @cosmin@@@",
                    new[] { "adrian123", "cosminX", "abba", "adrian", "adrian1",  "cosmin",  "cosmin2" },
                    new[] { "cosminx", "adrian1", "cosmin"})
            };

            int i = 0;

            foreach (TestCase tc in testCases) {
                string[] result = FindAtMentions(tc.Text, tc.Usernames);
                Debug.Assert(result.Length == tc.ExpectedResult.Length);
                foreach (string exp in tc.ExpectedResult) {
                    string found = result.FirstOrDefault(r => String.Equals(r, exp, StringComparison.InvariantCultureIgnoreCase));
                    Debug.Assert(found != null);
                }
                i++;
            }
        }

        string[] FindAtMentions(string text, string[] usernames) {
            text = text.ToLower();

            HashSet<string> result = new HashSet<string>();
            StringComparison comp = StringComparison.InvariantCultureIgnoreCase;

            for (int i = text.IndexOf("@", comp); i >= 0; i = text.IndexOf("@", i + 1, comp)) {
                if (i == text.Length - 1) break;

                string textAfterAt = text.Substring(i + 1);
                string longestMatchingUsername = usernames
                    .Where(u => textAfterAt.StartsWith(u, comp))
                    .OrderByDescending(u => u.Length)
                    .FirstOrDefault();

                if (longestMatchingUsername != null)
                    result.Add(longestMatchingUsername);
            }

            return result.ToArray();
        }

        class TestCase
        {
            public string Text;
            public string[] Usernames;
            public string[] ExpectedResult;

            public TestCase(string text, string[] usernames, string[] expected) {
                Text = text;
                Usernames = usernames;
                ExpectedResult = expected;
            }
        }
    }
}
