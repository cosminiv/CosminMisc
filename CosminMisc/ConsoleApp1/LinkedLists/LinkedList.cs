using System;
using System.Diagnostics;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.LinkedLists
{
    static class LinkedList
    {
        public static void Test() {
            ListNode list1 = MakeList(5, 6, 7, 8);

            ListNode list = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4))));

            ListNode result = list.Filter(n => n.val % 2 == 0);

            Debug.Assert(result != null);
            Debug.Assert(result.val == 2);
            Debug.Assert(result.next.val == 4);
            Debug.Assert(result.next.next == null);

            PrintList(list);
        }

        public static ListNode MakeList(params int[] arr) {
            ListNode head = null;
            ListNode tail = null;

            for (int i = 0; i < arr.Length; i++) {
                ListNode node = new ListNode(arr[i]);
                if (head == null) {
                    head = node;
                    tail = node;
                }
                else {
                    tail.next = node;
                    tail = node;
                }
            }
            return head;
        }

        public static ListNode ReverseList(ListNode list) {
            ListNode prev = null;
            ListNode next = null;

            for (ListNode current = list; current != null; current = next) {
                next = current.next;
                current.next = prev;
                prev = current;
            }

            return prev;
        }

        // Modifies the initial list
        public static ListNode Filter(this ListNode list, Func<ListNode, bool> pred) {
            ListNode result = null;
            ListNode resultTail = null;

            for (ListNode crt = list; crt != null; ) {
                ListNode next = crt.next;

                if (pred(crt)) {
                    if (resultTail == null) {
                        resultTail = crt;
                        result = crt;
                    }
                    else {
                        resultTail.next = crt;
                        resultTail = crt;
                    }
                    crt.next = null;
                }

                crt = next;
            }

            return result;
        }

        public static void PrintList(ListNode list) {
            StringBuilder sb = new StringBuilder();
            for (ListNode crt = list; crt != null; crt = crt.next) {
                sb.Append(crt.val);
                sb.Append(" ");
            }
            Console.WriteLine(sb.ToString());
        }
    }
}
