using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    //
    // Write a function to find the longest common prefix string amongst an array of strings.
    // If there is no common prefix, return an empty string "".
    //
    class Leet_014
    {
        public string LongestCommonPrefix(string[] strs) {
            if (strs.Length == 0) return "";
            if (strs.Length == 1) return strs[0];

            int minLen = int.MaxValue;

            for (int i = 0; i < strs.Length; i++) {
                if (strs[i].Length < minLen)
                    minLen = strs[i].Length;
            }

            //Console.WriteLine(minLen);
            int maxCommonLen = 0;

            for (int charIdx = 0; charIdx < minLen; charIdx++) {
                bool isCharCommon = true;

                for (int strIdx = 1; strIdx < strs.Length; strIdx++) {
                    if (strs[strIdx][charIdx] != strs[0][charIdx]) {
                        isCharCommon = false;
                        break;
                    }
                }

                if (isCharCommon)
                    maxCommonLen++;
                else
                    break;
            }

            return strs[0].Substring(0, maxCommonLen);
        }
    }
}
