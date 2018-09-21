using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_018
    {
        static readonly string _dataStr =
@"75
95 64
17 47 82
18 35 87 10
20 04 82 47 65
19 01 23 75 03 34
88 02 77 73 07 63 67
99 65 04 28 06 16 70 92
41 41 26 56 83 40 80 70 33
41 48 72 33 47 32 37 16 94 29
53 71 44 65 25 43 91 52 97 51 14
70 11 33 28 77 73 17 78 39 68 17 57
91 71 52 38 17 14 91 43 58 50 27 29 48
63 66 04 68 89 53 67 30 73 16 69 87 40 31
04 62 98 27 23 09 70 98 73 93 38 53 60 04 23";

        static List<Path> _paths = new List<Path>();

        public static long Solve()
        {
            int[][] data = //ParseData(File.ReadAllText(@"C:\Temp\p067_triangle.txt")); 
                           ParseData(_dataStr);
            GeneratePaths(data, data.Length - 1);
            long result = _paths.Select(p => p.Sum).Max();
            return result;
        }

        private static int[][] ParseData(string dataStr)
        {
            string[] lines = dataStr.Split('\n');
            int[][] data = new int[lines.Length][];
            int i = 0;

            foreach (string line in lines)
            {
                if (line.Trim().Length > 0)
                    data[i++] = line.Split(' ').Select(a => int.Parse(a)).ToArray();
            }

            return data;
        }

        public static void GeneratePaths(int[][] elements, int level)
        {
            if (level == 0)
            {
                DebugWriteLine("Level " + level);
                int elem = elements[0][0];
                _paths.Add(new Path { //Elements = new List<int>() { elem },
                    LastElemIndex = 0, Sum = elem });
                DebugWriteLine("[" + elem.ToString() + "]\n");
                return;
            }

            GeneratePaths(elements, level - 1);

            DebugWriteLine("Level " + level);
            for (int i = 0; i < _paths.Count; i++)
                DebugWriteLine(i.ToString() + ": " + PathToString(_paths[i]));
            DebugWriteLine("");

            int pathsCount = _paths.Count;

            for (int i = 0; i < pathsCount; i++)
            {
                DebugWriteLine("i = " + i);
                Path path = _paths[i];
                Path newPath = new Path
                {
                    //Elements = new List<int>(path.Elements),
                    LastElemIndex = path.LastElemIndex + 1,
                    Sum = path.Sum
                };

                int elem = elements[level][path.LastElemIndex];
                DebugWriteLine(string.Format("{0} -> {1}", PathToString(path), elem));
                //path.Elements.Add(elem);
                path.Sum += elem;

                int newElem = elements[level][newPath.LastElemIndex];
                DebugWriteLine(string.Format("{0} -> {1} (new)", PathToString(newPath), newElem));
                //newPath.Elements.Add(newElem);
                newPath.Sum += newElem;

                _paths.Add(newPath);

                DebugWriteLine("");
            }
        }

        public static string PathToString(Path path)
        {
            string result = "[";

            //foreach (int n in path.Elements)
            //    result += (n.ToString() + " ");

            result = (result.Trim() + "]");
            result += (" S=" + path.Sum);

            return result;
        }

        public static void DebugWriteLine(string text)
        {
            //Console.WriteLine(text);
        }

        public class Path
        {
            //public List<int> Elements { get; set; }
            public int LastElemIndex { get; set; }
            public long Sum { get; set; }
        }
    }
}
