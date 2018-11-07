using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    class Leet_151
    {
        public int Solve() {
            string a = ReverseWords("  Ana are mere   ");
            return 0;
        }

        public string ReverseWords(string s) {
            string[] words = s.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", words.Reverse());
        }
    }
}
