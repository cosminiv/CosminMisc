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
        public string Solve(string line) {

            List<Stage> stages = StageGenerator.GenerateValidStages(4);
            
            foreach (var stage in stages.OrderByDescending(s=>s.Score).Take(5)) {
                Console.WriteLine(stage);
            }
            Console.WriteLine($"{stages.Count} stages");

            return "";
        }

        class StageGenerator
        {
            public static List<Stage> GenerateValidStages(int size) {
                List<Stage> validStages = new List<Stage>();

                List<string> validRows = GenerateValidRows(size);
                Dictionary<int, string> validRowsMap = validRows.ToDictionary(r => r.GetHashCode());

                List<List<int>> rowPermutations = GeneratePermutations(validRowsMap.Keys.ToList(), size);

                foreach (List<int> rowPerm in rowPermutations) {
                    string rowPermAsString = string.Join("\n", rowPerm.Select(rowId => validRowsMap[rowId]));
                    Stage stage = new Stage(rowPermAsString);
                    if (stage.IsValid())
                        validStages.Add(stage);
                }

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

        public static List<List<T>> GeneratePermutations<T>(List<T> elements, int take) {
            List<List<T>> result = new List<List<T>>();

            if (take == 1) {
                result.AddRange(elements.Select(el => new List<T> { el }));
                goto end;
            }

            result = new List<List<T>>();
            List<List<T>> smallerPermutations = GeneratePermutations(elements, take - 1);

            for (int j = 0; j <= elements.Count - 1; j++) {
                for (int i = 0; i < smallerPermutations.Count; i++) {
                    List<T> smallerPerm = smallerPermutations[i];
                    List<T> newPerm = smallerPerm.Prepend(elements[j]).ToList();
                    result.Add(newPerm);
                }
            }

            end:
            return result;
        }

        class Stage
        {
            readonly List<string> Models;
            
            public Stage(int size) {
                Models = new List<string>();
                for (int i = 0; i < size; i++)
                    Models[i] = "";
            }

            public Stage(string str) {
                string[] lines = str.Split('\n');
                Models = lines.ToList();
            }

            public int RowCount { get { return Models.Count; } }

            public int ColumnCount { get { return Models[0].Length; } }

            public int Score {
                get {
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

            internal bool IsValid() {
                if (!AreColumnsValid()) return false;
                if (!AreDiagonalsValid()) return false;
                return true;
            }

            private bool AreDiagonalsValid() {
                // top-left half, including main diagonal
                for (int col = 1; col < Models.Count; col++) {
                    int nonXModels = 0;
                    for (int row = 0; row <= col; row++) {
                        char model = Models[row][col-row];
                        if (model != '.' && model != 'x' && ++nonXModels > 1)
                            return false;
                    }
                }

                // bottom-right diagonal
                for (int j = 1; j < Models.Count; j++) {
                    int diagLength = Models.Count - j;
                    int nonXModels = 0;

                    int i2 = Models.Count - 1;
                    int j2 = j;

                    for (int k = 0; k < diagLength; k++) {
                        char model = Models[i2--][j2++];
                        if (model != '.' && model != 'x' && ++nonXModels > 1)
                            return false;
                    }
                    
                }

                // top-right half, including main diagonal
                for (int j = 0; j < Models.Count - 1; j++) {
                    int diagLength = Models.Count - j;
                    int nonXModels = 0;

                    int i2 = 0;
                    int j2 = j;

                    for (int k = 0; k < diagLength; k++) {
                        char model = Models[i2++][j2++];
                        if (model != '.' && model != 'x' && ++nonXModels > 1)
                            return false;
                    }

                }

                // bottom-left half
                for (int i = 1; i < Models.Count - 1; i++) {
                    int diagLength = Models.Count - i;
                    int nonXModels = 0;

                    int i2 = i;
                    int j2 = 0;

                    for (int k = 0; k < diagLength; k++) {
                        char model = Models[i2++][j2++];
                        if (model != '.' && model != 'x' && ++nonXModels > 1)
                            return false;
                    }

                }

                return true;
            }

            private bool AreColumnsValid() {
                for (int col = 0; col < Models.Count; col++) {
                    int nonPlusModels = 0;
                    for (int row = 0; row < Models.Count; row++) {
                        char model = Models[row][col];
                        if (model != '.' && model != '+' && ++nonPlusModels > 1)
                            return false;
                    }
                }

                return true;
            }
        }
    }
}
