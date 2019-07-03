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
            ListNode list = new ListNode(5, new ListNode(1, new ListNode(8, new ListNode(0, new ListNode(3)))));
            ListNode result = DoPartition(list, -1);

            //Node list = new Node(5, new Node(1, new Node(8, new Node(0, new Node(3)))));
            //Node result = DoPartition(list, 3);

            PrintList(result);
        }

        private static ListNode DoPartition(ListNode list, int pivot) {
            ListNode smallHead = null, smallTail = null;
            ListNode bigHead = null, bigTail = null;

            for (ListNode crt = list; crt != null;) {
                ListNode next = crt.next;

                if (crt.val < pivot) {
                    if (smallTail == null) {
                        smallTail = crt;
                        smallHead = crt;
                    }
                    else {
                        smallTail.next = crt;
                        smallTail = crt;
                    }
                }
                else {
                    if (bigTail == null) {
                        bigTail = crt;
                        bigHead = crt;
                    }
                    else {
                        bigTail.next = crt;
                        bigTail = crt;
                    }
                }

                crt.next = null;
                crt = next;
            }

            if (smallTail != null)
                smallTail.next = bigHead;

            if (smallHead != null)
                return smallHead;
            else
                return bigHead;
        }
    }
}
