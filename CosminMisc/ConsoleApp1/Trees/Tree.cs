using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Trees
{
    class Tree
    {
        Node Root;

        public Tree(Node root) {
            Root = root;
        }

        public static void Test() {
            Tree tree = new Tree(new Node(1,
                new Node(2),
                new Node(3)));
            var inOrder = tree.TraverseInOrder();
            var enumerator = inOrder.GetEnumerator();

            for (int i = 0; i < 10; i++) {
                if (enumerator.MoveNext())
                    Console.Write($"{enumerator.Current} ");
                else
                    break;
            }
        }

        public IEnumerable<int> TraverseInOrder() {
            Stack<Node> stack = new Stack<Node>();
            Node crt = Root;

            while (true) {
                while (crt != null) {
                    stack.Push(crt);
                    crt = crt.Left;
                }

                if (stack.Count > 0) {
                    Node node = stack.Pop();
                    yield return node.Val;

                    crt = node.Right;
                }
                else
                    break;
            }
        }

        public class Node
        {
            public int Val;
            public Node Left;
            public Node Right;

            public Node(int val, Node left = null, Node right = null) {
                Val = val;
                Left = left;
                Right = right;
            }

            //public Node(int val, int left, int right) {
            //    Val = val;
            //    Left = new Node(left, null, null);
            //    Right = new Node(right, null, null); 
            //}
        }
    }
}
