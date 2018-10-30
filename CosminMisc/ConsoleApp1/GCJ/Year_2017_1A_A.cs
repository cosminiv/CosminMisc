using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GCJ
{
    // Cake!
    public class Year_2017_1A_A : GCJBase
    {
        protected override string Solve(ITestCase testCase2) {
            TestCase testCase = (TestCase)testCase2;
            List<Coords> letterCoords = ReadLettersCoords(testCase);
            bool reachedRightSide = false;
            char[][] data = testCase.Data;

            foreach (Coords letterCoord in letterCoords) {
                char letter = data[letterCoord.Row][letterCoord.Column];

                // Go left
                int leftmostModifiedColumn = letterCoord.Column;
                for (int col = letterCoord.Column - 1; col >= 0; col--) {
                    if (data[letterCoord.Row][col] == '?') {
                        data[letterCoord.Row][col] = letter;
                        leftmostModifiedColumn = col;
                    }
                    else
                        break;
                }

                // Go right
                int rightmostModifiedColumn = letterCoord.Column;
                for (int col = letterCoord.Column + 1; col < testCase.ColumnCount; col++) {
                    if (data[letterCoord.Row][col] == '?') {
                        data[letterCoord.Row][col] = letter;
                        rightmostModifiedColumn = col;
                    }
                    else
                        break;
                }

                // Go up (only for the topmost letters)
                if (!reachedRightSide) {
                    for (int row = 0; row < letterCoord.Row; row++) {
                        for (int col = leftmostModifiedColumn; col <= rightmostModifiedColumn; col++) {
                            data[row][col] = letter;
                        }
                    }
                }

                // Go down
                
                // 1. First find how far we can go.
                int bottommostCandidateRow = letterCoord.Row;
                for (int row = letterCoord.Row + 1; row < testCase.RowCount; row++) {
                    bottommostCandidateRow++;
                    for (int col = leftmostModifiedColumn; col <= rightmostModifiedColumn; col++) {
                        if (data[row][col] != '?') {
                            bottommostCandidateRow--;    // Current row not good
                            goto afterLoops;
                        }
                    }
                }

                afterLoops:

                // 2. Then write letters.
                for (int row = letterCoord.Row + 1; row <= bottommostCandidateRow; row++) {
                    for (int col = leftmostModifiedColumn; col <= rightmostModifiedColumn; col++) {
                        data[row][col] = letter;
                    }
                }

                if (rightmostModifiedColumn == testCase.ColumnCount - 1)
                    reachedRightSide = true;
            }

            return "\n" + string.Join("\n", testCase.Data.Select(d => new string(d)));
        }

        protected override ITestCaseBuilder MakeTestCaseBuilder() {
            return new TestCaseBuilder();
        }

        List<Coords> ReadLettersCoords(TestCase testCase) {
            List<Coords> result = new List<Coords>();

            for (int i = 0; i < testCase.RowCount; i++) {
                for (int j = 0; j < testCase.ColumnCount; j++) {
                    if (testCase.Data[i][j] != '?')
                        result.Add(new Coords { Row = i, Column = j });
                }
            }

            return result;
        }

        struct Coords
        {
            public int Row;
            public int Column;
        }

        class TestCase : ITestCase
        {
            public int RowCount = 0;
            public int ColumnCount = 0;
            public char[][] Data = new char[0][];
        }

        class TestCaseBuilder : ITestCaseBuilder
        {
            int _testCases;
            int _rowsRead;
            TestCase _testCase;

            public TestCaseBuilder() {
                ResetState();
            }

            private void ResetState() {
                _rowsRead = 0;
                _testCase = new TestCase();
            }

            public ITestCase ParseInputLine(string line) {
                if (_testCases == 0) {
                    _testCases = int.Parse(line);
                    return null;
                }

                if (_testCase.RowCount == 0) {
                    string[] tokens = line.Split(' ');
                    _testCase.RowCount = int.Parse(tokens[0]);
                    _testCase.ColumnCount = int.Parse(tokens[1]);
                    _testCase.Data = new char[_testCase.RowCount][];
                    return null;
                }

                if (_rowsRead < _testCase.RowCount)
                    _testCase.Data[_rowsRead++] = line.ToArray();

                if (_rowsRead == _testCase.RowCount) {
                    TestCase result = _testCase;
                    ResetState();
                    return result;
                }

                return null;
            }
        }
    }
}
