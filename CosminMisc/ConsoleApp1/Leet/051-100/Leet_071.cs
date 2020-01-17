using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Leet
{
    public class Leet_071
    {
        public void Solve()
        {
            string a = SimplifyPath("/home/");
            string a2 = SimplifyPath("/home/is/where/");
            string a3 = SimplifyPath("/home/is/../");
            string a4 = SimplifyPath("/home/is/../boom");
            string a5 = SimplifyPath("///");
            string a6 = SimplifyPath("//./");
            string a7 = SimplifyPath("/../");
        }

        public string SimplifyPath(string path)
        {
            string[] tokens = path.Split(new []{'/'}, StringSplitOptions.RemoveEmptyEntries)
                .Where(t => t.Length > 0 && t != ".")
                .ToArray();

            List<string> stack = new List<string>();

            foreach (var token in tokens)
            {
                if (token == "..")
                {
                    if (stack.Count > 0)
                        stack.RemoveAt(stack.Count - 1);
                }
                else
                    stack.Add(token);
            }

            string result = stack.Aggregate("", (a, b) => a + "/" + b);

            if (result.Length == 0)
                result = "/";

            return result;
        }
    }
}
