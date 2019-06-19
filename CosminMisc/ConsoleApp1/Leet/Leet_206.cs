using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    // Reverse a singly linked list.
    class Leet_206
    {
        public void Test() {
            ListNode lst =
                new ListNode(1) {
                    next = new ListNode(2) {
                        next = new ListNode(3)
                    }
                };

            ListNode revLst = ReverseList(lst);
            Debug.Assert(revLst.val == 3);
            Debug.Assert(revLst.next.val == 2);
            Debug.Assert(revLst.next.next.val == 1);
            Debug.Assert(revLst.next.next.next == null);
        }

        public ListNode ReverseList(ListNode head) {
            if (head == null) return null;
            if (head.next == null) return head;

            ListNode crt = head.next;
            head.next = null;
            ListNode newHead = head;

            while (crt != null) {
                ListNode next = crt.next;
                crt.next = newHead;
                newHead = crt;
                crt = next;
            }

            return newHead;
        }

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }
    }
}
