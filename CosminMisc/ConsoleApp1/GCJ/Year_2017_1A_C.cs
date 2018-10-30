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

            DragonAStar algorithm = new DragonAStar(testCase);

            List<AStarNode> path = algorithm.FindShortestPath(new DragonAStarNode(testCase));

            if (path == null)
                return "IMPOSSIBLE";
            else {
                foreach (AStarNode node2 in path) {
                    DragonAStarNode node = (DragonAStarNode)node2;
                }
                return (path.Count - 1).ToString();
            }
        }

        protected override ITestCaseBuilder MakeTestCaseBuilder() {
            return new TestCaseBuilder();
        }

        class DragonAStar : AStar
        {
            readonly TestCase _testCase;

            public DragonAStar(TestCase testCase) {
                _testCase = testCase;
            }

            protected override IEnumerable<AStarNode> GetNeighbors(AStarNode node2) {
                DragonAStarNode node = (DragonAStarNode)node2;

                if (node.DistToGoal == double.MaxValue)
                    yield break;    // Dragon is dead

                yield return node.MakeNeighbor(DragonAStarNode.Attack, 0);
                yield return node.MakeNeighbor(DragonAStarNode.Buff, _testCase.Buff);
                yield return node.MakeNeighbor(DragonAStarNode.Cure, _testCase.DragonHealth);
                yield return node.MakeNeighbor(DragonAStarNode.Debuff, _testCase.Debuff);
            }
        }

        class DragonAStarNode : AStarNode
        {
            public long DragonHealth;
            public long DragonAttack;
            public long KnightHealth;
            public long KnightAttack;

            DragonAStarNode() {
            }

            public DragonAStarNode(TestCase testCase) {
                DragonHealth = testCase.DragonHealth;
                DragonAttack = testCase.DragonAttack;
                KnightHealth = testCase.KnightHealth;
                KnightAttack = testCase.KnightAttack;
            }

            public override bool Equals(object obj) {
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

            public override string ToString() {
                return $"{this.PreviousAction} ({this.DragonHealth}, {this.KnightHealth})  ";
            }

            internal DragonAStarNode MakeNeighbor(Action<DragonAStarNode, long> action, long param) {
                DragonAStarNode neighbor = new DragonAStarNode();
                Copy(this, neighbor);
                neighbor.PreviousNode = this;
                action(neighbor, param);
                neighbor.ComputeDistances(this);
                return neighbor;
            }

            internal static void Attack(DragonAStarNode node, long bogus) {
                node.KnightHealth -= node.DragonAttack;
                node.DragonHealth -= node.KnightAttack;
                node.PreviousAction = "Attack";
            }

            internal static void Buff(DragonAStarNode node, long buff) {
                node.DragonAttack += buff;
                node.DragonHealth -= node.KnightAttack;
                node.PreviousAction = "Buff";
            }

            internal static void Cure(DragonAStarNode node, long health) {
                node.DragonHealth = health;
                node.DragonHealth -= node.KnightAttack;
                node.PreviousAction = "Cure";
            }

            internal static void Debuff(DragonAStarNode node, long debuff) {
                node.KnightAttack -= debuff;
                if (node.KnightAttack < 0)
                    node.KnightAttack = 0;
                node.DragonHealth -= node.KnightAttack;
                node.PreviousAction = "Debuff";
            }

            private void ComputeDistances(DragonAStarNode prev) {
                DistFromPrevious = this.Equals(prev) ? 0 : 1;
                DistFromStart = prev.DistFromStart + 1;
                DistToGoal = ComputeDistanceToGoal();
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

                if (node.KnightHealth <= 0) return 0;
                if (node.DragonHealth <= 0) return double.MaxValue;

                long turnsUntilKill = node.KnightHealth / node.DragonAttack;
                if (node.KnightHealth % node.DragonAttack > 0)
                    turnsUntilKill++;

                long totalHealthLost = turnsUntilKill * node.KnightAttack;
                long healTurnsNeeded = (totalHealthLost - node.DragonHealth) / node.DragonHealth + 1;

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
