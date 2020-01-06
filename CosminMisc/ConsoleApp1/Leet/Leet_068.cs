using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApp1.Leet
{
    public class Leet_068
    {
        public void Solve()
        {
            IList<string> lines = FullJustify(new[] { "This", "is", "an", "example", "of", "text", "justification." }, 16);
            Debug.Print(lines.Aggregate("", (a, b) => a + "\n" + b));
            Debug.Print("");

            IList<string> lines2 = FullJustify(new[] { "What", "must", "be", "acknowledgment", "shall", "be" }, 16);
            Debug.Print(lines2.Aggregate("", (a, b) => a + "\n" + b));
            Debug.Print("");

            IList<string> lines3 = FullJustify(new[] { "This", "is", "an" }, 16);
            Debug.Print(lines3.Aggregate("", (a, b) => a + "\n" + b));
            Debug.Print("");

            IList<string> lines4 = FullJustify(new[] { "Science","is","what","we","understand","well","enough","to","explain",
                "to","a","computer.","Art","is","everything","else","we","do" }, 20);
            Debug.Print(lines4.Aggregate("", (a, b) => a + "\n" + b));
            Debug.Print("");
        }

        public IList<string> FullJustify(string[] words, int maxWidth)
        {
            List<string> result = new List<string>();
            int startWordIndex = 0, endWordIndex = 0;
            int lineLength = 0;

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                if (word.Length > maxWidth) throw new ArgumentException("Word is too long");
                if (string.IsNullOrEmpty(word)) throw new ArgumentException("Word is empty");

                int separatorLength = lineLength > 0 ? 1 : 0;

                if (lineLength + separatorLength + word.Length <= maxWidth)
                {
                    lineLength += separatorLength + word.Length;
                    endWordIndex++;
                }
                else
                {
                    // make line
                    string justifiedLine = MakeJustifiedLine(words, startWordIndex, endWordIndex - 1, lineLength, maxWidth, false);
                    result.Add(justifiedLine);

                    // reset counters
                    startWordIndex = i;
                    endWordIndex = i + 1;
                    lineLength = word.Length;
                }
            }

            string justifiedLine2 = MakeJustifiedLine(words, startWordIndex, endWordIndex - 1, lineLength, maxWidth, true);
            result.Add(justifiedLine2);

            return result;
        }

        private string MakeJustifiedLine(string[] words, int startWordIndex, int endWordIndex, int lineLength, int maxWidth, bool isLastLine)
        {
            char[] resultChars = new char[maxWidth];
            int spacesToAdd = maxWidth - lineLength;
            int wordBreaks = endWordIndex - startWordIndex;
            if (wordBreaks == 0)
                wordBreaks = 1;
            int supplementarySpacesPerBreak = isLastLine ? 0 : spacesToAdd / wordBreaks;
            int leftOverSpaces = isLastLine ? 0 : spacesToAdd % wordBreaks;
            int destinationIndex = 0;

            for (int i = startWordIndex; i <= endWordIndex; i++)
            {
                string word = words[i];
                CopyWord(resultChars, word, destinationIndex);
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

                    SetChars(resultChars, ' ', spaceCount, destinationIndex);
                    destinationIndex += spaceCount;
                }
            }

            if (isLastLine)
                SetChars(resultChars, ' ', maxWidth - destinationIndex, destinationIndex);

            string result = new string(resultChars);
            return result;
        }

        private void CopyWord(char[] destination, string word, int destinationStartIndex)
        {
            for (int i = 0; i < word.Length; i++)
                destination[destinationStartIndex + i] = word[i];
        }

        private void SetChars(char[] destination, char character, int count, int destinationStartIndex)
        {
            for (int i = 0; i < count; i++)
                destination[destinationStartIndex + i] = character;
        }
    }
}
