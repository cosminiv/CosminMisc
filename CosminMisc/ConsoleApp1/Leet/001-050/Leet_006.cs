using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    class Leet_006
    {
        public int Solve() {
            string s = Convert("PAYPALISHIRING", 3);
            return 0;
        }

        public string Convert(string s, int numRows) {
            if (s == null) return null;
            if (s == "") return "";
            if (numRows == 1) return s;

            List<char>[] texts = Enumerable.Range(1, numRows)
                .Select(n => new List<char>()).ToArray();
            int row = 0;
            int rowDelta = 1;
            int idx = 0;

            while (idx < s.Length) {
                char ch = s[idx];

                texts[row].Add(ch);
                
                if (row == numRows - 1 && rowDelta == 1) 
                    rowDelta = -1;
                else if (row == 0 && rowDelta == -1) 
                    rowDelta = 1;

                idx++;
                row += rowDelta;
            }

            StringBuilder sb = new StringBuilder();
            foreach (var textRow in texts)
                sb.Append(new string(textRow.ToArray()));

            return sb.ToString();
        }
    }
}
