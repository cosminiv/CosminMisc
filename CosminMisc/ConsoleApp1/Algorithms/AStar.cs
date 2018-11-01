using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApp1.Algorithms
{
    public class AStar
    {
        static AStarNodeTotalDistComparer _nodeDistComparer = new AStarNodeTotalDistComparer();
        static readonly int MAX_LIST_LEN = 8;

        public List<AStarNode> FindShortestPath(AStarNode start) {
            // Will be sorted and iterated by increasing total distance.
            List<AStarNode> openNodes = new List<AStarNode> { start };

            HashSet<AStarNode> closedNodes = new HashSet<AStarNode>();
            
            while (openNodes.Count > 0) {
                AStarNode node = PopNode(openNodes, closedNodes);

                if (node.DistToGoal <= 0) {
                    //Console.WriteLine($"Explored nodes: {exploredNodesHash.Count}");
                    return MakePathFromStartToEnd(node);
                }

                //node.Print();
                //Console.Write("\t\t");

                foreach (AStarNode neighbor in node.GetNeighbors()) {
                    if (closedNodes.Contains(neighbor))
                        continue;

                    //neighbor.Print();
                    //Console.Write("\t\t");

                    InsertNodeInSortedList(openNodes, neighbor);
                    TrimNodeList(openNodes, closedNodes);
                }
                //Console.WriteLine();
            }

            return null;
        }

        // Private methods

        private List<AStarNode> MakePathFromStartToEnd(AStarNode endNode) {
            List<AStarNode> result = new List<AStarNode>();
            for (AStarNode crtNode = endNode; crtNode != null; crtNode = crtNode.PreviousNode) {
                if (result.Count == 0) result.Add(crtNode);
                else result.Insert(0, crtNode);
            }

            //result.Reverse();
            return result;
        }

        private static AStarNode PopNode(List<AStarNode> openNodes, HashSet<AStarNode> closedNodes) {
            AStarNode node = openNodes[0];
            closedNodes.Add(node);
            openNodes.RemoveAt(0);
            return node;
        }

        private static void InsertNodeInSortedList(List<AStarNode> openNodes, AStarNode nodeToInsert) {
            int index = openNodes.BinarySearch(nodeToInsert, _nodeDistComparer);
            if (index >= 0)
                openNodes.Insert(index, nodeToInsert);
            else
                openNodes.Insert(~index, nodeToInsert);
        }

        private static void TrimNodeList(List<AStarNode> openNodes, HashSet<AStarNode> closedNodes) {
            if (openNodes.Count > MAX_LIST_LEN) {
                for (int i = MAX_LIST_LEN; i < openNodes.Count; i++) {
                    closedNodes.Add(openNodes[i]);
                }
                openNodes = openNodes.Take(MAX_LIST_LEN).ToList();
            }
        }
    }

    public abstract class AStarNode
    {
        public double DistFromPrevious;
        public double DistFromStart;
        public double DistToGoal;
        public double TotalDist;
        public AStarNode PreviousNode;
        public string PreviousAction;

        public AStarNode() {
        }

        public AStarNode(double distFromStart, AStarNode prevNode) {
            DistFromStart = distFromStart;
            PreviousNode = prevNode;
        }

        public abstract override int GetHashCode();
        public abstract override bool Equals(object obj);
        public abstract void ComputeDistanceToGoal();
        public abstract IEnumerable<AStarNode> GetNeighbors();

        public virtual void Print() {
        }

        protected static void Copy(AStarNode source, AStarNode dest) {
            dest.DistFromPrevious = source.DistFromPrevious;
            dest.DistFromStart = source.DistFromStart;
            dest.DistToGoal = source.DistToGoal;
            dest.TotalDist = dest.DistFromStart + dest.DistToGoal;
        }
    }

    class AStarNodeTotalDistComparer : IComparer<AStarNode>
    {
        public int Compare(AStarNode x, AStarNode y) {
            int n = x.TotalDist.CompareTo(y.TotalDist);
            if (n != 0)
                return n;

            int n2 = x.DistToGoal.CompareTo(y.DistToGoal);
            return n2;
        }
    }
}
