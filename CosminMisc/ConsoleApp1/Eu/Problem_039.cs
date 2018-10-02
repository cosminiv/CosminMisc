using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_039
    {
        public static int Solve()
        {
            Dictionary<int, List<Triangle>> perimAndTriangles = new Dictionary<int, List<Triangle>>();
            HashSet<string> trianglesSeen = new HashSet<string>();

            for (int a = 1; a < 1001; a++)
            {
                for (int b = a; b < 1001; b++)
                {
                    var c = Math.Sqrt(a * a + b * b);
                    if (c > 1000)
                    {
                        if (b == a) goto outOfTheLoops;
                        else break;
                    }

                    if (Math.Ceiling(c) == Math.Floor(c))
                    {
                        int perim = a + b + (int)c;
                        bool found = perimAndTriangles.TryGetValue(perim, out List<Triangle> triangles);
                        Triangle triangle = new Triangle { A = a, B = b, C = (int)c };
                        if (found)
                        {
                            //string triangleStr = string.Join(", ", new[] { a, b, c }.OrderBy(x => x));
                            //if (!trianglesSeen.Contains(triangleStr))
                            {
                                perimAndTriangles[perim].Add(triangle);
                                //trianglesSeen.Add(triangleStr);
                            }
                        }
                        else
                            perimAndTriangles.Add(perim, new List<Triangle> { triangle });
                    }
                }
            }

            outOfTheLoops:
            int perimWithMaxCount = 0;
            int maxCount = 0;

            foreach (int perim in perimAndTriangles.Keys)
                if (perimAndTriangles[perim].Count > maxCount)
                {
                    perimWithMaxCount = perim;
                    maxCount = perimAndTriangles[perim].Count;
                }

            Debug.Print($"{perimWithMaxCount}: {perimAndTriangles[perimWithMaxCount].Count}");

            foreach (Triangle t in perimAndTriangles[perimWithMaxCount])
                Debug.Print($"{t.A}, {t.B}, {t.C}");

            return perimWithMaxCount;
        }

        class Triangle
        {
            public int A;
            public int B;
            public int C;
        }
    }
}
