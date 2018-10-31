using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApp1.Algorithms
{
    public class AStar
    {
        static AStarNodeTotalDistComparer _nodeDistComparer = new AStarNodeTotalDistComparer();
        static readonly int MAX_LIST_LEN = 4;

        public List<AStarNode> FindShortestPath(AStarNode start) {
            // Will be sorted and iterated by increasing total distance.
            List<AStarNode> nodesToExplore = new List<AStarNode> { start };

            HashSet<AStarNode> exploredNodesHash = new HashSet<AStarNode>();
            
            while (nodesToExplore.Count > 0) {
                AStarNode node = PopNode(nodesToExplore, exploredNodesHash);

                if (node.DistToGoal <= 0) {
                    //Console.WriteLine($"Explored nodes: {exploredNodesHash.Count}");
                    return MakePathFromStartToEnd(node, exploredNodesHash);
                }

                //node.Print();
                //Console.Write("\t\t");

                foreach (AStarNode neighbor in node.GetNeighbors()) {
                    if (exploredNodesHash.Contains(neighbor))
                        continue;

                    //neighbor.Print();
                    //Console.Write("\t\t");

                    InsertNodeInSortedList(nodesToExplore, neighbor);
                    TrimNodeList(nodesToExplore, exploredNodesHash);
                }
                //Console.WriteLine();
            }

            return null;
        }

        // Private methods

        private List<AStarNode> MakePathFromStartToEnd(AStarNode endNode, HashSet<AStarNode> exploredNodesHash) {
            List<AStarNode> result = new List<AStarNode>();
            for (AStarNode crtNode = endNode; crtNode != null; crtNode = crtNode.PreviousNode) 
                result.Add(crtNode);
            result.Reverse();
            return result;
        }

        private static AStarNode PopNode(List<AStarNode> nodesToExplore, HashSet<AStarNode> exploredNodesHash) {
            AStarNode node = nodesToExplore[0];
            exploredNodesHash.Add(node);
            nodesToExplore.RemoveAt(0);
            return node;
        }

        private static void InsertNodeInSortedList(List<AStarNode> nodeList, AStarNode nodeToInsert) {
            int index = nodeList.BinarySearch(nodeToInsert, _nodeDistComparer);
            if (index >= 0)
                nodeList.Insert(index, nodeToInsert);
            else
                nodeList.Insert(~index, nodeToInsert);
        }

        private static void TrimNodeList(List<AStarNode> nodeList, HashSet<AStarNode> exploredNodesHash) {
            if (nodeList.Count > MAX_LIST_LEN) {
                for (int i = MAX_LIST_LEN; i < nodeList.Count; i++) {
                    exploredNodesHash.Add(nodeList[i]);
                }
                nodeList = nodeList.Take(MAX_LIST_LEN).ToList();
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
        public abstract double ComputeDistanceToGoal();
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
