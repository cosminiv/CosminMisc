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
            LoadWords(trie);

            //FindElementCountPerLevel(trie);

            //string longest = trie.FindLongestWordWithAllPrefixesWords();
            //Console.WriteLine(longest);

            //string longest = trie.FindLongestWord();
            //Console.WriteLine(longest);

            while (true) {
                Console.Write("prefix = ");
                string prefix = Console.ReadLine();
                if (prefix.Trim().Length > 0)
                    FindWordsWithPrefix(trie, prefix);
            }

            //Console.WriteLine("=====================================");
            //FindWordsWithPrefixBruteForce(words, prefix);
            //FindWords(trie);
        }

        private static void FindElementCountPerLevel(Trie trie) {
            var map = trie.FindNodeCountPerLevel();
            for (int i = 0; map.ContainsKey(i); i++) {
                Console.WriteLine($"{i}: {map[i]}");
            }
        }

        private static void LoadWords(Trie trie) {
            Console.WriteLine("Loading...");
            Stopwatch sw = Stopwatch.StartNew();
            IEnumerable<string> words =
                File.ReadLines(@"C:\Temp\german_words.txt", Encoding.GetEncoding(1252));
            //new[] { "apple", "app", "wo", "being", "ap", "be", "b", "a" };
            int count = 0;

            foreach (string line in words) {
                trie.Insert(line);
                count++;
            }

            Console.WriteLine($"Loaded {count} words in {sw.ElapsedMilliseconds}ms\n");
        }

        private static void FindWordsWithPrefix(Trie trie, string prefix) {
            Stopwatch sw = Stopwatch.StartNew();

            var words = trie.FindWordsWithPrefix(prefix);
            long elapsedMs = sw.ElapsedMilliseconds;

            Console.WriteLine($"FindWordsWithPrefix: {prefix}\n");
            StringBuilder wordsText = WordsToText(words);
            Console.WriteLine($"{wordsText}");
            Console.WriteLine($"\nFound {words.Count} words in {elapsedMs}ms\n");
        }

        private static StringBuilder WordsToText(List<string> words) {
            StringBuilder wordsText = new StringBuilder();
            int columnWidth = 30;
            int columnCount = 6;
            int wordsOnLine = 0;

            foreach (string word in words) {
                wordsText.Append(word);
                if (word.Length < columnWidth)
                    wordsText.Append(' ', columnWidth - word.Length);
                else
                    wordsText.Append("  ");

                wordsOnLine = (wordsOnLine + 1) % columnCount;
                if (wordsOnLine == 0)
                    wordsText.AppendLine();
            }

            return wordsText;
        }

        private static void FindWordsWithPrefixBruteForce(IEnumerable<string> allWords, string prefix) {
            Stopwatch sw = Stopwatch.StartNew();

            var words = allWords.Where(w => w.StartsWith(prefix)).ToList();
            long elapsedMs = sw.ElapsedMilliseconds;

            Console.WriteLine($"FindWordsWithPrefixBruteForce: {prefix}\n");
            foreach (string word in words) {
                Console.WriteLine($"{word}");
            }

            Console.WriteLine($"\nFound {words.Count} words in {elapsedMs}ms\n");
        }

        private static void FindWords(Trie trie) {
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
