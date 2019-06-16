using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    // Given a string, find the first non-repeating character in it and return it's index. If it doesn't exist, return -1.
    // You may assume the string contain only lowercase letters
    class Leet_387
    {
        public void Test() {
            byte a = 2;
            a++;
        }

        public int FirstUniqChar(string s) {
            // index
            short[] index = new short['z' + 1];
            int len = s.Length;

            for (int i = 0; i < len; i++) {
                char ch = s[i];
                index[ch]++;
            }

            // search
            for (int i = 0; i < len; i++) {
                char ch = s[i];
                if (index[ch] == 1)
                    return i;
            }

            return -1;
        }
    }
}
