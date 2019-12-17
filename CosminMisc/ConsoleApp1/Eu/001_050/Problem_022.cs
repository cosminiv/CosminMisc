using System.IO;
using System.Linq;

namespace ConsoleApp1.Eu._001_050
{
    public class Problem_022
    {
        public static long Solve()
        {
            string file = @"C:\Temp\p022_names.txt";
            string[] names = File.ReadAllText(file).Split(',').Select(n => n.Trim('"')).OrderBy(n => n).ToArray();
            long score = names.Select((n, i) => (i + 1) * GetScore(n)).Sum();
            return score;

            int GetScore(string n) => n.ToCharArray().Sum(c => c - 'A' + 1);
        }

    }
}
