using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Algorithms
{
    public class List
    {
        public void Test() {
            Node list = new Node(1, new Node(2, new Node(3, new Node(4, null))));
            Node rev = ReverseList(list);
        }

        Node ReverseList(Node list) {
            Node prev = null;
            Node next = null;

            for (Node current = list; current != null; current = next) {
                next = current.Next;
                current.Next = prev;
                prev = current;
            }

            return prev;
        }

        public class Node
        {
            public int Value;
            public Node Next;

            public Node(int data, Node next) {
                Value = data;
                Next = next;
            }

            public override string ToString() {
                return $"{Value}";
            }
        }
    }
}
