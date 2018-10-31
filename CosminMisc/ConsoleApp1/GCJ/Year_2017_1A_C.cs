using ConsoleApp1.Algorithms;
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
        protected override string Solve(ITestCase testCase2) {
            TestCase testCase = (TestCase)testCase2;
            AStar algorithm = new AStar();
            DragonAStarNode startState = new DragonAStarNode(testCase);

            List<AStarNode> path = algorithm.FindShortestPath(startState);

            if (path == null)
                return "IMPOSSIBLE";
            else {
                Console.WriteLine("\n=============================\n");
                foreach (AStarNode node2 in path) {
                    DragonAStarNode node = (DragonAStarNode)node2;
                    node.Print();
                    Console.WriteLine();
                }
                return (path.Count - 1).ToString();
            }
        }

        protected override ITestCaseBuilder MakeTestCaseBuilder() {
            return new TestCaseBuilder();
        }

        class DragonAStarNode : AStarNode
        {
            public long DragonHealth;
            public long DragonAttack;
            public long KnightHealth;
            public long KnightAttack;
            TestCase _testCase;

            public DragonAStarNode(TestCase testCase) {
                DragonHealth = testCase.DragonHealth;
                DragonAttack = testCase.DragonAttack;
                KnightHealth = testCase.KnightHealth;
                KnightAttack = testCase.KnightAttack;
                _testCase = testCase;
                ComputeDistances();
            }

            public override bool Equals(object obj) {
                if (obj == null)
                    return false;
                return this.GetHashCode() == (obj as DragonAStarNode).GetHashCode();
            }

            public override int GetHashCode() {
                long hash = 23;
                hash = hash * 31 + DragonHealth;
                hash = hash * 31 + DragonAttack;
                hash = hash * 31 + KnightHealth;
                hash = hash * 31 + KnightAttack;
                return (int)hash;
            }

            public override void Print() {
                PrintPreviousAction();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"\t({ DragonHealth}, {DragonAttack}, { KnightHealth}, { KnightAttack}) ({TotalDist})");
            }

            private void PrintPreviousAction() {
                if (PreviousAction == "Attack")
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (PreviousAction == "Cure")
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (PreviousAction == "Buff")
                    Console.ForegroundColor = ConsoleColor.Blue;
                else if (PreviousAction == "Debuff")
                    Console.ForegroundColor = ConsoleColor.Cyan;

                Console.Write(PreviousAction);
            }

            public override string ToString() {
                return $"{PreviousAction}\n({DragonHealth}, {DragonAttack}, {KnightHealth}, {KnightAttack}) ({TotalDist})\n";
            }

            public override IEnumerable<AStarNode> GetNeighbors() {
                DragonAStarNode node = this;

                if (node.DragonHealth <= 0 || node.KnightHealth <= 0)
                    yield break;

                DragonAStarNode n1 = node.MakeNeighbor(DragonAStarNode.Attack);
                if (n1 != null) yield return n1;

                if (_testCase.Buff > 0) {
                    DragonAStarNode n2 = node.MakeNeighbor(DragonAStarNode.Buff);
                    if (n2 != null) yield return n2;
                }

                DragonAStarNode n3 = node.MakeNeighbor(DragonAStarNode.Cure);
                if (n3 != null) yield return n3;

                if (_testCase.Debuff > 0 && node.KnightAttack > 0) {
                    DragonAStarNode n4 = node.MakeNeighbor(DragonAStarNode.Debuff);
                    if (n4 != null) yield return n4;
                }
            }

            internal DragonAStarNode MakeNeighbor(Action<DragonAStarNode> Action) {
                DragonAStarNode neighbor = new DragonAStarNode(_testCase);
                Copy(this, neighbor);
                neighbor.PreviousNode = this;

                Action(neighbor);
                neighbor.DragonHealth -= neighbor.KnightAttack;

                if (neighbor.IsLosingState())
                    return null;

                neighbor.ComputeDistances();
                return neighbor;
            }

            private bool IsLosingState() {
                return DragonHealth <= 0 && KnightHealth > 0;
            }

            internal static void Attack(DragonAStarNode node) {
                node.KnightHealth -= node.DragonAttack;
                node.PreviousAction = "Attack";
            }

            internal static void Buff(DragonAStarNode node) {
                node.DragonAttack += node._testCase.Buff;
                node.PreviousAction = "Buff";
            }

            internal static void Cure(DragonAStarNode node) {
                node.DragonHealth = node._testCase.DragonHealth;
                node.PreviousAction = "Cure";
            }

            internal static void Debuff(DragonAStarNode node) {
                node.KnightAttack -= node._testCase.Debuff;
                if (node.KnightAttack < 0)
                    node.KnightAttack = 0;
                node.PreviousAction = "Debuff";
            }

            private void ComputeDistances() {
                DistFromPrevious = this.Equals(PreviousNode) ? 0 : 1;
                DistFromStart = (PreviousNode != null ? PreviousNode.DistFromStart + 1 : 0);
                ComputeDistanceToGoal();
                TotalDist = DistFromStart + DistToGoal;
            }

            static void Copy(DragonAStarNode source, DragonAStarNode dest) {
                AStarNode.Copy(source, dest);

                dest.DragonHealth = source.DragonHealth;
                dest.DragonAttack = source.DragonAttack;
                dest.KnightHealth = source.KnightHealth;
                dest.KnightAttack = source.KnightAttack;
            }

            public override double ComputeDistanceToGoal() {
                DragonAStarNode node = this;

                if (node.KnightHealth <= 0) { node.DistToGoal = 0; return 0; }
                if (node.DragonHealth <= 0) { node.DistToGoal = double.MaxValue; return double.MaxValue; }

                long turnsUntilKill = node.KnightHealth / node.DragonAttack;
                if (node.KnightHealth % node.DragonAttack > 0)
                    turnsUntilKill++;

                long totalHealthLost = turnsUntilKill * node.KnightAttack;
                long healTurnsNeeded = (totalHealthLost - node.DragonHealth) / _testCase.DragonHealth + 1;

                node.DistToGoal = turnsUntilKill + healTurnsNeeded;

                return node.DistToGoal;
            }
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
