using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Algorithms
{
    class RadixSort
    {
        int[] _powersOfTen = MakePowersOfTen();

        private static int[] MakePowersOfTen() {
            int[] result = new int[10];

            for (int i = 0; i < 10; i++) {
                result[i] = i > 0 ? result[i - 1] * 10 : 1;
            }

            return result;
        }

        public void Test() {
            int[] nums = { 10000, 445, 731, 221, 983, 333, 121 };
            int[] sortedNums = Sort(nums, 5);

            string[] words = { "COW", "DOG", "SEA", "RUG", "ROW", "MOB", "BOX", "TAB", "BAR", "EAR", "TAR", "DIG", "BIG", "TEA", "NOW", "FOX"};
            string[] sortedWords = Sort(words, 3);

            string sortedNumsStr = string.Join(" ", sortedNums);
        }

        public string[] Sort(string[] data, int maxDigits) {
            string[] result = data;
            CountingSort countingSort = new CountingSort();

            for (int d = 0; d < maxDigits; d++) {
                result = countingSort.SortV2(result, SelectChar, d, 26);
            }

            return result;
        }

        public int[] Sort(int[] data, int maxDigits) {
            int[] result = data;
            CountingSort countingSort = new CountingSort();

            for (int d = 0; d < maxDigits; d++) {
                result = countingSort.SortV2(result, SelectDigit, d, 9);
            }

            return result;
        }

        public int SelectDigit(int number, int digitIndex) {
            int x = (int)(number / _powersOfTen[digitIndex]);
            int d = x % 10;
            return d;
        }

        public int SelectChar(string word, int charIndex) {
            int len = word.Length;
            return word[len - charIndex - 1] - 'A';
        }
    }
}
