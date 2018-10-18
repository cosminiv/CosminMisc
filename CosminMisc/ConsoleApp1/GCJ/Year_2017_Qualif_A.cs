using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GCJ
{
    class Year_2017_Qualif_A
    {
        HashSet<PancakeState> _allStatesExplored;
        HashSet<PancakeState> _previousStepStates;

        public int Solve(string inputFile, string outputFile) {
            Year_2017_Qualif_A solver = new Year_2017_Qualif_A();
            StringBuilder sb = new StringBuilder();
            int i = 1;

            foreach (string line in File.ReadLines(inputFile)) {
                if (line.Length == 0 || int.TryParse(line, out int n)) continue;

                int result = solver.Solve(new TestCase(line.Split(' ')[0], int.Parse(line.Split(' ')[1]), -1));
                string resultStr = result >= 0 ? result.ToString() : "IMPOSSIBLE";
                string outputLine = $"Case #{i++}: {resultStr}";

                sb.AppendLine(outputLine);
                Console.WriteLine(outputLine);
            }

            File.WriteAllText(outputFile, sb.ToString());
            return 0;
        }

        public static int BasicTest() {
            var tests = new[] {
                new TestCase("++++++", 2, 0),
                new TestCase("--++++", 2, 1),
                new TestCase("+--+++", 2, 1),
                new TestCase("---", 3, 1),
                new TestCase("+++++++++---", 3, 1),
                new TestCase("+--+--", 2, 2),
                new TestCase("---+-++-", 3, 3),
                new TestCase("+++++", 4, 0),
                new TestCase("-+-+-", 4, -1),
            };

            var solver = new Year_2017_Qualif_A();

            foreach (var test in tests) {
                Console.Write(test.InitialState);
                int result = solver.Solve(test);
                Debug.Assert(result == test.ExpectedResult);
                Console.WriteLine("   OK");
            }

            return 0;
        }

        public int Solve(TestCase testCase) {
            ResetState();

            PancakeState initialState = testCase.InitialState;
            int problemSize = initialState.Size;
            int flipperSize = testCase.FlipperSize;
            if (IsFinal(initialState)) return 0;
            
            _previousStepStates.Add(initialState);
            _allStatesExplored.Add(initialState);
            bool foundNewMove = true;  // so we can enter the loop

            for (int moves = 1; foundNewMove; moves++) {
                HashSet<PancakeState> currentStates = new HashSet<PancakeState>();
                foundNewMove = false;

                foreach (PancakeState prevState in _previousStepStates) {
                    for (int flipperPos = 0; flipperPos <= problemSize - flipperSize; flipperPos++) {
                        PancakeState state = prevState.NextState(flipperPos, flipperSize);
                        if (IsFinal(state))
                            return moves;
                        if (!WasAlreadyExplored(state)) {
                            currentStates.Add(state);
                            _allStatesExplored.Add(state);
                            foundNewMove = true;
                        }
                    }
                }

                _previousStepStates = currentStates;
            }

            return -1;  // no solution found
        }

        void ResetState() {
            _allStatesExplored = new HashSet<PancakeState>();
            _previousStepStates = new HashSet<PancakeState>();
        }

        private bool WasAlreadyExplored(PancakeState state) {
            return _allStatesExplored.Contains(state);
        }

        bool IsFinal(PancakeState state) {
            return state.ToString().All(c => c == '+');
        }

        public class PancakeState
        {
            readonly string Config;
            //List<int> InputFlipperPositions = new List<int>();

            public PancakeState(string state) {
                Config = state;
            }

            public override string ToString() {
                return Config;
            }

            // Needed by HashSet
            public override int GetHashCode() {
                return Config.GetHashCode();
            }

            // Needed by HashSet
            public override bool Equals(object obj) {
                return Config == (obj as PancakeState).Config;
            }

            internal PancakeState NextState(int flipperPosition, int flipperSize) {
                StringBuilder sb = new StringBuilder(Config);
                for (int i = flipperPosition; i < flipperPosition + flipperSize; i++) {
                    sb[i] = (sb[i] == '+' ? '-' : '+');
                }

                PancakeState result = new PancakeState(sb.ToString());
                //result.InputFlipperPositions = InputFlipperPositions.Append(flipperPosition).ToList();
                return result;
            }

            public int Size { get { return Config.Length; } }
            //public string InputFlipperPos { get { return string.Join(" ", InputFlipperPositions); } }
        }

        public class TestCase
        {
            public PancakeState InitialState;
            public int FlipperSize;
            public int ExpectedResult;

            public TestCase(string initialState, int flipperSize, int expectedResult) {
                InitialState = new PancakeState(initialState);
                FlipperSize = flipperSize;
                ExpectedResult = expectedResult;
            }
        }
    }
}
