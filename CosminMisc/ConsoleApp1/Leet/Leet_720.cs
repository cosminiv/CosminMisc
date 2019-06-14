using ConsoleApp1.Trees;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    // Given a list of strings words representing an English Dictionary, find the longest word in words that can be built one character at a time by other words in words. If there is more than one possible answer, return the longest word with the smallest lexicographical order.
    // If there is no answer, return the empty string.

    public class Leet_720
    {
        public static void Test() {
            Trie trie = new Trie();

            foreach (string line in File.ReadLines(@"C:\Temp\words_alpha.txt"))
                trie.Insert(line);

            TestFindWordsWithPrefix(trie);
            //TestFindWords(trie);
        }

        private static void TestFindWordsWithPrefix(Trie trie) {
            Stopwatch sw = Stopwatch.StartNew();

            string prefix = "comp";
            var words = trie.FindWordsWithPrefix(prefix);
            long elapsedMs = sw.ElapsedMilliseconds;

            Console.WriteLine($"FindWordsWithPrefix: {prefix}");
            foreach (string word in words) {
                Console.WriteLine($"{word}");
            }

            Console.WriteLine($"\nFound {words.Count} words in {elapsedMs}ms");
        }

        private static void TestFindWords(Trie trie) {
            string[] wordsToFind = new[] { "stav", "preacknowledgment", "dialyphyllous", "crackings", "revolutionizement", "revolutionizemenx", "stauroscopically" };

            Stopwatch sw = Stopwatch.StartNew();

            foreach (string word in wordsToFind) {
                sw.Reset();
                bool found = trie.FindWord(word);
                string foundText = found ? "found" : "not found";
                Console.WriteLine($"{word}: {foundText} ({sw.ElapsedMilliseconds}ms)");
            }
        }

        // ["a", "banana", "app", "appl", "ap", "apply", "apple"]
        public static string LongestWord_Basic(string[] words) {
            var hs = words.ToHashSet();
            int maxLen = 0;
            List<string> wordsWithMaxLen = new List<string>();

            foreach (string word in words) {
                if (word.Length >= maxLen) {
                    bool foundAllPrefixes = true;

                    for (int i = 1; i < word.Length; i++) {
                        string prefix = word.Substring(0, i);
                        if (!hs.Contains(prefix)) {
                            foundAllPrefixes = false;
                            break;
                        }
                    }

                    if (foundAllPrefixes) {
                        if (word.Length > maxLen) {
                            maxLen = word.Length;
                            wordsWithMaxLen = new List<string>();
                        }
                        wordsWithMaxLen.Add(word);
                    }
                }
            }

            if (wordsWithMaxLen.Count == 0)
                return "";

            string minWord = wordsWithMaxLen[0];
            for (int i = 1; i < wordsWithMaxLen.Count; i++) {
                if (string.Compare(wordsWithMaxLen[i], minWord) < 0)
                    minWord = wordsWithMaxLen[i];
            }

            return minWord;
        }


        
    }
}
