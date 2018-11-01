using ConsoleApp1.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.AppliedAlgorithms.PathFinding
{
    class GeometricPathAStarNode : AStarNode
    {
        public Coords Coords;
        GeometricPathTestCase TestCase;

        public GeometricPathAStarNode(GeometricPathTestCase testCase) {
            TestCase = testCase;
            Coords = TestCase.StartCoords;
            ComputeDistances();
        }

        //public GeometricPathAStarNode() {
        //}

        public override void ComputeDistanceToGoal() {
            DistToGoal = Math.Sqrt(
                Math.Pow(Coords.Column - TestCase.GoalCoords.Column, 2) +
                Math.Pow(Coords.Row - TestCase.GoalCoords.Row, 2));
        }

        public override bool Equals(object otherObj) {
            GeometricPathAStarNode other = otherObj as GeometricPathAStarNode;
            return this.Coords.Equals(other.Coords);
        }

        public override int GetHashCode() {
            return Coords.GetHashCode();
        }

        public override IEnumerable<AStarNode> GetNeighbors() {
            char freeSpot = '.';
            List<string> t = TestCase.Terrain;

            bool canGoUp = Coords.Row > 0;
            bool canGoRight = Coords.Column < t[0].Length - 1;
            bool canGoDown = Coords.Row < t.Count - 1;
            bool canGoLeft = Coords.Column > 0;

            // straight up
            if (canGoUp && t[Coords.Row - 1][Coords.Column] == freeSpot)
                yield return MakeNeighbor(Coords.Row - 1, Coords.Column);
            // up and right
            if (canGoUp && canGoRight && t[Coords.Row - 1][Coords.Column + 1] == freeSpot)
                yield return MakeNeighbor(Coords.Row - 1, Coords.Column + 1);
            // right
            if (canGoRight && t[Coords.Row][Coords.Column + 1] == freeSpot)
                yield return MakeNeighbor(Coords.Row, Coords.Column + 1);
            // right and down
            if (canGoRight && canGoDown && t[Coords.Row + 1][Coords.Column + 1] == freeSpot)
                yield return MakeNeighbor(Coords.Row + 1, Coords.Column + 1);
            // straight down
            if (canGoDown && t[Coords.Row + 1][Coords.Column] == freeSpot)
                yield return MakeNeighbor(Coords.Row + 1, Coords.Column);
            // down and left
            if (canGoDown && canGoLeft && t[Coords.Row + 1][Coords.Column - 1] == freeSpot)
                yield return MakeNeighbor(Coords.Row + 1, Coords.Column - 1);
            // left
            if (canGoLeft && t[Coords.Row][Coords.Column - 1] == freeSpot)
                yield return MakeNeighbor(Coords.Row, Coords.Column - 1);
            // up and left
            if (canGoLeft && canGoUp && t[Coords.Row - 1][Coords.Column - 1] == freeSpot)
                yield return MakeNeighbor(Coords.Row - 1, Coords.Column - 1);
        }

        public override string ToString() {
            return Coords.ToString();
        }

        internal GeometricPathAStarNode MakeNeighbor(int row, int col) {
            GeometricPathAStarNode neighbor = new GeometricPathAStarNode(TestCase);
            neighbor.PreviousNode = this;

            neighbor.Coords = new Coords(row, col);
            neighbor.TestCase = this.TestCase;

            neighbor.ComputeDistances();
            return neighbor;
        }

        private void ComputeDistances() {
            GeometricPathAStarNode prev = PreviousNode as GeometricPathAStarNode;
            bool isLateralMovement = this.Coords.Column == prev?.Coords.Column || this.Coords.Row == prev?.Coords.Row;

            if (prev == null) {
                DistFromPrevious = 0;
                DistFromStart = 0;
            }
            else {
                DistFromPrevious = isLateralMovement ? 1 : Math.Sqrt(2);
                DistFromStart = prev.DistFromStart + DistFromPrevious;
            }

            ComputeDistanceToGoal();
            TotalDist = DistFromStart + DistToGoal;
       }
    }
}
