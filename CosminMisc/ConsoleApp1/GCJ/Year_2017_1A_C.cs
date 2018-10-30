using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GCJ
{
    // Dragon!
    public class Year_2017_1A_C : GCJBase
    {
        protected override string Solve(ITestCase testCase) {
            return "";
        }

        protected override ITestCaseBuilder MakeTestCaseBuilder() {
            return new TestCaseBuilder();
        }

        public class TestCase : ITestCase
        {
            public long DragonHealth;
            public long DragonAttack;
            public long KnightHealth;
            public long KnightAttack;
            public long Buff;
            public long Debuff;
        }

        class TestCaseBuilder : ITestCaseBuilder
        {
            int _testCases;

            public ITestCase ParseInputLine(string line) {
                if (string.IsNullOrWhiteSpace(line))
                    return null;

                if (_testCases == 0) {
                    _testCases = int.Parse(line);
                    return null;
                }

                long[] t = line.Split(' ').Select(tt => long.Parse(tt)).ToArray();

                TestCase testCase = new TestCase {
                    DragonHealth = t[0],
                    DragonAttack = t[1],
                    KnightHealth = t[2],
                    KnightAttack = t[3],
                    Buff = t[4],
                    Debuff = t[5]
                };

                return testCase;
            }
        }
    }
}
