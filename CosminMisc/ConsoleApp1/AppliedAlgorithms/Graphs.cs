using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.AppliedAlgorithms
{
    public class Graphs
    {
        public void Solve() {
            Node<int> root = BuildGraph();
            BreadthFirstTraversal(root);
            BreadthFirstTraversalRecursive(root);
            DepthFirstTraversal(root);
        }

        private void BreadthFirstTraversalRecursive(Node<int> root) {
            HashSet<int> visited = new HashSet<int>();
            BreadthFirstTraversalRecursive(root, visited);
            Console.WriteLine();
        }

        private void BreadthFirstTraversalRecursive(Node<int> node, HashSet<int> visited) {
            visited.Add(node.Data);
            Console.Write($"{node.Data}  ");

            foreach (Node<int> neighbor in node.Edges) {
                if (!visited.Contains(neighbor.Data))
                    BreadthFirstTraversalRecursive(neighbor, visited);
            }
        }

        private void DepthFirstTraversal(Node<int> node) {
            HashSet<int> visited = new HashSet<int>();
            int depth = 1;
            DepthFirstTraversal(node, visited, depth);
            Console.WriteLine();
        }

        private void DepthFirstTraversal(Node<int> node, HashSet<int> visited, int depth) {
            depth++;
            visited.Add(node.Data);

            foreach (Node<int> neighbor in node.Edges) {
                if (!visited.Contains(neighbor.Data))
                    DepthFirstTraversal(neighbor, visited, depth);
            }

            Console.Write($"{node.Data} ({depth})  ");
        }

        private void BreadthFirstTraversal(Node<int> root) {
            List<Node<int>> nodes = new List<Node<int>>();
            nodes.Add(root);
            HashSet<int> visited = new HashSet<int>();

            while (nodes.Count > 0) {
                Node<int> node = nodes[0];
                if (!visited.Contains(node.Data)) {

                    Console.Write($"{node.Data}  ");
                    visited.Add(node.Data);

                    foreach (Node<int> neighbor in node.Edges) {
                        if (!visited.Contains(neighbor.Data))
                            nodes.Add(neighbor);
                        //else
                        //    Console.Write($"Skipping {neighbor.Data}  ");
                    }
                }

                nodes.RemoveAt(0);
            }

            Console.WriteLine();
        }

        Node<int> BuildGraph() {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            Dictionary<int, Node<int>> nodes = numbers.Select(n => new Node<int>(n)).ToDictionary(n=>n.Data);
            (int, int)[] edges = {
                (1, 2), (1, 3), (2, 4), (3, 4),
                (4, 5), (5, 6), (5, 7), (6, 8),
                (7, 8), (7, 9), (8, 10), (9, 10), (10, 11)
            };

            foreach ((int,int) edge in edges) {
                nodes[edge.Item1].Edges.Add(nodes[edge.Item2]);
                //nodes[edge.Item2].Edges.Add(nodes[edge.Item1]);
            }

            return nodes[1];
        }
    }

    class Node<T> {
        public T Data;
        public List<Node<T>> Edges = new List<Node<T>>();

        public Node(T data){
            Data = data;
        }

        public override string ToString() {
            return Data.ToString();
        }
    }
}
