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

        public void SolveTestCases(string inputFile, string outputFile) {
            StringBuilder sb = new StringBuilder();
            int i = 1;

            foreach (string line in File.ReadLines(inputFile)) {
                if (line.Length > 0) {
                    string result = Solve(line);

                    if (result != null) {
                        string outputLine = $"Case #{i}: {result}";
                        sb.AppendLine(outputLine);
                        Console.WriteLine(outputLine);
                        i++;
                    }
                }
            }

            File.WriteAllText(outputFile, sb.ToString());
        }

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

        protected string Solve(string line, ITestCaseBuilder testCaseBuilder) {
            ITestCase testCase = testCaseBuilder.ParseInputLine(line);

            if (testCase != null) {
                string result = Solve(testCase);
                return result;
            }
            else
                return null;
        }

        protected abstract string Solve(ITestCase testCase);
        protected abstract ITestCaseBuilder MakeTestCaseBuilder();
    }

    public interface ITestCaseBuilder
    {
        ITestCase ParseInputLine(string line);
    }

    public interface ITestCase
    {
    }
}
