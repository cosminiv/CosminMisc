using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GCJ
{
    class Year_2017_Qualif_D
    {
        // TODO: This is way too slow - 50s to generate a solution of size 4 (max = 100)
        public string Solve(string line) {

            Stopwatch sw = Stopwatch.StartNew();
            List<Stage> stages = StageGenerator.GenerateValidStages(4, sw);

            long t1 = sw.ElapsedMilliseconds;
            List<Stage> topStages = stages.OrderByDescending(s => s.Score).Take(5).ToList();
            long t2 = sw.ElapsedMilliseconds;
            Console.WriteLine($"Sort stages: {t2 - t1}ms");

            foreach (var stage in topStages) {
                Console.WriteLine(stage);
            }

            Console.WriteLine($"{stages.Count} stages");

            return "";
        }

        class StageGenerator
        {
            public static List<Stage> GenerateValidStages(int size, Stopwatch sw) {
                List<Stage> validStages = new List<Stage>();
                long t0 = sw.ElapsedMilliseconds;

                // Generate valid rows
                List<string> validRows = GenerateValidRows(size);
                long t1 = sw.ElapsedMilliseconds;
                Console.WriteLine($"GenerateValidRows: {t1 - t0}ms ({validRows.Count} rows)");

                // Generate row permutations
                List<List<string>> rowPermutations = GeneratePermutations(validRows, size, Stage.IsValid).ToList();
                long t3 = sw.ElapsedMilliseconds;
                Console.WriteLine($"GeneratePermutations: {t3 - t1}ms ({rowPermutations.Count} permutations)");

                foreach (List<string> rowPerm in rowPermutations) {
                    Stage stage = new Stage(rowPerm);
                    validStages.Add(stage);
                }
                long t4 = sw.ElapsedMilliseconds;
                Console.WriteLine($"Get valid stages: {t4 - t3}ms ({validStages.Count} stages)");

                Console.WriteLine();

                return validStages;
            }

            public static List<string> GenerateValidRows(int size) {
                List<string> candidates = new List<string>() { "o", "+", "x", "." };
                int prevCandidatesCount = candidates.Count;

                for (int problemSize = 2; problemSize <= size; problemSize++) {
                    for (int prevCandidateIndex = 0; prevCandidateIndex < prevCandidatesCount; prevCandidateIndex++)
                        GenerateCandidateRows(candidates[prevCandidateIndex], candidates);

                    candidates.RemoveRange(0, prevCandidatesCount);
                    prevCandidatesCount = candidates.Count;
                }

                return candidates;
            }

            private static void GenerateCandidateRows(string candidatePrevSize, List<string> candidates) {
                if (!candidatePrevSize.Contains("x") && !candidatePrevSize.Contains("o")) {
                    candidates.Add(candidatePrevSize + "o");
                    candidates.Add(candidatePrevSize + "x");
                }
                candidates.Add(candidatePrevSize + "+");
                candidates.Add(candidatePrevSize + ".");
            }
        }

        public static IEnumerable<List<T>> GeneratePermutations<T>(List<T> elements, int take, Func<List<T>, bool> permValidator) {
            if (take == 1) {
                foreach (T elem in elements) {
                    List<T> elemAsList = new List<T> { elem };
                    if (permValidator(elemAsList))
                        yield return elemAsList;
                }
                yield break;
            }

            IEnumerable<List<T>> smallerPermutations = GeneratePermutations(elements, take - 1, permValidator);

            for (int j = 0; j <= elements.Count - 1; j++) {
                foreach (List<T> smallerPerm in smallerPermutations) {
                    List<T> newPerm = smallerPerm.Prepend(elements[j]).ToList();
                    if (permValidator(newPerm))
                        yield return newPerm;
                }
            }
        }

        class Stage
        {
            readonly List<string> Models;
            readonly int _Score;

            //public Stage(int size) {
            //    Models = new List<string>();
            //    for (int i = 0; i < size; i++)
            //        Models[i] = "";
            //}

            //public Stage(string str) {
            //    string[] lines = str.Split('\n');
            //    Models = lines.ToList();
            //}

            public Stage() {
                Models = new List<string>();
                _Score = 0;
            }

            public Stage(List<string> rows) {
                Models = rows;
                //Models = new List<string>(rows);
                _Score = ComputeScore();
            }

            public int RowCount { get { return Models.Count; } }

            public int ColumnCount { get { return Models[0].Length; } }

            public int Score { get { return _Score; } }

            int ComputeScore() {
                int sum = 0;
                for (int col = 0; col < Models.Count; col++) {
                    for (int row = 0; row < Models.Count; row++) {
                        char model = Models[row][col];
                        if (model == '+' || model == 'x')
                            sum++;
                        else if (model == 'o')
                            sum += 2;
                    }
                }

                return sum;
            }

            public string Row(int index) {
                return Models[index];
            }

            public char this[int i, int j] {
                get { return Models[i][j]; }
                //set { Models[i][j] = value; }
            }

            public override string ToString() {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < Models.Count; i++) {
                    sb.Append(Models[i]);
                    sb.Append("\n");
                }
                return sb.ToString();
            }

            //internal bool IsValid() {
            //    return AreColumnsValid(Models) && AreDiagonalsValid(Models);
            //}

            public static bool IsValid(List<string> rows) {
                return AreColumnsValid(rows) && AreDiagonalsValid(rows);
            }

            private static bool AreDiagonalsValid(List<string> rows) {
                int columnCount = rows[0].Length;
                int rowCount = rows.Count;

                // top-left half, including main diagonal
                for (int i = 1; i < rowCount; i++) {
                    int diagLength = i + 1;
                    int nonXModels = 0;
                    int i2 = i;
                    int j2 = 0;
                    for (int k = 0; k < diagLength; k++) {
                        char model = rows[i2--][j2++];
                        if (model != '.' && model != 'x' && ++nonXModels > 1)
                            return false;
                    }
                }

                // bottom-right diagonal
                for (int j = 1; j < columnCount - 1; j++) {
                    int diagLength = Math.Min(columnCount - j, rowCount);
                    int nonXModels = 0;
                    int i2 = rowCount - 1;
                    int j2 = j;
                    for (int k = 0; k < diagLength; k++) {
                        char model = rows[i2--][j2++];
                        if (model != '.' && model != 'x' && ++nonXModels > 1)
                            return false;
                    }
                }

                // top-right half, including main diagonal
                for (int j = 0; j < columnCount - 1; j++) {
                    int diagLength = Math.Min(columnCount - j, rowCount);
                    int nonXModels = 0;
                    int i2 = 0;
                    int j2 = j;
                    for (int k = 0; k < diagLength; k++) {
                        char model = rows[i2++][j2++];
                        if (model != '.' && model != 'x' && ++nonXModels > 1)
                            return false;
                    }
                }

                // bottom-left half
                for (int i = 1; i < rowCount - 1; i++) {
                    int diagLength = rowCount - i;
                    int nonXModels = 0;
                    int i2 = i;
                    int j2 = 0;
                    for (int k = 0; k < diagLength; k++) {
                        char model = rows[i2++][j2++];
                        if (model != '.' && model != 'x' && ++nonXModels > 1)
                            return false;
                    }
                }

                return true;
            }

            private static bool AreColumnsValid(List<string> rows) {
                int cols = rows[0].Length;
                for (int col = 0; col < cols; col++) {
                    int nonPlusModels = 0;
                    for (int row = 0; row < rows.Count; row++) {
                        char model = rows[row][col];
                        if (model != '.' && model != '+' && ++nonPlusModels > 1)
                            return false;
                    }
                }

                return true;
            }
        }
    }
}
