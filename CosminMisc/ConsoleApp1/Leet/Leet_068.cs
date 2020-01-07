using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Leet
{
    public class Leet_068
    {
        private readonly string _newline = "\r\n";

        public void Solve()
        {
            IList<string> lines = FullJustify(LoadWordsFromFile(), 80);
            Debug.Print(lines.Aggregate("", (a, b) => a + "\n" + b));
            Debug.Print("");

            //IList<string> lines = FullJustify(new[] { "This", "is", "an", "example", "of", "text", "justification." }, 16);
            //Debug.Print(lines.Aggregate("", (a, b) => a + "\n" + b));
            //Debug.Print("");

            //IList<string> lines2 = FullJustify(new[] { "What", "must", "be", "acknowledgment", "shall", "be" }, 16);
            //Debug.Print(lines2.Aggregate("", (a, b) => a + "\n" + b));
            //Debug.Print("");

            //IList<string> lines3 = FullJustify(new[] { "This", "is", "an" }, 16);
            //Debug.Print(lines3.Aggregate("", (a, b) => a + "\n" + b));
            //Debug.Print("");

            //IList<string> lines4 = FullJustify(new[] { "Science","is","what","we","understand","well","enough","to","explain",
            //    "to","a","computer.","Art","is","everything","else","we","do" }, 20);
            //Debug.Print(lines4.Aggregate("", (a, b) => a + "\n" + b));
            //Debug.Print("");
        }

        string[] LoadWordsFromFile()
        {
            string file = @"C:\Temp\text.txt";
            string text = File.ReadAllText(file);
            List<string> wordList = LoadWordsFromText(text);
            return wordList.ToArray();
        }

        private List<string> LoadWordsFromText(string text)
        {
            List<string> wordList = new List<string>();
            StringBuilder wordBuilder = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];

                if (ch != ' ' && ch != '\r' && ch != '\n')
                {
                    wordBuilder.Append(ch);
                }
                else if (ch == ' ')
                {
                    if (wordBuilder.Length > 0)
                        wordList.Add(wordBuilder.ToString());

                    wordBuilder.Clear();
                }
                else if (ch == '\r')
                {
                    if (i + 1 <= text.Length - 1 && text[i + 1] == '\n')
                    {
                        if (wordBuilder.Length > 0)
                            wordList.Add(wordBuilder.ToString());

                        wordBuilder.Clear();

                        wordList.Add(_newline);  // Add newline as new word
                        i++;                     // Advance pointer beyond the \n char
                    }
                    else
                    {
                        Debug.Assert(false, "Case not handled");
                    }
                }
            }

            return wordList;
        }

        public IList<string> FullJustify(string[] words, int maxWidth)
        {
            List<string> lineList = new List<string>();
            int startWordIndex = 0, endWordIndex = 0;
            int lineLength = 0;

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                if (GetLength(word) > maxWidth) throw new ArgumentException("Word is too long");
                if (string.IsNullOrEmpty(word)) throw new ArgumentException("Word is empty");

                int separatorLength = lineLength > 0 ? 1 : 0;

                if (word != _newline && startWordIndex < 0)
                {
                    startWordIndex = i;
                    endWordIndex = i;
                }

                if (word == _newline)
                {
                    // make line
                    if (lineLength > 0)
                    {
                        string line = MakeLine(words, startWordIndex, endWordIndex - 1, lineLength,
                            maxWidth, false);
                        lineList.Add(line);
                    }

                    // update counters
                    startWordIndex = -1;
                    endWordIndex = -1;
                    lineLength = 0;

                    lineList.Add("");
                }
                else if (lineLength + separatorLength + GetLength(word) <= maxWidth)
                {
                    lineLength += separatorLength + GetLength(word);
                    endWordIndex++;
                }
                else
                {
                    // make line
                    string line = MakeLine(words, startWordIndex, endWordIndex - 1, lineLength, maxWidth, true);
                    lineList.Add(line);

                    // update counters
                    startWordIndex = i;
                    endWordIndex = i + 1;
                    lineLength = GetLength(word);
                }
            }

            string line2 = MakeLine(words, startWordIndex, endWordIndex - 1, lineLength, maxWidth, false);
            lineList.Add(line2);

            return lineList;
        }

        private int GetLength(string word)
        {
            int len = word.Count(ch => ch != '\r' && ch != '\n');
            return len;
        }

        private string MakeLine(string[] words, int startWordIndex, int endWordIndex, int lineLength, int maxWidth, bool justify)
        {
            char[] lineChars = new char[maxWidth];
            int spacesToAdd = maxWidth - lineLength;
            int wordBreaks = endWordIndex - startWordIndex;
            if (wordBreaks == 0)
                wordBreaks = 1;
            int supplementarySpacesPerBreak = justify ? spacesToAdd / wordBreaks : 0;
            int leftOverSpaces = justify ? spacesToAdd % wordBreaks : 0;
            int destinationIndex = 0;

            for (int i = startWordIndex; i <= endWordIndex; i++)
            {
                string word = words[i];
                CopyWord(lineChars, destinationIndex, word);
                destinationIndex += word.Length;

                if (destinationIndex < maxWidth)
                {
                    // Add spaces
                    int separatorSpaceCount = i < endWordIndex ? 1 : 0;
                    int spaceCount = separatorSpaceCount + supplementarySpacesPerBreak;
                    if (leftOverSpaces > 0)
                    {
                        spaceCount++;
                        leftOverSpaces--;
                    }

                    SetChars(lineChars, ' ', spaceCount, destinationIndex);
                    destinationIndex += spaceCount;
                }
            }

            if (!justify)
                SetChars(lineChars, ' ', maxWidth - destinationIndex, destinationIndex);

            string lineString = new string(lineChars);
            return lineString;
        }

        private void EnsureLineArraySize(ref char[] lineChars, int sizeNeeded)
        {
            if (lineChars.Length < sizeNeeded)
            {
                char[] newLineChars = new char[sizeNeeded];
                lineChars.CopyTo(newLineChars, 0);
                lineChars = newLineChars;
            }
        }

        private void CopyWord(char[] destinationChars, int destinationIndex, string word)
        {
            word.CopyTo(0, destinationChars, destinationIndex, word.Length);
        }

        private void SetChars(char[] destination, char character, int count, int destinationStartIndex)
        {
            for (int i = 0; i < count; i++)
                destination[destinationStartIndex + i] = character;
        }
    }
}
