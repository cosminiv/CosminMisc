using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_042
    {
        public static int Solve()
        {
            string file = @"C:\Temp\p042_words.txt";
            HashSet<int> triangles = new HashSet<int>(Enumerable.Range(1, 1000).Select(a => a * (a + 1) / 2));
            string[] words = File.ReadAllText(file).Split(new char[] { ',', '"' }, StringSplitOptions.RemoveEmptyEntries);
            int triangleWords = words.Count(w => triangles.Contains(w.ToCharArray().Select(c => c - 'A' + 1).Sum()));
            return triangleWords;
        }
    }
}
