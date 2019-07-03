using ConsoleApp1.Algorithms;
using ConsoleApp1.LinkedLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    //
    // Merge an arbitrary number of sorted linked lists
    //
    class Leet_023
    {
        public void Test() {
            ListNode[] lists = new[] {
                LinkedList.MakeList(-4),
                LinkedList.MakeList(-10,-6,-6),
                LinkedList.MakeList(0,3),
                LinkedList.MakeList(2),
                LinkedList.MakeList(-10,-9,-8,3,4,4),
                LinkedList.MakeList(-10,-10,-8,-6,-4,-3,1),
                LinkedList.MakeList(2),
                LinkedList.MakeList(-9,-4,-2,4,4),
                LinkedList.MakeList(-4,0),
            };

            ListNode res = MergeKLists(lists);
            LinkedList.PrintList(res);
        }

        public ListNode MergeKLists(ListNode[] lists) {
            ListNode resHead = new ListNode(0), resTail = resHead;
            
            // Use a min heap to efficiently keep track of the current minimum 
            // (insertion and extraction in O(log(n)) )
            Func<Min, Min, int> comparer = (x, y) => x.Value - y.Value;
            BinaryHeap<Min> minHeap = new BinaryHeap<Min>(comparer);
            Min min = null;

            // Initialize the heap with the elements at the heads of the lists
            for (int i = 0; i < lists.Length; i++) {
                ListNode list = lists[i];
                if (list != null) 
                    minHeap.Insert(new Min(list.val, i));
            }

            while (minHeap.Size > 0) {
                min = minHeap.Extract();
                ListNode newNode = new ListNode(min.Value); // build new node for result
                resTail.next = newNode;     // add node to result
                resTail = newNode;

                // iterate - advance the list which we extracted from
                ListNode node = lists[min.ListIndex];
                ListNode next = node.next;
                lists[min.ListIndex] = next;

                if (next != null) 
                    minHeap.Insert(new Min(next.val, min.ListIndex));
            }

            return resHead.next;
        }

        public class Min
        {
            public int Value { get; set; }
            public int ListIndex { get; set; }

            public Min(int value, int listIndex) {
                Value = value;
                ListIndex = listIndex;
            }

            public override string ToString() {
                return $"{Value}";
            }
        }

        public class MinComparer : IComparer<Min>
        {
            public int Compare(Min x, Min y) {
                return x.Value - y.Value;
            }
        }
    }
}
