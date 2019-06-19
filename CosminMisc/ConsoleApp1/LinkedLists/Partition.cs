using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp1.LinkedLists.LinkedList;

namespace ConsoleApp1.LinkedLists
{
    //
    // Given a linked list of numbers and a pivot k, partition the linked list 
    // so that all nodes less than k come before nodes greater than or equal to k.
    //
    // For example, given the linked list 5 -> 1 -> 8 -> 0 -> 3 and k = 3, 
    // the solution could be 1 -> 0 -> 5 -> 8 -> 3.
    //
    class Partition
    {
        public static void Test() {
            Node list = new Node(5, new Node(1, new Node(8, new Node(0, new Node(3)))));
            Node result = DoPartition(list, -1);

            //Node list = new Node(5, new Node(1, new Node(8, new Node(0, new Node(3)))));
            //Node result = DoPartition(list, 3);

            PrintList(result);
        }

        private static Node DoPartition(Node list, int pivot) {
            Node smallHead = null, smallTail = null;
            Node bigHead = null, bigTail = null;

            for (Node crt = list; crt != null;) {
                Node next = crt.Next;

                if (crt.Value < pivot) {
                    if (smallTail == null) {
                        smallTail = crt;
                        smallHead = crt;
                    }
                    else {
                        smallTail.Next = crt;
                        smallTail = crt;
                    }
                }
                else {
                    if (bigTail == null) {
                        bigTail = crt;
                        bigHead = crt;
                    }
                    else {
                        bigTail.Next = crt;
                        bigTail = crt;
                    }
                }

                crt.Next = null;
                crt = next;
            }

            if (smallTail != null)
                smallTail.Next = bigHead;

            if (smallHead != null)
                return smallHead;
            else
                return bigHead;
        }
    }
}
