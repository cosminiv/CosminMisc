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
        int _testCaseCount;

        public string Solve(string line) {
            if (_testCaseCount == 0) {
                _testCaseCount = int.Parse(line);
                return null;
            }

            ResetState();
            TestCase testCase = new TestCase(line.Split(' ')[0], int.Parse(line.Split(' ')[1]), "");

            PancakeState initialState = testCase.InitialState;
            int problemSize = initialState.Size;
            int flipperSize = testCase.FlipperSize;
            if (IsFinal(initialState)) return "0";

            PancakeState prevState = initialState;
            _allStatesExplored.Add(initialState);
            bool foundNewMove = true;  // so we can enter the loop
            int moves = 1;

            // Move to the first pancake which needs to be flipped
            for (int flipperPos = Math.Min(prevState.FirstMinusPos, problemSize - flipperSize);
                foundNewMove;
                flipperPos = Math.Min(prevState.FirstMinusPos, problemSize - flipperSize)) {
                foundNewMove = false;
                PancakeState state = prevState.NextState(flipperPos, flipperSize);
                if (IsFinal(state)) {
                    return moves.ToString();
                }
                if (!WasAlreadyExplored(state)) {
                    _allStatesExplored.Add(state);
                    foundNewMove = true;
                    prevState = state;
                }
                moves++;
            }

            return "IMPOSSIBLE";
        }

        public static int BasicTest() {
            var tests = new[] {
                new TestCase("++++++", 2, "0"),
                new TestCase("--++++", 2, "1"),
                new TestCase("+--+++", 2, "1"),
                new TestCase("---", 3, "1"),
                new TestCase("+++++++++---", 3, "1"),
                new TestCase("+--+--", 2, "2"),
                new TestCase("---+-++-", 3, "3"),
                new TestCase("+++++", 4, "0"),
                new TestCase("-+-+-", 4, "-1"),
            };

            var solver = new Year_2017_Qualif_A();

            foreach (var test in tests) {
                Console.Write(test.InitialState);
                string result = solver.Solve($"{test.InitialState} {test.FlipperSize}");
                Debug.Assert(result == test.ExpectedResult);
                Console.WriteLine("   OK");
            }

            return 0;
        }

        void ResetState() {
            _allStatesExplored = new HashSet<PancakeState>();
            _testCaseCount = 0;
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
            public int FirstMinusPos { get { return Config.IndexOf('-'); } }
            //public string InputFlipperPos { get { return string.Join(" ", InputFlipperPositions); } }
        }

        public class TestCase
        {
            public PancakeState InitialState;
            public int FlipperSize;
            public string ExpectedResult;

            public TestCase(string initialState, int flipperSize, string expectedResult) {
                InitialState = new PancakeState(initialState);
                FlipperSize = flipperSize;
                ExpectedResult = expectedResult;
            }
        }
    }
}
