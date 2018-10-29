using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GCJ
{
    // Ratatouille
    public class Year_2017_1A_B
    {
        TestCaseBuilder _testCaseBuilder = new TestCaseBuilder();

//2 1
//500 300
//900
//660

        readonly static string _input =
@" 6
2 1
500 300
1500
809

2 2
50 100
450 449
1100 1101

2 1
500 300
300
500

1 8
10
11 13 17 11 16 14 12 18

3 3
70 80 90
1260 1500 700
800 1440 1600
1700 1620 900
";

        // TODO: does not work
        public static void Main() {
            Year_2017_1A_B solver = new Year_2017_1A_B();
            //int caseIndex = 0;

            foreach (string lineIn in _input.Split('\n')) {
                string lineOut = solver.Solve(lineIn);
                if (lineOut != null)
                    Console.Write($"{lineOut}\n\n");
            }

            Console.WriteLine("DONE!");
            Console.ReadLine();
        }

        public string Solve(string line) {
            TestCase testCase = _testCaseBuilder.ParseInputLine(line);

            if (testCase != null) {
                string result = Solve(testCase);
                return result;
            }
            else
                return null;
        }


        private string Solve(TestCase testCase) {
            int kits = 0;
            int ingrQty = testCase.IngredientQty[0];

            for (int packIdx = 0; packIdx < testCase.PackageCount; packIdx++) {
                int firstIngredPackSize = testCase.PackageSize[0][packIdx];

                int multiple = ComputeMultiple(firstIngredPackSize, ingrQty);
                Console.Write($"psfi: {firstIngredPackSize}g;  ");
                if (multiple < 0) continue;

                List<Tuple<int, int>> foundPackIndices = new List<Tuple<int, int>>();

                for (int ingrIdx = 1; ingrIdx < testCase.IngredientCount; ingrIdx++) {
                    double ingredRatio = (double)testCase.IngredientQty[0] / testCase.IngredientQty[ingrIdx];
                    Console.Write($"ratio: {testCase.IngredientQty[0]} / {testCase.IngredientQty[ingrIdx]} = {ingredRatio}  ");

                    int foundPackIndex = FindMatchingPackage(testCase.PackageSize[ingrIdx], ingredRatio, firstIngredPackSize);
                    if (foundPackIndex >= 0) {
                        foundPackIndices.Add(new Tuple<int, int>(ingrIdx, foundPackIndex));
                    }
                    else {
                        Console.Write($"multiple not found: {multiple}  ");
                        break;
                    }
                }

                if (foundPackIndices.Count == testCase.IngredientCount - 1) {
                    kits++;
                    foreach (var fpi in foundPackIndices)
                        testCase.PackageSize[fpi.Item1].RemoveAt(fpi.Item2);
                    //Console.WriteLine($"Package {packSize}g  ");
                }
            }

            Console.WriteLine();
            return kits.ToString();
            //return $"{testCase.IngredientCount} ingredients, " 
            //+ $"{testCase.PackageCount} packages, "
            //+ $"last package size: {testCase.PackageSize[ testCase.IngredientCount-1 ][testCase.PackageCount-1]}";
        }

        int FindMatchingPackage(List<int> packSizes, double targetRatio, int firstIngredQty) {
            for (int i = packSizes.Count - 1; i >= 0; i--) {
                double ratio = (double)packSizes[i] / firstIngredQty;
                if ((targetRatio < 1 && ratio > 1) || (targetRatio > 1 && ratio < 1))
                    ratio = 1.0 / ratio;
                if (targetRatio / ratio > 0.8)
                    return i;
            }

            return -1;
        }

        //bool IsMultiple(int n, int stdQty, int mult) {
        //    bool result = (n >= 0.9 * stdQty * mult && n <= 1.1 * stdQty * mult);
        //    Console.Write($"IsMultiple({n}, {stdQty}, {mult}) = {result}  ");
        //    return result;
        //}

        void PrintPreparedData(Dictionary<string, int> packageQty) {
            Console.Write("Data: ");
            foreach (string key in packageQty.Keys)
                Console.Write($"({key}: {packageQty[key]})  ");
            Console.Write("\n\n");
        }


        int ComputeMultiple(int packageSize, int standardQty) {
            double multiple = 1.0 * packageSize / standardQty;
            int multipleInt = packageSize / standardQty;
            double q = multiple / multipleInt;
            double q2 = multiple / (multipleInt + 1);

            if (q >= 0.9 && q <= 1.1)
                return multipleInt;
            else if (q2 >= 0.9 && q2 <= 1.1)
                return multipleInt + 1;
            else
                return -1;
        }

        string MakeKey(int ingredientIndex, int multiple) {
            return $"{ingredientIndex}_{multiple}";
        }

        class TestCase
        {
            public int IngredientCount = 0;
            public int PackageCount = 0;
            public int[] IngredientQty;
            public List<int>[] PackageSize;
        }

        class TestCaseBuilder
        {
            int _testCases;
            int _packageRowsRead;
            TestCase _testCase;

            public TestCaseBuilder() {
                ResetState();
            }

            private void ResetState() {
                _packageRowsRead = 0;
                _testCase = new TestCase();
            }

            public TestCase ParseInputLine(string line) {
                if (line.Trim().Length == 0)
                    return null;

                if (_testCases == 0) {
                    _testCases = int.Parse(line);
                    //Console.WriteLine("Reading number of cases");
                    return null;
                }

                if (_testCase.IngredientCount == 0) {
                    string[] tokens = line.Split(' ');
                    _testCase.IngredientCount = int.Parse(tokens[0]);
                    _testCase.PackageCount = int.Parse(tokens[1]);
                    _testCase.PackageSize = new List<int>[_testCase.IngredientCount];
                    //Console.WriteLine($"Reading number of ingredients and packages: {line}");
                    return null;
                }

                if (_testCase.IngredientQty == null) {
                    _testCase.IngredientQty = line.Split(' ').Select(t => int.Parse(t)).ToArray();
                    //Console.WriteLine($"Reading ingredient qty: {line}");
                    return null;
                }

                if (_packageRowsRead < _testCase.IngredientCount) {
                    //Console.WriteLine($"Reading package line {_packageRowsRead}: {line}");
                    _testCase.PackageSize[_packageRowsRead++] =
                        line.Split(' ').Select(t => int.Parse(t)).ToList();
                }

                if (_packageRowsRead == _testCase.IngredientCount) {
                    //Console.WriteLine("Returning test case");                	
                    TestCase result = _testCase;
                    ResetState();
                    return result;
                }
                else
                    return null;
            }
        }
    }
}
