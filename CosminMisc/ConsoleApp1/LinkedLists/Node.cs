using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.LinkedLists
{
    class Node
    {
        public int Value;
        public Node Next;

        public Node(int val, Node next = null) {
            Value = val;
            Next = next;
        }

        public override string ToString() {
            return $"{Value}";
        }
    }
}
