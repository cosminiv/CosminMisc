using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GCJ
{
    public abstract class GCJBase
    {
        ITestCaseBuilder _testCaseBuilder;

        public void Solve(string inputFile, string outputFile) {
            StringBuilder sb = new StringBuilder();
            IEnumerable<string> inputLines = File.ReadLines(inputFile);
            IEnumerable<string> outputLines = Solve(inputLines);

            foreach (string line in outputLines)
                sb.AppendLine(line);

            File.WriteAllText(outputFile, sb.ToString());
        }

        public IEnumerable<string> Solve(IEnumerable<string> lines) {
            int i = 1;

            foreach (string line in lines.Select(l => l.Trim())) {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string result = Solve(line);

                if (result != null) {
                    string outputLine = $"Case #{i}: {result}";
                    yield return outputLine;
                    i++;
                }
            }
        }


        // Abstract methods

        protected abstract string Solve(ITestCase testCase);

        protected abstract ITestCaseBuilder MakeTestCaseBuilder();


        // Private methods

        private string Solve(string line) {
            ITestCaseBuilder testCaseBuilder = GetTestCaseBuilder();
            string result = Solve(line, testCaseBuilder);
            return result;
        }

        private ITestCaseBuilder GetTestCaseBuilder() {
            if (_testCaseBuilder != null)
                return _testCaseBuilder;

            _testCaseBuilder = MakeTestCaseBuilder();
            return _testCaseBuilder;
        }

        private string Solve(string line, ITestCaseBuilder testCaseBuilder) {
            ITestCase testCase = testCaseBuilder.ParseInputLine(line);

            if (testCase != null) {
                string result = Solve(testCase);
                return result;
            }
            else
                return null;
        }
    }

    public interface ITestCaseBuilder
    {
        ITestCase ParseInputLine(string line);
    }

    public interface ITestCase
    {
    }
}
